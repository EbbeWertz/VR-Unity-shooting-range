using UnityEngine;

[RequireComponent(typeof(OVRGrabbable))]
public class GunPhysicsDisabler : MonoBehaviour
{
    private OVRGrabbable grabbable;
    private Rigidbody rb;
    private Collider[] colliders;

    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    void Update()
    {
        if (grabbable.isGrabbed)
        {
            DisablePhysics();
        }
        else
        {
            EnablePhysics();
        }
    }

    private void DisablePhysics()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        foreach (var col in colliders)
        {
            col.enabled = false;
        }
    }

    private void EnablePhysics()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        foreach (var col in colliders)
        {
            col.enabled = true;
        }
    }
}
