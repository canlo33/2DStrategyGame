using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    private static Camera mainCamera;
    public static Vector3 GetMousePositionOnWorld()
    {
        //This functions finds Camera Position on World, changes its z value to 0 and returns it.
        if (mainCamera == null) mainCamera = Camera.main;        
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }
    public static Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radiant = Mathf.Atan2(vector.y, vector.x);
        float angleDegree = radiant * Mathf.Rad2Deg;
        return angleDegree;
    }
}
