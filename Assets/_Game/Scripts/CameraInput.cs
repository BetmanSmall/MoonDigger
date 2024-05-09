using UnityEngine;
public class CameraInput : MonoBehaviour {
    private Camera mainCamera;

    private void Start() {
        mainCamera = Camera.main;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("CameraInput::Update(); -- Input.mousePosition:" + Input.mousePosition);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.white);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                Debug.Log("CameraInput::Update(); -- hit.collider:" + hit.collider);
                if (hit.collider != null) {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}