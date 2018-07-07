using Ducksoft.SOA.BL.Logging.Infrastructure;
using Ninject;
using Ninject.Web.Common.WebHost;

namespace Ducksoft.SOA.Logging.WebHost
{
    /// <summary>
    /// Class which is used to intialize NInject dependency injection container used by this service.
    /// </summary>
    /// <seealso cref="Ninject.Web.Common.WebHost.NinjectHttpApplication" />
    public class Global : NinjectHttpApplication
    {
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new Log4NetModule());
        }
    }
}