using System;
using UnityEngine;
namespace _Game.Scripts.Grid3d {
    /// <summary>
    /// Grid will create a uniform distribution of GameObjects that allow easy use of attaching
    /// other objects, making things like tile based placement much easier
    /// </summary>
    public class Grid3d : MonoBehaviour, IGrid {
        [SerializeField] private GridDimension length; //x
        [SerializeField] private GridDimension width; //y
        [SerializeField] private GridDimension height; //z
        public GridDimension Length {
            get { return length; }
        }
        public GridDimension Width {
            get { return width; }
        }
        public GridDimension Height {
            get { return height; }
        }
        private GridEntry[,,] grid; // should be part of the interface

        void Awake() {
            grid = new GridEntry[length.CellCount, width.CellCount, height.CellCount];
        }

        public GameObject prefab;
        
        private void Start() {
            for (int x = 0; x < length.CellCount; x++) {
                for (int y = 0; y < width.CellCount; y++) {
                    for (int z = 0; z < height.CellCount; z++) {
                        Point point = new Point(x, y, z);
                        GameObject newObject = Instantiate(prefab);
                        SetObject(point, newObject);
                    }
                }
            }
        }

        public GameObject GetObject(Point coordinate) {
            return grid[coordinate.X, coordinate.Y, coordinate.Z].Entry;
        }

        /// <summary>
        /// Removes the GameObject on the grid.
        /// </summary>
        /// <param name="coordinate">The location of the object that needs to be removed.</param>
        public void RemoveObject(Point coordinate) {
#warning This doesn't destroy the wrapper object.
            Destroy(grid[coordinate.X, coordinate.Y, coordinate.Z].Entry);
            grid[coordinate.X, coordinate.Y, coordinate.Z] = null;
        }

        /// <summary>
        /// Assign an object to a coordinate point on the grid.
        /// </summary>
        /// <param name="coordinate">The location on the grid where the object should be placed.</param>
        /// <param name="gObject">The GameObject being added to the grid.</param>
        public void SetObject(Point coordinate, GameObject gObject) {
#warning Need to change transform on new objects to be placed properly
            GridEntry entry = new GridEntry(gObject);
            entry.Wrapper.name = string.Format("({0},{1},{2})", coordinate.X, coordinate.Y, coordinate.Z);
            entry.Wrapper.transform.SetParent(transform);
            entry.Wrapper.transform.localPosition = calcPosition(coordinate);
            grid[coordinate.X, coordinate.Y, coordinate.Z] = entry;
        }

        /// <summary>
        /// Calculate the localPosition for a given Point
        /// </summary>
        /// <param name="coordinate">The location to calculate the position for.</param>
        /// <returns></returns>
        private Vector3 calcPosition(Point coordinate) {
            return new Vector3(
                coordinate.X * (length.CellDistance / length.CellCount),
                coordinate.Y * (width.CellDistance / width.CellCount),
                coordinate.Z * (height.CellDistance / height.CellCount)
            );
        }
    }
}