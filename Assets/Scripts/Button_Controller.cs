using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Controller : MonoBehaviour
{
    public GameObject startButton;
    public GameObject tutoButton;
    public GameObject minimizeButton;
    public GameObject pauseMenu;

    public void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    public void Introduction()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Introduction");
    }

    //public void Menu()
    //{
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene("MainMenu");
    //}

    public void Play()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Tuto()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
}
