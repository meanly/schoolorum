using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public int curHealth;
    public int maxHealth;
    public int damage;
    public float moveSpeed;
    public float attackRate;
    private float attackTimer;
    public float bulletSpeed;

    private Vector3 mousePos;

    //Prefabs
    public GameObject bulletPrefab;
    public GameObject deathEffect;

    //Components
    public Rigidbody2D rig;
    public SpriteRenderer sr;

    //attacck
    public GameObject slashPrefab; // Assign in Inspector
    [SerializeField] private float attackRadius = 3f;
    [SerializeField] private float attackInterval = 1.0f;
    private float attackCooldown = 0f;
                            
    void OnDrawGizmosSelected()
    {
        // Draw the attack range in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius  );
    }
    void Update()
    {
        //attackTimer += Time.deltaTime;
        attackCooldown += Time.deltaTime;

        if (attackCooldown >= attackInterval)
        {
            attackCooldown = 0f;
            AutoSlashAttack();
        }

        Camera cam = Camera.main;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -cam.orthographicSize * cam.aspect + 0.4f, cam.orthographicSize * cam.aspect - 0.4f),
        Mathf.Clamp(transform.position.y, -cam.orthographicSize + 0.4f, cam.orthographicSize - 0.4f), 0);

        Inputs();
    }

    void AutoSlashAttack()
    {
        // Direction to cursor
        Vector3 dir = (mousePos - transform.position).normalized;

        // Flip sprite
        sr.flipX = (dir.x < 0);

        // Position the slash slightly in front of player
        Vector3 spawnPos = transform.position + dir * 0.8f;

        // Instantiate slash animation prefab
        GameObject slash = Instantiate(slashPrefab, spawnPos, Quaternion.identity);
        slash.transform.right = dir; // Rotate slash to face cursor
        Destroy(slash, 0.5f);

        // Damage enemies in radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (Collider2D col in hits)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }

    void Inputs()
    {
        //Using KEYBOARD & MOUSE as well as GAMEPAD inputs.
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);

        if (mousePos.x > transform.position.x)
            sr.flipX = false;  // Facing right
        else
            sr.flipX = true;   // Facing left

        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            if (attackTimer >= attackRate)
            {
                attackTimer = 0.0f;
                Shoot();
            }
        }

        //Movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Move(x, y);
        // 🕺 Animation
        if (x != 0 || y != 0)
            _animator.SetBool("isWalking", true);
        else
            _animator.SetBool("isWalking", false);

        //Look at Mouse / Joystick
        //Vector3 dir = transform.position - mousePos;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    //Sets the player's rigidbody to the sent x and y, multiplied by the moveSpeed.
    void Move(float x, float y)
    {
        rig.velocity = new Vector2(x, y) * moveSpeed;
    }

    //Spawns a projectile and shoots it forward.
    void Shoot()
    {
        //GameObject proj = Instantiate(bulletPrefab, transform.position + (transform.up * 0.7f), transform.rotation);
        //Projectile projScript = proj.GetComponent<Projectile>();

        //projScript.damage = damage;
        //projScript.rig.velocity = (mousePos - transform.position).normalized * bulletSpeed;
        _animator.SetTrigger("Zach_PlayerAttack"); // Play slash animation

        // Slash radius center = player position (can be offset)
    }

    //Called when an enemy projectile hits the player. Takes damage.
    public void TakeDamage(int dmg)
    {
        if (curHealth - dmg <= 0)
        {
            Die();
        }
        else
        {
            curHealth -= dmg;
            Game.game.Shake(0.1f, 0.1f, 50.0f);
            Game.game.ui.ShakeSlider(0.2f, 0.05f, 30.0f);
            Game.game.ui.StartCoroutine("HealthDown", curHealth);
            StartCoroutine(DamageFlash());
        }
    }

    //Called when damage is taken. Flashes sprite red quickly.
    IEnumerator DamageFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        sr.color = Color.white;
    }

    //Called when the health reaches 0. Kills the player and ends the game.
    void Die()
    {
        curHealth = 0;
        Game.game.ui.StartCoroutine("HealthDown", curHealth);
        GameObject pe = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(pe, 2);
        Game.game.EndGame();
        Destroy(gameObject);
    }
}
