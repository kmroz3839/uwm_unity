using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static void ModifyTransformX(Transform a, float valX) {
        Vector3 eulerRot = a.rotation.eulerAngles;
        eulerRot.x = valX;
        a.rotation = Quaternion.Euler(eulerRot);
    }
    public static void ModifyTransformY(Transform a, float valY) {
        Vector3 eulerRot = a.rotation.eulerAngles;
        eulerRot.y = valY;
        a.rotation = Quaternion.Euler(eulerRot);
    }

    public static Vector3 ClampVec(Vector3 target, float min, float max)
    {
        Vector3 ret = target;
        ret.x = Mathf.Clamp(ret.x, min, max);
        ret.y = Mathf.Clamp(ret.y, min, max);
        ret.z = Mathf.Clamp(ret.z, min, max);

        return ret;
    }
    public static Vector2 ClampVec(Vector2 target, float min, float max)
    {
        Vector2 ret = target;
        ret.x = Mathf.Clamp(ret.x, min, max);
        ret.y = Mathf.Clamp(ret.y, min, max);

        return ret;
    }
}
