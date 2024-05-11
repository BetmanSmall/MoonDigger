using System.Collections.Generic;
using _Game.Scripts.CameraControl;
using _Game.Scripts.Ui;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
namespace _Game.Scripts {
    public class LevelsChanger : MonoBehaviour {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CinemachineBrain cinemachineBrain;
        [SerializeField] private CameraTargetOrientationScript cameraTargetOrientation;
        [SerializeField] private HudGameTimer hudGameTimer;
        [SerializeField] private List<PlayableDirector> playableDirectors;
        // [SerializeField] private PlayableDirector playableDirector1;
        // [SerializeField] private PlayableDirector playableDirector2;
        // [SerializeField] private PlayableDirector playableDirector3;
        // [SerializeField] private int _levelCount = 3;
        [SerializeField] private int _levelIndex = 0;

        private static LevelsChanger instance;
        private void Start() {
            instance = this;
        }

        // public void EnterTimeLineToLevel1() {
        //     hudGameTimer.gameObject.SetActive(false);
        //     playableDirector1.Play();
        // }
        //
        // public void StartLevel1() {
        //     Grid3d.Grid3d grid3d = playableDirector1.GetComponentInChildren<Grid3d.Grid3d>();
        //     grid3d.SetTimeForLevel();
        //     hudGameTimer.gameObject.SetActive(true);
        //     _levelIndex = 1;
        // }
        //
        // public void EnterTimeLineToLevel2() {
        //     hudGameTimer.gameObject.SetActive(false);
        //     playableDirector2.Play();
        // }
        //
        // public void StartLevel2() {
        //     Grid3d.Grid3d grid3d = playableDirector2.GetComponentInChildren<Grid3d.Grid3d>();
        //     grid3d.SetTimeForLevel();
        //     hudGameTimer.gameObject.SetActive(true);
        //     _levelIndex = 2;
        // }
        //
        // public void EnterTimeLineToLevel3() {
        //     hudGameTimer.gameObject.SetActive(false);
        //     playableDirector3.Play();
        // }
        //
        // public void StartLevel3() {
        //     Grid3d.Grid3d grid3d = playableDirector3.GetComponentInChildren<Grid3d.Grid3d>();
        //     grid3d.SetTimeForLevel();
        //     hudGameTimer.gameObject.SetActive(true);
        //     _levelIndex = 3;
        // }

        public void EnterTimeLineToLevel() {
            EnterTimeLineToLevel(_levelIndex);
        }

        public void EnterTimeLineToLevel(int index) {
            hudGameTimer.gameObject.SetActive(false);
            cinemachineBrain.enabled = true;
            playableDirectors[index].Play();
        }

        public void StartLevel() {
            StartLevel(_levelIndex);
        }

        public void StartLevel(int index) {
            Grid3d.Grid3d grid3d = playableDirectors[index].GetComponentInChildren<Grid3d.Grid3d>();
            grid3d.SetTimeForLevel();
            hudGameTimer.gameObject.SetActive(true);
            cinemachineBrain.enabled = false;
            cameraTargetOrientation.SetTarget(grid3d.cameraTarget.transform);
            _levelIndex = index+1;
        }

        public static void ChangeLevelStatic() {
            if (instance) instance.ChangeLevel();
        }

        public void ChangeLevel() {
            if (_levelIndex >= playableDirectors.Count) {
                hudGameTimer.TimerStarted = false;
                GameOverCanvas.instance.ShowWinPanel();
            } else {
                EnterTimeLineToLevel();
            }
        }
    }
}