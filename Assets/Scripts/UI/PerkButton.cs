using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour 
{
	
	public Perk perkToAdd;
	public Button buttonToDisable;


	public void AddPerk()
	{
		PerksManager.instance.ApplyPerk(perkToAdd);

		if (buttonToDisable != null)
		{
			buttonToDisable.interactable = false;
		}
	}
}
