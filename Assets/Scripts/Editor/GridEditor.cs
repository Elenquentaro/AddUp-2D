using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridField))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Show rect info"))
        {
            RectTransform rt = (target as GridField).GetComponent<RectTransform>();
            Debug.Log(rt.rect);
            Debug.Log("rect.min: " + rt.rect.min + "; max: " + rt.rect.max);
            Debug.Log("rect size: " + rt.rect.size);
            Debug.Log("rect center: " + rt.rect.center);
            Debug.Log("rt offset min and max: " + rt.offsetMin + "; " + rt.offsetMax);
            Debug.Log("rt anch pos: " + rt.anchoredPosition);
            Debug.Log("matrix: " + rt.localToWorldMatrix);
            // Debug.Log(rt.)
        }
        DrawDefaultInspector();
    }
}
