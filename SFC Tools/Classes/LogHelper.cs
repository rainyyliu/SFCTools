using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SFC_Tools.Classes
{
    public class LogHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteLog(Type t, Exception ex)

        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(Type t, string msg)

        public static void WriteLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        public static void WriteLog(string msg)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.ILog log = log4net.LogManager.GetLogger("logdebug");
            log.Debug(msg);
        }

        public static void  WriteError(string msg)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.ILog log = log4net.LogManager.GetLogger("logerror");
            log.Error(msg);
        }

        #endregion
    }
}
