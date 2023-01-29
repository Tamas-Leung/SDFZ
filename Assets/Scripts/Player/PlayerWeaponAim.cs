using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAim : MonoBehaviour
{
    [SerializeField] private Transform weaponHolderTransform;

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
    }

    void HandleAiming()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        //Calculating rotations to rotate weapon
        Vector3 aimDirection = (mousePosition - weaponHolderTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weaponHolderTransform.eulerAngles = new Vector3(0, 0, angle);
    }
}
