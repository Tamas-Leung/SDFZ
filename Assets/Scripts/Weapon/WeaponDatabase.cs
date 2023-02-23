using System;
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

    public Weapon GetWeapon(Type type)
    {
        return Array.Find(weapons, weapon => weapon.type == type);
    }

    public int GetLength()
    {
        return weapons.Length;
    }

}

