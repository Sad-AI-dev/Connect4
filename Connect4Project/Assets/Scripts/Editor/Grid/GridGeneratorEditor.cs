using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game;

[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorEditor : Editor
{
    private GridGenerator generator;

    private void OnEnable()
    {
        if (!generator) { generator = target as GridGenerator; }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //generate grid button
        EditorGUILayout.Space(15f);
        EditorGUILayout.LabelField("Grid Preview", EditorStyles.boldLabel);
        //Clear grid button
        if (GUILayout.Button("Clear Grid"))
        {
            generator.ClearGrid();
        }
        //generate grid preview button
        if (GUILayout.Button("Generate Grid Preview"))
        {
            generator.GenerateGrid();
        }
    }
}
