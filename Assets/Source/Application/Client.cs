using System;
using Source.Services.Mono;
using Source.Services.ServicesResolver;
using UnityEngine;

namespace Application
{
    public class Client : MonoBehaviour
    {
        private MonoService _monoService;
        private ControllerFactory _controllerFactory;

        private ServiceResolver _serviceResolver;

        private void Awake()
        {
            InitServices();
        }

        private void InitServices()
        {
            _serviceResolver = new ServiceResolver();
            _serviceResolver.Add(new MonoService(_serviceResolver));
            _serviceResolver.Add(new ControllerFactory(_serviceResolver));
            _serviceResolver.Initialize();
        }

        private void InitControllers()
        {
            
        }
    }
}
