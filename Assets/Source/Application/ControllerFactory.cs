using Source.Services.ServicesResolver;

namespace Application
{
    public class ControllerFactory : BaseService
    {

        public ControllerFactory(ServiceResolver serviceResolver) : base(serviceResolver)
        {
        }

        protected override void Initialize() { }

        public override void Dispose() { }
        
        public TController CreateController<TController>(TController controller) where TController : BaseController
        {
            controller.InjectDependencies(this, ServiceResolver);
            return controller;
        }
        
        public TController CreateController<TController>() where TController : BaseController, new()
        {
            TController controller = new TController();
            controller.InjectDependencies(this, ServiceResolver);
            return controller;
        }
    }
}
