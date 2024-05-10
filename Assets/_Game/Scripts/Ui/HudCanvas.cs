using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts.Ui {
    public class HudCanvas : MonoBehaviour {
        [SerializeField] private Image gemImage;
        public Vector3 GetImagePos => gemImage.transform.position;
        [SerializeField] private TMP_Text gemCollectCountTMPText;
        private int _gemCollectCount;
        [SerializeField] private Image blockLopataImage;

        public static HudCanvas instance;
        private void Start() {
            instance = this;
            Canvas canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
        }

        public void IncreaseGemCount() {
            _gemCollectCount++;
            gemCollectCountTMPText.text = _gemCollectCount.ToString();
        }

        public void ToggleBlockLopataImage(bool b) {
            blockLopataImage.gameObject.SetActive(b);
        }
    }
}