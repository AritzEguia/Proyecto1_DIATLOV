using UnityEngine;

public class Bullets : MonoBehaviour
{

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy (gameObject);
    }
}
