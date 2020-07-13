using System;
using System.IO;

namespace Zadatak_1.Models
{
    class Logger
    {
        readonly string source = @"../../Actions.txt";
        readonly string formatForDateTime = "dd.MM.yyyy HH:mm";

        public delegate void Logging(string text);
        public event Logging OnLogging;
        /// <summary>
        /// This method raises an event.
        /// </summary>
        /// <param name="text">Text to be written in the file.</param>
        protected void LogAction(string text)
        {
            OnLogging = Action;
            OnLogging?.Invoke(text);
        }
        /// <summary>
        /// This method is a handler for OnLogging event. Writes in file an action.
        /// </summary>
        /// <param name="text">Text which writes.</param>
        public void Action(string text)
        {
            StreamWriter str = new StreamWriter(source, true);
            str.WriteLine("[{1}] {0}", text, DateTime.Now.ToString(formatForDateTime));
            str.Close();
        }
    }
}