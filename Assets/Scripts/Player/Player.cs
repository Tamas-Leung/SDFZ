using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform weaponHolderTransform;
    public Weapon currentWeapon { get; private set; }
    private WeaponDatabase weaponDatabase;

    [SerializeField] private int baseMaxHealthPoints;

    public float movementSpeed;
    public float attackPower;
    public float dashCooldown;
    public float dashRange;
    public float dashDuration;
    public float attackSpeedReduction;

    public List<Type> currrentLearnedTypes;
    public int currentTypeIndex;

    
    private GameController gameController;

    public AudioSource hit;

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

     public delegate void OnCurrentLearnedTypesChangeDelegate(List<Type> newVal);
    public event OnCurrentLearnedTypesChangeDelegate OnCurrentLearnedTypesChange;

    private float damagedInvulnerabilityTimer;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();;
        weaponDatabase = gameController.GetComponent<WeaponDatabase>();
        currentHealth = baseMaxHealthPoints;
        currentMaxHealth = baseMaxHealthPoints;
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        damagedInvulnerabilityTimer = 0;
        CreateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameState == GameState.NotActive) {
            return;
        }

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
        currentTypeIndex = (currentTypeIndex + 1) % currrentLearnedTypes.Count;
        Destroy(currentWeapon.gameObject);
        CreateWeapon();
    }

    void CreateWeapon()
    {
        Type type = Type.Fire;
        if (!(currentTypeIndex >= currrentLearnedTypes.Count || currrentLearnedTypes.Count < 1))
        {
            type = currrentLearnedTypes[currentTypeIndex];
        }

        Weapon currentWeaponPrefab = weaponDatabase.GetWeapon(type);
        currentWeapon = Instantiate(currentWeaponPrefab);
        currentWeapon.transform.parent = weaponHolderTransform;
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }

    public void AddForm(Type form)
    {
        if (!currrentLearnedTypes.Contains(form))
        {
            currrentLearnedTypes.Add(form);
            OnCurrentLearnedTypesChange(currrentLearnedTypes);
        }
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (damagedInvulnerabilityTimer <= 0 && !enemy.isDead)
            {
                hit.Play();
                currentHealth = currentHealth - enemy.damage;
                damagedInvulnerabilityTimer += 2;
            }
        }
    }

    public void UsePowerUp(PowerUp powerUp)
    {
        switch (powerUp.upgrade)
        {
            case UpgradeOption.IncreaseMaxHealth:
                currentMaxHealth += (int)powerUp.value;
                currentHealth += (int)powerUp.value;
                break;
            case UpgradeOption.IncreaseAttackPower:
                attackPower += (float)powerUp.value;
                break;
            case UpgradeOption.IncreaseMoveSpeed:
                movementSpeed += (float)powerUp.value;
                break;
            case UpgradeOption.DecreaseAttackCooldown:
                attackSpeedReduction += (float)powerUp.value;
                break;
            case UpgradeOption.IncreaseDashRange:
                dashRange += (float)powerUp.value;
                break;
            case UpgradeOption.DecreaseDashCooldown:
                dashCooldown -= (float)powerUp.value;
                break;
            case UpgradeOption.AddForm:
                AddForm((Type)powerUp.value);
                break;
        }
    }
}
