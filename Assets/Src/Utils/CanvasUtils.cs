using System;
using UnityEngine;
using UnityEngine.UI;

public static class CanvasUtils
{
    public static Canvas Canvas { get; set; }

    public static void SetElementAtWorldPosition(RectTransform element, Vector3 worldPosition)
    {
        element.gameObject.SetActive(true);
        element.position = worldPosition;
    }

    public static bool ElementContainsScreenPosition(RectTransform element, Vector2 position)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
                                    element, 
                                    position, 
                                    Camera.main);
    }
}