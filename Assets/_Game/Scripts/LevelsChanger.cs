using _Game.Scripts.CameraControl;
using UnityEngine;
using UnityEngine.Playables;
namespace _Game.Scripts {
    public class LevelsChanger : MonoBehaviour {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CameraTargetOrientationScript cameraTargetOrientation;
        [SerializeField] private PlayableDirector playableDirector1;
        private int _levelIndex;
    }
}