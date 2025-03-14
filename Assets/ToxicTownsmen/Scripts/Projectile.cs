using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
	public bool playerOwned; //Does the player own this projectile?
	public bool hittable;
	public bool followPlayer;
	public float followSpeed;
	public int damage;
	public Rigidbody2D rig;
	private GameObject player;
	public GameObject projectileDeathParticle;

	//Side Stepping
	private bool stepLeft;
	private float stepTimer;

	void Start ()
	{
		if(!followPlayer)
			Destroy(gameObject, 3.0f);

		if(GetComponent<TrailRenderer>())
			GetComponent<TrailRenderer>().sortingLayerName = "Enemy";
	}

	void Update ()
	{
		//If the projectile follows the player, follow them.
		if(followPlayer)
		{
			if(!player)
			{
				if(Game.game.player)
				{
					player = Game.game.player.gameObject;
				}
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed * Time.deltaTime);
			}
		}

		//Side Stepping
		if(name.Contains("Orb"))
		{
			//Used for the king's green orbs. Bounces side to side.
			if(stepTimer <= 0.0f)
			{
				stepTimer = Random.Range(0.5f, 1.0f);
				stepLeft = !stepLeft;
			}

			stepTimer -= Time.deltaTime;

			transform.position += (stepLeft?transform.right:-transform.right) * 2.0f * Time.deltaTime;
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		//If it's the player's projectile then just check for the enemy tag.
		if(playerOwned)
		{
			if(col.gameObject.tag == "Enemy")
			{
				if(!col.gameObject.name.Contains("King"))
				{
					col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
				}
				else
				{
					col.gameObject.GetComponent<King>().TakeDamage(damage);
				}

				Destroy(gameObject);
			}
			//If the player projectile hits another and it's "hittable" then destroy both.
			else if(col.gameObject.tag == "Projectile")
			{
				if(col.gameObject.GetComponent<Projectile>().hittable)
				{
					Destroy(col.gameObject);
					GameObject pe = Instantiate(projectileDeathParticle, col.gameObject.transform.position, Quaternion.identity);
					Destroy(pe, 1);
					Destroy(gameObject);
				}
			}
		}
		//Otherwise check for the player tag.
		else
		{
			if(col.gameObject.tag == "Player")
			{
				Game.game.player.TakeDamage(damage);
				Destroy(gameObject);
			}
		}
	}
}
