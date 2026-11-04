#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace AHSokoban
{
    // 导入后自动追加三关到场景列表；保留班级里其他人的所有条目。
    // 不生成关卡，也不创建顶部菜单。
    [InitializeOnLoad]
    public static class AHSceneRegistration
    {
        static AHSceneRegistration() { EditorApplication.delayCall += Register; }

        static void Register()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;
            var scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            bool changed = false;
            for (int i = 1; i <= 3; i++)
            {
                string path = "Assets/AH/Scenes/AH_SHIFT_0" + i + ".unity";
                if (AssetDatabase.LoadAssetAtPath<SceneAsset>(path) == null) continue;
                int existing = scenes.FindIndex(s => s.path == path);
                if (existing < 0) { scenes.Add(new EditorBuildSettingsScene(path, true)); changed = true; }
                else if (!scenes[existing].enabled) { scenes[existing] = new EditorBuildSettingsScene(path, true); changed = true; }
            }
            if (changed) EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}
#endif
