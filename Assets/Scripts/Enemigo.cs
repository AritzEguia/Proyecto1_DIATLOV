using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima = 100f;
    public GameObject enemiePrefab;

    [Header("Seguimiento")]
    public Transform objetivo;
    public float velocidad = 5f;

    void Start()
    {
    }

    void Update()
    {
        if (objetivo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            vidaMaxima -= 10;
        }
        if (vidaMaxima <= 0)
        {
            Destroy(enemiePrefab);
        }
    }   
}
