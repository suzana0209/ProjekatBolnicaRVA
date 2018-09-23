using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KlijentBolnica.ViewModel
{
    class LogVM : AppenderSkeleton
    {
        private static LogVM instance = null;

        public static ObservableCollection<LogInfos> Logs { get; set; } = new ObservableCollection<LogInfos>();

        public LogVM() : base()
        {
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            Console.WriteLine("-------------------------> Apendeer radi!");

            LogInfos infos = new LogInfos()
            {
                Timestamp = loggingEvent.TimeStamp,
                Level = loggingEvent.Level.DisplayName,
                Message = loggingEvent.RenderedMessage
            };

            Application.Current.Dispatcher.Invoke(new Action(() => { Logs.Add(infos); }));
        }

        public static LogVM Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogVM();
                }
                return instance;
            }
        }
    }

    public class LogInfos
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
    }
}
