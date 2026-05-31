using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemigo;
    [SerializeField]
    public float enemigoInterval = 3.5f;

    public int numeroEnemigos  = 10;
    
    static int spawnedNumber = 0;

    private bool enemySpawned = false;

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        while (spawnedNumber < numeroEnemigos)
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-40f, -1f), Random.Range(-28f, -5f), 0), Quaternion.identity);
            spawnedNumber++;
            Debug.Log(spawnedNumber);
            yield return new WaitForSeconds(interval);
        }
        enemySpawned = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !enemySpawned){
            StartCoroutine(spawnEnemy(enemigoInterval, enemigo));
            enemySpawned=true;
        }
    }
}