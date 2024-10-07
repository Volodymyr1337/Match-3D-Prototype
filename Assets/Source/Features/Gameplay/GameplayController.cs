using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Board;
using Source.Features.Gameplay.Items;

namespace Source.Features.Gameplay
{
    public class GameplayController : BaseController
    {
        private BoardController _boardController;
        
        public override async UniTask Initialize()
        {
            _boardController = CreateController<BoardController>();
            await _boardController.Initialize();

            CreateController(new ItemMovementController(_boardController.Configuration)).Initialize();
            
            StartGame();
        }
        
        private void StartGame()
        {
            _boardController.GenerateBoard();
        }

        // var timer = new Timer(10);
        // timer.OnComplete += OnTimerComplete;
        // timer.StartTimer().Forget();
        // private void OnTimerComplete()
        // {
        //     Debug.Log("game over!");
        // }
    }
}