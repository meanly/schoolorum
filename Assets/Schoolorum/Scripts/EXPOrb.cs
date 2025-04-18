using UnityEngine;

public class EXPOrb : MonoBehaviour
{
    public int expAmount = 20;
    public float lifetime = 10f;
    public float moveSpeed = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime); // auto-destroy if left on the ground
    }

    void Update()
    {
        // Optional: Slowly float or pulse to make it look more alive
        transform.Translate(Vector2.up * Mathf.Sin(Time.time * 3f) * 0.001f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EXPSystem expSystem = FindObjectOfType<EXPSystem>();
            if (expSystem != null)
            {
                expSystem.GainEXP(expAmount);
            }

            Destroy(gameObject);
        }
    }
}
