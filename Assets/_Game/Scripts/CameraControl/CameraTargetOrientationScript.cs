using UnityEngine;
namespace _Game.Scripts.CameraControl {
    public class CameraTargetOrientationScript : MonoBehaviour {
        [Header("Mouse input:", order = 0)]
        [Space(-10, order = 1)]
        [Header("- Hold and drag RMB to rotate", order = 2)]
        [Space(-10, order = 3)]
        [Header("- Use mouse wheel to zoom in/out", order = 4)]
        [Space(5, order = 5)]
        [Header("Touch input:", order = 6)]
        [Space(-10, order = 7)]
        [Header("- Swipe left/right to rotate", order = 8)]
        [Space(-10, order = 9)]
        [Header("- Use multitouch to zoom in/out", order = 10)]
        [Space(15, order = 11)]
        [SerializeField] private bool enableRotation = true;
        [Header("Choose target")] [SerializeField] private Transform target;

        [Header("Camera fields")]
        private float _smoothness = 0.5f;
        private Vector3 _cameraOffset;

        [Header("Mouse control fields")]
        [Space(2)] [Header("Mouse Controls")] [SerializeField] private float rotationSpeedMouse = 5;
        [SerializeField] private float zoomSpeedMouse = 10;
        [SerializeField] private float defaultZoomMouseMin = 3;
        [SerializeField] private float defaultZoomMouseMax = 15;
        private float _zoomMouseMin = 3;
        private float _zoomMouseMax = 15;
        private float _zoomAmountMouse = 0;
        private float _maxToClampMouse = 10;

        [Header("Touch control fields")]
        [Space(2)] [Header("Touch Controls")] [SerializeField] private float rotationSpeedTouch = 5;
        [SerializeField] private float zoomSpeedTouch = 0.5f;

        private Vector3 _defaultPosition;

        private void Start() {
            SetTarget(target);
        }

        public void SetTarget(Transform target) {
            this.target = target;
            _defaultPosition = transform.position;
            _cameraOffset = transform.position - target.position;
            _zoomMouseMin = target.position.y + defaultZoomMouseMin;
            _zoomMouseMax = target.position.y + defaultZoomMouseMax;
            transform.LookAt(target);
        }

        private void LateUpdate() {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
            // Rotating camera with RMB dragging on PC.
            if (enableRotation && (Input.GetMouseButton(1))) {
                Quaternion camAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeedMouse, Vector3.up);
                Vector3 newPos = target.position + _cameraOffset;
                _cameraOffset = camAngle * _cameraOffset;
                transform.position = Vector3.Slerp(transform.position, newPos, _smoothness);
                transform.LookAt(target);
            }
#endif
            // Rotating camera with touch dragging on mobiles.
#if UNITY_ANDROID || UNITY_IOS
        if (enableRotation && (Input.touchCount==1)) {
            float touchDelta = Mathf.Clamp(Input.GetTouch(0).deltaPosition.x, -1.0f, 1.0f);
            Quaternion camAngle = Quaternion.AngleAxis(touchDelta * rotationSpeedTouch, Vector3.up);
            Vector3 newPos = target.position + _cameraOffset;
            _cameraOffset = camAngle * _cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPos, _smoothness);
            transform.LookAt(target);
        }
#endif
            else {
                // Translating camera on PC with mouse wheel.
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
                float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
                if ((mouseScrollWheel > 0 && transform.position.y > _zoomMouseMin) || (mouseScrollWheel < 0 && transform.position.y < _zoomMouseMax)) {
                    _zoomAmountMouse += mouseScrollWheel;
                    _zoomAmountMouse = Mathf.Clamp(_zoomAmountMouse, -_maxToClampMouse, _maxToClampMouse);
                    var translate = Mathf.Min(Mathf.Abs(mouseScrollWheel), _maxToClampMouse - Mathf.Abs(_zoomAmountMouse));
                    float z = translate * zoomSpeedMouse * Mathf.Sign(mouseScrollWheel);
                    transform.Translate(0, 0, z);
                    _cameraOffset = transform.position - target.position;
                    _zoomAmountMouse = 0f;
                }
#endif
                // Changing FOV on mobiles with multitouch.
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount == 2) {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                this.GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * zoomSpeedTouch;
                // Clamp the field of view to make sure it's between 0 and 180.
                this.GetComponent<Camera>().fieldOfView = Mathf.Clamp(this.GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
            }
#endif
            }
            if (transform.position.y <= 0) {
                transform.position = _defaultPosition;
                transform.LookAt(target);
            }
        }
    }
}