using UnityEngine;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using System;
public class Player : MonoBehaviour
{
    [Header("Municion")]
    public int balasMaximas = 30;
    private int balasActuales;
    private int balasSumar;
    public TMP_Text textoBalas;

    [Header("Disparo")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;
    public Transform firePoint;
    private float lifeTime = 3f;

    [Header("Movimiento")]
    public float speed = 5f;
    public GameObject player;
    private Rigidbody2D rb2D;
    private Vector2 movementInput;
    private Vector2 lastDirection = Vector2.down;
    private Animator animator;

    [Header("Vida")]
    public float VidaMax = 100f;
    private float VidaActual;
    public Image barraDeVida;
    public event EventHandler MuerteJugador;
    public bool Muerto = false;



    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        VidaActual = VidaMax;

        balasActuales = balasMaximas;
        ActualizarUIBalas();
    }

    void Update()
    {
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput = movementInput.normalized;

        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.magnitude);

        if (movementInput != Vector2.zero)
        {
            lastDirection = movementInput;
        }
        //else if (movementInput == Vector2.zero) 
        //{ 
        //    lastDirection = Vector2.down;
        //}
        if (Input.GetKey(KeyCode.Space) &&
        Time.time >= nextFireTime &&
        balasActuales > 0)
        {
            Shoot();
            balasActuales--;
            if (balasActuales != 0)
            {
                balasSumar++;
            }
            ActualizarUIBalas();

            nextFireTime = Time.time + fireRate;
        }
        if (Input.GetKeyDown(KeyCode.R) && balasMaximas > 0 && balasActuales < 30)
        {
            balasActuales += balasSumar;
            balasMaximas -= balasSumar;
            balasSumar = 0;
            ActualizarUIBalas();
        }
        Death();
    }
    private void FixedUpdate()
    {
        rb2D.linearVelocity = movementInput * speed;
    }
    void ActualizarUIBalas()
    {
        textoBalas.text = balasActuales + " / " + balasMaximas;
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = lastDirection * bulletSpeed;

        Destroy(bullet, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            VidaActual -= 10;
            barraDeVida.fillAmount = VidaActual / VidaMax;
        }
        if (other.gameObject.CompareTag("Municion"))
        {
            balasMaximas += 30;
            ActualizarUIBalas();
        }
    }
    void Death()
    {
        if (VidaActual <= 0)
        {
            Muerto = true;
            MuerteJugador?.Invoke(this, EventArgs.Empty);
            Destroy(player);
        }
    }
}
