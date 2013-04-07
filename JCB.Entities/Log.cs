using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Web;


namespace JCB.DAL
{

    public class Log
    {

        public static void Append(Exception e)
        {

            if (ConfigurationManager.AppSettings["LogEnable"] == "ON")
            {
                string logFormat = string.Format("{0}:{1} <br> {2}<br><br>", DateTime.Now.ToString("hh:mm:ss"),
                    e.Message,
                    e.StackTrace).Replace("<br>", Environment.NewLine);

                Write(logFormat);
            }
        }

        public static void Append(string logText)
        {
            if (ConfigurationManager.AppSettings["LogEnable"] == "ON")
            {
                string logFormat = string.Format("{0}:{1} <br>",
                    DateTime.Now.ToString("hh:mm:ss"),
                    logText).Replace("<br>", Environment.NewLine);

                Write(logFormat);
            }

        }

        private static void Write(string logText)
        {

            string date = DateTime.Now.ToString(ConfigurationManager.AppSettings["LogFileDateFormat"]);
            string logFilePath = ConfigurationManager.AppSettings["LogFolderPath"] + string.Format(ConfigurationManager.AppSettings["LogFileFormat"], date);
            StreamWriter log = null;
            try
            {
                // Create a writer and open the file:
                log = File.Exists(logFilePath) ? File.AppendText(logFilePath) : new StreamWriter(logFilePath);
                // Write to the file:
                log.WriteLine(logText);
                log.Flush();
            }
            finally
            {
                // Close the stream:
                if (log != null)
                    log.Close();
            }
        }
    }

}

