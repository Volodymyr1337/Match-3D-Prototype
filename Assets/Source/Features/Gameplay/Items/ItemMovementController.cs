using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay.Board;
using Source.Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Features.Gameplay.Items
{
    public class ItemMovementController : BaseController
    {
        private const string ITEM_LAYER = "Tiles";
        private const float DRAG_HEIGHT = 1.5f;

        private Camera _mainCamera;

        private bool _isPlaying = false;
        private bool _isDragging = false;
        private Transform _currentlyDraggingObject;
        private readonly BoardConfiguration _boardConfiguration;

        public ItemMovementController(BoardConfiguration boardConfiguration)
        {
            _boardConfiguration = boardConfiguration;
        }
        
        public override UniTask Initialize()
        {
            _mainCamera = Camera.main;

            GameplayController.OnStartGame += OnStartGame;
            GameplayController.OnGameOver += OnGameFinished;
            var inputService = GetService<IInputService>();
            inputService.OnPointerDown += StartDrag;
            inputService.OnPointerDrag += DragObject;
            inputService.OnPointerUp += StopDrag;
            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            GameplayController.OnStartGame += OnStartGame;
            GameplayController.OnGameOver += OnGameFinished;
            var inputService = GetService<IInputService>();
            inputService.OnPointerDown -= StartDrag;
            inputService.OnPointerDrag -= DragObject;
            inputService.OnPointerUp -= StopDrag;
            
            base.Dispose();
        }

        private void StartDrag()
        {
            if (!EventSystem.current.IsPointerOverGameObject() || !_isPlaying)
            {
                return;
            }
            
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = 1 << LayerMask.NameToLayer(ITEM_LAYER);
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                _currentlyDraggingObject = hit.transform;
                _currentlyDraggingObject.GetComponent<ItemView>().Rigidbody.isKinematic = false;
                
                _isDragging = true;
            }
        }

        private void DragObject()
        {
            if (_isDragging && _currentlyDraggingObject != null)
            {
                Vector3 targetPosition = GetPointerWorldPositionAtHeight();

                Vector2 area = _boardConfiguration.Area;
                Vector2 offset = _boardConfiguration.Offset;
                
                float minX = offset.x - (area.x * 0.5f);
                float maxX = offset.x + (area.x * 0.5f);
                float minY = offset.y - (area.y * 0.5f);
                float maxY = offset.y + (area.y * 0.5f);

                float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
                float clampedY = Mathf.Clamp(targetPosition.y, 0f, DRAG_HEIGHT);
                float clampedZ = Mathf.Clamp(targetPosition.z, minY, maxY);

                _currentlyDraggingObject.position = new Vector3(clampedX, clampedY, clampedZ);
                _currentlyDraggingObject.localScale = Vector3.one * 1.5f;
            }
        }

        private void StopDrag()
        {
            _isDragging = false;
            if (_currentlyDraggingObject != null)
            {
                _currentlyDraggingObject.localScale = Vector3.one;
            }
            _currentlyDraggingObject = null;
        }
        
        private Vector3 GetPointerWorldPositionAtHeight()
        {
            var plane = new Plane( Vector3.up, -DRAG_HEIGHT);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            float enter;
            if (plane.Raycast( ray, out enter))
            {
                Vector3 rayPoint = ray.GetPoint(enter);
                return rayPoint;
            }

            return Vector3.zero;
        }

        private void OnGameFinished(bool result)
        {
            _isPlaying = false;
        }

        private void OnStartGame()
        {
            _isPlaying = true;
        }
    }
}