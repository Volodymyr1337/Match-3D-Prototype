using System;
using Source.Services.Mono;
using Source.Services.ServicesResolver;
using UnityEngine;

namespace Application
{
    public class Client : MonoBehaviour
    {
        private readonly ServiceResolver _serviceResolver = new();

        private void Awake()
        {
            InitServices();
        }

        private void OnDestroy()
        {
            _serviceResolver.DisposeAllServices();
        }

        private void InitServices()
        {
            _serviceResolver.Add(new MonoService(_serviceResolver));
            _serviceResolver.Add(new ControllerFactory(_serviceResolver));
            _serviceResolver.Initialize();
        }

        private void InitControllers()
        {
            
        }
    }
}
