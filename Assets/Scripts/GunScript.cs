using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Transform puntjeVanDeBarrel;
    public float hitForce = 10f;

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
            // Spawn bolletje voor "bullet hole"
            GameObject bulletHole = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bulletHole.transform.position = hit.point;
            bulletHole.transform.localScale = Vector3.one * 0.02f;
            bulletHole.GetComponent<Renderer>().material.color = Color.black;
            bulletHole.transform.SetParent(hit.transform);
            Destroy(bulletHole, 2f);

            // Als de ray een rigidbody raakt, apply een force
            Rigidbody rb = hit.rigidbody;
            if (rb != null)
            {
                rb.AddForceAtPosition(puntjeVanDeBarrel.forward * hitForce, hit.point, ForceMode.Impulse);
            }
        }
    }

}
