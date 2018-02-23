using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

	//======================================================
	//Serialized variables
	//======================================================
	[SerializeField] private float dashSpeed;
	[SerializeField] private float cooldown;
	//[SerializeField] private float decelerationPercentage;

	//======================================================
	//private physics-related variables
	//======================================================
	private Rigidbody2D playerRig;
	private SpriteRenderer playerSprite;
	private Animator playerAnimator;
	private PlayerMove playerMove;

	//======================================================
	//private variables
	//======================================================
	private bool isDashing = false;
	private float nextTimeToDash;
	private Vector2 dashingVelocity;
	private bool capturedVelocity = false;

	//======================================================
	//Constants
	//======================================================
	//private const int NUMBER_OF_JUMPS = numberOfJumps;

	// Use this for initialization
	void Start () {
		playerRig = GetComponent<Rigidbody2D> ();
		playerSprite = GetComponent<SpriteRenderer> ();
		playerAnimator = GetComponent<Animator> ();
		playerMove = gameObject.GetComponent<PlayerMove>();
		nextTimeToDash = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.LeftShift) && Time.time >= nextTimeToDash) 
		{
			isDashing = true;
			nextTimeToDash = Time.time + cooldown;
			if(playerMove.direction == 1)
			{
				playerRig.AddForce (new Vector2 (dashSpeed, 0), ForceMode2D.Impulse);
			}
			else
			{
				playerRig.AddForce (new Vector2 (-dashSpeed, 0), ForceMode2D.Impulse);
			}
		}

		if(isDashing)
		{
			dashingVelocity = playerRig.velocity;
			//Debug.Log("captured volicity: " + dashingVelocity.x + ", " + dashingVelocity.y);
			capturedVelocity = true;
		}

		if (isDashing && ((playerMove.direction == 1 && playerRig.velocity.x <= 9) || (playerMove.direction == 0 && playerRig.velocity.x >= -9)))
		{
			isDashing = false;
			Debug.Log ("stop dashing");
			Debug.Log (playerRig.velocity.x);
		}

	}

//	void OnCollisionEnter2D(Collision2D coll) 
//	{
//		if (coll.gameObject.tag == "Enemy") 
//		{
//			
//		}
//	}

}
