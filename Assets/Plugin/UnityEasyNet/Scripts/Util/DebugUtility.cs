using System.Diagnostics;

public static class DebugUtility
{
    [Conditional("UNITY_EDITOR")]
    public static void Log(string message)
    {
        UnityEngine.Debug.Log(message);
    }
    
    [Conditional("UNITY_EDITOR")]
    public static void LogError(string message)
    {
        UnityEngine.Debug.LogError(message);
    }
}