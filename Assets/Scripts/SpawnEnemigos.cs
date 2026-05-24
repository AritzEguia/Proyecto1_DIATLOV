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
    [SerializeField]
    public int numeroEnemigos  = 3;

    private bool spawning = false;
    void Start()
    {
    }
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        int contador = 0;
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-10f, 10f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        yield return new WaitForSeconds(interval);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            if (!spawning)
            {
                spawning = true;
                StartCoroutine(spawnEnemy(enemigoInterval, enemigo));
            }
        }
    }
}