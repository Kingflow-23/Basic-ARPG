using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    [HideInInspector] public float currentHealth;
    public GameObject deathEffectPrefab; 
    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();  // Ensure your enemy has an Animator.
    }

    // Apply damage to the enemy.
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Handle enemy death.
    void Die()
    {
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        }

        if (anim != null)
        {
            anim.SetTrigger("Death");
        }
        
        // Optionally, add loot dropping or score adding logic here.

        // Destroy the enemy after a short delay to allow the death animation to play.
        Destroy(gameObject, 2f);
    }
}
