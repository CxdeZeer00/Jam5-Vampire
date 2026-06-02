using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject keyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemSpawner[] everySpawner = FindObjectsOfType<ItemSpawner>();
        if(everySpawner.Length > 0)
        {
            int numberSelected=Random.Range(0,everySpawner.Length);
            for (int i = 0; i < numberSelected; i++)
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
