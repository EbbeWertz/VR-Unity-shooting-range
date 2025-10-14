using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
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

    

    [Header("Laser Settings")]
    public bool showLaserAlways = true;
    public Color laserColor = new Color(1f, 0f, 0f, 0.5f);

    private LineRenderer laser;

    void Awake()
    {
        laser = GetComponent<LineRenderer>();
        laser.startWidth = 0.005f;
        laser.endWidth = 0.005f;
        laser.material = new Material(Shader.Find("Unlit/Color"));
        laser.material.color = laserColor;
        laser.positionCount = 2;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(puntjeVanDeBarrel.position, puntjeVanDeBarrel.forward);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Schiet();
        }

        if (showLaserAlways)
        {
            UpdateLaser();
        }
        else
        {
            laser.enabled = false;
        }
    }

    private void UpdateLaser()
    {
        laser.enabled = true;

        Vector3 origin = puntjeVanDeBarrel.position;
        Vector3 direction = puntjeVanDeBarrel.forward;
        Vector3 endPoint = origin + direction * maxDistance;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            endPoint = hit.point;
        }

        laser.SetPosition(0, origin);
        laser.SetPosition(1, endPoint);
    }

    private void Schiet()
    {
        print("piew!");

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
