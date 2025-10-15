using UnityEngine;

[RequireComponent(typeof(OVRGrabbable))]
public class GunHandDisabler : MonoBehaviour
{
    private OVRGrabbable grabbable;
    private GameObject grabbedHand;

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
            }
        }
        else
        {
            // als die losgelaten is re-enable weer
            if (grabbedHand != null)
            {
                DisableHand(grabbedHand, true);
                grabbedHand = null;
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
