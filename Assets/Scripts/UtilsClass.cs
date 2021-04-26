using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    private static Camera mainCamera;
    public static Vector3 GetMousePositionOnWorld()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        //This functions finds Camera Position on World, changes its z value to 0 and returns it.
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }
}
