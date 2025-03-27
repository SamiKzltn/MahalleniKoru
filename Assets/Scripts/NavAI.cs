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
    private Vector3 _endNavigationTarget;

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
            if (_endNavigationTarget != player.position)
            {
                _endNavigationTarget = player.position;
                agent.SetDestination(_endNavigationTarget); // Oyuncuya doðru hareket et

            }
        }
        else
        {
            isAttackingPlayer = false;
            if (agent.destination != house.position)
            {
                if (_endNavigationTarget != house.position)
                {
                    _endNavigationTarget = house.position;
                    agent.SetDestination(_endNavigationTarget);
                }// Eðer oyuncu uzaktaysa eve geri git
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
