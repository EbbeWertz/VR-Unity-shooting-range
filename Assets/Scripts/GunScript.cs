using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("Gun Settings")]
    public Transform puntjeVanDeBarrel;
    public float hitForce = 10f;
    public float maxDistance = 100f;

    [Header("Effects")]
    public GameObject impactStoneEffectPrefab;
    public GameObject impactWoodEffectPrefab;
    public GameObject impactMetalEffectPrefab;

    public GameObject muzzleEffect;

    public Animator recoilAnim;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Schiet();
        }
    }

    private void Schiet()
    {
        print("piew!");

        recoilAnim.SetTrigger("Recoil");

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
        }
    }
}
