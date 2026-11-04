using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace AHSokoban
{
    // 只管理我们自己的关卡；老师的移动与网格脚本不需要修改。
    [DefaultExecutionOrder(100)]
    public class AHPuzzle : MonoBehaviour
    {
        public Player player;
        public Block[] crates;
        public Vector2Int[] goals;
        public Renderer[] goalLights;
        public Material goalOff, goalOn;

        [Header("Level progression")]
        public int roomNumber = 1;
        // 使用完整场景路径，避免和同学的同名场景冲突。最后一关留空。
        public string nextScenePath;
        // 默认按 Enter 继续，符合老师的要求；勾选后则延时自动过关。
        public bool autoAdvance = false;
        public float advanceDelay = 1.5f;
        public AudioClip pushSound, winSound;

        bool won, loading;
        float wonTime;
        Vector2Int[] previousPositions;
        AudioSource sound;

        void Start()
        {
            sound = GetComponent<AudioSource>();
            if (sound == null) sound = gameObject.AddComponent<AudioSource>();
            sound.playOnAwake = false;
            sound.spatialBlend = 0;
            if (player == null || crates == null || crates.Length == 0 ||
                goals == null || goals.Length != crates.Length ||
                goalLights == null || goalLights.Length != goals.Length)
            {
                Debug.LogError("AH: Check player, crates, goals and goalLights in AHPuzzle.", this);
                enabled = false;
                return;
            }
            previousPositions = new Vector2Int[crates.Length];
            for (int i = 0; i < crates.Length; i++) previousPositions[i] = crates[i].gridPos;
        }

        void Update()
        {
            if (loading) return;
            var keys = Keyboard.current;
            // R 随时重新加载当前关卡，箱子、方向和目标全部复原。
            if (keys != null && keys.rKey.wasPressedThisFrame)
            {
                LoadScene(gameObject.scene.path);
                return;
            }
            if (!won || string.IsNullOrEmpty(nextScenePath)) return;
            if ((keys != null && (keys.enterKey.wasPressedThisFrame || keys.numpadEnterKey.wasPressedThisFrame)) ||
                (autoAdvance && Time.unscaledTime - wonTime >= advanceDelay))
                LoadScene(nextScenePath);
        }

        void LateUpdate()
        {
            if (won || loading || previousPositions == null) return;
            bool allIdle = player.State == Block.MoveStates.idle;
            bool pushed = false;
            for (int i = 0; i < crates.Length; i++)
            {
                pushed |= previousPositions[i] != crates[i].gridPos;
                previousPositions[i] = crates[i].gridPos;
                allIdle &= crates[i].State == Block.MoveStates.idle;
            }
            if (pushed && pushSound != null) sound.PlayOneShot(pushSound, .35f);

            bool allFilled = true;
            for (int i = 0; i < goals.Length; i++)
            {
                bool filled = false;
                foreach (Block crate in crates) filled |= crate.gridPos == goals[i];
                if (goalLights[i] != null) goalLights[i].sharedMaterial = filled ? goalOn : goalOff;
                allFilled &= filled;
            }
            // 网格位置会在移动开始时更新，所以必须等移动动画结束再判胜。
            if (allFilled && allIdle)
            {
                won = true;
                wonTime = Time.unscaledTime;
                player.enabled = false;
                if (winSound != null) sound.PlayOneShot(winSound, .55f);
            }
        }

        void LoadScene(string path)
        {
            // 没加入构建场景时给出清楚的错误，避免无反应或无限重试。
            if (!Application.CanStreamedLevelBeLoaded(path))
            {
                Debug.LogError("AH: Add this scene to Build Profiles > Scene List: " + path, this);
                return;
            }
            loading = true;
            SceneManager.LoadScene(path);
        }

        // 平时不显示 HUD；只有完成后出现小提示。
        void OnGUI()
        {
            if (!won) return;
            float scale = Mathf.Clamp(Mathf.Min(Screen.width / 900f, Screen.height / 600f), .5f, 2.5f);
            GUI.matrix = Matrix4x4.Scale(Vector3.one * scale);
            float w = Screen.width / scale, h = Screen.height / scale;
            Rect panel = new Rect(w / 2 - 210, h - 112, 420, 92);
            GUI.Box(panel, "");
            var style = new GUIStyle(GUI.skin.label) { fontSize = 20, alignment = TextAnchor.MiddleCenter };
            GUI.Label(new Rect(panel.x, panel.y + 8, panel.width, 32),
                string.IsNullOrEmpty(nextScenePath) ? "ALL THREE ROOMS COMPLETE" : "ROOM " + roomNumber + " COMPLETE", style);
            style.fontSize = 16;
            GUI.Label(new Rect(panel.x, panel.y + 44, panel.width, 30),
                string.IsNullOrEmpty(nextScenePath) ? "R: play this room again" :
                (autoAdvance ? "Next room...    R: restart" : "Enter: next room    R: restart"), style);
        }
    }
}
