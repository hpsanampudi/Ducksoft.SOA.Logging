using Ducksoft.SOA.Common.Contracts;
using Ducksoft.SOA.Common.DataContracts;
using log4net;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace Ducksoft.SOA.BL.Logging.Services
{
    /// <summary>
    /// Class which holds the implementation of contracts exposed by logging WCF service.
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class LoggingService : ILoggingService
    {
        /// <summary>
        /// The log builder
        /// </summary>
        private readonly ILog log4NetBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LoggingService(ILog logger)
        {
            log4NetBuilder = logger;
        }

        /// <summary>
        /// Debugs the specified message and exception.
        /// </summary>
        /// <param name="fault">The fault.</param>
        public Guid Debug(CustomFault fault) => WriteLog(fault, LogMessageTypes.Debug);

        /// <summary>
        /// Errors the specified message and exception.
        /// </summary>
        /// <param name="fault">The fault.</param>
        public Guid Error(CustomFault fault) => WriteLog(fault, LogMessageTypes.Error);

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="fault">The fault.</param>
        /// <returns></returns>
        public Guid Fatal(CustomFault fault) => WriteLog(fault, LogMessageTypes.Fatal);

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="fault">The fault.</param>
        /// <returns></returns>
        public Guid Warning(CustomFault fault) => WriteLog(fault, LogMessageTypes.Warning);

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="fault">The fault.</param>
        /// <returns></returns>
        public Guid Information(CustomFault fault) => WriteLog(fault, LogMessageTypes.Information);

        /// <summary>
        /// Writes the log message to register Log4Net appenders in config file.
        /// </summary>
        /// <param name="fault">The fault.</param>
        /// <param name="logType">Type of the log.</param>
        /// <returns></returns>
        private Guid WriteLog(CustomFault fault, LogMessageTypes logType)
        {
            //Hp --> Logic: Break if we are debugging the code
            if ((Debugger.IsAttached) &&
                ((logType == LogMessageTypes.Error) || (logType == LogMessageTypes.Fatal)))
            {
                Debugger.Break();
            }

            var isSuccess = false;
            var ticketId = Guid.Empty;
            if (fault == null)
            {
                return (ticketId);
            }

            try
            {
                fault.LogType = logType;
                switch (fault.LogType)
                {
                    case LogMessageTypes.None:
                        break;

                    case LogMessageTypes.Debug:
                        {
                            if (log4NetBuilder.IsDebugEnabled)
                            {
                                log4NetBuilder.Debug(fault);
                                isSuccess = true;
                            }
                        }
                        break;

                    case LogMessageTypes.Error:
                        {
                            if (log4NetBuilder.IsErrorEnabled)
                            {
                                log4NetBuilder.Error(fault);
                                isSuccess = true;
                            }
                        }
                        break;

                    case LogMessageTypes.Fatal:
                        {
                            if (log4NetBuilder.IsFatalEnabled)
                            {
                                log4NetBuilder.Fatal(fault);
                                isSuccess = true;
                            }
                        }
                        break;

                    case LogMessageTypes.Warning:
                        {
                            if (log4NetBuilder.IsWarnEnabled)
                            {
                                log4NetBuilder.Warn(fault);
                                isSuccess = true;
                            }
                        }
                        break;

                    case LogMessageTypes.Information:
                        {
                            if (log4NetBuilder.IsInfoEnabled)
                            {
                                log4NetBuilder.Info(fault);
                                isSuccess = true;
                            }
                        }
                        break;

                    default:
                        {
                            isSuccess = false;
                        }
                        break;
                }
            }
            catch
            {
                isSuccess = false;
            }
            finally
            {
                if (!isSuccess)
                {
                    //Hp --> Logic: If log4Net fails to write then return empty ticket id.
                    fault.TicketNumber = Guid.Empty;
                }

                ticketId = fault.TicketNumber;
                if (fault.IsNotifyUser)
                {
                    //Hp --> Logic: Shows generic error message to user.
                    throw new FaultException<CustomFault>(fault, fault.Reason);
                }
            }

            return (ticketId);
        }
    }
}
