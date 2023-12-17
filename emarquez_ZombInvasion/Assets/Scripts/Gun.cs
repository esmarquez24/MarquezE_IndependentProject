using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunBarrel;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity);
        // shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(gunBarrel.forward.normalized * bulletSpeed, ForceMode.Impulse);
    }
}
