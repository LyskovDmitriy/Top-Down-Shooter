using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponsCollection : ScriptableObject 
{
	[SerializeField] private WeaponInfoAndType[] weapons;


	public WeaponStats GetWeaponInfo(WeaponType type)
	{
		for (int i = 0; i < weapons.Length; i++)
		{
			if (type == weapons[i].type)
			{
				return weapons[i].stats;
			}
		}

		return weapons[0].stats;
	}
}
