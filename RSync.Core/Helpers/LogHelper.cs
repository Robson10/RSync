using RSync.AppResources.Configuration;
using RSync.AppResources.Localization;
using System;
using System.Globalization;
using System.IO;

namespace RSync.Core.Helpers
{
    /// <summary>
    /// Messages logger.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Log Error to .csv file.
        /// </summary>
        /// <param name="exception"></param>
        public static void LogError(Exception exception, CultureInfo culture)
        {
            if (exception is null)
            {
                ArgumentNullException ex= new ArgumentNullException(res.exLogNullException);
                LogError(ex, CultureInfo.CurrentCulture);
                throw ex;
            }

            string content = Environment.NewLine +
                    string.Format(culture,
                        res.ErrorLogFormat,
                        DateTime.Now,
                        ReadInnerExceptionMessages(exception),
                        exception.ToString(),
                        exception.StackTrace,
                        exception.Source
                );

            File.AppendAllText(AppPaths.LogFilePath, content);
        }

        /// <summary>
        /// Read all inner exceptions message and return as one message.
        /// </summary>
        /// <param name="exception">Exception.</param>
        /// <returns>Message containing all inner exceptions message.</returns>
        private static string ReadInnerExceptionMessages(Exception exception)
        {
            if (exception != null && !string.IsNullOrEmpty(exception.Message))
            {
                string message = exception.Message;
                string innerMessage = ReadInnerExceptionMessages(exception.InnerException);

                if (!string.IsNullOrEmpty(innerMessage))
                {
                    message += Environment.NewLine + Environment.NewLine + innerMessage;
                }

                return message;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}