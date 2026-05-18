using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemigo;
    [SerializeField]
    private float enemigoInterval = 3.5f;
    [SerializeField]
    public int numeroEnemigos  = 3;

    private bool spawning = false;
    void Start()
    {
    }
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        int contador = 0;
        while (contador != numeroEnemigos)
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
            contador++;
            yield return new WaitForSeconds(interval);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(spawnEnemy(enemigoInterval, enemigo));
        }
    }
}