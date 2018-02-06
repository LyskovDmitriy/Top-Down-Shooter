using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerup : BasePowerup 
{

	public WeaponType type;


	protected override void UsePowerUp()
	{
		PowerupsApplier.instance.ChangeWeapon(type);
	}

}
