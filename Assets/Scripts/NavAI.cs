using UnityEngine;
using UnityEngine.AI;

public class NavAI : MonoBehaviour
{
    private Transform player;
    private Transform house;
    private NavMeshAgent agent;
    public float attackRange = 30f; // Oyuncuya yaklaþma mesafesi
    public float attackSpeed = 2f; // Saldýrý hýzýnýn belirlenmesi
    private bool isAttackingPlayer = false; // Oyuncuya saldýrýyor mu?


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        house = HouseScript.Instance.transform;
        player = GameController.Instance.Player; // Merkezi eriþim
        if (player != null)
        {
            agent.destination = house.position;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Eðer bot oyuncuya yaklaþýrsa
        if (distanceToPlayer < attackRange)
        {
            isAttackingPlayer = true;
            agent.SetDestination(player.position); // Oyuncuya doðru hareket et
        }
        else
        {
            isAttackingPlayer = false;
            if (agent.destination != house.position)
            {
                agent.SetDestination(house.position); // Eðer oyuncu uzaktaysa eve geri git
            }
        }

        // Eðer bot oyuncuya çok yakýnsa saldýrmaya baþla (burada saldýrý mantýðý ekleyebilirsiniz)
        if (isAttackingPlayer)
        {
            // Saldýrý iþlevini burada baþlatabilirsiniz (örneðin, saldýrý animasyonu, hasar verme vb.)
            AttackPlayer();
        }
    }
    void AttackPlayer()
    {
        // Oyuncuya saldýrma iþlemleri burada yapýlabilir
        Debug.Log("Bot player is attacking!");
    }
}
