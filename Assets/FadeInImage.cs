using UnityEngine;
using UnityEngine.UI;

public class FadeInEffect : MonoBehaviour
{
    public float fadeInDuration = 2f; // Duração do efeito de fade in (em segundos)
    private float alpha = 0f; // Valor de transparência inicial
    private Image image; // Componente de imagem para aplicar o efeito

    void Start()
    {
        image = GetComponent<Image>(); // Obtém o componente Image
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha); // Inicializa a transparência
        StartCoroutine(FadeIn()); // Inicia o coroutine de fade in
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float startTime = Time.time; // Tempo inicial
        while (alpha < 1f)
        {
            alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeInDuration); // Interpola a transparência
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha); // Atualiza a transparência
            yield return null;
        }
    }
}