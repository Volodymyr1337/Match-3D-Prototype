using Source.Features.User;
using Source.Services.ServicesResolver;

namespace Source.Features.Gameplay.EndGame
{
    public abstract class BaseGameOverStrategy : IGameOverStrategy
    {
        protected UserModel _userModel;
        protected ServiceResolver _serviceResolver;
        
        protected BaseGameOverStrategy(UserModel userModel, ServiceResolver serviceResolver)
        {
            _serviceResolver = serviceResolver;
            _userModel = userModel;
        }
        
        public abstract void Execute();
    }
}