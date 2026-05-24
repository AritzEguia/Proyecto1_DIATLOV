using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuMuerto : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;

    private Player combateJugador;

    private void Start()
    {
        combateJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        combateJugador.MuerteJugador += ActivarMenu;
    }
    private void ActivarMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
    }
    public void reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
