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
        if (collision.CompareTag("Player"))
        {
            // Buscamos el script de la mochila en el jugador
            ItemFunctions mochila = collision.GetComponent<ItemFunctions>();

            if (mochila != null)
            {
                // Añadimos los DATOS a la lista del inventario
                mochila.inventory.Add(itemData);
                Debug.Log("Recogido: " + itemData.name);

                Destroy(gameObject);
            }
        }
    }
}
