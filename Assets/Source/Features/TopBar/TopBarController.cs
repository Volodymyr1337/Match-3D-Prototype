using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.User;

namespace Source.Features.TopBar
{
    public class TopBarController : BaseViewController<TopBarView>
    {
        public TopBarController() : base("TopBarView") { }

        public override async UniTask Initialize()
        {
            await base.Initialize();
            UserModel.OnModelUpdated += OnModelUpdated;
        }

        public override void Dispose()
        {
            UserModel.OnModelUpdated -= OnModelUpdated;
            base.Dispose();
        }

        private void OnModelUpdated(UserModel model)
        {
            View.SetLevel(model.Level);
            View.SetLives(model.Lives);
        }
    }
}