using UnityEngine;

public class Camera_Movement : MonoBehaviour    //Paula Pinilla
{
    public float Speed = 100f;
    float RotX = 0f;

    public Transform Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Speed = PlayerPrefs.GetFloat("SensivityCamera", 100f); //PlayerPrefs.Get__(string key, __ value);  si no encuentra lo q le pedimos, te devolverá lo q le hayas puesto último (...__ value…)
        //Zoe García ^^^

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return;

        AltCursor();
    }

    void AltCursor() 
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.Tab)) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float MouseX = Input.GetAxis("Mouse X") * Speed * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * Speed * Time.deltaTime;

            RotX -= MouseY;
            RotX = Mathf.Clamp(RotX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(RotX, 0f, 0f);
            Player.Rotate(Vector3.up * MouseX);
        }
    }
}
