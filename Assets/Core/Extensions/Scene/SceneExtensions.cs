using UnityEngine.SceneManagement;

namespace Core.Extensions.Scene {
    public static class SceneExtensions {
        public static void LoadSceneByMode(string sceneToLoad, LoadSceneMode mode = LoadSceneMode.Single) {
            SceneManager.LoadScene(sceneToLoad, mode);
        }

        public static void UnloadScene(string sceneToUnload) {
            SceneManager.UnloadSceneAsync(sceneToUnload);
        }
    }
}