using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] availableItems;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void NormalItem()
    {
        if (availableItems.Length > 0)
        {
            int randomItem = Random.Range(0, availableItems.Length);
            Instantiate(availableItems[randomItem], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    public void KeyItem(GameObject prefabKey)
    {
        Instantiate(prefabKey, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
