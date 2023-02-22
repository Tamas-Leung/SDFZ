using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform weaponHolderTransform;
    private int currentWeaponIndex = 0;
    public Weapon currentWeapon { get; private set; }
    private WeaponDatabase weaponDatabase;

    [SerializeField] private int baseMaxHealthPoints;

    [SerializeField] private float movementSpeed;
    public float attackPower;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashRange;


    private int _currentHealth;
    public int currentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if (_currentHealth == value) return;
            _currentHealth = value;
            if (OnCurrentHealthChange != null)
                OnCurrentHealthChange(_currentHealth);
        }
    }
    public delegate void OnCurrentHealthChangeDelegate(int newVal);
    public event OnCurrentHealthChangeDelegate OnCurrentHealthChange;

    private int _currentMaxHealth;
    public int currentMaxHealth
    {
        get
        {
            return _currentMaxHealth;
        }
        set
        {
            if (_currentMaxHealth == value) return;
            _currentMaxHealth = value;
            if (OnCurrentMaxHealthChange != null)
                OnCurrentMaxHealthChange(_currentMaxHealth);
        }
    }
    public delegate void OnCurrentMaxHealthChangeDelegate(int newVal);
    public event OnCurrentMaxHealthChangeDelegate OnCurrentMaxHealthChange;

    private float damagedInvulnerabilityTimer;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        weaponDatabase = GameObject.FindWithTag("GameController").GetComponent<WeaponDatabase>();
        currentHealth = baseMaxHealthPoints;
        currentMaxHealth = baseMaxHealthPoints;
        spriteRenderer = GetComponent<SpriteRenderer>();
        damagedInvulnerabilityTimer = 0;
        CreateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (damagedInvulnerabilityTimer > 0)
        {
            damagedInvulnerabilityTimer -= Time.deltaTime;
            if (spriteRenderer.color == Color.white)
                spriteRenderer.color = Color.yellow;
        }
        else
        {
            if (spriteRenderer.color == Color.yellow) spriteRenderer.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeapons();
        }
    }

    void SwitchWeapons()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weaponDatabase.GetLength();
        Destroy(currentWeapon.gameObject);
        CreateWeapon();
    }

    void CreateWeapon()
    {
        Weapon currentWeaponPrefab = weaponDatabase.GetWeapon(currentWeaponIndex);
        currentWeapon = Instantiate(currentWeaponPrefab);
        currentWeapon.transform.parent = weaponHolderTransform;
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (damagedInvulnerabilityTimer <= 0 && !enemy.isDead)
            {
                currentHealth = currentHealth - enemy.damage;
                damagedInvulnerabilityTimer += 2;
            }
        }
    }
}
