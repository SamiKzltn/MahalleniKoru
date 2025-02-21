using UnityEngine;
using UnityEngine.AI;

public class NavAI : MonoBehaviour
{
    private Transform player;
    private Transform house;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        house = HouseScript.Instance.transform;
        player = GameController.Instance.Player; // Merkezi eriþim
    }

    void Update()
    {
        if (player != null)
        {
            agent.destination = house.position;
        }
    }
}
