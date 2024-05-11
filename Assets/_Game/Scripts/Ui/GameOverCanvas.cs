using UnityEngine;
using UnityEngine.SceneManagement;
namespace _Game.Scripts.Ui {
    public class GameOverCanvas : MonoBehaviour {
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private AudioClip loseAudioClip;
        [SerializeField] private AudioClip winAudioClip;

        public static GameOverCanvas instance;

        private void Start() {
            instance = this;
        }

        public void ShowLosePanel() {
            losePanel.SetActive(true);
            AudioManager.PlayAudioClip(loseAudioClip);
        }

        public void ShowWinPanel() {
            winPanel.SetActive(true);
            AudioManager.PlayAudioClip(winAudioClip);
        }

        public void Reset() {
            losePanel.SetActive(false);
            winPanel.SetActive(false);
            SceneManager.LoadScene(0);
        }
    }
}