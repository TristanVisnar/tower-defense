using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{
    PathCreator creator;
    Path Path
    {
        get
        {
            return creator.path;
        }
    }


    public void OnSceneGUI()
    {
        Input();
        Draw();
    }

    void Input()
    {
        Event guiEvent = Event.current;
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.control)
        {
            Undo.RecordObject(creator, "Add segment");
            Path.AddSegment(new Vector3(mousePos.x, creator.transform.position.y,mousePos.z));
        }
    }

    void Draw()
    {
        for(int i = 0; i< Path.NumSegments; i++)
        {
            Vector3[] points = Path.GetPointsInSegment(i);
            Handles.color = Color.black;
            Handles.DrawLine(points[1], points[0]);
            Handles.DrawLine(points[2], points[3]);
            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, creator.pathWidth);
        }

        Handles.color = Color.red;
        for (int i = 0; i < Path.NumPoints; i++)  
        {
            Vector3 newPos = Handles.FreeMoveHandle(Path[i], Quaternion.identity, .1f, Vector3.zero, Handles.CylinderHandleCap);
            if(Path[i] != newPos)
            {
                Undo.RecordObject(creator, "Move point");
                Path.MovePoint(i, newPos);
            }
        }
    }

    void OnEnable()
    {
        creator = (PathCreator)target;
        if(creator.path == null) 
        {
            creator.CreatePath();
        }
    }
}
