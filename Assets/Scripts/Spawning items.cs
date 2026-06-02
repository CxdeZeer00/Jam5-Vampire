using UnityEngine;

public class Spawningitems : MonoBehaviour
{
    public Items itemData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Algo ha entrado en mi zona: " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            // Buscamos el script de la mochila en el jugador
            ItemFunctions mochila = collision.GetComponent<ItemFunctions>();

            if (mochila != null && itemData != null)
            {
                // Añadimos los DATOS a la lista del inventario
                mochila.PickItem(itemData);
                Debug.Log("Recogido: " + itemData.name);

                Destroy(gameObject);
            }
        }
    }
}
