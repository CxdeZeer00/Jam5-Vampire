using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public float Speed = 100f;
    float RotX = 0f;

    public Transform Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        AltCursor();
    }

    void AltCursor() 
    {
        if (Input.GetKey(KeyCode.LeftAlt)) 
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
