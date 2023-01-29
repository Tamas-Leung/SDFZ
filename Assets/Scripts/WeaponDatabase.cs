using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;

    public GameObject GetWeapon(int index)
    {
        return weapons[index];
    }

    public int GetLength()
    {
        return weapons.Length;
    }

}

