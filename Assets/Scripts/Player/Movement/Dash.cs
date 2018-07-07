#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

	//======================================================
	//Serialized variables
	//======================================================
	[SerializeField] private float dashSpeed;
	[SerializeField] private float dashDistance;
	[SerializeField] private float cooldown;
	[SerializeField] private float lagTimeBeforeDash;
	//[SerializeField] private float decelerationPercentage;

	//======================================================
	//private physics-related variables
	//======================================================
	private Rigidbody2D playerRig;
	private SpriteRenderer playerSprite;
	private Animator playerAnimator;
	private Player playerMove;

	//======================================================
	//private variables
	//======================================================
	private bool isDashing = false;
	private float nextTimeToDash;
	private Vector2 dashingVelocity;
	private bool capturedVelocity = false;
	private bool dashPressed = false;

	private float nextTimeToDashBeforeLag;
	private float initialPositionX;


	//======================================================
	//Constants
	//======================================================
	private const int AFTER_DASHING_VELOCITY_X = 9;


	// Use this for initialization
	void Start () {
		playerRig = GetComponent<Rigidbody2D> ();
		playerSprite = GetComponent<SpriteRenderer> ();
		playerAnimator = GetComponent<Animator> ();
		playerMove = gameObject.GetComponent<Player>();
		nextTimeToDash = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.LeftShift) && Time.time >= nextTimeToDash) 
		{
			lagAndDash();
		}

		if(Time.time >= nextTimeToDashBeforeLag && dashPressed && !isDashing)
		{
			isDashing = true;
			nextTimeToDash = Time.time + cooldown;
			initialPositionX = transform.position.x;

			dashPressed = false;
		}

		if(isDashing)
		{
			playerRig.AddForce (new Vector2 (playerMove.direction * dashSpeed, 0), ForceMode2D.Impulse);

			if (transform.position.x >= initialPositionX + dashDistance || transform.position.x <= initialPositionX - dashDistance)
				stopDashing ();

			if ((playerMove.direction == 1 && playerRig.velocity.x <= AFTER_DASHING_VELOCITY_X) || (playerMove.direction == -1 && playerRig.velocity.x >= -AFTER_DASHING_VELOCITY_X))
			{
				isDashing = false;

				#if DEBUG
				Debug.Log ("stop dashing");
				Debug.Log (playerRig.velocity.x);
				#endif
			}
		}
	}

	private void stopDashing()
	{
		isDashing = false;
		playerRig.velocity = new Vector2 (9 * playerMove.direction, playerRig.velocity.y);
	}

	private void lagAndDash()
	{
		nextTimeToDashBeforeLag = Time.time + lagTimeBeforeDash;
		dashPressed = true;
	}
}
