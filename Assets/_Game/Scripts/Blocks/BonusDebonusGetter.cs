using _Game.Scripts.Ui;
using UnityEngine;
namespace _Game.Scripts.Blocks {
    public class BonusDebonusGetter : MonoBehaviour {
        [SerializeField] private float bonusPercent = 10f;
        [SerializeField] private float debonusPercent = 7.5f;
        [SerializeField] private float halfPercent = 50f;
        [SerializeField] private int bonusGemCount = 5;
        [SerializeField] private float bonusTimeCount = 5f;
        [SerializeField] private float debonusTimeCount = -5f;
        [SerializeField] private int debonusClickToDestroy = 2;

        public void RollBonusAndDebonus(GemGetter gemGetter) {
            if (Random.Range(0f, 100f) <= bonusPercent) {
                if (Random.Range(0f, 100f) <= halfPercent) {
                    gemGetter.SpawnGemToUser(bonusGemCount);
                } else {
                    HudGameTimer.instance.IncreaseOrDecreaseTime(bonusTimeCount);
                }
            } else if (Random.Range(0f, 100f) <= debonusPercent) {
                if (Random.Range(0f, 100f) <= halfPercent) {
                    BlocksDestroyReverter.ChangedClickToDestroy(debonusClickToDestroy);
                } else {
                    HudGameTimer.instance.IncreaseOrDecreaseTime(debonusTimeCount);
                }
            }
        }
    }
}