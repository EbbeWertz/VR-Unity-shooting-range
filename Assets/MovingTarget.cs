using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float moveRange = 3f;
    public Vector3 moveDirection = Vector3.right;

    [Header("Scoring Settings")]
    public int maxScore = 100;
    public int minScore = 10;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Move the target back and forth
        transform.position = startPos + moveDirection * Mathf.Sin(Time.time * moveSpeed) * moveRange;
    }

    public void OnHit(Vector3 hitPoint)
    {
        float distance = Vector3.Distance(hitPoint, transform.position);
        float t = Mathf.InverseLerp(0f, 1f, distance);
        int score = Mathf.RoundToInt(Mathf.Lerp(maxScore, minScore, t));
        score = Mathf.Clamp(score, minScore, maxScore);

        Debug.Log($"Target hit! Distance: {distance:F2}, Score: {score}");
    }
}
