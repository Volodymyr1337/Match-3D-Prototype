﻿using System;
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
        
        public bool TryGet<T>(out T service) where T : BaseService
        {
            bool serviceExists = _services.TryGetValue(typeof(T), out BaseService iService);
            service = iService as T;
            if (service == null)
            {
                Debug.LogError($"Failed to resolve service {typeof(T)}: " +
                               $"service exists: {serviceExists} " +
                               $"IService exists: {iService != null}");
            }

            return service != null;
        }

        #endregion

        #region Dispose Services
        
        public void DisposeService<T>() where T : BaseService
        {
            if (!TryGet<T>(out T service)) return;

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
