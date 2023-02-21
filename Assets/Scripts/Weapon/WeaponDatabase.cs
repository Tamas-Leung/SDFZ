using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    public Weapon GetWeapon(int index)
    {
        return weapons[index];
    }

    public int GetLength()
    {
        return weapons.Length;
    }

}

