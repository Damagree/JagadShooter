using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 100;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void Healing(float heal)
    {
        currentHealth += heal;
    }

}
