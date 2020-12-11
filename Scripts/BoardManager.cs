using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public int columns = 12;
    public int rows = 8;
    public GameObject[] floorTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] enemyTiles;

    private Transform boardHolder;

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -7; x < columns + 6; x++)
        {
            for (int y = -1; y < rows + 5; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -7 || x == columns + 5 || y == -1 || y == rows)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    void SpawnEnemy(GameObject[] tileArray, int level)
    {
        
        if (GameManager.stage == 1 && level == 1) // main
        {
            Instantiate(tileArray[(level - 1) % GameManager.instance.allEnemyCount], new Vector3(8, 4, 0f), Quaternion.identity);
        }
        else if (GameManager.stage == 1 && level >1) // tutorial
        {
            Instantiate(tileArray[(level - 1) % GameManager.instance.allEnemyCount], new Vector3(26, 5.4f, 0f), Quaternion.identity);
        }
        else if (GameManager.stage == 2) // forest
        {
            Instantiate(tileArray[(level - 1) % GameManager.instance.allEnemyCount], new Vector3(-9.3f, -10.8f, 0f), Quaternion.identity);
        }
        else if (GameManager.stage == 3) // cave
        {
            Instantiate(tileArray[(level - 1) % GameManager.instance.allEnemyCount], new Vector3(25.5f, -9.23f, 0f), Quaternion.identity);
        }
        /*else if (level == 5) // village
        {
            Instantiate(tileArray[(level - 1) % GameManager.instance.allEnemyCount], new Vector3(-10, 0, 0f), Quaternion.identity);
        }*/
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void SetupScene(int level)
    {
        if (level == 1 || level > 4)
            BoardSetup();
        SpawnEnemy(enemyTiles, level);
        GameObject.FindWithTag("Player").GetComponent<Player>().FindEnemyObject();
    }
}
