using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float min_Y = -4f, max_Y = 4f;

    public GameObject[] asteroid_Prefabs;
    public GameObject enemyPrefab;

    public float timer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        float pos_Y = Random.Range(min_Y, max_Y);
        Vector3 temp = transform.position;
        temp.y = pos_Y;

        if (Random.Range(0, 2) > 0)
        {
            Instantiate(asteroid_Prefabs[Random.Range(0, asteroid_Prefabs.Length)], temp, Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefab, temp, Quaternion.Euler(0f, 0f, 90f));
        }

        Invoke("SpawnEnemies", timer);
    }
}
