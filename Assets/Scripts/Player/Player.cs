﻿#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character 
{
	//======================================================
	// Public variables
	//======================================================
	public int direction {get; set;}

	//======================================================
	// Serialized variables
	//======================================================
	[SerializeField] private float jumpSpeed;
	[SerializeField] private float fallingMultiplier;
	[SerializeField] private int numberOfJumps;

	//======================================================
	// Private variables
	//======================================================
	private int jumpNumber;
	private bool isJumping = false;
	private bool isFalling = false;

	void Start () {
	}

	public void UpdatePlayer()
	{
		Utils.DecelerateX(ref characterRigidbody, decelerationPercentage);

		if(characterRigidbody.velocity.y > 0)
		{
			isFalling = true;
			characterRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallingMultiplier - 1) * Time.deltaTime;
		}
	}

	public void MoveLeft()
	{
		direction = 0;
		characterSprite.flipX = true;
		characterRigidbody.AddForce (new Vector2 (-speed, 0), ForceMode2D.Impulse);
	}

	public void MoveRight()
	{
		direction = 1;
		characterSprite.flipX = false;
		characterRigidbody.AddForce (new Vector2 (speed, 0), ForceMode2D.Impulse);
	}

	public void Jump()
	{
		if (jumpNumber != 0) 
		{
			isJumping = true;
			jumpNumber--;

			characterRigidbody.velocity = new Vector2 (characterRigidbody.velocity.x, 0);
			characterRigidbody.AddForce(new Vector2(0, jumpSpeed));
		}
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

		#if DEBUG
		Debug.DrawRay (highPos, direction);
		Debug.DrawRay (midPos, direction);
		Debug.DrawRay (lowPos, direction);
		#endif

		List<Enemy> enemyList = new List<Enemy> ();

		//Raycasts
		foreach(RaycastHit2D ray in highRay)
		{
			if(ray.collider != null && ray.collider.gameObject.tag == "Enemy")
			{
				Debug.Log(ray.collider.gameObject.tag);
				Enemy enemy = ray.collider.gameObject.GetComponent<Enemy> ();

				PushEnemyOnAttack (ray.collider);

				if (!enemyList.Contains (enemy))
					enemyList.Add (enemy);
			}
		}

		foreach(RaycastHit2D ray in midRay)
		{
			if(ray.collider != null && ray.collider.gameObject.tag == "Enemy")
			{
				Debug.Log(ray.collider.gameObject.tag);
				Enemy enemy = ray.collider.gameObject.GetComponent<Enemy> ();

				PushEnemyOnAttack (ray.collider);

				if (!enemyList.Contains (enemy))
					enemyList.Add (enemy);
			}
		}

		foreach(RaycastHit2D ray in lowRay)
		{
			if(ray.collider != null && ray.collider.gameObject.tag == "Enemy")
			{
				Debug.Log(ray.collider.gameObject.tag);
				Enemy enemy = ray.collider.gameObject.GetComponent<Enemy> ();

				PushEnemyOnAttack (ray.collider);

				if (!enemyList.Contains (enemy))
					enemyList.Add (enemy);
			}
		}

		//Enemy lose health
		foreach (Enemy enemy in enemyList) 
		{
			enemy.LoseHealth (attackDamage);
		}

	}

	public override void Dies()
	{
		GameObject.Destroy (gameObject);
	}


	void OnCollisionEnter2D(Collision2D coll) 
	{
		jumpNumber = numberOfJumps;
		isJumping = false;
		isFalling = false;
		if (coll.gameObject.tag == "Enemy") 
		{
			Physics2D.IgnoreCollision (characterCollider, coll.collider);
		}
	}

	void OnCollisionExit2D(Collision2D coll) 
	{
		if (!isJumping && !characterCollider.IsTouchingLayers())
			jumpNumber--;
	}

	#region Private Methods
	private void PushEnemyOnAttack(Collider2D enemyCollider)
	{
		Vector2 direction = enemyCollider.GetComponent<Rigidbody2D>().transform.position - this.transform.position;
		enemyCollider.GetComponent<Rigidbody2D>().AddForceAtPosition(direction.normalized * attackPushForce, this.transform.position);
	}

	#endregion

}

