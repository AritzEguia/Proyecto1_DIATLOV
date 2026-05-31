using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransporter : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    [SerializeField] private string sceneName;

    [Header("Etiqueta del jugador")]
    [SerializeField] private string playerTag = "Player";

    [Header("Configuración de transición")]
    [SerializeField] private float fadeDuration = 1.5f; // tiempo del fundido
    [SerializeField] private Image fadeImage; // imagen negra en el Canvas

    private bool isTransitioning = false;

    private void Start()
    {
        // Si existe imagen de fade, asegurarse de que comienza transparente
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTransitioning && other.CompareTag(playerTag))
        {
            StartCoroutine(TransitionAndLoadScene());
        }
    }

    private IEnumerator TransitionAndLoadScene()
    {
        isTransitioning = true;

        if (fadeImage != null)
        {
            yield return StartCoroutine(Fade(0f, 1f)); // fundido a negro
        }

        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = color;
            yield return null;
        }
    }
}
