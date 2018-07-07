using log4net;
using log4net.Config;
using Ninject.Modules;

namespace Ducksoft.SOA.BL.Logging.Infrastructure
{
    /// <summary>
    /// Class which is used to configure bindings related to dependency injection through NInject.
    /// </summary>
    public class Log4NetModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            XmlConfigurator.Configure();
            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(ctx.Request.Target.Member.DeclaringType));
        }
    }
}
