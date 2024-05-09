using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts.Ui {
    public class UiCanvas : MonoBehaviour {
        public Image gemImage;
        [SerializeField] private TMP_Text gemCollectCountTMPText;
        private int _gemCollectCount;

        public static UiCanvas instance;

        private void Start() {
            instance = this;
            Canvas canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
        }

        public void IncreaseGemCount() {
            _gemCollectCount++;
            gemCollectCountTMPText.text = _gemCollectCount.ToString();
        }
    }
}