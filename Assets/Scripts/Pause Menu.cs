using UnityEngine;
using System.Collections;


public class PauseMenu : MonoBehaviour //Zoe García
{
    public GameObject pauseInterface;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        pauseInterface.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pauseInterface.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        //if (pauseInterface == false)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //    Time.timeScale = 1f;
        //}
    }
}
