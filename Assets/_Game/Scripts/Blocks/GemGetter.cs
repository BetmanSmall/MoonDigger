using _Game.Scripts.Ui;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
namespace _Game.Scripts.Blocks {
    public class GemGetter : MonoBehaviour {
        [SerializeField] private GameObject[] gemsPrefabs;
        private float _percent = 15f;
        private Bounds _bounds;

        private void Start() {
            var renderers = gameObject.GetComponentsInChildren<Renderer>();
            _bounds = renderers[0].bounds;
        }

        public bool TryGetGem() {
            if (Random.Range(0f, 100f) <= _percent) {
                SpawnGemToUser();
                return true;
            }
            return false;
        }

        public void SetPercent(float percent) {
            _percent = percent;
        }

        private float _duration = 4f;
        public void SpawnGemToUser(int count = 1) {
            for (int i = 0; i < count; i++) {
                int index = Random.Range(0, gemsPrefabs.Length);
                Vector3 pos = RandomPointInBounds();
                GameObject gemGameObject = Instantiate(gemsPrefabs[index], pos, Quaternion.identity);
                Debug.DrawLine(pos, HudCanvas.instance.GetImagePos, Color.red, _duration);
                gemGameObject.transform.DOMove(HudCanvas.instance.GetImagePos, _duration-1f).onComplete += () => HudCanvas.instance.IncreaseGemCount();
                gemGameObject.transform.DOScale(Vector3.zero, _duration);
                gemGameObject.transform.DOLocalRotate(Random.insideUnitSphere, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative();
            }
        }

        private Vector3 RandomPointInBounds() {
            return RandomPointInBounds(_bounds);
        }

        private Vector3 RandomPointInBounds(Bounds bounds) {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}