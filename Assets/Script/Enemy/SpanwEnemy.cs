using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SpanwEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform water;
    [SerializeField] private GameObject player;
    [SerializeField] private int minEnemies = 10; 
    [SerializeField] private int maxEnemies = 15; 
    [SerializeField] private float minDistanceBetweenEnemies = 5.0f;
    [SerializeField] private float minDistanceFromPlayer = 10.0f;
    
    private readonly List<Vector3> _spawnPositions = new List<Vector3>();
    private int _upgradedPv;
    private int _upgradedAttack;
    private int _upgradedSpeed;
    private int _upgradedScore;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomSpawnEnemy();
        StartCoroutine(CallEveryThreeMinutes());
    }

    IEnumerator CallEveryThreeMinutes()
    {
        while (true)
        {
            yield return new WaitForSeconds(120f);
            UpgradeEnemy();
        }
    }
    
    private void RandomSpawnEnemy()
    {
        Transform waterTransform = water.transform;
        Vector3 waterScale = waterTransform.localScale;
        Vector3 waterPosition = waterTransform.position;
        
        float halfWidth = (waterScale.x / 2f)*4000;
        float halfLength = (waterScale.z / 2f)*4000;

        float minX = waterPosition.x - halfWidth;
        float maxX = waterPosition.x + halfWidth;
        float minZ = waterPosition.z - halfLength;
        float maxZ = waterPosition.z + halfLength;

        
        int numberOfEnemies = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition;
            bool validPosition;

            // Essaye de trouver une position valide
            int attempts = 0;
            do
            {
                spawnPosition = new Vector3(Random.Range(minX, maxX), waterPosition.y, Random.Range(minZ, maxZ));

                validPosition = true;
                
                if (Vector3.Distance(spawnPosition, player.transform.position) < minDistanceFromPlayer)
                {
                    validPosition = false;
                }
                
                foreach (Vector3 pos in _spawnPositions)
                {
                    if (Vector3.Distance(spawnPosition, pos) < minDistanceBetweenEnemies)
                    {
                        validPosition = false;
                        break;
                    }
                }

                attempts++;
                if (attempts > 100)
                {
                    break;
                }

            } while (!validPosition && numberOfEnemies < minEnemies);

            if (validPosition)
            {
                _spawnPositions.Add(spawnPosition);
                GameObject enemySpawn = Instantiate(enemy, spawnPosition, Quaternion.identity);
                enemySpawn.GetComponent<ManageEnemy>().Player(player);
                enemySpawn.GetComponent<EnemyController>().Player(player);
            }
        }
    }

    private void UpgradeEnemy()
    {
        if (enemy.GetComponent<ManageEnemy>().GetPv() < 4000)
        {
            _upgradedPv = enemy.GetComponent<ManageEnemy>().GetPv() * 2; //max 800
            _upgradedAttack = (int)Mathf.Ceil(enemy.GetComponent<ManageEnemy>().GetAttack() * 1.6f); 
            _upgradedSpeed = (int)Mathf.Ceil(enemy.GetComponent<ManageEnemy>().GetMoveSpeed() * 1.2f); 
            _upgradedScore = (int)Mathf.Ceil(enemy.GetComponent<ManageEnemy>().GetScore() * 1.6f);
        
            enemy.GetComponent<ManageEnemy>().Upgrade(_upgradedPv, _upgradedAttack, _upgradedSpeed, _upgradedScore);
            RandomSpawnEnemy();
        }
        else
        {
            RandomSpawnEnemy();
        }
    }
}
