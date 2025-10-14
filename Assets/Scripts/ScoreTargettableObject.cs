using UnityEngine;

public class ScoreTargettableObject : MonoBehaviour
{
    [Header("Scoring Settings")]
    public float baseScore = 10f;

    public bool scaleScoreFromHitpointToCenter = true;

    public Transform scoreCenter;

    public int HitScore(RaycastHit hit)
    {
        float score = baseScore;

        if (scaleScoreFromHitpointToCenter && scoreCenter != null)
        {
            float dist = Vector3.Distance(hit.point, scoreCenter.position);
            float maxDist = 1.0f;
            float falloff = Mathf.Clamp01(1f - (dist / maxDist));
            score *= falloff;
        }

        Debug.Log($"{gameObject.name} was hit! Score: {score:F1}");
        return Mathf.RoundToInt(score);
    }
}
