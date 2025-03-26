using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    public Transform FindNearestTarget(string targetTag, float maxDistance)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < nearestDistance && distance <= maxDistance)
            {
                nearestDistance = distance;
                nearestTarget = target.transform;
            }
        }

        return nearestTarget;
    }
}