namespace System
{
    public static class GuidExtensions
    {
        // number of bytes in uuid
        private const int ByteArraySize = 16;

        // multiplex variant info
        private const int VariantByte = 8;
        private const int VariantByteMask = 0x3f;
        private const int VariantByteShift = 0x80;

        // indexes within the uuid array for certain boundaries
        private const byte TimestampByte = 0;
        private const byte GuidClockSequenceByte = 7;
        private static readonly Random Random;

        private static DateTimeOffset _lastTimestampForNoDuplicatesGeneration = DateTime.UtcNow;

        // offset to move from 1/1/0001, which is 0-time for .NET, to gregorian 0-time of 10/15/1582
        private static readonly DateTimeOffset GregorianCalendarStart = new DateTimeOffset(1900, 1, 1, 0, 0, 0,
            TimeSpan.Zero);

        static GuidExtensions()
        {
            Random = new Random();

            ClockSequenceBytes = GenerateClockSequenceBytes();
        }

        private static byte[] ClockSequenceBytes { get; set; }

        /// <summary>
        ///     Generates a random clock sequence.
        /// </summary>
        private static byte[] GenerateClockSequenceBytes()
        {
            var bytes = new byte[9];
            Guid g = Guid.NewGuid();
            byte[] bs = g.ToByteArray();

            Array.Copy(bs, 0, bytes, 0, 9);
            //Random.NextBytes(bytes);
            return bytes;
        }

        public static byte[] GenerateClockSequenceBytes(long ticks)
        {
            byte[] bytes = BitConverter.GetBytes(ticks);

            if (bytes.Length == 0)
                return new byte[] {0x0, 0x0};

            if (bytes.Length == 1)
                return new byte[] {0x0, bytes[0]};

            return new[] {bytes[0], bytes[1]};
        }

        private static DateTimeOffset GetDateTimeOffset(this Guid guid)
        {
            byte[] bytes = guid.ToByteArray();

            var timestampBytes = new byte[8];
            Array.Copy(bytes, TimestampByte, timestampBytes, 0, 8);

            long timestamp = BitConverter.ToInt64(timestampBytes, 0);
            long ticks = timestamp + GregorianCalendarStart.Ticks;

            return new DateTimeOffset(ticks, TimeSpan.Zero);
        }

        public static DateTime GetDateTime(this Guid guid)
        {
            return GetDateTimeOffset(guid).DateTime;
        }

        public static DateTime GetLocalDateTime(this Guid guid)
        {
            return GetDateTimeOffset(guid).LocalDateTime;
        }

        public static DateTime GetUtcDateTime(this Guid guid)
        {
            return GetDateTimeOffset(guid).UtcDateTime;
        }

        public static Guid NewCombGuid()
        {
            ClockSequenceBytes = GenerateClockSequenceBytes();
            return GenerateTimeBasedGuid(DateTime.UtcNow, ClockSequenceBytes);
        }

        private static Guid GenerateTimeBasedGuid(DateTimeOffset dateTime, byte[] clockSequence)
        {
            if (clockSequence == null)
                throw new ArgumentNullException("clockSequence");

            if (clockSequence.Length != 9)
                throw new ArgumentOutOfRangeException("clockSequence", "The clockSequence must be 3 bytes.");

            long ticks = (dateTime - GregorianCalendarStart).Ticks;
            var guid = new byte[ByteArraySize];
            byte[] timestamp = BitConverter.GetBytes(ticks);

            // copy clock sequence
            Array.Copy(clockSequence, 0, guid, GuidClockSequenceByte, Math.Min(9, clockSequence.Length));

            // copy timestamp
            Array.Copy(timestamp, 0, guid, TimestampByte, Math.Min(8, timestamp.Length));

            // set the variant
            guid[VariantByte] &= VariantByteMask;
            guid[VariantByte] |= VariantByteShift;

            return new Guid(guid);
        }
    }
}