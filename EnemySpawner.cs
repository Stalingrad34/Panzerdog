using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint = null;
    [SerializeField] private Enemy[] _enemys = null; 
    [Range(0.0f, 10.0f)]
    [SerializeField] private float spawnTime = 1.0f;
    private List<Enemy> enemysInScene = null;

    public IEnumerator SpawnEnemy(int numberOfEnemys)
    {
        int counter = 0;
        enemysInScene = new List<Enemy>();

        while (counter < numberOfEnemys)
        {
            int rndPoint = Random.Range(0, spawnPoint.Length);
            int rndEnemy = Random.Range(0, _enemys.Length);
            Enemy go = Instantiate(_enemys[rndEnemy], spawnPoint[rndPoint].position, Quaternion.identity);
            enemysInScene.Add(go);
            counter++;
            yield return new WaitForSeconds(spawnTime);
        }          
    }

    public void Restart()
    {
        foreach (Enemy enemy in enemysInScene)
        {
            Destroy(enemy.gameObject);
        }
    }
}
