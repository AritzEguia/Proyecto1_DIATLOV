using UnityEngine;
using System.Threading;
public class Player : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;
    public Transform firePoint;

    [Header("Movimiento")]
    public float speed = 5f;

    private Rigidbody2D rb2D;
    private Vector2 movementInput;
    private Vector2 lastDirection = Vector2.down;
    private Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
        else if (movementInput == Vector2.zero) 
        { 
            lastDirection = Vector2.down;
        }
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    private void FixedUpdate()
    {
        rb2D.linearVelocity = movementInput * speed;
    }
    
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = lastDirection * bulletSpeed;
    }
}
