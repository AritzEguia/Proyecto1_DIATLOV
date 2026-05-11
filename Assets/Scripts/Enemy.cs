using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima = 100f;
    public GameObject enemiePrefab;

    [Header("Navigation")]
    public Transform target;
    private NavMeshAgent agent;
    public float tiempoParado = 1f;

    [Header("Movimiento")]
    private Vector2 movementInput;
    private Rigidbody2D rb2D;
    private Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            vidaMaxima -= 10;
            Parar();
        }
        if (vidaMaxima <= 0)
        {
            Destroy(enemiePrefab);
        }
    }
    void Parar()
    {
        StartCoroutine(PararYReanudar());
    }
    IEnumerator PararYReanudar()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(tiempoParado);
        agent.isStopped = false;
    }
}
