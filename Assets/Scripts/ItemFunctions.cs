using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class ItemFunctions : MonoBehaviour
{
   
    //Sangre obliga a acercarse al punto donde se deja, ajo obliga a huir de ti mientras lo llevas puesto
    public List <Items> inventory = new List<Items>();
    
    public GameObject bloodVial;
    public GameObject HolyWater;
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

            #region //////STAKE//////

            else if (itemActual.vampireSpeed > 0) // para la estaca
            {
                GameObject vampiro = GameObject.FindWithTag("Vampire"); // *REVISAR*

                if (vampiro != null)
                {
                    UnityEngine.AI.NavMeshAgent agente = vampiro.GetComponent<UnityEngine.AI.NavMeshAgent>();

                    if (agente != null)
                    {
                        StartCoroutine(SlowDown(agente)); // corutina para poder cambiar velocidad durante unos segundos
                    }
                }

            }
            #endregion

            #region //////HOLYWATER//////
            else if (itemActual.distractedTime > 0 && itemActual.vampireSpeed > 0) // para el agua bendita
            {
                GameObject vampiro = GameObject.FindWithTag("Vampire"); // *REVISAR*

                if (vampiro != null)
                {
                    UnityEngine.AI.NavMeshAgent agente = vampiro.GetComponent<UnityEngine.AI.NavMeshAgent>();

                    if (agente != null)
                    {
                        StartCoroutine(Angry(agente)); // corutina para poder cambiar velocidad durante unos segundos
                    }
                }
            }
            #endregion
        }
    }

    IEnumerator SlowDown(UnityEngine.AI.NavMeshAgent agente)
    {
        agente.speed = 3.5f;
        yield return new WaitForSeconds(2f); // espera 2 segundos y recupera su velocidad inicial
        if (agente != null)
        {
            agente.speed = 7f;
        }
    }

    IEnumerator Angry(UnityEngine.AI.NavMeshAgent agente)
    {
        agente.speed = 0;
        yield return new WaitForSeconds(2f); // espera 2 segundos y acelera
        if (agente != null)
        {
            agente.speed = 10.5f;
            yield return new WaitForSeconds(2f); // vuelve a esperar 2 segundos y recupera su velocidad inicial
            if (agente != null)
            {
                agente.speed = 7f;
            }
        }
    }
}
