using System;
using StructureMap;

namespace IoC.Initialize
{
    public sealed class Application
    {
        private static readonly object LockObject = new object();
        private static Application currentApplicationInstance = new Application();

        private readonly IContainer container;

        private Application()
        {
            container = new Container();
            container.Configure(x => x.For<IContainer>().Singleton().Use(container));
        }

        public static bool IsInitialized { get; private set; }

        public static Application Current
        {
            get { return currentApplicationInstance; }
        }

        public IContainer Container
        {
            get { return container; }
        }

        public static Application Initialize(Action<ConfigurationExpression> configurationAction)
        {
            lock (LockObject)
            {
                currentApplicationInstance = new Application();
                Current.Container.Configure(configurationAction);
                IsInitialized = true;
            }
            return Current;
        }
    }
}
