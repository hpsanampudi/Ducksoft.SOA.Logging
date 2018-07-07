using Ducksoft.SOA.BL.Logging.Infrastructure;
using Ducksoft.SOA.Common.Infrastructure;
using log4net;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common.WebHost;
using System.Collections.Generic;

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
            ILog logBuilder = null;
            try
            {
                logBuilder = NInjectHelper.Instance.GetInstance<ILog>();
            }
            catch
            {
                //Hp --> Logic: If log4Net instance is not yet loaded in IOC container, then load it here.
                NInjectHelper.Instance.LoadModules(new List<INinjectModule>
                {
                    new Log4NetModule(),
                });
            }

            return (NInjectHelper.Instance.GetKernel());
        }
    }
}