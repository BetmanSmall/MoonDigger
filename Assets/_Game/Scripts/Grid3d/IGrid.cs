using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Scripts.Grid3d {
    public interface IGrid {
        GameObject GetObject(Point coordinate);
        void SetObject(Point coordinate, GameObject gObject);
        void RemoveObject(Point coordinate);
    }
}