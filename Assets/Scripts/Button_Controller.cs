using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Controller : MonoBehaviour //Zoe García
{
    //public GameObject startButton;
    //public GameObject tutoButton;
    //public GameObject minimizeButton;
    public GameObject pauseMenu;

    public void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; //meter a este par en los starts ya q si viene desde el juego no se entera de q tiene q enseñar el cursor.
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

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameLevel");
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Tuto()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Final");
        }
    }

}
