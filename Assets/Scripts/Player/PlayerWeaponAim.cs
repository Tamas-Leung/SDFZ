using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAim : MonoBehaviour
{
    [SerializeField] private Transform weaponHolderTransform;
    private SpriteRenderer sr;

    void Start()
    {
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

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
        if (aimDirection.magnitude > 0.1 && aimDirection.x != 0)
        {
            if (aimDirection.x > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }

    }
}
