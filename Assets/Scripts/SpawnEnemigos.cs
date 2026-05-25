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
            while (contador != 3)
            {
                GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-40f, -1f), Random.Range(-28f, -5f), 0), Quaternion.identity);
                contador++;
            }
            yield return new WaitForSeconds(interval);
            spawning = false;
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