using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

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

	//======================================================
	// Private physics-related variables
	//======================================================
	private Rigidbody2D playerRig;
	private SpriteRenderer playerSprite;
	private Animator playerAnimator;
	private BoxCollider2D playerCollider;

	//======================================================
	// Private variables
	//======================================================
	private int jumpNumber;
	private bool isJumping = false;
	private bool isFalling = false;

	//======================================================
	// Constants
	//======================================================
	//private const int NUMBER_OF_JUMPS = numberOfJumps;

	// Use this for initialization
	void Start () {
		playerRig = GetComponent<Rigidbody2D> ();
		playerSprite = GetComponent<SpriteRenderer> ();
	//	playerAnimator = GetComponent<Animator> ();
		playerCollider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Utils.DecelerateX(ref playerRig, decelerationPercentage);
		//manageMaxJumpHeight ();

		//playerAnimator.

		if (Input.GetKey (KeyCode.D)) 
		{
			direction = 1;
			playerSprite.flipX = false;
			playerRig.AddForce (new Vector2 (speed, 0), ForceMode2D.Impulse);
			//playerAnimator.SetBool ("isWalking", true);
		} 
		else if (Input.GetKey (KeyCode.A)) 
		{
			direction = 0;
			playerSprite.flipX = true;
			playerRig.AddForce (new Vector2 (-speed, 0), ForceMode2D.Impulse);
			//playerAnimator.SetBool ("isWalking", true);
		} 
		else 
		{
			//playerAnimator.SetBool ("isWalking", false);
		}

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if (jumpNumber != 0) 
			{
				isJumping = true;
				jumpNumber--;

				playerRig.velocity = new Vector2 (playerRig.velocity.x, 0);
				playerRig.AddForce(new Vector2(0, jumpSpeed));
			}
		}

		if(playerRig.velocity.y > 0)
		{
			isFalling = true;
			playerRig.velocity += Vector2.up * Physics2D.gravity.y * (fallingMultiplier - 1) * Time.deltaTime;
		}
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

	//======================================================
	// Private functions
	//======================================================
//	private void die()
//	{
//		playerAnimator.SetBool ("isDead", true);
//
//		//yield return new WaitForSeconds(playerAnimator.GetCurrentAnimatorStateInfo(0).length + playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
//
//		//Destroy(this.gameObject);
//	}
}

