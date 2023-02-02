using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterDatas))]
public class EditorPreview : Editor
{
    CharacterDatas data;

    private void OnEnable()
    {
        data = target as CharacterDatas;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (data == null) return;
        if (data.icon == null) return;
        Texture2D texture = AssetPreview.GetAssetPreview(data.icon);
        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}
