using UnityEngine;

public class ItemSpawner : MonoBehaviour //Zoe García
{
    public GameObject[] availableItems;
    public float respawnTime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void NormalItem()
    {
        if (availableItems.Length > 0)
        {
            int randomItem = Random.Range(0, availableItems.Length);
            GameObject clon = Instantiate(availableItems[randomItem], transform.position, Quaternion.identity);

            if(clon.GetComponent<Spawningitems>() != null)
            {
                clon.GetComponent<Spawningitems>().spawnOrigin = this;
            }
        }
    }

    // Update is called once per frame
    public void KeyItem(GameObject prefabKey)
    {
        if(prefabKey != null)
        {
            Instantiate(prefabKey, transform.position, Quaternion.identity);
        }
    }

    public void RespawnObjects()
    {
        Invoke("NormalItem", respawnTime); //el invoke es para llamar a una funcion pero q pueda esperar, asi no usamos corrutina ni nah
    }
}
