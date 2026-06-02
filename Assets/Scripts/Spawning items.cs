using UnityEngine;

public class Spawningitems : MonoBehaviour
{
    public Items itemData;

    public GameObject[] availableItems;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (availableItems.Length > 0)
        {
            int randomItem = Random.Range(0, availableItems.Length);
            Instantiate(availableItems[randomItem], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
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
