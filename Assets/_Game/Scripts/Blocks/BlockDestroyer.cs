using UnityEngine;
namespace _Game.Scripts.Blocks {
    [RequireComponent(typeof(GemGetter), 
        typeof(BonusDebonusGetter))]
    public class BlockDestroyer : MonoBehaviour {
        [SerializeField] private GemGetter gemGetter;
        [SerializeField] private BonusDebonusGetter bonusDebonusGetter;
        private int _clickToDestroy = 1;
        [SerializeField] private ParticleSystem particleSystemDust;

        private void Start() {
            gemGetter = GetComponent<GemGetter>();
            bonusDebonusGetter = GetComponent<BonusDebonusGetter>();
            BlocksDestroyReverter.AddBlockDestroer(this);
        }

        public void SetClickToDestroy(int value) {
            _clickToDestroy = value;
        }

        public void BlockDestroy() {
            particleSystemDust.gameObject.transform.SetParent(null);
            particleSystemDust.gameObject.SetActive(true);
            if (!particleSystemDust.isPlaying) particleSystemDust.Play();
            _clickToDestroy--;
            AudioManager.PlayRandomDigs();
            if (_clickToDestroy == 0) {
                gameObject.SetActive(false);
                if (!gemGetter.TryGetGem()) {
                    bonusDebonusGetter.RollBonusAndDebonus(gemGetter);
                }
                if (Grid3d.Grid3d.BlockDestroyAndNowEmpty()) {
                    LevelsChanger.ChangeLevelStatic();
                }
                Invoke(nameof(DestroyParticle), 5f);
            }
        }

        private void DestroyParticle() {
            Destroy(particleSystemDust.gameObject);
        }
    }
}