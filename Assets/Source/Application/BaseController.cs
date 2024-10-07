// using System;
// using System.Collections.Generic;
// using Services.Mono;
//
// namespace Application
// {
//     public abstract class BaseController : IDisposable
//     {
//         private List<BaseController> _childControllers = new List<BaseController>();
//         
//         protected ControllerFactory ControllerFactory { get; private set; }
//         protected MonoService MonoService { get; private set; }
//
//         public abstract void Initialize();
//
//         public void InjectDependencies(ControllerFactory controllerFactory, MonoService monoService)
//         {
//             ControllerFactory = controllerFactory;
//             MonoService = monoService;
//         }
//         
//         public virtual void Dispose()
//         {
//             foreach (BaseController childController in _childControllers)
//             {
//                 childController.Dispose();
//             }
//         }
//
//         protected TController CreateController<TController>(TController controller) where TController : BaseController
//         {
//             TController ctr = ControllerFactory.CreateController(controller);
//             _childControllers.Add(ctr);
//             return ctr;
//         }
//         
//         public TController CreateController<TController>() where TController : BaseController, new()
//         {
//             TController ctr = ControllerFactory.CreateController<TController>();
//             _childControllers.Add(ctr);
//             return ctr;
//         }
//     }
// }
