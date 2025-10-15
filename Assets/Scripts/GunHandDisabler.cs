using UnityEngine;

[RequireComponent(typeof(OVRGrabbable))]
public class GunHandDisabler : MonoBehaviour
{
    private OVRGrabbable grabbable;
    private GameObject grabbedHand;

    public float delay = 0.5f;  // de re-enable time

    private float reenableTime = -1f;   // timer voor wanneer die re-enabled wordt


    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
    }

    void Update()
    {
        if (grabbable.isGrabbed)
        {
            // Als de gun ge-grabbed is, disable de hand.
            if (grabbedHand == null && grabbable.grabbedBy != null)
            {
                grabbedHand = grabbable.grabbedBy.gameObject;
                DisableHand(grabbedHand, false);
                reenableTime = -1f;
            }
        }
        else
        {
            // als die losgelaten is re-enable weer
            if (grabbedHand != null && reenableTime < 0f)
            {
                reenableTime = Time.time + delay;
            }
            if (grabbedHand != null && reenableTime > 0f && Time.time >= reenableTime)
            {
                DisableHand(grabbedHand, true);
                grabbedHand = null;
                reenableTime = -1f;
            }
        }
    }

    private void DisableHand(GameObject hand, bool visible)
    {
        // Disable renderers
        foreach (var renderer in hand.GetComponentsInChildren<Renderer>())
            renderer.enabled = visible;

        // Disable colliders
        foreach (var col in hand.GetComponentsInChildren<Collider>())
            col.enabled = visible;
    }
}
