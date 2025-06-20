using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 15f;
    public float lifetime = 3f;
    public float damage = 20f;
    public GameObject explosion;
    public float healthGain = 5f; 

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after 'lifetime' seconds
    }

    void Update()
    {
        if (gameObject != null)
        {
        transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the projectile hits an enemy
        if (other.CompareTag("Enemy"))
        {
            // Instantiate explosion effect at the projectile's position
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }

            // Apply damage to the enemy
            EnemyHealth targetHealth = other.GetComponent<EnemyHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
            else
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }

        // If the projectile hits a regular box
        else if (other.CompareTag("Box"))
        {
            Destroy(other.gameObject);
            HealPlayer();
            Destroy(gameObject);
        }
        // If the projectile hits a GoalBox
        else if (other.CompareTag("GoalBox"))
        {
            // Instead of destroying the goal box
            other.gameObject.SetActive(false);
            HealPlayer();
            Destroy(gameObject);
        }
    }

    // Helper function to heal the player.
    private void HealPlayer()
    {
        // Find the PlayerHealth script in the scene.
        PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healthGain);
            Debug.Log("Player healed for " + healthGain + " health.");
        }
        else
        {
            Debug.LogWarning("PlayerHealth component not found!");
        }
    }
}