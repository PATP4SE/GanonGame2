using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

	[SerializeField] private Animator playerAnimator;

	private bool attacked;

	// Use this for initialization
	void Start () 
	{
		attacked = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
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
		else if (Input.GetKey (KeyCode.A))
		{
			
		}
		else if (Input.GetKey (KeyCode.D))
		{

		}
		else if (Input.GetKey (KeyCode.Space))
		{

		}
		else if (Input.GetKey (KeyCode.LeftShift))
		{

		}

	}
}
