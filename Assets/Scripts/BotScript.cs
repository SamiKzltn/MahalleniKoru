using System.Collections;
using UnityEngine;


public class BotScript : MonoBehaviour, IDamageable
{
    public int Health;
    public int Attack;

    public GameObject House; // Ev objesi

    private bool isAttacking = false; // Ev ile çarpýþýyorsa true olacak

    private void Start()
    {
        Health = 100;
        Attack = 5;

        House = HouseScript.Instance.gameObject;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == House && !isAttacking) // Eve temas ediyorsa ve zaten saldýrmýyorsa
        {
            isAttacking = true;
            StartCoroutine(DamageHouse());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == House) // Evden uzaklaþtýysa
        {
            isAttacking = false;
        }
    }

    private IEnumerator DamageHouse()
    {
        if (House.TryGetComponent(out HouseScript houseScript))
        {
            while (isAttacking && House != null)
            {
                houseScript.TakeDamage(Attack);
                yield return new WaitForSeconds(1f); // 1 saniye bekle
            }
        }
    }

    public bool Hit(int damage)
    {
        Health -= damage;
        return (Health <= 0);
    }
}
