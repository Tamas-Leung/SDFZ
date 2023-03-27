using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform pfDamagePopUp;
    [SerializeField] private float healthPoints;

    public Type type;
    public float movementSpeed;
    public int damage;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private float currentHealth;
    public bool isDead { get; private set; }

    public AudioClip DamagedSoundEffect;
    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = Camera.main.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = healthPoints;
        type = (Type)Random.Range(0, System.Enum.GetValues(typeof(Type)).Length);
        SetupColor();
    }

    public void ScaleStats(int roundNumber)
    {
        currentHealth = currentHealth * (1 + roundNumber * 0.1f);
        movementSpeed = movementSpeed * (1 + roundNumber * 0.01f);
    }

    public void SetBossType(Type type)
    {
        this.type = type;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDead) return;

        if (collider.gameObject.TryGetComponent<Projectile>(out Projectile projectile) && !projectile.IsEnemy)
        {
            Transform damagePopUpTransform = Instantiate(pfDamagePopUp, transform.position, Quaternion.identity);
            float damageDealt = projectile.damage;
            bool advantageDamage = false;
            if (TypeMethods.GetDisavantageType(type) == projectile.damageType)
            {
                damageDealt = damageDealt * 1.5f;
                advantageDamage = true;
            }

            DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
            damagePopUp.Setup(damageDealt, advantageDamage, projectile.damageType);
            currentHealth -= projectile.damage;
            // audioSource.PlayOneShot(DamagedSoundEffect, 0.2f);
        }
    }

    void Update()
    {
        if (!isDead && currentHealth <= 0)
        {
            isDead = true;
            anim.SetBool("Dead", true);
        }
    }

    void SetupColor()
    {
        spriteRenderer.color = TypeMethods.GetColorFromType(type);
    }

}