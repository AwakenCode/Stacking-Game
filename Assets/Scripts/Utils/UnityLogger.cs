using UnityEngine;

namespace Utils
{
    public static class UnityLogger
    {
        public static void Log(object message)
        {
            Debug.Log(message);
        }
    }
}