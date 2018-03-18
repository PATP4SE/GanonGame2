using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {


	[SerializeField] private float attackRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ApplyDamage()
	{
		Debug.Log ("Apply damage!");

		if (GetComponent<SpriteRenderer> ().flipX)
		{
			RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (-1, 0), attackRange);
			Debug.DrawRay (this.gameObject.transform.position, new Vector2 (-attackRange, 0));

			Debug.Log(hit.collider.gameObject.tag);

//			if (hit.collider.gameObject.tag == "Enemy") 
//			{
//				
//			}
		}
		else
		{
			RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (1, 0), attackRange);
			Debug.DrawRay (this.gameObject.transform.position, new Vector2 (attackRange, 0));

			Debug.Log(hit.collider.gameObject.tag);
//			if (hit.collider.gameObject.tag == "Enemy") 
//			{
//				Debug.Log("else");
//			}

		}

		

	}

//	public void Explode()
//	{
//		this.OnExplode (this, this.gameObject);
//		Collider[] colliders = Physics.OverlapSphere(this.transform.position, this.explosionRadius);
//		float explosion;
//		foreach (Collider hit in colliders)
//		{
//			if (hit && hit.GetComponent<Rigidbody>() != null)
//			{
//				CharacterHealth characterHealth = hit.GetComponent<CharacterHealth>();
//
//				if (characterHealth != null)
//				{
//					explosion = this.playerExplosionForce;
//					this.dealDamage(characterHealth);
//				}
//				else
//				{
//					explosion = this.explosionForce;
//				}
//
//				if(hit.gameObject.name.Contains("Doodad"))
//				{
//					GameObject.FindGameObjectWithTag("Level").GetComponent<BlockGenerator>().RemovePrefabFromMap(hit.gameObject);
//					this.OnObjectDestroyed(this);
//				}
//
//				Vector3 direction = hit.GetComponent<Rigidbody>().transform.position - this.transform.position;
//				hit.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * explosion, this.transform.position);
//			}
//		}
//	}
}
