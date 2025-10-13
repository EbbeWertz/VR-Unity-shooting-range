using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDebugRayRenderer : MonoBehaviour
{

    public Transform camera;
    void OnDrawGizmos()
    {
        if (camera == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(camera.position, camera.forward * 100f);
    }

}
