using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima = 100f;
    public GameObject enemiePrefab;
    private Player muerto;

    [Header("Navigation")]
    public Transform target;
    private NavMeshAgent agent;
    public float tiempoParado = 1f;
    public Transform IA;

    [Header("Movimiento")]
    private Vector2 movementInput;
    private Rigidbody2D rb2D;
    private Animator animator;
    private bool enMovimiento;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        enMovimiento = true;
        agent.SetDestination(target.position);
        animator.SetBool("enMovimiento", enMovimiento);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            vidaMaxima -= 25;
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
