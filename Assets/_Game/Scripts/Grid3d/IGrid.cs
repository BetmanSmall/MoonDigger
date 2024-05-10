using UnityEngine;
namespace _Game.Scripts.Grid3d {
    public interface IGrid {
        public GameObject GetObject(Point coordinate);
        public void SetObject(Point coordinate, GameObject gObject);
        public void RemoveObject(Point coordinate);
    }
}