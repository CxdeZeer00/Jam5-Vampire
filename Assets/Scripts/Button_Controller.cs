using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Controller : MonoBehaviour
{
    public GameObject startButton;
    public GameObject tutoButton;
    public GameObject minimizeButton;
    public GameObject pauseMenu;

    public void Introduction()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Intro");
    }

    //public void Menu()
    //{
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene("MainMenu");
    //}

    //public void Play()
    //{
    //    Time.timeScale = 1f;
    //    pauseMenu.SetActive(false);
    //}

    public void Tuto()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(true);
    }
}
