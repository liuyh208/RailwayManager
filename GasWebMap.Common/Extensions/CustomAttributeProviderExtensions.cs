namespace System.Reflection
{
    public static class CustomAttributeProviderExtensions
    {
        //
        public static T GetOneAttribute<T>(this ICustomAttributeProvider member)
            where T : Attribute
        {
            return member.GetOneAttribute<T>(false);
        }

        public static T GetOneAttribute<T>(this ICustomAttributeProvider member, bool inherit)
            where T : Attribute
        {
            var attributes = member.GetCustomAttributes(typeof (T), inherit) as T[];

            if ((attributes == null) || (attributes.Length == 0))
                return null;
            return attributes[0];
        }

        public static T[] GetAllAttributes<T>(this ICustomAttributeProvider member)
            where T : Attribute
        {
            return member.GetAllAttributes<T>(false);
        }

        public static T[] GetAllAttributes<T>(this ICustomAttributeProvider member, bool inherit)
            where T : Attribute
        {
            return member.GetCustomAttributes(typeof (T), inherit) as T[];
        }

        public static bool HasAttribute<T>(this ICustomAttributeProvider member)
            where T : Attribute
        {
            return member.HasAttribute<T>(false);
        }

        public static bool HasAttribute<T>(this ICustomAttributeProvider member, bool inherit)
            where T : Attribute
        {
            return member.IsDefined(typeof (T), inherit);
        }
    }
}