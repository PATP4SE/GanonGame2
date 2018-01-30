using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {


	[SerializeField] public float speed;
	[SerializeField] public float jumpSpeed;

	private Rigidbody2D playerRig;
	private SpriteRenderer playerSprite;
	private BoxCollider2D playerCollider;
	private Animator playerAnimator;

	private bool isJumping = false;

	// Use this for initialization
	void Start () {
		playerRig = GetComponent<Rigidbody2D> ();
		playerSprite = GetComponent<SpriteRenderer> ();
		playerAnimator = GetComponent<Animator> ();
		playerCollider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.D)) 
		{
			playerSprite.flipX = false;
			playerRig.AddForce (new Vector2 (speed, 0));
			playerAnimator.SetBool ("isWalking", true);
		} 
		else if (Input.GetKey (KeyCode.A)) 
		{
			playerSprite.flipX = true;
			playerRig.AddForce (new Vector2 (-speed, 0));
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

	}

	void OnCollisionEnter2D(Collision2D coll) {
		isJumping = false;

		//if (coll.gameObject.tag == "Enemy")
		//	coll.gameObject.SendMessage("ApplyDamage", 10);

	}

}
