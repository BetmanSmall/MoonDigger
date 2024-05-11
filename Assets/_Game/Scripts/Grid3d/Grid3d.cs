using _Game.Scripts.Blocks;
using _Game.Scripts.Ui;
using UnityEngine;
namespace _Game.Scripts.Grid3d {
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

        private void Awake() {
            grid = new GridEntry[length.CellCount, width.CellCount, height.CellCount];
        }

        [SerializeField] private GameObject prefab;
        [SerializeField] private float gemGetPercent = 15f;
        // [SerializeField] private float bonusPercent = 10f;
        // [SerializeField] private float debonusPercent = 7.5f;
        [SerializeField] private float timeForLevel = 150f;
        public static int ActiveBlocksCount { get; set; }

        private void Start() {
            Debug.Log("Grid3d::Start(); -- time:" + Time.time);
            ActiveBlocksCount = 0;
            for (int x = 0; x < length.CellCount; x++) {
                for (int y = 0; y < width.CellCount; y++) {
                    for (int z = 0; z < height.CellCount; z++) {
                        Point point = new Point(x, y, z);
                        GameObject newObject = Instantiate(prefab);
                        if (newObject.TryGetComponent(out GemGetter gemGetter)) {
                            gemGetter.SetPercent(gemGetPercent);
                        }
                        // if (newObject.TryGetComponent(out BonusDebonusGetter bonusDebonusGetter)) {
                        //     bonusDebonusGetter.bonusPercent = bonusPercent;
                        //     bonusDebonusGetter.debonusPercent = debonusPercent;
                        // }
                        SetObject(point, newObject);
                    }
                }
            }
        }

        private void OnEnable() {
            Debug.Log("Grid3d::OnEnable(); -- time:" + Time.time);
            ActiveBlocksCount = 0;
            // HudGameTimer.instance.SetTimerTime(timeForLevel);
        }

        public void SetObject(Point coordinate, GameObject gObject) {
            GridEntry entry = new GridEntry(gObject);
            entry.Wrapper.name = string.Format("({0},{1},{2})", coordinate.X, coordinate.Y, coordinate.Z);
            entry.Wrapper.transform.SetParent(transform);
            entry.Wrapper.transform.localPosition = calcPosition(coordinate);
            grid[coordinate.X, coordinate.Y, coordinate.Z] = entry;
            ActiveBlocksCount++;
        }

        private Vector3 calcPosition(Point coordinate) {
            return new Vector3(
                coordinate.X * (length.CellDistance / length.CellCount),
                coordinate.Y * (width.CellDistance / width.CellCount),
                coordinate.Z * (height.CellDistance / height.CellCount)
            );
        }

        public GameObject GetObject(Point coordinate) {
            return grid[coordinate.X, coordinate.Y, coordinate.Z].Entry;
        }

        public void RemoveObject(Point coordinate) {
            Destroy(grid[coordinate.X, coordinate.Y, coordinate.Z].Entry);
            grid[coordinate.X, coordinate.Y, coordinate.Z] = null;
        }
    }
}