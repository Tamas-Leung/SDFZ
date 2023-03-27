using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform aimWeaponEndPointTransform;

    private Player player;
    private Weapon weapon;

    private GameController gameController;

    private float currentWeaponCooldown;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        player = FindObjectOfType<Player>();
        weapon = GetComponent<Weapon>();
        currentWeaponCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameState == GameState.NotActive)
        {
            return;
        }

        if (currentWeaponCooldown > 0)
        {
            currentWeaponCooldown -= Time.deltaTime;
        }

        HandleShooting();
    }

    void HandleShooting()
    {
        if (currentWeaponCooldown <= 0 && Input.GetButton("Fire1"))
        {
            currentWeaponCooldown = weapon.attackSpeedCooldown * (1 - player.attackSpeedReduction);


            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            GameObject projectile = Instantiate(projectilePrefab, aimWeaponEndPointTransform.position, Quaternion.identity);
            Vector3 shootDirection = (mousePosition - transform.position).normalized;
            projectile.GetComponent<Projectile>().Setup(shootDirection, player.attackPower * weapon.damage, player.currentWeapon.type);
        }
    }
}
