using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace AHSokoban
{
    // Minimal prototype: no UI, no scene progression, no input lock on completion.
    public class AHPuzzle : MonoBehaviour
    {
        public Player player;
        public Block[] crates;
        public Vector2Int[] goals;
        public Renderer[] goalLights;
        public Material goalOff, goalOn;

        void Update()
        {
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
                SceneManager.LoadScene(gameObject.scene.path);
        }

        void LateUpdate()
        {
            for (int i = 0; i < goals.Length; i++)
            {
                bool filled = false;
                foreach (Block crate in crates) filled |= crate.gridPos == goals[i];
                goalLights[i].sharedMaterial = filled ? goalOn : goalOff;
            }
        }
    }
}
