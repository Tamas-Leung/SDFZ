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

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = healthPoints;
        type = (Type)Random.Range(0, System.Enum.GetValues(typeof(Type)).Length);
        SetupColor();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDead) return;

        if (collider.gameObject.TryGetComponent<Projectile>(out Projectile projectile))
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
            damagePopUp.Setup(damageDealt, advantageDamage);
            currentHealth -= projectile.damage;
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