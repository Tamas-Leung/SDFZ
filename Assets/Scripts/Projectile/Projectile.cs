using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float damage { get; private set; }
    public Type damageType { get; private set; }

    public void Setup(Vector3 direction, float damage, Type damageType)
    {
        this.damage = damage;
        this.damageType = damageType;
        this.SetupDirection(direction);
    }

    abstract public void SetupDirection(Vector3 direction);
}
