using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    [HideInInspector] public float currentHealth;
    public GameObject deathEffectPrefab; 
    public Image healthBar; 
    public TextMeshProUGUI gameLostText;
    private Animator anim;
    public GameController gameController;

    void Start()
    {
        gameLostText.gameObject.SetActive(false);
        currentHealth = maxHealth;
        UpdateHealthUI();
        anim = GetComponent<Animator>();  // Ensure your Player has an Animator component.
    }

    // Call this method to apply damage to the player.
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Call this method to heal the player.
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    // Update the UI health bar based on the current health.
    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            float fillValue = currentHealth / maxHealth;
            healthBar.fillAmount = fillValue;
        }
        else
        {
            Debug.LogWarning("Health bar image is not assigned!");
        }
    }

    // Handle player death.
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

        if (gameLostText != null)
        {
            gameLostText.gameObject.SetActive(true);
        }

        if (gameController != null)
        {
            gameController.ShowRestartButton();
        }

        StartCoroutine(DeactivatePlayerAfterDelay(2f)); 

        // Time.timeScale = 0f; // pause game
    }

    private IEnumerator DeactivatePlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified time
        if (gameController != null)
        {
            // Deactivate the player object using GameController reference
            gameController.DeactivatePlayer();  // Use a method in GameController to deactivate the player
        }
    }
}
