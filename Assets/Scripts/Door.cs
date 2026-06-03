using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerInside = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Alpha5))
        {
            isPlayerInside = true;

            animator.Play("Puerta");            
        }
    }
}

