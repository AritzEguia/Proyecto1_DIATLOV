using NUnit.Framework;
using UnityEngine;

public class CuelloDeBotella : MonoBehaviour
{
    public Collider2D accionador;
    public GameObject Monstruo1;
    public GameObject Monstruo2;
    public GameObject Monstruo3;
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player")){
            Monstruo1.SetActive(true);
            Monstruo2.SetActive(true);
            Monstruo3.SetActive(true);
        }
    }
}
