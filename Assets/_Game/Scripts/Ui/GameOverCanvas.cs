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
            AudioSource.PlayClipAtPoint(loseAudioClip, Vector3.zero);
        }

        public void ShowWinPanel() {
            winPanel.SetActive(true);
            AudioSource.PlayClipAtPoint(winAudioClip, Vector3.zero);
        }

        public void Reset() {
            losePanel.SetActive(false);
            winPanel.SetActive(false);
            SceneManager.LoadScene(0);
        }
    }
}