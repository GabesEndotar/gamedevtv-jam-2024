using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public int playerLife = 3; // Vida inicial do jogador
    public int monsterLife = 3; // Vida inicial do monstro

    public TextMeshProUGUI playerLifeTextTMP; // Referência ao TextMeshProUGUI para a vida do jogador
    public TextMeshProUGUI monsterLifeTextTMP; // Referência ao TextMeshProUGUI para a vida do monstro
    public TextMeshProUGUI resultTextTMP; // Referência ao TextMeshProUGUI para mostrar o resultado
    public GameObject resultTextBg;
    [SerializeField]
    private GameController gameController;
    [SerializeField] private GameObject gameOverScreens;
    [SerializeField] private GameObject allTexts;
    [SerializeField] private GameMusicManager gameMusicManager;
    public AudioSource audioSource;

    void Start()
    {
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>(); // Obtém a referência ao GameController
        }
        UpdateLifeUI();
    }

    public void UpdateLifeUI()
    {
        playerLifeTextTMP.text = "Player Life: " + playerLife;
        monsterLifeTextTMP.text = "Enemy Life: " + monsterLife;
    }

    public void AdjustLife(bool playerWon)
    {
        if (playerWon)
        {
            monsterLife--;
        }
        else
        {
            playerLife--;
        }
        UpdateLifeUI();
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (playerLife <= 0)
        {
            resultTextBg.SetActive(true);
            resultTextTMP.text = "Game Over! You lose.";
            gameController.canMakeChoice = false;
            gameController.doNothing = true; // Desabilita escolhas após o jogo acabar
            gameController.PlayAnimation(gameController.deathAnimation);
            allTexts.SetActive(false);
            gameMusicManager.GameOverSound();
            ActivateWithFadeIn(gameOverScreens.transform.GetChild(0).transform.GetChild(1).gameObject);
        }
        else if (monsterLife <= 0)
        {
            resultTextBg.SetActive(true);
            resultTextTMP.text = "Congratulations! You have defeated the enemy.";
            gameController.canMakeChoice = false; // Desabilita escolhas após o jogo acabar
            gameController.doNothing = true;
            allTexts.SetActive(false);
            ActivateWithFadeIn(gameOverScreens.transform.GetChild(0).transform.GetChild(0).gameObject);
        }
    }
        private void ActivateWithFadeIn(GameObject gameObject)
    {
        // Ativa o GameObject
        gameObject.SetActive(true);

        // Adiciona o componente FadeInEffect se ainda não estiver presente
        FadeInEffect fadeInEffect = gameObject.GetComponent<FadeInEffect>();
        if (fadeInEffect == null)
        {
            fadeInEffect = gameObject.AddComponent<FadeInEffect>();
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}