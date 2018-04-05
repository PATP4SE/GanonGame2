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
	[SerializeField] private float speed;
	[SerializeField] private float decelerationPercentage;

	[SerializeField] private float jumpSpeed;
	[SerializeField] private float fallingMultiplier;
	[SerializeField] private int numberOfJumps;

	[SerializeField] private float attackRange;
	[SerializeField] private float attackPushForce;

	//======================================================
	// Private physics-related variables
	//======================================================
	private Rigidbody2D playerRig;
	private SpriteRenderer playerSprite;
	private BoxCollider2D playerCollider;

	//======================================================
	// Private variables
	//======================================================
	private int jumpNumber;
	private bool isJumping = false;
	private bool isFalling = false;

	void Start () {
		playerRig = GetComponent<Rigidbody2D> ();
		playerSprite = GetComponent<SpriteRenderer> ();
		playerCollider = GetComponent<BoxCollider2D> ();
	}

	public void UpdatePlayer()
	{
		Utils.DecelerateX(ref playerRig, decelerationPercentage);

		if(playerRig.velocity.y > 0)
		{
			isFalling = true;
			playerRig.velocity += Vector2.up * Physics2D.gravity.y * (fallingMultiplier - 1) * Time.deltaTime;
		}
	}

	public void MoveLeft()
	{
		direction = 0;
		playerSprite.flipX = true;
		playerRig.AddForce (new Vector2 (-speed, 0), ForceMode2D.Impulse);
	}

	public void MoveRight()
	{
		direction = 1;
		playerSprite.flipX = false;
		playerRig.AddForce (new Vector2 (speed, 0), ForceMode2D.Impulse);
	}

	public void Jump()
	{
		if (jumpNumber != 0) 
		{
			isJumping = true;
			jumpNumber--;

			playerRig.velocity = new Vector2 (playerRig.velocity.x, 0);
			playerRig.AddForce(new Vector2(0, jumpSpeed));
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

		Debug.DrawRay (highPos, direction);
		Debug.DrawRay (midPos, direction);
		Debug.DrawRay (lowPos, direction);

		List<Enemy> enemyList = new List<Enemy> ();

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


	void OnCollisionEnter2D(Collision2D coll) 
	{
		jumpNumber = numberOfJumps;
		isJumping = false;
		isFalling = false;
		if (coll.gameObject.tag == "Enemy") 
		{
			Physics2D.IgnoreCollision (playerCollider, coll.collider);
		}
	}

	void OnCollisionExit2D(Collision2D coll) 
	{
		if (!isJumping && !playerCollider.IsTouchingLayers())
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

