using UnityEngine;

public class EXPOrb : MonoBehaviour
{
    public int expAmount = 20;
    public float lifetime = 10f;
    public float moveSpeed = 10f; // Faster speed toward player

    public float attractionRadius = 1.5f; // Radius for player attraction
    private Transform player;
    private bool isAttracting = false;

    void Start()
    {
        Destroy(gameObject, lifetime);
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // Slight floating effect
        transform.Translate(Vector2.up * Mathf.Sin(Time.time * 3f) * 0.001f);

        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attractionRadius)
            isAttracting = true;

        if (isAttracting)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
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

    // Draw the attraction radius in the editor for easier tweaking
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }
}
