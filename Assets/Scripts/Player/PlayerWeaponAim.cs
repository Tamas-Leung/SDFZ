using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAim : MonoBehaviour
{
    [SerializeField] private Transform weaponHolderTransform;
    [SerializeField] private SpriteRenderer sr;

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
        

        //Flips character
        if (aimDirection.magnitude > 0.1 && aimDirection.x != 0 && ((sr.transform.localScale.x > 0 && aimDirection.x < 0) || (sr.transform.localScale.x < 0 && aimDirection.x > 0)))
        {
            float xScale = Math.Abs(sr.transform.localScale.x);
            sr.transform.localScale = new Vector2(aimDirection.x > 0 ? xScale : -xScale, sr.transform.localScale.y);
        }
    }
}
