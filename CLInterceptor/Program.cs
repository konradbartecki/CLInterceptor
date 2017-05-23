using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Common;

namespace CLInterceptor
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            var config = ConfigurationManager.OpenExeConfiguration("CLInterceptor.exe.config");
            
            var targetExecutable = config.AppSettings.Settings["TargetExecutable"].Value;
            var arguments = string.Join(" ", args);

            var startinfo = new ProcessStartInfo(targetExecutable, arguments)
            {
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            logger.Info($"Starting task \"{targetExecutable}\" with arguments \"{arguments}\"");
            logger.Factory.Flush();
            Process.Start(startinfo);

        }
    }
}
