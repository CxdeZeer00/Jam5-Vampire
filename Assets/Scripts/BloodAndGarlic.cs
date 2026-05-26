using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BloodAndGarlic : MonoBehaviour
{
   
    //Sangre obliga a acercarse al punto donde se deja, ajo obliga a huir de ti mientras lo llevas puesto
    public List <Items> inventory = new List<Items>();
    
    public GameObject bloodVial;
    private Transform playerTransform; //Saber dónde está el jugador


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); //Saber desde el primer momento dónde está el player
        if (player != null)
        {
            playerTransform = player.transform; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && inventory.Count > 0)
        {        
            Items itemActual = inventory[0];  // Primer ítem que haya en la lista de inventario

            #region //////BLOOD//////

            if (itemActual.distractedTime > 0) 
            {
                Debug.Log("Dejar en el suelo. Tiempo de distracción: " + itemActual.distractedTime);

                GameObject spawnedVial = Instantiate(bloodVial, playerTransform.position, Quaternion.identity); // Dejar frasco en la posición donde estaba el jugador

                CircleCollider2D attractionArea = spawnedVial.AddComponent<CircleCollider2D>();  //Crear un collider para "cambiar el target" del NavMesh
                attractionArea.isTrigger = true;
                attractionArea.radius = 5f;
                spawnedVial.name = "BloodAttractionPoint";

                inventory.RemoveAt(0); 
            }
            #endregion


            #region  //////GARLIC//////

            else if (itemActual.wearingTime > 0)
            {
                Debug.Log("Colgarte ajo. Tiempo de huída: " + itemActual.wearingTime);

                GameObject invisibleGarlic = new GameObject("ActiveGarlic"); //No vemos el ajo porque lo lleva puesto el player
                invisibleGarlic.transform.SetParent(playerTransform); //"Pegar" ajo a player
                invisibleGarlic.transform.localPosition = Vector3.zero;

                CircleCollider2D repulsionArea = invisibleGarlic.AddComponent<CircleCollider2D>(); //Crear un collider (de nuevo) que espanta al vampiro
                repulsionArea.isTrigger = true;
                repulsionArea.radius = 4f;

                Destroy(invisibleGarlic, itemActual.wearingTime);

                inventory.RemoveAt(0);
            }
            #endregion
        }
    }
}
