using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject attackPrefab;

    public void ActivateSpell()
    {
        attackPrefab.SetActive(true); 
    }

    public void DeactivateSpell()
    {
        attackPrefab.SetActive(false);
    }
}
