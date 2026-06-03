using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ItemFunctions : MonoBehaviour
{
   
    //Sangre obliga a acercarse al punto donde se deja, ajo obliga a huir de ti mientras lo llevas puesto
    public List <Items> inventory = new List<Items>();

    [Header("---Canva Inventory---")]
    public GameObject slotStake;
    public GameObject slotGarlic;
    public GameObject slotHolyWater;
    public GameObject slotBloodVial;
    public GameObject slotKey;

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
        
        if (inventory.Count > 0)
        {
            Items itemActual = null;  //vairable vacía
            #region //////Buttons//////
            //Zoe García
            //hagamos q el player tenga elección en qué item usar (numericos)
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                itemActual = inventory.Find(i => i.vampireSpeed > 0 && i.distractedTime == 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                itemActual = inventory.Find(i => i.healAmount > 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                itemActual = inventory.Find(i => i.distractedTime > 0 && i.vampireSpeed > 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                itemActual = inventory.Find(i => i.distractedTime > 0 && i.vampireSpeed == 0);
            }
            #endregion
            if (itemActual != null)
            {
                #region //////BLOOD////// 
                //Tom

                if (itemActual.distractedTime > 0 && itemActual.vampireSpeed == 0)
                {
                    Debug.Log("Dejar en el suelo. Tiempo de distracción: " + itemActual.distractedTime);

                    GameObject spawnedVial = Instantiate(bloodVial, playerTransform.position, Quaternion.identity); // Dejar frasco en la posición donde estaba el jugador

                    SphereCollider attractionArea = spawnedVial.AddComponent<SphereCollider>();  //Crear un collider para "cambiar el target" del NavMesh
                    attractionArea.isTrigger = true;
                    attractionArea.radius = 5f;
                    spawnedVial.name = "BloodAttractionPoint";

                    inventory.Remove(itemActual);
                    slotBloodVial.SetActive(false);
                }
                #endregion

                #region /////GARLIC/////
                //Tom

                else if (itemActual.healAmount > 0)
                {
                    PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>(); //para mirar el código con la salud

                    if (playerHealth != null)
                    {

                        if (!playerHealth.IsMaxHealth) //si el jugador NO tiene la vida al tope te cura
                        {
                            Debug.Log($"Consumiendo ajo. Curando {itemActual.healAmount} de vida.");

                            playerHealth.Heal(itemActual.healAmount);

                            inventory.Remove(itemActual);
                            slotGarlic.SetActive(false);
                        }
                        else //si tiene la vida al tope no te cura
                        {
                            Debug.Log("I can't use this.");
                        }
                    }
                }

                #endregion

                #region //////STAKE//////

                //Alex
                else if (itemActual.vampireSpeed > 0 && itemActual.distractedTime == 0) // para la estaca
                {
                    GameObject vampiro = GameObject.FindWithTag("Vampire"); // *REVISAR*

                    if (vampiro != null)
                    {
                        UnityEngine.AI.NavMeshAgent agente = vampiro.GetComponent<UnityEngine.AI.NavMeshAgent>();

                        if (agente != null)
                        {
                            StartCoroutine(SlowDown(agente)); // corutina para poder cambiar velocidad durante unos segundos
                            inventory.Remove(itemActual);
                            slotStake.SetActive(false);
                        }
                    }

                }
                #endregion

                #region //////HOLYWATER//////
                //Alex

                else if (itemActual.distractedTime > 0 && itemActual.vampireSpeed > 0) // para el agua bendita
                {
                    GameObject vampiro = GameObject.FindWithTag("Vampire"); // *REVISAR*

                    if (vampiro != null)
                    {
                        UnityEngine.AI.NavMeshAgent agente = vampiro.GetComponent<UnityEngine.AI.NavMeshAgent>();

                        if (agente != null)
                        {
                            StartCoroutine(Angry(agente)); // corutina para poder cambiar velocidad durante unos segundos
                            inventory.Remove(itemActual);
                            slotHolyWater.SetActive(false);
                        }
                    }
                }
                #endregion

            }
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

    public void PickItem(Items newItem) //Zoe
    {
        inventory.Add(newItem);
        Debug.Log($"[MOCHILA] Guardado con éxito. Total de ítems en mochila: {inventory.Count}");
        if (newItem.type == KindOfItem.BloodVial)
        {
            Debug.Log("-> UI: Activando imagen de la Sangre");
            if (slotBloodVial != null) slotBloodVial.SetActive(true);
        }
        else if (newItem.type == KindOfItem.Garlic)
        {
            Debug.Log("-> UI: Activando imagen del Ajo");
            if (slotGarlic != null) slotGarlic.SetActive(true);
        }
        else if (newItem.type == KindOfItem.Stake)
        {
            Debug.Log("-> UI: Activando imagen de la Estaca");
            if (slotStake != null) slotStake.SetActive(true);
        }
        else if (newItem.type == KindOfItem.HolyWater)
        {
            Debug.Log("-> UI: Activando imagen del Agua Bendita");
            if (slotHolyWater != null) slotHolyWater.SetActive(true);
        }
        else if (newItem.type == KindOfItem.Key)
        {
            Debug.Log("-> UI: Activando imagen de la Llave ¡A ESCAPAR!");
            if (slotKey != null) slotKey.SetActive(true);
        }
    }
}
