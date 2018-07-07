using Ducksoft.SOA.Common.DataContracts;
using log4net.Core;
using log4net.Util;
using System.IO;

namespace Ducksoft.SOA.BL.Logging.Converters
{
    /// <summary>
    /// Class which is used to convert custom fault object content while writing into log appenders.
    /// </summary>
    public class CustFaultConverter : PatternConverter
    {
        /// <summary>
        /// Evaluate this pattern converter and write the output to a writer.
        /// </summary>
        /// <param name="writer"><see cref="T:System.IO.TextWriter" /> that will receive the formatted result.</param>
        /// <param name="state">The state object on which the pattern converter should be executed.</param>
        /// <remarks>
        /// Derived pattern converters must override this method in order to
        /// convert conversion specifiers in the appropriate way.
        /// </remarks>
        protected override void Convert(TextWriter writer, object state)
        {
            if (null == state)
            {
                writer.Write(SystemInfo.NullText);
                return;
            }

            var loggingEvent = state as LoggingEvent;
            if (loggingEvent == null) return;

            var custFaultInfo = loggingEvent.MessageObject as CustomFault;
            if (null == custFaultInfo)
            {
                writer.Write(SystemInfo.NullText);
            }
            else
            {
                switch (Option.ToLower())
                {
                    case "ticketid":
                        {
                            writer.Write(custFaultInfo.TicketNumber.ToString());
                        }
                        break;

                    case "username":
                        {
                            writer.Write(custFaultInfo.UserName);
                        }
                        break;

                    case "appname":
                        {
                            writer.Write(custFaultInfo.AppName);
                        }
                        break;

                    case "message":
                        {
                            writer.Write(custFaultInfo.Message);
                        }
                        break;

                    case "method":
                        {
                            writer.Write(custFaultInfo.SourceMethodName);
                        }
                        break;

                    case "filepath":
                        {
                            writer.Write(custFaultInfo.SourceFilePath);
                        }
                        break;

                    case "lineno":
                        {
                            writer.Write(custFaultInfo.SourceLineNumber);
                        }
                        break;

                    case "callstack":
                        {
                            writer.Write(custFaultInfo.CallStack);
                        }
                        break;

                    case "helplink":
                        {
                            writer.Write(custFaultInfo.HelpLink);
                        }
                        break;

                    default:
                        {
                            writer.Write(SystemInfo.NullText);
                        }
                        break;
                }
            }
        }
    }
}
