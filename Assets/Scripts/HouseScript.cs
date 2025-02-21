using UnityEngine;

public class HouseScript : MonoBehaviour
{
    private UIManager uiManager => UIManager.Instance;

    public int Health = 500;

    public static HouseScript Instance;

    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log(Health);
        uiManager.HouseHealthUpdate(Health);
        if (Health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("You Lost!");
        }
    }
}
