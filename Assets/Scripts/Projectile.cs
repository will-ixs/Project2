using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int iterations = 20;
    [SerializeField] private Frog frog;
    private LineRenderer lineRenderer;
    

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void UpdateArc(float speed, float distance, float gravity, float angle, Vector3 direction, bool valid)
    {
        Vector2[] arcPoints = ProjectileArcPoints(iterations, speed, distance, gravity, angle);
        Vector3[] points3d = new Vector3[arcPoints.Length];

        for (int i = 0; i < arcPoints.Length; i++)
        {
            points3d[i] = new Vector3(0, arcPoints[i].y, arcPoints[i].x);
        }

        lineRenderer.positionCount = arcPoints.Length;
        lineRenderer.SetPositions(points3d);

        transform.position = frog.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public float LaunchSpeed(float distance, float yOffset, float gravity, float angle)
    {
        return (distance * Mathf.Sqrt(gravity) * Mathf.Sqrt(1 / Mathf.Cos(angle))) / Mathf.Sqrt(2 * distance * Mathf.Sin(angle) + 2 * yOffset * Mathf.Cos(angle));
    }

    public Vector2[] ProjectileArcPoints(int iterations, float speed, float distance, float gravity, float angle)
    {
        float iterationSize = distance / iterations;
        float radians = angle;
        Vector2[] points = new Vector2[iterations + 1];

        for (int i = 0; i <= iterations; i++)
        {
            float x = iterationSize * i;
            float t = x / (speed * Mathf.Cos(radians));
            float y = -0.5f * gravity * (t * t) + speed * Mathf.Sin(radians) * t;

            Vector2 p = new Vector2(x, y);

            points[i] = p;
        }

        return points;
    }
}
