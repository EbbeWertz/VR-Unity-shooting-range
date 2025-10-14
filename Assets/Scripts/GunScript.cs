using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Transform puntjeVanDeBarrel;
    public float hitForce = 10f;
    public GameObject impactStoneEffect;
    public GameObject impactWoodEffect;
    public GameObject impactMetalEffect;

    void Start()
    {

    }

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

        RaycastHit hit;
        if (Physics.Raycast(puntjeVanDeBarrel.position, puntjeVanDeBarrel.forward, out hit, 100f))
        {

            GameObject effectPrefab = impactStoneEffect;

            if (hit.transform.CompareTag("Wood"))
            {
                effectPrefab = impactWoodEffect;
            }
            else if (hit.transform.CompareTag("Metal"))
            {
                effectPrefab = impactMetalEffect;
            }

            GameObject effect = Instantiate(
                    effectPrefab,
                    hit.point + hit.normal * 0.01f,
                    Quaternion.LookRotation(hit.normal)
                );

            effect.transform.SetParent(hit.transform);
            Destroy(effect, 5f);

            // Als de ray een rigidbody raakt, apply een force
            Rigidbody rb = hit.rigidbody;
            if (rb != null)
            {
                rb.AddForceAtPosition(puntjeVanDeBarrel.forward * hitForce, hit.point, ForceMode.Impulse);
            }
        }
    }

}
