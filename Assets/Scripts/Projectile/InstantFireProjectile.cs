using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantFireProjectile : MonoBehaviour, Projectile
{
    private Vector3 direction;
    [SerializeField] private float duration;

    // Start is called before the first frame update
    public void Setup(Vector3 direction)
    {
        this.direction = direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        Destroy(gameObject, duration);
    }
}
