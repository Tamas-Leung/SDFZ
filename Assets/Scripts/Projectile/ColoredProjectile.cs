using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredProjectile : MonoBehaviour
{

    Projectile projectile;
    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Projectile>();

        SpriteRenderer sr = projectile.GetComponentInChildren<SpriteRenderer>();
        sr.color = TypeMethods.GetColorFromType(projectile.damageType);
    }
}
