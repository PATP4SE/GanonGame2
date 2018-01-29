using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	private Rigidbody2D playerRig;
	[SerializeField] public float speed;

	// Use this for initialization
	void Start () {
		playerRig = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Horizontal"))
		{
			playerRig.MovePosition (new Vector2 (speed, 0));
		}


	}
}
