using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShopScreen))]
public class ShopScreenEditor : Editor
{
    [SerializeField] Vector2 maxAnchor, minAnchor;
    public override void OnInspectorGUI()
    {
        RectTransform rt = (target as ShopScreen).GetComponent<RectTransform>();

        minAnchor = EditorGUILayout.Vector2Field("anchors min", minAnchor);
        maxAnchor = EditorGUILayout.Vector2Field("anchors max", maxAnchor);
        if (GUILayout.Button("Change anchors"))
        {
            WriteAnchorsValue(rt);
        }
        if (GUILayout.Button("Change anchors with recalc"))
        {
            WriteAnchorsValue(rt);
            rt.ForceUpdateRectTransforms();
        }
        DrawDefaultInspector();
    }

    void WriteAnchorsValue(RectTransform rt)
    {
        rt.anchorMax = maxAnchor;
        rt.anchorMin = minAnchor;
    }
}
