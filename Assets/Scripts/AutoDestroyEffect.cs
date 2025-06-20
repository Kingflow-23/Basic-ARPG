using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour
{
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            // Destroy after the effect finishes playing
            Destroy(gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            // Just in case, destroy after 5 seconds fallback
            Destroy(gameObject, 5f);
        }
    }  
}
 