using UnityEngine;
namespace _Game.Scripts.Blocks {
    [RequireComponent(typeof(GemGetter), 
        typeof(BonusDebonusGetter))]
    public class BlockDestroyer : MonoBehaviour {
        [SerializeField] private GemGetter gemGetter;
        [SerializeField] private BonusDebonusGetter bonusDebonusGetter;
        private int _clickToDestroy = 1;

        private void Start() {
            gemGetter = GetComponent<GemGetter>();
            bonusDebonusGetter = GetComponent<BonusDebonusGetter>();
            BlocksDestroyReverter.AddBlockDestroer(this);
        }

        public void SetClickToDestroy(int value) {
            _clickToDestroy = value;
        }

        public void BlockDestroy() {
            _clickToDestroy--;
            AudioManager.PlayRandomDigs();
            if (_clickToDestroy == 0) {
                gameObject.SetActive(false);
                if (!gemGetter.TryGetGem()) {
                    bonusDebonusGetter.RollBonusAndDebonus(gemGetter);
                }
                Grid3d.Grid3d.ActiveBlocksCount--;
                if (Grid3d.Grid3d.ActiveBlocksCount <= 0) {
                    LevelsChanger.ChangeLevelStatic();
                }
            }
        }
    }
}