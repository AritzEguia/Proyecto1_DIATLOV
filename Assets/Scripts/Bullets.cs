using UnityEngine;

public class Bullets : MonoBehaviour
{

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            Destroy(gameObject);
        }
    }
}
