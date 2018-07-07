using Ducksoft.SOA.BL.Logging.Converters;
using log4net.Layout;
using log4net.Util;

namespace Ducksoft.SOA.BL.Logging.Layouts
{
    /// <summary>
    /// Class which is used to customize the default layout pattern by taking custom fault object.
    /// </summary>
    public class CustFaultLayoutConverter : PatternLayout
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustFaultLayoutConverter"/> class.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The default pattern just produces the application supplied message.
        /// </para>
        /// <para>
        /// Note to Inheritors: This constructor calls the virtual method
        /// <see cref="M:log4net.Layout.PatternLayout.CreatePatternParser(System.String)" />. If you override this method be
        /// aware that it will be called before your is called constructor.
        /// </para>
        /// <para>
        /// As per the <see cref="T:log4net.Core.IOptionHandler" /> contract the <see cref="M:log4net.Layout.PatternLayout.ActivateOptions" />
        /// method must be called after the properties on this object have been
        /// configured.
        /// </para>
        /// </remarks>
        public CustFaultLayoutConverter()
        {
            AddConverter(new ConverterInfo
            {
                Name = "custFaultInfo",
                Type = typeof(CustFaultConverter)

            });
        }
    }
}
