using UnityEditor;
using UnityEngine;

public class DrawLimits : MonoBehaviour
{
    [SerializeField] private Transform topRight, topLeft, bottomRight, bottomLeft;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(topLeft.position, topRight.position);
        Gizmos.DrawLine(topRight.position, bottomRight.position);
        Gizmos.DrawLine(bottomRight.position, bottomLeft.position);
        Gizmos.DrawLine(bottomLeft.position, topLeft.position);
    }
}