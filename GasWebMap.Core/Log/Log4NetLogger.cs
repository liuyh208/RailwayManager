using System;
using log4net;
using log4net.Config;

namespace GasWebMap.Core
{
    internal class Log4NetLogger : ILogger
    {
        private readonly ILog Log;

        #region Implementation of ILogger

        public void Warning(string message)
        {
            Log.Warn(message);
        }

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Info(string message)
        {
            Log.Info(message);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(string message, Exception t)
        {
            Log.Error(message);
            Log.ErrorFormat("Error: {0}, exception: {1}", t.Message, t);
        }

        public void Debug(string format, params object[] args)
        {
            Log.DebugFormat(format, args);
        }

        public void Error(string format, params object[] args)
        {
            Log.ErrorFormat(format, args);
        }

        public void Info(string format, params object[] args)
        {
            Log.InfoFormat(format, args);
        }

        public void Warning(string format, params object[] args)
        {
            Log.WarnFormat(format, args);
        }

        #endregion

        internal Log4NetLogger()
            : this(typeof (ILogger))
        {
        }

        internal Log4NetLogger(Type type)
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(type);
        }
    }
}