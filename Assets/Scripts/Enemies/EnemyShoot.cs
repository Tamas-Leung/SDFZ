using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private Player player;
    private Enemy enemy;

    [SerializeField] private Projectile projectilePrefab;

    public float baseAttackCooldown;
    private float currentAttackCooldown;



    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = GetComponent<Enemy>();
        currentAttackCooldown = baseAttackCooldown + Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
        }
        else if (!enemy.isDead)
        {
            currentAttackCooldown = baseAttackCooldown;
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition.z = 0;

        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector3 shootDirection = (playerPosition - transform.position).normalized;
        projectile.Setup(shootDirection, enemy.damage, enemy.type);
    }
}
