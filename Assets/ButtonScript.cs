using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    public LifeManager lifeManager;
    public void EndGame()
    {
        lifeManager.PlaySFX(clip);
        StartCoroutine(LoadSceneRoutine(SceneManager.GetActiveScene().buildIndex -1 , clip.length));

    }
    public void TryAgain()
    {
        lifeManager.PlaySFX(clip);
        StartCoroutine(LoadSceneRoutine(SceneManager.GetActiveScene().buildIndex , clip.length));
    }

    private IEnumerator LoadSceneRoutine(int index,float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }

}
