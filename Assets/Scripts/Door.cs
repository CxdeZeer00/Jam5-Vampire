using UnityEngine;

public class Door : MonoBehaviour
{
    private ItemFunctions inventarioScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventarioScript = Object.FindFirstObjectByType<ItemFunctions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player") && inventarioScript != null)
        {           
            inventarioScript.SetJugadorDentro(true, GetComponent<Animator>());
            Debug.Log("Puerta: El jugador ha entrado. Avisando al inventario.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && inventarioScript != null)
        {
            // Le decimos al script de inventario que el jugador se ha ido
            inventarioScript.SetJugadorDentro(false, null);
            Debug.Log("Puerta: El jugador se ha ido. Avisando al inventario.");
        }
    }
}

