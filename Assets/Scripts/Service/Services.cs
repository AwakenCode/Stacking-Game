using System;
using System.Collections.Generic;

namespace Service
{
    public class Services
    {
        private static Services _instance;
        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static Services Container => _instance ??= new Services();

        public void Register<TService>(TService service) where TService : IService
        {
            _services.Add(typeof(TService), service);
        }

        public TService Resolve<TService>() where TService : IService
        {
            return (TService)_services[typeof(TService)];
        }
    }
}