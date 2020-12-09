using UnityEngine;

public static class Utility
{
    /// <summary>
    /// Get touch or mouse position
    /// </summary>
    public static Vector3 InputPosition()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return Input.GetTouch(0).position;
#else
        return Input.mousePosition;
#endif
    }

    /// <summary>
    /// Remap a value
    /// </summary>
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
