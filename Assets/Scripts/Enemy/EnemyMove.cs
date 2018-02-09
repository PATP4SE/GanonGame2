using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	[SerializeField] private float speed;
	[SerializeField] private float decelerationPercentage;
	[SerializeField] private GameObject player;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Utils.DecelerateX(ref rigidBody, decelerationPercentage);

		float posX = (player.transform.position.x - rigidBody.transform.position.x) > 0 ? 1 : -1;

		rigidBody.AddForce(new Vector3 ( posX * speed, 0), ForceMode2D.Impulse);
	}
}
