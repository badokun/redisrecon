using System.Threading;
using log4net;
using log4net.Config;
using Ninject;
using ServiceStack.Redis;

namespace RedisRecon.Attacker.Console
{
    public class NinjectRegistration
    {
        public IKernel Build()
        {
            var log = log4net.LogManager.GetLogger(typeof (NinjectRegistration));
            XmlConfigurator.Configure();

            IKernel kernel = new StandardKernel();
            kernel.Bind<IAttacker>().To<Rambo>();
            kernel.Bind<IRedisClientsManager>().ToConstant(new BasicRedisClientManager());
            kernel.Bind<ILog>().ToConstant(log);

            return kernel;
        }
    }
}