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
    }
}
