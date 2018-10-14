using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Veises.Common.Logging
{
    public static class LogProvider
    {
        private static IServiceProvider _serviceProvider = null;

        public static void Inject([NotNull] IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public static void Inject([NotNull] IServiceCollection serviceCollection)
        {
            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddSingleton(typeof(ILog<>), typeof(DefaultLog<>));
        }
        
        public static ILog GetLogFor<T>()
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("Logging service provider is not defined.");
            
            return _serviceProvider.GetService<ILog<T>>();
        }
    }
}