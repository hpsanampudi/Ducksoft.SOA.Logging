using Ducksoft.SOA.Common.DataContracts;
using log4net.Core;
using log4net.Layout;
using System.Xml;

namespace Ducksoft.SOA.BL.Logging.Layouts
{
    /// <summary>
    /// Class which is used to customize the default layout pattern of rolling flat file appender
    /// to Xml format.
    /// </summary>
    public class CustLog4NetXmlLayout : XmlLayoutBase
    {
        /// <summary>
        /// Does the actual writing of the XML.
        /// </summary>
        /// <param name="writer">The writer to use to output the event to.</param>
        /// <param name="loggingEvent">The event to write.</param>
        /// <remarks>
        /// Subclasses should override this method to format
        /// the <see cref="T:log4net.Core.LoggingEvent" /> as XML.
        /// </remarks>
        protected override void FormatXml(XmlWriter writer, LoggingEvent loggingEvent)
        {
            var myFault = loggingEvent.MessageObject as CustomFault;

            #region Writing log entry related information.
            writer.WriteStartElement("LogEntry");
            writer.WriteAttributeString("ticketId",
                (null != myFault) ? myFault.TicketNumber.ToString() : string.Empty);

            writer.WriteAttributeString("level", loggingEvent.Level.DisplayName);
            writer.WriteAttributeString("userName",
                (null != myFault) ? myFault.UserName : loggingEvent.UserName);

            writer.WriteAttributeString("dateTime",
                loggingEvent.TimeStamp.ToString("dd/MM/yyyy HH:mm:ss"));

            #region Writing message related information.
            writer.WriteStartElement("Message");
            if (null != myFault)
            {
                writer.WriteAttributeString("appName", myFault.AppName);
                writer.WriteAttributeString("helpLink", myFault.HelpLink);
            }

            writer.WriteString((null != myFault) ? myFault.Message : loggingEvent.RenderedMessage);
            writer.WriteEndElement();
            #endregion

            #region Writing call stack related information.
            writer.WriteStartElement("CallStack");
            if (null != myFault)
            {
                writer.WriteAttributeString("method", myFault.SourceMethodName);
                writer.WriteAttributeString("line", myFault.SourceLineNumber.ToString());
                writer.WriteAttributeString("file", myFault.SourceFilePath);

            }

            writer.WriteString((null != myFault) ? myFault.CallStack :
                ((null != loggingEvent.ExceptionObject) ? loggingEvent.ExceptionObject.ToString() :
                string.Empty));

            writer.WriteEndElement();
            #endregion

            writer.WriteEndElement();
            #endregion
        }
    }
}
