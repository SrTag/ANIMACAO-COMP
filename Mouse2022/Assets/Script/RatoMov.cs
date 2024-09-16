using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatoMov : MonoBehaviour
{
    public BezierPath bezierPath;
    public float speed = 5.0f;

    private int currentPointIndex = 0;

    void Update()
    {
        if (bezierPath.bezierPoints == null || bezierPath.bezierPoints.Count == 0)
            return;

        if (currentPointIndex < bezierPath.bezierPoints.Count)
        {
            // Ponto atual
            Vector3 targetPoint = bezierPath.bezierPoints[currentPointIndex];

            // Movimenta��o
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

            // Rota��o para o pr�ximo ponto
            transform.LookAt(targetPoint);

            if (transform.position == targetPoint)
            {
                currentPointIndex++;
            }
        }
    }
}
