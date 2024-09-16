using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPath : MonoBehaviour
{
    public Transform[] controlPoints; // Array de objetos que servem como pontos de controle
    public int resolution = 50; // Número de pontos por segmento
    public List<Vector3> bezierPoints; // Lista que armazenará todos os pontos gerados

    // Enum para escolher o tipo de easing no Inspector
    public enum EasingType { Linear, EaseIn, EaseOut, EaseInOut };
    public EasingType easingType = EasingType.Linear;

    void Start()
    {
        bezierPoints = new List<Vector3>();
        GenerateBezierPath();
    }

    void GenerateBezierPath()
    {
        for (int i = 0; i < controlPoints.Length - 2; i += 2)
        {
            Vector3 pointA = controlPoints[i].position;
            Vector3 controlPoint = controlPoints[i + 1].position;
            Vector3 pointB = controlPoints[i + 2].position;

            for (int j = 0; j <= resolution; j++)
            {
                float t = j / (float)resolution;

                // Aplicar a função de easing ao valor de t
                t = ApplyEasing(t);

                Vector3 point = CalculateBezierPoint(t, pointA, controlPoint, pointB);
                bezierPoints.Add(point);
            }
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // (1-t)² * P0
        p += 2 * u * t * p1; // 2(1-t)t * P1
        p += tt * p2; // t² * P2

        return p;
    }

    // Funções de easing
    float ApplyEasing(float t)
    {
        switch (easingType)
        {
            case EasingType.EaseIn:
                return EaseInQuad(t);
            case EasingType.EaseOut:
                return EaseOutQuad(t);
            case EasingType.EaseInOut:
                return EaseInOutQuad(t);
            default:
                return t; // Linear
        }
    }

    // Easing-in (começa devagar, acelera)
    float EaseInQuad(float t)
    {
        return t * t;
    }

    // Easing-out (começa rápido, desacelera)
    float EaseOutQuad(float t)
    {
        return t * (2 - t);
    }

    // Easing-in-out (devagar no começo e no final, mais rápido no meio)
    float EaseInOutQuad(float t)
    {
        if (t < 0.5f)
            return 2 * t * t;
        return -1 + (4 - 2 * t) * t;
    }

    void OnDrawGizmos()
    {
        if (bezierPoints == null || bezierPoints.Count == 0)
            return;

        Gizmos.color = Color.red;
        foreach (Vector3 point in bezierPoints)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
