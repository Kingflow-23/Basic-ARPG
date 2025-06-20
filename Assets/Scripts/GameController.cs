    using UnityEngine;
    using UnityEngine.AI;
    using UnityEngine.UI;
    using TMPro;

    public class GameController : MonoBehaviour
    {

        [Header("References")]
        public GameObject player;
        public ObjectiveManager objectiveManager;
        public Button restartButton;
        public TextMeshProUGUI winMessage;  
        public TextMeshProUGUI loseMessage;
        public GameObject goal;
        public GameObject goalBox;

        [Header("Box Spawning Settings")]
        public GameObject boxPrefab;   
        public int boxesToSpawn = 10;    
        public Vector2 spawnAreaMin;     
        public Vector2 spawnAreaMax;  

        void Start()
        {
            restartButton.gameObject.SetActive(false);
            SpawnBoxes();
        }

        void SpawnBoxes()
        {
            for (int i = 0; i < boxesToSpawn; i++)
            {
                float randX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
                float randZ = Random.Range(spawnAreaMin.y, spawnAreaMax.y);

                Vector3 randomPoint = new Vector3(randX, 0, randZ);

                Vector3 spawnPos = GetNavMeshPosition(randomPoint);

                if (spawnPos != Vector3.zero)  // Check if the spawn position is valid.
                {
                    Instantiate(boxPrefab, spawnPos, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("Could not find a valid NavMesh position for box spawn.");
                }
            }
        }

        // Get a valid position on the NavMesh.
        Vector3 GetNavMeshPosition(Vector3 randomPoint)
        {
            NavMeshHit hit;
            
            // Sample position on the NavMesh
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                // Return the hit position that is on the NavMesh
                return hit.position;
            }
            else
            {
                // Return Vector3.zero if no valid position was found
                return Vector3.zero;
            }
        }

        public void RestartGame()
        {
            Time.timeScale = 1f; 

            // Hide win/lose messages
            winMessage.gameObject.SetActive(false);
            loseMessage.gameObject.SetActive(false);

            // Reactivate player and move to (0, 0, 0)
            if (player != null)
            {
                player.SetActive(true);
                player.transform.position = Vector3.zero;

                // Restore full health
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.currentHealth = playerHealth.maxHealth;
                    playerHealth.Heal(100);
                }
            }

            // Destroy all enemies
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
                Debug.Log("Enemies destroyed !!");
            }

            // Destroy all boxes
            foreach (GameObject box in GameObject.FindGameObjectsWithTag("Box"))
            {
                Destroy(box);
                Debug.Log("Boxes destroyed !!");
            }

            activateGoal();
            activateGoalBox();

            // Respawn boxes
            SpawnBoxes();
            
            if (objectiveManager != null)
            {
                objectiveManager.PlaceObjective();
            }

            restartButton.gameObject.SetActive(false);

            Debug.Log("Game restarted: player repositioned, world reset.");
        }

        public void ShowRestartButton()
        {
            restartButton.gameObject.SetActive(true);
        }

        // Call this method to hide the restart button when needed
        public void HideRestartButton()
        {
            restartButton.gameObject.SetActive(false);
        }

        public void DeactivatePlayer()
        {
            if (player != null)
            {
                player.SetActive(false);  // Deactivate the player after death
            }
        }

        public void deactivateGoal()
        {
            if (goal != null)
            {
                goal.SetActive(false);  // Deactivate the goal after death
            }
        }

        public void activateGoal()
        {
            if (goal != null)
            {
                goal.SetActive(true);  // Activate the goal after death
            }
        }

        public void deactivateGoalBox()
        {
            if (goalBox != null)
            {
                goalBox.SetActive(false);  // Deactivate the goal box after death
            }
        }

        public void activateGoalBox()
        {
            if (goalBox != null)
            {
                goalBox.SetActive(true);  // activate the goal box after death
            }
        }
    }
