using System.Collections.Generic;
using _Game.Scripts.Blocks;
using _Game.Scripts.Ui;
using UnityEngine;
namespace _Game.Scripts {
    public class BlocksDestroyReverter : MonoBehaviour {
        [SerializeField] private int timeToRevert = 10;
        private List<BlockDestroyer> _blockDestroyers;

        public static BlocksDestroyReverter instance;
        private void Start() {
            instance = this;
        }

        public static void AddBlockDestroer(BlockDestroyer blockDestroyer) {
            if (instance) {
                instance._blockDestroyers ??= new List<BlockDestroyer>();
                instance._blockDestroyers.Add(blockDestroyer);
            }
        }

        public static void ChangedClickToDestroy(int value) {
            Debug.Log("BlocksDestroyReverter::ChangedClickToDestroy(); -- value:" + value);
            Debug.Log("BlocksDestroyReverter::ChangedClickToDestroy(); -- instance:" + instance);
            if (instance) {
                foreach (BlockDestroyer blockDestroyer in instance._blockDestroyers) {
                    blockDestroyer.SetClickToDestroy(value);
                }
                HudCanvas.instance.ToggleBlockLopataImage(true);
                instance.Invoke(nameof(ChangeBackClickToDestroy), instance.timeToRevert);
            }
        }

        public void ChangeBackClickToDestroy() {
            Debug.Log("BlocksDestroyReverter::ChangeBackClickToDestroy(); -- ");
            foreach (BlockDestroyer blockDestroyer in instance._blockDestroyers) {
                blockDestroyer.SetClickToDestroy(1);
            }
            HudCanvas.instance.ToggleBlockLopataImage(false);
        }
    }
}