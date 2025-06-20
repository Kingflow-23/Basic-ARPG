using UnityEngine;
using TMPro;

public class GoalTrigger : MonoBehaviour
{
    public TextMeshProUGUI winScreen;
    public GameController gameController;

    void Start()
    {
        winScreen.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("YOU WIN!");

            if (winScreen != null)
                winScreen.gameObject.SetActive(true);

            if (gameController != null)
            {
                gameController.ShowRestartButton();  // Show the restart button (you should have this method in your GameController)
                gameController.deactivateGoal();
                gameController.deactivateGoalBox();
            }

            Time.timeScale = 0f; // pause game
        }
    }
}
