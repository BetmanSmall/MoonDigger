using System;
using UnityEngine;
namespace _Game.Scripts.Grid3d {
    [Serializable]
    public struct GridDimension {
        [SerializeField] int cellCount;
        [SerializeField] float cellDistance;
        public int CellCount {
            get { return cellCount; }
        }
        public float CellDistance {
            get { return cellDistance; }
        }
    }
}