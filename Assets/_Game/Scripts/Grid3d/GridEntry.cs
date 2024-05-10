using UnityEngine;
namespace _Game.Scripts.Grid3d {
    public class GridEntry {
        public GameObject Entry { get; private set; }
        public Vector3 Offset { get; private set; }
        public bool GetActive { get; private set; }
        public GameObject Wrapper { get; private set; }

        public GridEntry(GameObject entry = null) {
            Entry = entry;
            Wrapper = new GameObject();
            if (Entry) {
                SetActive(true);
                Entry.transform.SetParent(Wrapper.transform);
            }
        }

        public void SetActive(bool active) {
            GetActive = active;
            Entry.SetActive(active);
        }

        public void UpdateOffset(Vector3 position) {
            // must first calculate the relative move since there won't be any reference to the parent grid
        }
    }
}