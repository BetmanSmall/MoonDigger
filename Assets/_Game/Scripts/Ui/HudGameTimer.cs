using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts.Ui {
    public class HudGameTimer : MonoBehaviour {
        [SerializeField] private Image timerImage;
        [SerializeField] private TMP_Text timerTMP;
        [SerializeField] float timerTime = 100f;

        public static HudGameTimer instance;
        private void Start() {
            instance = this;
        }

        private void Update() {
            timerTime -= Time.deltaTime;
            if (timerTime <= 0f) {
                // todo lose canvas
                Debug.Log("lose");
            }
            timerTMP.text = timerTime.ToString("F1");
        }

        public void IncreaseOrDecreaseTime(float value) {
            timerTime += value;
            Color colorChange = value > 0 ? Color.green : Color.red;
            timerTMP.DOColor(colorChange, 1f).onComplete += () => {
                timerTMP.DOColor(Color.white, 1f);
            };
        }

        public void SetTimerTime(float value) {
            timerTime = value;
        }
    }
}