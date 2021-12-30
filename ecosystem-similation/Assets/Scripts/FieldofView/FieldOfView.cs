using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;
    public LayerMask targetMask;
    public List<GameObject> visibleTargets = new List<GameObject>();

    [HideInInspector]
    public GameObject returnedGameObject;
    [HideInInspector]
    public GameObject selfRef;
    [HideInInspector]
    public LayerMask obstackeMask;

    IEnumerator FindTargetsWithDelay(float delay)
    {
        returnedGameObject = null;
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    GameObject FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            GameObject targetObject = targetsInViewRadius[i].gameObject;
            Transform targetTransform = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (targetTransform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, targetTransform.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstackeMask))
                {
                    visibleTargets.Add(targetObject);
                    returnedGameObject = visibleTargets[visibleTargets.Count - 1];
                    if (!(visibleTargets[visibleTargets.Count - 1].transform == selfRef.transform))
                    {
                        return returnedGameObject;
                    }
                    visibleTargets.Remove(selfRef);
                    returnedGameObject = null;
                    return null;
                }
            }
        }
        returnedGameObject = null;
        return null;
    }

    // getting and angle and returning the direction
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
