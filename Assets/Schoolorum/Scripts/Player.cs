using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    void Update()
    {
        attackTimer += Time.deltaTime;

        Camera cam = Camera.main;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -cam.orthographicSize * cam.aspect + 0.4f, cam.orthographicSize * cam.aspect - 0.4f),
        Mathf.Clamp(transform.position.y, -cam.orthographicSize + 0.4f, cam.orthographicSize - 0.4f), 0);

        Inputs();
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
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

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
        GameObject proj = Instantiate(bulletPrefab, transform.position + (transform.up * 0.7f), transform.rotation);
        Projectile projScript = proj.GetComponent<Projectile>();

        projScript.damage = damage;
        projScript.rig.velocity = (mousePos - transform.position).normalized * bulletSpeed;
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
