using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed;
    public GameObject ways;
    public Transform[] wayPoints;

    private Vector3 targetPos;
    private int pointIndex;
    private int pointCount;
    private int direction = 1;

    private void Awake()
    {
        
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i);
        }
    }

    private void Start()
    {
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].position;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (transform.position == targetPos)
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        if (pointIndex == pointCount - 1) // ??n ?i?m cu?i cùng
        {
            direction = -1;
        }
        else if (pointIndex == 0) // Quay l?i ?i?m ??u tiên
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].position;
    }
}
