using Application;
using Cysharp.Threading.Tasks;

namespace Source.Features.User
{
    public class UserController : BaseController
    {
        private UserModel _userModel;
        
        public override UniTask Initialize()
        {
            // TODO load user data
            return UniTask.CompletedTask;
        }
        
        
    }
}