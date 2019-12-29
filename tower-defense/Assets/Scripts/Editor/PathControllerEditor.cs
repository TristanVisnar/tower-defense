using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathController))]
public class PathControllerEditor : Editor
{ 
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PathController pathController = (PathController)target;

        EditorGUILayout.LabelField("Number of waypoints", pathController.path.Length.ToString());
    }
}
