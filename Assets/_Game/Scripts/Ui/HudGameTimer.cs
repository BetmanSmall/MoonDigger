using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts.Ui {
    public class HudGameTimer : MonoBehaviour {
        [SerializeField] private Image timerImage;
        [SerializeField] private TMP_Text timerTMP;
        [SerializeField] private float timerTime = 100f;
        public bool TimerStarted { get; set; }
        [SerializeField] private GameObject areYouReadyPanel;

        public static HudGameTimer instance;
        private void Start() {
            Debug.Log("HudGameTimer::Start(); -- time:" + Time.time);
            instance = this;
            gameObject.SetActive(false);
        }

        private void Update() {
            if (TimerStarted) {
                timerTime -= Time.deltaTime;
                if (timerTime <= 0f) {
                    GameOverCanvas.instance.ShowLosePanel();
                    enabled = false;
                }
                timerTMP.text = timerTime.ToString("F1");
            }
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

        private void OnEnable() {
            Debug.Log("HudGameTimer::OnEnable(); -- TimerStarted:" + TimerStarted);
            TimerStarted = false;
            areYouReadyPanel.SetActive(true);
        }
    }
}