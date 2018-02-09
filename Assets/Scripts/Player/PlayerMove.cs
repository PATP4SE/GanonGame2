using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {


	[SerializeField] private float speed;
	[SerializeField] private float jumpSpeed;
	[SerializeField] private float decelerationPercentage;

	private Rigidbody2D playerRig;
	private SpriteRenderer playerSprite;
	//private BoxCollider2D playerCollider;
	private Animator playerAnimator;

	private bool isJumping = false;

	// Use this for initialization
	void Start () {
		playerRig = GetComponent<Rigidbody2D> ();
		playerSprite = GetComponent<SpriteRenderer> ();
		playerAnimator = GetComponent<Animator> ();
		//playerCollider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Utils.DecelerateX(ref playerRig, decelerationPercentage);

		if (Input.GetKey (KeyCode.D)) 
		{
			playerSprite.flipX = false;
			playerRig.AddForce (new Vector2 (speed, 0), ForceMode2D.Impulse);
			playerAnimator.SetBool ("isWalking", true);
		} 
		else if (Input.GetKey (KeyCode.A)) 
		{
			playerSprite.flipX = true;
			playerRig.AddForce (new Vector2 (-speed, 0), ForceMode2D.Impulse);
			playerAnimator.SetBool ("isWalking", true);
		} 
		else 
		{
			playerAnimator.SetBool ("isWalking", false);
		}

		if (Input.GetKey (KeyCode.Space)) 
		{
			if (!isJumping) 
			{
				isJumping = true;
				playerRig.AddForce(new Vector2(0, jumpSpeed));
			}
		}


		if (playerAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Die")) 
		{
		}


	}

	void OnCollisionEnter2D(Collision2D coll) {
		isJumping = false;

		if (coll.gameObject.tag == "Enemy") 
		{
			die ();
		}

		//if (coll.gameObject.tag == "Enemy")
		//	coll.gameObject.SendMessage("ApplyDamage", 10);

	}


	private void die()
	{
		playerAnimator.SetBool ("isDead", true);

		//yield return new WaitForSeconds(playerAnimator.GetCurrentAnimatorStateInfo(0).length + playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

		Destroy(this.gameObject);
	}
}

