using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabDatabase;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private ChoosePowerUp choosePowerUpPrefab;
    [SerializeField] private Health healthPrefab;
    [SerializeField] private GameObject loseScreenPrefab;

    private List<Enemy> enemiesOnField;

    private List<Bounds> spawnAreas;

    private Dictionary<Type, Map> maps;

    private Type activeMapType;

    private Player player;

    public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        spawnAreas = new List<Bounds>();
        enemiesOnField = new List<Enemy>();
        maps = new Dictionary<Type, Map>();

        maps.Add(Type.Water, GameObject.FindObjectOfType<WaterMap>());
        maps.Add(Type.Fire, GameObject.FindObjectOfType<FireMap>());
        maps.Add(Type.Wood, GameObject.FindObjectOfType<WoodMap>());

        ChangeMap(Type.Fire);
        SpawnPlayer();
        SpawnEnemies();

        player.OnCurrentHealthChange += CurrentHealthChange;

        gameState = GameState.Active;
    }

    void ChangeMap(Type type)
    {
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
        foreach (Bounds spawnArea in spawnAreas)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 spawnPosition = new Vector2(UnityEngine.Random.Range(spawnArea.min.x, spawnArea.max.x), UnityEngine.Random.Range(spawnArea.min.y, spawnArea.max.y));
                enemiesOnField.Add(Instantiate<Enemy>(enemyPrefabDatabase[0], spawnPosition, Quaternion.identity));
            }
        }
    }

    void SpawnPlayer()
    {
        player = Instantiate<Player>(playerPrefab, Vector3.zero, Quaternion.identity);
        Health health = Instantiate<Health>(healthPrefab);
        health.Init(player);
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
        ChoosePowerUp choosePowerUp = Instantiate<ChoosePowerUp>(choosePowerUpPrefab);
        String[] options = { "Test Option 1", "Test option2", "Test option 3" };
        choosePowerUp.Init(options);
    }

    public void ChoosePowerUp()
    {
        NextMap();
        SpawnEnemies();
        CenterPlayer();
        gameState = GameState.Active;
    }

    void CenterPlayer()
    {
        player.transform.position = Vector3.zero;
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
