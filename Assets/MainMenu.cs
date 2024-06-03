using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        //this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(3).gameObject.SetActive(true);

    }
    public void Next()
    {
        this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
        public void Tutorial()
    {
        this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(3).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(4).gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 );
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
