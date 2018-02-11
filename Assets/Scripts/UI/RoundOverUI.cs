using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundOverUI : MonoBehaviour 
{

	public GameObject roundOverScreen;
	public Text roundNumberText;


	public void RoundOver()
	{
		Time.timeScale = 0.0f;
		roundOverScreen.SetActive(true);
		roundNumberText.text = "Round " + SurvivalModeManager.instance.RoundNumber + " completed";
	}


	void CloseRoundOverScreen()
	{
		Time.timeScale = 1.0f;
		roundOverScreen.SetActive(false);
	}
	

	void Awake()
	{
		EnemySpawner.onAllEnemiesDeath += RoundOver;
		PerksManager.onPerkApplied += CloseRoundOverScreen;
	}


	void OnDestroy()
	{
		EnemySpawner.onAllEnemiesDeath -= RoundOver;
		PerksManager.onPerkApplied -= CloseRoundOverScreen;
	}
}
