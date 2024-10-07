using Cysharp.Threading.Tasks;
using Source.Application;

namespace Source.Features.User
{
    public class UserController : BaseController
    {
        private UserModel _userModel;
        
        public override UniTask Initialize()
        {
            // TODO load user data
            _userModel = new UserModel();
            return UniTask.CompletedTask;
        }
        
        
    }
}