using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
	[SerializeField] private float attackDamage;
	[SerializeField] private float attackRange;
	[SerializeField] private float rangeToAttack;
	[SerializeField] private float attackPushForce;

	[SerializeField] private float speed;
	[SerializeField] private float decelerationPercentage;
	[SerializeField] private GameObject player;

	private SpriteRenderer enemySprite;
	private Rigidbody2D rigidBody;
	private BoxCollider2D enemyCollider;


	//public Enemy()
	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D> ();
		enemyCollider = GetComponent<BoxCollider2D> ();
		Physics2D.IgnoreCollision (player.GetComponent<BoxCollider2D>(), enemyCollider);

		enemySprite = GetComponent<SpriteRenderer> ();
	}

	public void FollowPlayer()
	{
		Utils.DecelerateX(ref rigidBody, decelerationPercentage);

		float posX = (player.transform.position.x - rigidBody.transform.position.x) > 0 ? 1 : -1;

		if (posX == 1) 
		{
			enemySprite.flipX = false;
		} 
		else 
		{
			enemySprite.flipX = true;
		}

		rigidBody.AddForce(new Vector3 ( posX * speed, 0), ForceMode2D.Impulse);
	}

	public void Attack()
	{
		int dir = 0;

		if (GetComponent<SpriteRenderer> ().flipX)
			dir = -1;	
		else
			dir = 1;

		Vector2 highPos = new Vector2 (transform.position.x, transform.position.y + 0.5f);
		Vector2 midPos = transform.position;
		Vector2 lowPos = new Vector2 (transform.position.x, transform.position.y - 0.5f);

		Vector2 direction = new Vector2 (dir * attackRange, 0);


		RaycastHit2D[] highRay = Physics2D.RaycastAll (highPos, new Vector2 (dir, 0), attackRange);
		RaycastHit2D[] midRay = Physics2D.RaycastAll (midPos, new Vector2 (dir, 0), attackRange);
		RaycastHit2D[] lowRay = Physics2D.RaycastAll (lowPos, new Vector2 (dir, 0), attackRange);

		Debug.DrawRay (highPos, direction);
		Debug.DrawRay (midPos, direction);
		Debug.DrawRay (lowPos, direction);

		Collider2D colliderHit = null;

		foreach(RaycastHit2D ray in highRay)
		{
			if(ray.collider != null && ray.collider.gameObject.tag == "Player")
			{
				colliderHit = ray.collider;
				Debug.Log(ray.collider.gameObject.tag);
				PushPlayerOnAttack (ray.collider);
			}
		}

		foreach(RaycastHit2D ray in midRay)
		{
			if(ray.collider != null && ray.collider.gameObject.tag == "Player")
			{
				colliderHit = ray.collider;
				Debug.Log(ray.collider.gameObject.tag);
				PushPlayerOnAttack (ray.collider);
			}
		}

		foreach(RaycastHit2D ray in lowRay)
		{
			if(ray.collider != null && ray.collider.gameObject.tag == "Player")
			{
				colliderHit = ray.collider;
				Debug.Log(ray.collider.gameObject.tag);
				PushPlayerOnAttack (ray.collider);
			}
		}

		if(colliderHit != null)
		{
			colliderHit.gameObject.GetComponent<Player> ().LoseHealth(attackDamage);
		}
	}

	public float GetAttackRange()
	{
		return attackRange;
	}

	public float GetRangeToAttack()
	{
		return rangeToAttack;
	}

	public override void LoseHealth(float damage)
	{
		this.Health -= (damage - (this.Defense / 2));
		//Start losing health animation
		if (this.Health <= 0)
			Dies ();
	}

	public override void Dies()
	{
		GameObject.Destroy (gameObject);
	}

	#region Private Methods
	private void PushPlayerOnAttack(Collider2D playerCollider)
	{
		Vector2 direction = playerCollider.GetComponent<Rigidbody2D>().transform.position - this.transform.position;
		playerCollider.GetComponent<Rigidbody2D>().AddForceAtPosition(direction.normalized * attackPushForce, this.transform.position);
	}
	#endregion

//	public abstract void SpecialAttack();
//	public override void LoseHealth(float damage);
//	public override void Attack();
//	public override void Dies();
//	public override void Spawns();
}
