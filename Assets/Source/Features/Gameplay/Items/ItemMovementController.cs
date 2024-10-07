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
        private const string DRAGGABLE_TAG = "Draggable";
        private const float DRAG_HEIGHT = 1.5f;

        private Camera _mainCamera;
        
        private bool _isDragging = false;
        private Vector3 _offset;
        private Transform _currentlyDraggingObject;
        private Rigidbody _currentlyDraggingRigidbody;
        
        private readonly BoardConfiguration _boardConfiguration;

        public ItemMovementController(BoardConfiguration boardConfiguration)
        {
            _boardConfiguration = boardConfiguration;
        }
        
        public override UniTask Initialize()
        {
            _mainCamera = Camera.main;
            
            var inputService = GetService<IInputService>();
            inputService.OnPointerDown += StartDrag;
            inputService.OnPointerDrag += DragObject;
            inputService.OnPointerUp += StopDrag;
            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            var inputService = GetService<IInputService>();
            inputService.OnPointerDown -= StartDrag;
            inputService.OnPointerDrag -= DragObject;
            inputService.OnPointerUp -= StopDrag;
            
            base.Dispose();
        }

        private void StartDrag()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 100f) && hit.transform.CompareTag(DRAGGABLE_TAG))
            {
                _currentlyDraggingObject = hit.transform;
                _offset = _currentlyDraggingObject.position - GetPointerWorldPositionAtHeight();

                _currentlyDraggingRigidbody = _currentlyDraggingObject.GetComponent<Rigidbody>();
                _currentlyDraggingRigidbody.useGravity = false;
                
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
                float clampedZ = Mathf.Clamp(targetPosition.z, minY, maxY);

                _currentlyDraggingObject.position = new Vector3(clampedX, targetPosition.y, clampedZ);

                // if (_currentlyDraggingObject.position.y <= DRAG_HEIGHT)
                // {
                //     _currentlyDraggingRigidbody.velocity = Vector3.up * _currentlyDraggingRigidbody.mass;
                // }
                // else
                // {
                //     _currentlyDraggingRigidbody.velocity = Vector3.zero;
                // }
            }
        }

        private void StopDrag()
        {
            _isDragging = false;
            _currentlyDraggingRigidbody.useGravity = true;
            _currentlyDraggingObject = null;
            //_currentlyDraggingRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        }
        
        private Vector3 GetPointerWorldPositionAtHeight()
        {
            var plane = new Plane( Vector3.up, -DRAG_HEIGHT);
            Ray ray = _mainCamera.ScreenPointToRay( Input.mousePosition);
            float enter;
            if( plane.Raycast( ray, out enter ) )
            {
                Vector3 rayPoint = ray.GetPoint(enter);
                return rayPoint;
            }

            return Vector3.zero;
        }
    }

}