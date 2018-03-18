using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Character 
{
	public override void Attack()
	{
		//Do whatever
	}

	public override void LoseHealth(float damage)
	{
		this.Health -= (damage - (this.Defense / 2));
		//Start losing health animation
	}

	public override void Dies()
	{
		
	}

	public override void Spawns()
	{
		
	}
}

