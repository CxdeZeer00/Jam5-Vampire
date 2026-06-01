using UnityEngine;
using UnityEngine.AI;

public class navmeshTarget_Vampire : MonoBehaviour  //Paula Pinilla
{
    public GameObject navMeshTarget;
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        navMeshTarget = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(navMeshTarget.transform.position);
        if (navMeshTarget != null)
        {
            agent.SetDestination(navMeshTarget.transform.position);
        }
    }
}
