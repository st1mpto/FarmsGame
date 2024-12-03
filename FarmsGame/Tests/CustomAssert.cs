using System;

namespace FarmsGame.Tests
{
    public static class CustomAssert
    {
        public static void AreEqual<T>(T expected, T actual, string message = "")
        {
            if (!Equals(expected, actual))
            {
                throw new Exception($"[Assert Failed] Expected: {expected}, Actual: {actual}. {message}");
            }
        }

        public static void AreNotEqual<T>(T notExpected, T actual, string message = "")
        {
            if (Equals(notExpected, actual))
            {
                throw new Exception($"[Assert Failed] NotExpected: {notExpected}, Actual: {actual}. {message}");
            }
        }

        public static void IsNull(object value, string message = "")
        {
            if (value != null)
            {
                throw new Exception($"[Assert Failed] Expected null, but got: {value}. {message}");
            }
        }

        public static void IsNotNull(object value, string message = "")
        {
            if (value == null)
            {
                throw new Exception($"[Assert Failed] Expected not null, but got null. {message}");
            }
        }
    }
}
