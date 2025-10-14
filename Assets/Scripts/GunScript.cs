using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("Gun Settings")]
    public Transform puntjeVanDeBarrel;
    public float hitForce = 10f;
    public float maxDistance = 100f;

    public float fireCooldownTime = 0.5f;

    [Header("Effects")]
    public GameObject impactStoneEffectPrefab;
    public GameObject impactWoodEffectPrefab;
    public GameObject impactMetalEffectPrefab;

    public GameObject muzzleEffect;

    public Animator recoilAnim;

    private OVRGrabbable grabbable;

    private float lastFireTime = -999f;


    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
    }

    void Update()
    {

        if (Time.time - lastFireTime < fireCooldownTime)
            return;

        if (grabbable == null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Schiet();
            }
        }
        else if (grabbable.isGrabbed)
        {
            var grabber = grabbable.grabbedBy;
            if (grabber != null)
            {
                // Check which hand is holding the gun
                OVRInput.Controller controller = grabber.CompareTag("LeftHand")
                    ? OVRInput.Controller.LTouch
                    : OVRInput.Controller.RTouch;

                // Check trigger pressure
                float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);

                if (triggerValue > 0.8f)
                {
                    Schiet();
                }
            }
        }
    }

    private void Schiet()
    {

        lastFireTime = Time.time;
        print("piew!");
        if (recoilAnim != null)
        {
            recoilAnim.SetTrigger("Recoil");
        }


        muzzleEffect.GetComponent<ParticleSystem>().Play();

        RaycastHit hit;
        Vector3 origin = puntjeVanDeBarrel.position;
        Vector3 direction = puntjeVanDeBarrel.forward;

        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            GameObject effectPrefab = impactStoneEffectPrefab;
            if (hit.transform.CompareTag("Wood"))
                effectPrefab = impactWoodEffectPrefab;
            else if (hit.transform.CompareTag("Metal"))
                effectPrefab = impactMetalEffectPrefab;

            GameObject effect = Instantiate(
                effectPrefab,
                hit.point + hit.normal * 0.01f,
                Quaternion.LookRotation(hit.normal)
            );

            effect.transform.SetParent(hit.transform);
            Destroy(effect, 5f);

            Rigidbody rb = hit.rigidbody;
            if (rb != null)
                rb.AddForceAtPosition(direction * hitForce, hit.point, ForceMode.Impulse);

            ScoreTargettableObject targetScoreScript = hit.transform.GetComponent<ScoreTargettableObject>();
            if (targetScoreScript != null)
                targetScoreScript.HitScore(hit);
        }
    }
}
