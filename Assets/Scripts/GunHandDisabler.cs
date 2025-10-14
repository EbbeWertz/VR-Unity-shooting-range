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
            // When grabbed, disable the hand mesh + colliders
            if (grabbedHand == null && grabbable.grabbedBy != null)
            {
                grabbedHand = grabbable.grabbedBy.gameObject;
                SetHandVisible(grabbedHand, false);
            }
        }
        else
        {
            // When released, re-enable the hand
            if (grabbedHand != null)
            {
                SetHandVisible(grabbedHand, true);
                grabbedHand = null;
            }
        }
    }

    private void SetHandVisible(GameObject hand, bool visible)
    {
        // Disable all renderers
        foreach (var renderer in hand.GetComponentsInChildren<Renderer>())
            renderer.enabled = visible;

        // Optionally disable colliders too
        foreach (var col in hand.GetComponentsInChildren<Collider>())
            col.enabled = visible;
    }
}
