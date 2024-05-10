using System;
using _Game.Scripts.Ui;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
namespace _Game.Scripts {
    public class GemGetter : MonoBehaviour {
        public GameObject[] gemsPrefabs;
        public float percent = 0.3f;

        public void TryGetGem() {
            gameObject.SetActive(false);
            if (Random.Range(0f, 1f) <= percent) {
                int index = Random.Range(0, gemsPrefabs.Length);
                GameObject gemGameObject = Instantiate(gemsPrefabs[index], gameObject.transform.position, Quaternion.identity);
                Debug.DrawLine(gemGameObject.transform.position, UiCanvas.instance.gemImage.transform.position, Color.red, 3f);
                gemGameObject.transform.DOMove(UiCanvas.instance.gemImage.transform.position, 3f).onComplete += () => UiCanvas.instance.IncreaseGemCount();
                gemGameObject.transform.DOScale(Vector3.zero, 4f);
                gemGameObject.transform.DOLocalRotate(Random.insideUnitSphere, 1.0f).SetRelative().SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            }
        }
    }
}