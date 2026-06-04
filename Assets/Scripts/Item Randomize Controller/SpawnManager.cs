using UnityEngine;

public class SpawnManager : MonoBehaviour //Zoe García
{
    public GameObject keyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemSpawner[] everySpawner = FindObjectsByType<ItemSpawner>(FindObjectsSortMode.None);
        if(everySpawner.Length > 0)
        {
            int numberSelected=Random.Range(0,everySpawner.Length);
            for (int i = 0; i < everySpawner.Length; i++)
            {
                if (i == numberSelected)
                {
                    everySpawner[i].KeyItem(keyPrefab);
                }
                else
                {
                    everySpawner[i].NormalItem();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
