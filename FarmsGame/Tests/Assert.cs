using System;

namespace FarmsGame.Tests
{
    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = "")
        {
            if (!Equals(expected, actual))
            {
                throw new Exception($"Expected: {expected}, Actual: {actual}. {message}");
            }
        }

        public static void IsTrue(bool condition, string message = "")
        {
            if (!condition)
            {
                throw new Exception($"Condition failed. {message}");
            }
        }

        public static void IsFalse(bool condition, string message = "")
        {
            if (condition)
            {
                throw new Exception($"Condition failed. {message}");
            }
        }
    }
}
