using System;
using UnityEngine;
using System.Collections.Generic;

public static class ObjectUtils
{
    private static System.Random rng = new System.Random();

    public static bool IsOrb(this BattleObject obj)
    {
        return obj.Properties.ContainsKey("isOrb") && obj.Properties["isOrb"] == "true";
    }

    public static GameObject GetVisualObject(this WarriorPattern pattern, PatternFlags flags, Vector2 referencePosition, bool inverted = false)
    {
        GameObject patternObject = new GameObject("_PATTERN_" + flags.ToString());
        patternObject.transform.position = new Vector3(patternObject.transform.position.x, patternObject.transform.position.y, -5); //TODO

        MeshFilter meshFilter = (MeshFilter)patternObject.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = MeshGenerator.GetPatternMesh(pattern, flags, referencePosition, inverted);

        MeshRenderer renderer = patternObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material.shader = Shader.Find ("Sprites/Default");

        Texture2D texture = (Texture2D)Resources.Load("Sprites/board");
        renderer.material.mainTexture = texture;

        return patternObject;
    }

    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}