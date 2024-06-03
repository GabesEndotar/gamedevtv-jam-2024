using UnityEngine;
using TMPro;
using Spine.Unity;
using System.Collections;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI resultTextTMP;
    public TextMeshProUGUI cooldownTextTMP; // Reference to TextMeshProUGUI for cooldown counter

    private enum Choice { Rock, Paper, Scissors };
    private SkeletonAnimation skeletonAnimation;
    public string startAnimationName;
    public string idleAnimationName;
    public string animationName1;
    public string animationName2;
    public string animationName3;
    public string deathAnimation;
    public string keyPressAnimation;
    private Choice playerChoice;
    private Choice aiChoice;
    [HideInInspector]
    public bool canMakeChoice = true; // Cooldown control
    public bool doNothing = false;

    [SerializeField]
    private LifeManager lifeManager;

    [SerializeField]
    private GameObject buttonQ;
    [SerializeField]
    private GameObject buttonW;
    [SerializeField]
    private GameObject buttonE;

    [SerializeField]
    private AudioSource attackSound;
    [SerializeField]
    private AudioSource defendSound;
    [SerializeField]
    private AudioSource damageSound;
    [SerializeField]
    private AudioSource dKdamageSound;

    [SerializeField]
    private AudioClip attackClip;
    [SerializeField]
    private AudioClip defendClip;
    [SerializeField]
    private AudioClip damageClip;
    [SerializeField]
    private AudioClip dKdamageClip;

    [SerializeField]
    private GameObject darkKnight;

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        canMakeChoice = false;
        doNothing = false;
        StartCoroutine(CooldownRoutine());
        PlayAnimation(startAnimationName);
        cooldownTextTMP.gameObject.SetActive(false); // Initially disable the cooldown counter
    }

    void Update()
    {
        if (canMakeChoice) // Check if a choice can be made
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                buttonQ.GetComponent<SkeletonAnimation>().state.SetAnimation(0, keyPressAnimation, false);
                PlayerMakesChoice(Choice.Rock);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                buttonW.GetComponent<SkeletonAnimation>().state.SetAnimation(0, keyPressAnimation, false);
                PlayerMakesChoice(Choice.Paper);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                buttonE.GetComponent<SkeletonAnimation>().state.SetAnimation(0, keyPressAnimation, false);
                PlayerMakesChoice(Choice.Scissors);
            }
        }
    }

    void PlayerMakesChoice(Choice choice)
    {
        playerChoice = choice;
        aiChoice = (Choice)Random.Range(0, 3);
        DetermineWinner();
        StartCoroutine(CooldownRoutine()); // Start cooldown
    }

    void DetermineWinner()
    {
        if (playerChoice == aiChoice)
        {
            PlayDKAnimation(animationName2);
            StartCoroutine(PlayAnimationWithDelay(animationName1,defendClip, 0.5f)); // Delay before tie animation
            resultTextTMP.text = "Tie! Both chose " + playerChoice;
            attackSound.PlayOneShot(attackClip);
        }
        else if ((playerChoice == Choice.Rock && aiChoice == Choice.Scissors) ||
                 (playerChoice == Choice.Paper && aiChoice == Choice.Rock) ||
                 (playerChoice == Choice.Scissors && aiChoice == Choice.Paper))
        {
            PlayAnimation(animationName2);
            StartCoroutine(PlayDKAnimationWithDelay(animationName3,dKdamageClip, 0.5f)); // Delay before win animation
            resultTextTMP.text = "You Won! You chose " + playerChoice + " and the enemy chose " + aiChoice;
            lifeManager.AdjustLife(true); // Player won
            
            // Play the attack sound when a choice is made
            attackSound.PlayOneShot(attackClip);
        }
        else
        {
            PlayDKAnimation(animationName2);
            StartCoroutine(PlayAnimationWithDelay(animationName3,damageClip, 0.5f)); // Delay before lose animation
            resultTextTMP.text = "You Lose! You chose " + playerChoice + " and the enemy chose " + aiChoice;
            lifeManager.AdjustLife(false); // Player lost
            attackSound.PlayOneShot(attackClip);
        }
    }

    public void PlayAnimation(string animationName)
    {
        var trackEntry = skeletonAnimation.state.SetAnimation(0, animationName, false);
        trackEntry.Complete += delegate
        {
            skeletonAnimation.state.SetAnimation(0, idleAnimationName, true);
        };
    }
    public void PlayDKAnimation(string animationName)
    {
        var trackEntry = darkKnight.GetComponent<SkeletonAnimation>().state.SetAnimation(0, animationName, false);
        trackEntry.Complete += delegate
        {
            darkKnight.GetComponent<SkeletonAnimation>().state.SetAnimation(0, idleAnimationName, true);
        };
    }

    IEnumerator PlayAnimationWithDelay(string animationName,AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        defendSound.PlayOneShot(clip);
        var trackEntry = skeletonAnimation.state.SetAnimation(0, animationName, false);
        trackEntry.Complete += delegate
        {
            skeletonAnimation.state.SetAnimation(0, idleAnimationName, true);
        };
    }
        IEnumerator PlayDKAnimationWithDelay(string animationName,AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        defendSound.PlayOneShot(clip);
        var trackEntry = darkKnight.GetComponent<SkeletonAnimation>().state.SetAnimation(0, animationName, false);
        trackEntry.Complete += delegate
        {
            darkKnight.GetComponent<SkeletonAnimation>().state.SetAnimation(0, idleAnimationName, true);
        };
    }

    IEnumerator CooldownRoutine()
    {
        canMakeChoice = false; // Disable new choices
        cooldownTextTMP.gameObject.SetActive(true); // Enable cooldown counter

        float cooldownTime = 3f; // Set cooldown time
        float elapsedTime = 0f;

        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            int remainingTime = Mathf.CeilToInt(cooldownTime - elapsedTime);
            cooldownTextTMP.text = remainingTime.ToString(); // Update counter text
            yield return null;
        }

        cooldownTextTMP.gameObject.SetActive(false); // Disable cooldown counter
        if (!doNothing)
        {
            canMakeChoice = true; // Enable new choices
        }
        resultTextTMP.text = "";
    }
}