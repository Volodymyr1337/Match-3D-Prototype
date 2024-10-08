using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Services.ServicesResolver;

namespace Source.Application
{
    public abstract class BaseController : IDisposable
    {
        private List<BaseController> _childControllers = new List<BaseController>();
        
        protected ControllerFactory ControllerFactory { get; private set; }
        protected ServiceResolver ServiceResolver { get; private set; }

        public abstract UniTask Initialize();

        public void InjectDependencies(ControllerFactory controllerFactory, ServiceResolver serviceResolver)
        {
            ControllerFactory = controllerFactory;
            ServiceResolver = serviceResolver;
        }
        
        public virtual void Dispose()
        {
            foreach (BaseController childController in _childControllers)
            {
                childController.Dispose();
            }
        }
        
        protected TService GetService<TService>() where TService : class, IDisposable
        {
            return ServiceResolver.Get<TService>();
        }

        protected TController CreateController<TController>(TController controller) where TController : BaseController
        {
            TController ctr = ControllerFactory.CreateController(controller);
            _childControllers.Add(ctr);
            return ctr;
        }
        
        public TController CreateController<TController>() where TController : BaseController, new()
        {
            TController ctr = ControllerFactory.CreateController<TController>();
            _childControllers.Add(ctr);
            return ctr;
        }
    }
}
