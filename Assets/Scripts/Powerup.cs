using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : BasePowerup 
{

	public PowerupType type;


	protected override void UsePowerUp()
	{
		PowerupsApplier.instance.Apply(type);
	}
}
