using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeTest : MonoBehaviour
{
    public float size; // analagous to speed
    public Vector3 baseRotation;
    public float spread;

    private void OnDrawGizmos() {
        var baseDirection = Quaternion.Euler(baseRotation) * Vector3.right * size;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, baseDirection);
    
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(spread / 2, 0, 0) * baseDirection);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(-spread / 2, 0, 0) * baseDirection);

        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, spread / 2) * baseDirection);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -spread / 2) * baseDirection);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(spread / 2, 0, spread / 2) * baseDirection);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(spread / 2, 0, -spread / 2) * baseDirection);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(-spread / 2, 0, spread / 2) * baseDirection);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(-spread / 2, 0, -spread / 2) * baseDirection);
    }
}
