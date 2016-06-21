using Autofac;

namespace Caiyuan.UnitTests
{
    public abstract class UnitTestBase
    {
        private IContainer Container
        {
            get;
            set;
        }
        public UnitTestBase ()
        {
            var builder = new ContainerBuilder();
            //var services = new ServiceCollection();
            
            RegisterTypes(builder);
            //builder.Populate(services);
            Container = builder.Build(); 
            
        }

        protected abstract void RegisterTypes (ContainerBuilder builder);

        protected T Resolve<T> ()
        {
            return Container.Resolve<T>();
        }
    }
}