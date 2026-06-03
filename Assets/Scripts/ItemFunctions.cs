using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ItemFunctions : MonoBehaviour
{
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
                itemActual = inventory.Find(i => i.distractedTime > 0 && i.vampireSpeed == 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                itemActual = inventory.Find(i => i.vampireSpeed > 0 && i.distractedTime == 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                itemActual = inventory.Find(i => i.healAmount > 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                itemActual = inventory.Find(i => i.distractedTime > 0 && i.vampireSpeed > 0);
            }
            #endregion
            if (itemActual != null)
            {
                #region /////BLOOD/////
                //Tom

                if (itemActual.distractedTime > 0 && itemActual.vampireSpeed == 0)
                {
                    Debug.Log("Dropping on the ground. Distraction time: " + itemActual.distractedTime); //Accedemos al objeto scripteable pa saber los segundos que se queda parado el señor colmillos

                    GameObject attractionPoint = new GameObject("BloodAttractionPoint");
                    attractionPoint.transform.position = playerTransform.position;

                    GameObject vampireGo = GameObject.FindWithTag("Vampire"); 
                    if (vampireGo != null)
                    {
                        UnityEngine.AI.NavMeshAgent vampireAgent = vampireGo.GetComponent<UnityEngine.AI.NavMeshAgent>(); //Por no tocar el código del Nav Mesh que me da miedo liarla

                        if (vampireAgent != null)
                        {
                            StartCoroutine(ForceVampireDestination(vampireAgent, attractionPoint.transform.position, itemActual.distractedTime));
                        }
                    }

                    inventory.Remove(itemActual); 

                    if (slotBloodVial != null)
                    {
                        slotBloodVial.SetActive(false);
                    }
                }
                #endregion

                #region /////GARLIC/////
                //Tom

                if (itemActual.healAmount > 0)
                {
                    PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        if (!playerHealth.IsMaxHealth)
                        {
                            Debug.Log($"Consumiendo ajo. Curando {itemActual.healAmount} de vida.");
                            playerHealth.Heal(itemActual.healAmount);

                            inventory.Remove(itemActual);
                            slotGarlic.SetActive(false);
                        }
                        else
                        {
                            Debug.Log("I can't use this. Vida al tope.");
                        }
                    }
                }

                #endregion

                #region /////STAKE/////
                //Alex

                if (itemActual.vampireSpeed > 0 && itemActual.distractedTime == 0) // para la estaca
                {
                    GameObject vampireGo = GameObject.FindWithTag("Vampire");

                    if (vampireGo != null)
                    {
                        UnityEngine.AI.NavMeshAgent vampireAgent = vampireGo.GetComponent<UnityEngine.AI.NavMeshAgent>();

                        if (vampireAgent != null)
                        {
                            StartCoroutine(SlowDown(vampireAgent));

                            inventory.Remove(itemActual);

                            if (slotStake != null)
                            {
                                slotStake.SetActive(false);
                            }
                        }
                    }
                }
                #endregion

                #region /////HOLYWATER/////
                //Alex

                else if (itemActual.vampireSpeed > 0 && itemActual != inventory.Find(i => i.distractedTime == 0 && i.vampireSpeed > 0))
                {
                    GameObject vampiGo = GameObject.FindWithTag("Vampire");

                    if (vampiGo != null)
                    {
                        UnityEngine.AI.NavMeshAgent agente = vampiGo.GetComponent<UnityEngine.AI.NavMeshAgent>();

                        if (agente != null)
                        {
                            StartCoroutine(Angry(agente));

                            inventory.Remove(itemActual);
                            if (slotHolyWater != null)
                            {
                                slotHolyWater.SetActive(false);
                            }
                        }
                    }
                }
                #endregion

            }
        }
    }

    private System.Collections.IEnumerator SlowDown(UnityEngine.AI.NavMeshAgent agent) //Corrutina de estaca, Alex
    {
        float originalSpeed = agent.speed;

        agent.speed = 3.5f; //Reduce la velocidad

        yield return new WaitForSeconds(2f); //Espera 2 segundos

        agent.speed = originalSpeed; //Vuelve a la de antes
    }

    private System.Collections.IEnumerator Angry(UnityEngine.AI.NavMeshAgent agent) //Corrutina de agua bendita, Alex
    {
        float originalSpeed = agent.speed;
        float elapsed = 0f;

        while (elapsed < 2f) //Ralentizar 2 seg
        {
            if (agent != null)
            {
                agent.speed = 2f; 
            }
            elapsed += Time.deltaTime;
            yield return null; //Espera al siguiente frame
        }

        elapsed = 0f;   //Reinicia el temporizador

        while (elapsed < 2f) //Acelerar 2 seg
        {
            if (agent != null)
            {
                agent.speed = originalSpeed + 2f; // Le obligamos a ir más rápido
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (agent != null)
        {
            agent.speed = originalSpeed;
        }
    }

    private System.Collections.IEnumerator ForceVampireDestination(UnityEngine.AI.NavMeshAgent agent, Vector3 targetPos, float duration) //Corrutina de sangre, Tom
    {
        float elapsed = 0f;

        while (elapsed < duration) 
        {
            if (agent != null) 
            {
                agent.SetDestination(targetPos); //Obliga al Nav Mesh a ir a por la sangre donde la sueltas
            }

            elapsed += Time.deltaTime;
            yield return null; 
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
