using UnityEngine;

namespace Core.Extensions.Exit {
    public static class ExitExtensions {
        public static void Quit() {
            Application.Quit();

# if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}