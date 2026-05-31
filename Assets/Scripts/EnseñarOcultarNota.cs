using UnityEngine;
using System;

public class EnseñarOcultarNota : MonoBehaviour
{
    public GameObject notaVisual;
    public bool activa;

    void Update()
    {
        if (activa == true)
        {
            notaVisual.SetActive(true);
        }
        else
        {
            notaVisual.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activa = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activa = false;
        }
    }
}
