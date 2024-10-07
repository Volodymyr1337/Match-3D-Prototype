// using Services.Mono;
//
// namespace Application
// {
//     public class ControllerFactory
//     {
//         private MonoService _monoService;
//         
//         public ControllerFactory(MonoService monoService)
//         {
//             _monoService = monoService;
//         }
//
//         public TController CreateController<TController>(TController controller) where TController : BaseController
//         {
//             controller.InjectDependencies(this, _monoService);
//             return controller;
//         }
//         
//         public TController CreateController<TController>() where TController : BaseController, new()
//         {
//             TController controller = new TController();
//             controller.InjectDependencies(this, _monoService);
//             return controller;
//         }
//     }
// }
