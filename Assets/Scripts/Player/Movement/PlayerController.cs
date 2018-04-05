using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private Animator playerAnimator;

	private bool attacked;
	private Player player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {

		player.UpdatePlayer ();

		if(attacked)
		{
			playerAnimator.ResetTrigger ("Attack");
			attacked = false;
		}

		if (Input.GetKey (KeyCode.E))
		{
			attacked = true;
			playerAnimator.SetTrigger("Attack");
		}
		if (Input.GetKey (KeyCode.A))
		{
			player.MoveLeft ();
		}
		if (Input.GetKey (KeyCode.D))
		{
			player.MoveRight ();
		}
		if (Input.GetKeyDown (KeyCode.Space))
		{
			player.Jump ();
		}
		if (Input.GetKey (KeyCode.LeftShift))
		{

		}
		
	}
}
