using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    public enum BezierCurve { Linear, Quadratic, Cubic }
    public BezierCurve curve;
    public LineRenderer lineRenderer;
    public Transform StartPoint;
    public Transform EndPoint;
    public Transform Middle1;
    public Transform Middle2;
    private readonly int numberOfPoints = 50;
    public readonly Vector3[] positions = new Vector3[50];
    public bool isReady;
    // Start is called before the first frame update
    void Start()
    {
        isReady = false;
        lineRenderer.positionCount = numberOfPoints;
        if(curve == BezierCurve.Linear)
        {
            DrawLinearCurve();
        }else if(curve == BezierCurve.Quadratic)
        {
            DrawQuadraticCurve();
        }else if(curve == BezierCurve.Cubic)
        {
            DrawCubicCurve();
        }
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (curve == BezierCurve.Linear)
        {
            DrawLinearCurve();
        }
        else if (curve == BezierCurve.Quadratic)
        {
            DrawQuadraticCurve();
        }
        else if (curve == BezierCurve.Cubic)
        {
            DrawCubicCurve();
        }
    }

    private void DrawLinearCurve()
    {
        for(int i = 1; i <= numberOfPoints; i++)
        {
            float time = (float)i / numberOfPoints;
            positions[i - 1] = CalculateLinearBezierPoint(time, StartPoint.position, EndPoint.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private void DrawQuadraticCurve()
    {
        for (int i = 1; i <= numberOfPoints; i++)
        {
            float time = (float)i / numberOfPoints;
            positions[i - 1] = CalculateQuadraticBezierPoint(time, StartPoint.position, Middle1.position, EndPoint.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private void DrawCubicCurve()
    {
        for (int i = 1; i <= numberOfPoints; i++)
        {
            float time = (float)i / numberOfPoints;
            positions[i - 1] = CalculateCubicBezierPoint(time, StartPoint.position, Middle1.position, Middle2.position, EndPoint.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateLinearBezierPoint(float time, Vector3 start, Vector3 end)
    {
        return start + time * (end - start);
            // Current = start + time*(end - start);
    }

    private Vector3 CalculateQuadraticBezierPoint(float time, Vector3 start, Vector3 middle, Vector3 end)
    {
        // B(t) = (1 - t)^2P0 + 2(1 - t)tP1 + t^2P2;
        //      = aa * Start + 2 * a * time * middle + time2*end;
        float a = 1 - time;
        float time2 = time * time;
        float aa = a * a;
        Vector3 newPosition = aa * start;
        newPosition += 2 * a * time * middle;
        newPosition += time2 * end;
        return newPosition;
    }

    private Vector3 CalculateCubicBezierPoint(float time, Vector3 start, Vector3 middle1, Vector3 middle2, Vector3 end)
    {
        Vector3 newPosition;
        float a = 1 - time;
        float aaa = a * a * a;
        float aa = a * a;
        float time2 = time * time;
        float time3 = time * time * time;
        newPosition = aaa * start;
        newPosition += 3 * aa * time * middle1;
        newPosition += 3 * a * time2 * middle2;
        newPosition += time3 * end;
        return newPosition;
    }
}
