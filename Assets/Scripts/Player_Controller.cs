using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour  //Paula Pinilla
{
    [Header("Movement")]
    float speed = 0.3f;

    [Header("Turn Around")]
    public Transform turnAround;    //meter aquí la cámara del jugador para que haga simulación de darse la vuelta
    float turnSpeed = 5f;    //a qué velocidad se va a girar el personaje
    private float originalAngle = 0f;   //el ángulo al que se encuentra actualmente es 0 / de frente

    void Update()
    {
        Movement();
        Run();
        TurnAround();
    }

    void Movement() 
    {
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * zDirection + transform.right * xDirection;

        GetComponent<Rigidbody>().MovePosition(transform.position +  moveDirection * speed);
    }

    void Run() 
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            speed = 0.6f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 0.3f;
        }
    }

    void TurnAround() 
    {
        if (turnAround == null) return; //si la cámara no está asignada evitará que el juego se paralice
        float backAngle = Input.GetKey(KeyCode.F) ? 180f : 0f;  //al pulsar la F, la cámara girará 180º. Si se deja de pulsar vuelve a 0
        originalAngle = Mathf.LerpAngle(originalAngle, backAngle, Time.deltaTime * turnSpeed);  //la transición entre ángulo original y ángulo de espaldas
        turnAround.localRotation = Quaternion.Euler(turnAround.localRotation.eulerAngles.x, originalAngle, 0f); //se aplica el giro para no romperlo si se mira a otro lugar
    }
}
