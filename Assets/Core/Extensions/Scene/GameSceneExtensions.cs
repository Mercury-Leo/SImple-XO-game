using System;

namespace Core.Extensions.Scene {
    public static class GameSceneExtensions 
    {
        public static string GetName(this GameConventions.Scenes scene) {
            return Enum.GetName(typeof(GameConventions.Scenes), scene);
        }
    }
}
