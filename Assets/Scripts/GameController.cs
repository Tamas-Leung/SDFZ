using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabDatabase;
    [SerializeField] private Enemy bossEnemyPrefab;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private ChoosePowerUp choosePowerUpPrefab;
    [SerializeField] private GameUi gameUiPrefab;
    [SerializeField] private GameObject loseScreenPrefab;

    private List<Enemy> enemiesOnField;
    private WeaponDatabase weaponDatabase;

    private List<Bounds> spawnAreas;

    private Dictionary<Type, Map> maps;

    private Type activeMapType;

    private Player player;

    private int roundNumber;

    public GameState gameState;

    public List<AudioSource> bgm;

    private AudioSource m;

    public AudioSource reward;

    // Start is called before the first frame update
    void Start()
    {
        spawnAreas = new List<Bounds>();
        enemiesOnField = new List<Enemy>();
        maps = new Dictionary<Type, Map>();
        weaponDatabase = GetComponent<WeaponDatabase>();
        roundNumber = 0;

        maps.Add(Type.Water, GameObject.FindObjectOfType<WaterMap>());
        maps.Add(Type.Fire, GameObject.FindObjectOfType<FireMap>());
        maps.Add(Type.Wood, GameObject.FindObjectOfType<WoodMap>());
        gameState = GameState.NotActive;

        ChoosePowerUp chooseWeapon = Instantiate<ChoosePowerUp>(choosePowerUpPrefab);
        PowerUp[] options = {
            new PowerUp(UpgradeOption.AddForm,Type.Fire, UpgradeTier.Epic),
            new PowerUp(UpgradeOption.AddForm,Type.Water, UpgradeTier.Epic),
            new PowerUp(UpgradeOption.AddForm,Type.Wood, UpgradeTier.Epic)
        };
        chooseWeapon.Init(options, StartGame);
    }


    void StartGame(PowerUp option)
    {
        ChangeMap((Type)option.value);
        roundNumber++;
        SpawnPlayer();
        ChoosePowerUp(option);

        player.OnCurrentHealthChange += CurrentHealthChange;

        gameState = GameState.Active;
    }

    public Player getPlayer()
    {
        return player;
    }

    void ChangeMap(Type type)
    {

        if (m)
        {
            m.Stop();
        }
        if (TypeMethods.GetNameFromType(type) == "Fire")
        {
            m = bgm.Find(x => x.name == "fire_normal");
            m.volume = 0.5f;
            m.Play();
        }
        else if (TypeMethods.GetNameFromType(type) == "Water")
        {
            m = bgm.Find(x => x.name == "water_normal");
            m.volume = 0.5f;
            m.Play();
        }
        else
        {
            m = bgm.Find(x => x.name == "wood_normal");
            m.volume = 0.5f;
            m.Play();
        }
        activeMapType = type;
        DeactivateMaps();
        maps[activeMapType].gameObject.SetActive(true);
        SetSpawnColliders();
    }

    void SetSpawnColliders()
    {
        spawnAreas.Clear();
        foreach (Collider2D collider in maps[activeMapType].transform.Find("SpawnAreas").GetComponentsInChildren<Collider2D>())
        {
            spawnAreas.Add(collider.bounds);
        }
    }

    void DeactivateMaps()
    {
        foreach (Map map in maps.Values)
        {
            map.gameObject.SetActive(false);
        }
    }

    void SpawnEnemies()
    {
        if (roundNumber % 4 == 0)
        {
            Bounds spawnArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
            Vector2 spawnPosition = new Vector2(UnityEngine.Random.Range(spawnArea.min.x, spawnArea.max.x), UnityEngine.Random.Range(spawnArea.min.y, spawnArea.max.y));
            Enemy bossEnemy = Instantiate<Enemy>(bossEnemyPrefab, spawnPosition, Quaternion.identity);
            bossEnemy.SetBossType(activeMapType);
            enemiesOnField.Add(bossEnemy);
        }
        for (int i = 0; i < 10 + roundNumber * 5; i++)
        {
            Bounds spawnArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
            Vector2 spawnPosition = new Vector2(UnityEngine.Random.Range(spawnArea.min.x, spawnArea.max.x), UnityEngine.Random.Range(spawnArea.min.y, spawnArea.max.y));
            enemiesOnField.Add(Instantiate<Enemy>(enemyPrefabDatabase[Random.Range(0, enemyPrefabDatabase.Length)], spawnPosition, Quaternion.identity));
        }
    }

    void SpawnPlayer()
    {
        player = Instantiate<Player>(playerPrefab, Vector3.zero, Quaternion.identity);
        GameUi gameUi = Instantiate<GameUi>(gameUiPrefab);
        gameUi.Init(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Ended) return;
        if (gameState == GameState.Active && CheckEnemiesAllDead())
        {
            EndWave();
        }
    }

    void CurrentHealthChange(int newVal)
    {
        if (newVal <= 0)
        {
            gameState = GameState.Ended;
            Instantiate(loseScreenPrefab);
        }
    }

    void EndWave()
    {
        gameState = GameState.NotActive;
        RemoveOldEnemies();
        if (roundNumber % 4 == 0)
        {
            player.AddForm(activeMapType);
        }
        ChoosePowerUp choosePowerUp = Instantiate<ChoosePowerUp>(choosePowerUpPrefab);
        PowerUp[] options = PowerUpMethods.GetThreeRandomPowerUps();
        choosePowerUp.Init(options, ChoosePowerUp);
    }

    public void ChoosePowerUp(PowerUp option)
    {
        player.UsePowerUp(option);
        reward.Play();
        StartNextRound();
    }

    public void StartNextRound()
    {
        roundNumber += 1;
        if (roundNumber % 4 == 0)
        {
            NextMap();
        }
        CenterPlayer();
        SpawnEnemies();
        gameState = GameState.Active;
    }

    void CenterPlayer()
    {
        player.GetComponent<Rigidbody2D>().position = Vector3.zero;
    }

    void NextMap()
    {
        if (activeMapType == Type.Wood)
        {
            ChangeMap(Type.Fire);
        }
        else if (activeMapType == Type.Fire)
        {
            ChangeMap(Type.Water);
        }
        else
        {
            ChangeMap(Type.Wood);
        }
    }

    bool CheckEnemiesAllDead()
    {
        foreach (Enemy enemy in enemiesOnField)
        {
            if (!enemy.isDead) return false;
        }
        return true;
    }

    void RemoveOldEnemies()
    {
        foreach (Enemy enemy in enemiesOnField)
        {
            Destroy(enemy.gameObject);
        }
        enemiesOnField.Clear();
    }
}
