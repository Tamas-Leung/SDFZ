using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform weaponHolderTransform;
    private int currentWeaponIndex = 0;
    private GameObject currentWeapon;
    private WeaponDatabase weaponDatabase;


    // Start is called before the first frame update
    void Start()
    {
        weaponDatabase = GameObject.FindWithTag("GameController").GetComponent<WeaponDatabase>();
        CreateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeapons();
        }
    }

    void SwitchWeapons()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weaponDatabase.GetLength();
        Destroy(currentWeapon);
        CreateWeapon();
    }

    void CreateWeapon()
    {
        GameObject currentWeaponPrefab = weaponDatabase.GetWeapon(currentWeaponIndex);
        currentWeapon = Instantiate(currentWeaponPrefab);
        currentWeapon.transform.parent = weaponHolderTransform;
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}
