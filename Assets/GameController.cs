using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI resultTextTMP;

    private enum Choice { Pedra, Papel, Tesoura };
    private Choice playerChoice;
    private Choice aiChoice;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerMakesChoice(Choice.Pedra);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerMakesChoice(Choice.Papel);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerMakesChoice(Choice.Tesoura);
        }
    }

    void PlayerMakesChoice(Choice choice)
    {
        playerChoice = choice;
        aiChoice = (Choice)Random.Range(0, 3);
        DetermineWinner();
    }

    void DetermineWinner()
    {
        if (playerChoice == aiChoice)
        {
            resultTextTMP.text = "Empate! Ambos escolheram " + playerChoice;
        }
        else if ((playerChoice == Choice.Pedra && aiChoice == Choice.Tesoura) ||
                 (playerChoice == Choice.Papel && aiChoice == Choice.Pedra) ||
                 (playerChoice == Choice.Tesoura && aiChoice == Choice.Papel))
        {
            resultTextTMP.text = "Você Ganhou! Você escolheu " + playerChoice + " e o computador escolheu " + aiChoice;
        }
        else
        {
            resultTextTMP.text = "Você Perdeu! Você escolheu " + playerChoice + " e o computador escolheu " + aiChoice;
        }
    }
}