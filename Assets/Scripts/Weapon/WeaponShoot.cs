using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform aimWeaponEndPointTransform;

    // Update is called once per frame
    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            GameObject projectile = Instantiate(projectilePrefab, aimWeaponEndPointTransform.position, Quaternion.identity);
            Vector3 shootDirection = (mousePosition - transform.position).normalized;
            projectile.GetComponent<Projectile>().Setup(shootDirection);
        }
    }
}
