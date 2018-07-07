using Ducksoft.SOA.Common.Contracts;
using Ducksoft.SOA.Common.DataContracts;
using Ducksoft.SOA.Common.Infrastructure;
using System;
using System.Collections.Generic;

namespace Ducksoft.SOA.Logging.Console
{
    class Program
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILoggingService logger = LoggingServiceHelper.AddOrGetLoggingService;

        static void Main(string[] args)
        {
            try
            {
                throw (new NotImplementedException("Testing Logging Service via console"));
            }
            catch (Exception ex)
            {
                var errMessage = string.Join(Environment.NewLine, ex.Messages());
                var ticket = logger.Error(new CustomFault(errMessage, ex));

                System.Console.WriteLine(string.Join(Environment.NewLine, new List<string>
                {
                    $"Ticket: {ticket}",
                    $"Message: {errMessage}",
                }));
            }
            finally
            {
                System.Console.ReadLine();
            }
        }
    }
}
