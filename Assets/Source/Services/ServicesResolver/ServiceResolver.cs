using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Services.ServicesResolver
{
    public class ServiceResolver
    {
        private readonly Dictionary<Type, BaseService> _services;
        
        public ServiceResolver()
        {
            _services = new Dictionary<Type, BaseService>();
        }

        #region Get / Add Services

        public void Add<T>(T service) where T : BaseService
        {
            Type type = typeof(T);
            if (!_services.ContainsKey(type))
            {
                _services.Add(type, service);
            }
            else
            {
                Debug.LogError($"Service: {type.Name}  is already bound!");
            }
        }
        
        public void Bind<TKey, TValue>(TValue service) where TValue : BaseService where TKey : IDisposable
        {
            Type type = typeof(TKey);
            if (!_services.ContainsKey(type))
            {
                _services.Add(type, service);
            }
            else
            {
                Debug.LogError($"Service: {type.Name}  is already bound!");
            }
        }
        
        internal void Initialize()
        {
            foreach (var service in _services.Values)
            {
                if (service is BaseService baseService)
                {
                    baseService.Init();
                }
            }
        }
        
        public T Get<T>() where T : class, IDisposable
        {
            if (!_services.TryGetValue(typeof(T), out BaseService iService))
            {
                Debug.LogError($"Failed to get service {typeof(T)}");
            }
            T service = iService as T;

            return service;
        }

        #endregion

        #region Dispose Services
        
        public void DisposeService<T>() where T : BaseService
        {
            var service = Get<T>();
            if (service == null) return;

            service.Dispose();
            _services.Remove(typeof(T));
        }
        
        public void DisposeAllServices()
        {
            foreach (IDisposable service in _services.Values)
            {
                service.Dispose();
            }

            _services.Clear();

        }

        #endregion
    }
}
