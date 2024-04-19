using System;
using System.Collections.Generic;

namespace Source.Infrastructure
{
    public class Services
    {
        private static Services _instance;
        
        public static Services Container => _instance ??= new Services();

        private readonly Dictionary<Type, object> _services;

        public TService Single<TService>() where TService : IService => (TService)_services[typeof(TService)];

        private Services()
        {
            _services = new Dictionary<Type, object>();
        }
        
        public TService RegisterSingle<TService>(TService implementation) where TService : IService
        {
            if (_services.ContainsKey(typeof(TService)))
            {
                throw new ArgumentException($"Service {typeof(TService)} already registered!");
            }
            
            _services.Add(typeof(TService), implementation);
            return implementation;
        }
        
        public TService ForceRegister<TService>(TService implementation) where TService : IService
        {
            Unregister<TService>();
            return RegisterSingle(implementation);
        }

        public void Unregister<TService>() where TService : IService
        {
            if (!_services.ContainsKey(typeof(TService)))
            {
                throw new ArgumentException($"Service {typeof(TService)} was not registered!");
            }

            Type serviceType = typeof(TService);
            _services.Remove(serviceType);
        }
    }
}
