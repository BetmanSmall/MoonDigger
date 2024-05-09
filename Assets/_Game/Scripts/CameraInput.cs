using _Game.Scripts;
using UnityEngine;
public class CameraInput : MonoBehaviour {
    private Camera _mainCamera;

    private void Start() {
        _mainCamera = Camera.main;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("CameraInput::Update(); -- Input.mousePosition:" + Input.mousePosition);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.white);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                Debug.Log("CameraInput::Update(); -- hit.collider:" + hit.collider);
                if (hit.collider) {
                    if (hit.collider.gameObject.TryGetComponent(out GemGetter gemGetter)) {
                        gemGetter.TryGetGem();
                    }
                }
            }
        }
    }
}