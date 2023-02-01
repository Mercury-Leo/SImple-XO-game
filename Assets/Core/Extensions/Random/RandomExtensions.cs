using System;

namespace Core.Extensions.Random {
    public static class RandomExtensions {
        private static readonly System.Random Random = new System.Random(Guid.NewGuid().GetHashCode());
        private static readonly (int lower, int upper) ChanceBounds = (0, 100);

        public static int GetRandomNumber(int lowerBound, int upperBound) {
            return Random.Next(lowerBound, upperBound);
        }

        public static bool GetChance(float chance) {
            if (chance < ChanceBounds.lower)
                return false;
            if (chance > ChanceBounds.upper)
                return true;
            return GetRandomNumber(ChanceBounds.lower, ChanceBounds.upper) < chance;
        }
    }
}