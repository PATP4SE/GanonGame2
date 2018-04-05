using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
	public float Health;
	//public float AttackDamage;
	public float Defense;
	//public float Speed;

	public abstract void LoseHealth(float damage);
	//public abstract void Attack();
	public abstract void Dies();
	//public abstract void Spawns();
}

