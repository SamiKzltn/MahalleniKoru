using UnityEngine;
using UnityEngine.AI;

public class NavAI : MonoBehaviour
{
    private Transform player;
    private Transform house;
    private NavMeshAgent agent;
    public float attackRange = 30f; // Oyuncuya yakla�ma mesafesi
    public float attackSpeed = 2f; // Sald�r� h�z�n�n belirlenmesi
    private bool isAttackingPlayer = false; // Oyuncuya sald�r�yor mu?


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        house = HouseScript.Instance.transform;
        player = GameController.Instance.Player; // Merkezi eri�im
        if (player != null)
        {
            agent.destination = house.position;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // E�er bot oyuncuya yakla��rsa
        if (distanceToPlayer < attackRange)
        {
            isAttackingPlayer = true;
            agent.SetDestination(player.position); // Oyuncuya do�ru hareket et
        }
        else
        {
            isAttackingPlayer = false;
            if (agent.destination != house.position)
            {
                agent.SetDestination(house.position); // E�er oyuncu uzaktaysa eve geri git
            }
        }

        // E�er bot oyuncuya �ok yak�nsa sald�rmaya ba�la (burada sald�r� mant��� ekleyebilirsiniz)
        if (isAttackingPlayer)
        {
            // Sald�r� i�levini burada ba�latabilirsiniz (�rne�in, sald�r� animasyonu, hasar verme vb.)
            AttackPlayer();
        }
    }
    void AttackPlayer()
    {
        // Oyuncuya sald�rma i�lemleri burada yap�labilir
        Debug.Log("Bot player is attacking!");
    }
}
