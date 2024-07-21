using System;
using System.Collections.Generic;

namespace Core
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static T Resolve<T>() where T : class
        {
            return _services[typeof(T)] as T;
        }

        public static void Bind<T>(T service)
        {
            _services.Add(typeof(T), service);
        }
    }
}