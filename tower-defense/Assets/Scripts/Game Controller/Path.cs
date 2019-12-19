using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path {

    [SerializeField]
    List<Vector3> points;

    private const float Y = 0f;

    public Path(Vector3 centre) {
        points = new List<Vector3>
        {
            centre + Vector3.left,
            centre + (Vector3.left+Vector3.forward)*.5f,
            centre + (Vector3.right+Vector3.back)*.5f,
            centre + Vector3.right
        };
    }

    public Vector3 this[int i]
    {
        get
        {
            return points[i];
        }
    }

    public int NumPoints
    {
        get 
        { 
            return points.Count; 
        }
    }

    public int NumSegments {
        get 
        {
            return points.Count/3; 
        }
    }

    public void AddSegment(Vector3 anchorPos)
    {
        points.Add(new Vector3(points[points.Count - 1].x * 2 - points[points.Count - 2].x, Y, points[points.Count - 1].z * 2 - points[points.Count - 2].z));
        points.Add(new Vector3((points[points.Count - 1].x * 2 + anchorPos.x) / 2, Y, (points[points.Count - 1].z * 2 + anchorPos.z) / 2));
        points.Add(new Vector3(anchorPos.x, Y, anchorPos.z));
    }

    public Vector3[] GetPointsInSegment(int i) {
        return new Vector3[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[i * 3 + 3] };
    }

    public void MovePoint(int i, Vector3 pos)
    {
        pos.y = Y;

        Vector3 deltaMove = pos - points[i];
        points[i] = pos;

        // moving anchor point
        if(i % 3 == 0)
        {
            if (i + 1 < points.Count)
            {
                points[i + 1] += deltaMove;
            }
            if (i - 1 >= 0)
            {
                points[i - 1] += deltaMove;
            }
        }
        else
        {
            bool nextPointIsAnchor = (i + 1) % 3 == 0;
            int correspondingControlIndex = (nextPointIsAnchor) ? i + 2 : i - 2;
            int anchorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;

            if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count)
            {
                float dst = (points[anchorIndex] - points[correspondingControlIndex]).magnitude;
                Vector3 dir = (points[anchorIndex] - pos).normalized;
                points[correspondingControlIndex] = points[anchorIndex] + dir * dst;
            }
            
        }
        
    }

    public Vector3[] CalculateEvenlySpacedPoints(float spacing, float resolution = 1) 
    {
        List<Vector3> evenlySpacedPoints = new List<Vector3>();
        evenlySpacedPoints.Add(points[0]);
        Vector3 previousPoint = points[0];
        float dstSinceLastEvenPoint = 0;
        for(int segmentIndex = 0; segmentIndex < NumSegments; segmentIndex++)
        {
            Vector3[] p = GetPointsInSegment(segmentIndex);
            float controlNetLenght = Vector3.Distance(p[0], p[1]) + Vector3.Distance(p[1], p[2]) + Vector3.Distance(p[2], p[3]);
            float estimatedCurveLenght = Vector3.Distance(p[0], p[3]) + controlNetLenght / 2f;
            int divisions = Mathf.CeilToInt(estimatedCurveLenght * resolution * 10); 
            float t = 0;
            while (t <= 1)
            {
                t += 1f/divisions;
                Vector3 pointOnCurve = Bezier.EvaluateCubic(p[0], p[1], p[2], p[3], t);
                dstSinceLastEvenPoint += Vector3.Distance(previousPoint, pointOnCurve);

                while(dstSinceLastEvenPoint >= spacing)
                {
                    float overshootDst = dstSinceLastEvenPoint - spacing;
                    Vector3 newEvenlySpacedPoint = pointOnCurve + (previousPoint - pointOnCurve).normalized * overshootDst;
                    evenlySpacedPoints.Add(newEvenlySpacedPoint);
                    dstSinceLastEvenPoint = overshootDst;
                    previousPoint = newEvenlySpacedPoint;
                }
            }
        }
        return evenlySpacedPoints.ToArray();
    }
}