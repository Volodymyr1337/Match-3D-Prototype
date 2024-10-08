using Cysharp.Threading.Tasks;
using Source.Application;

namespace Source.Features.User
{
    public class UserController : BaseController
    {
        public UserModel UserModel { get; private set; }

        public override UniTask Initialize()
        {
            // TODO: load user data
            UserModel = new UserModel();

            UserModel.OnModelUpdated += SaveUserData;
            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            UserModel.OnModelUpdated -= SaveUserData;
            base.Dispose();
        }

        private void SaveUserData(UserModel model)
        {
            // TODO: serialize user data
        }
    }
}