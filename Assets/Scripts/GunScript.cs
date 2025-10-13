using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Transform puntjeVanDeBarrel;

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
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = hit.point;
            sphere.transform.localScale = Vector3.one * 0.05f; // Tiny sphere
            sphere.GetComponent<Renderer>().material.color = Color.red;

            Destroy(sphere, 2f);
        }
    }
}
