using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public float damage = 5f;
    public float healthGain = 5f; 
    public GameController gameController;
    
    private void OnTriggerEnter(Collider other)
    {

        if (other == null) return; 

        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                Debug.Log("Hit enemy");
                enemy.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("Enemy does not have an EnemyHealth component: " + other.gameObject.name);
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Box"))
        {
            // For regular boxes, destroy the object
            Destroy(other.gameObject);
            HealPlayer(healthGain, "Box destroyed! Player healed for " + healthGain + " health.");
        }
        else if (other.CompareTag("GoalBox"))
        {
            // For GoalBox, deactivate it instead of destroying
            other.gameObject.SetActive(false);
            HealPlayer(healthGain, "GoalBox deactivated! Player healed for " + healthGain + " health.");
        }
    }
    
    // Helper method to heal the player and log a message.
    private void HealPlayer(float amount, string message)
    {
        PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();  
        if (playerHealth != null)
        {
            playerHealth.Heal(amount);
            Debug.Log(message);
        }
        else
        {
            Debug.LogWarning("PlayerHealth component not found!");
        }
    }
}