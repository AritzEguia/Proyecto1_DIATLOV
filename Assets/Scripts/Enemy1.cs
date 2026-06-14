using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Enemy1 : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima = 100f;
    public GameObject enemiePrefab;
    private Player muerto;
    public static int numMuertos = 0;

    [Header("Navigation")]
    public Transform target;
    private NavMeshAgent agent;
    public float tiempoParado = 1f;
    public Transform IA;

    [Header("Nombre de la escena a cargar")]
    public string nombreEscena;

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
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        
        animator.SetFloat("Horizontal", agent.velocity.x);
        animator.SetFloat("Vertical", agent.velocity.y);
        animator.SetFloat("Speed", agent.velocity.magnitude);
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
            Cambiar();
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
    private void Cambiar()
    {
        Debug.Log("Botón presionado, cargando escena: " + nombreEscena);

        if (!string.IsNullOrEmpty(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogWarning("No se ha asignado ninguna escena en el inspector.");
        }
    }
}
