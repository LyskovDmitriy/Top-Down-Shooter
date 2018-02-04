using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerup : MonoBehaviour 
{

	public new SpriteRenderer renderer;
	public PowerupsBehaviorInfo behaviorInfo;


	private Color startingColor;


	protected virtual void UsePowerUp() {}


	void Start()
	{
		startingColor = renderer.color;
		Destroy(gameObject, behaviorInfo.TimeToDisappear);	
		StartCoroutine(Blink());
	}
	

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.CompareTag("Player"))
		{
			UsePowerUp();
			Destroy(gameObject);
		}
	}


	IEnumerator Blink()
	{
		yield return new WaitForSeconds(behaviorInfo.TimeToDisappear - behaviorInfo.BlinkingDuration);

		bool moveTowardsStartingColor = false;
		float lerpModifier = 0.0f;

		while (true)
		{
			if (moveTowardsStartingColor)
			{
				lerpModifier += Time.deltaTime * behaviorInfo.BlinkTime * 2;
				renderer.color = Color.Lerp(startingColor, Color.red, lerpModifier);
				if (lerpModifier >= 1)
				{
					moveTowardsStartingColor = false;
				}
			}
			else
			{
				lerpModifier -= Time.deltaTime * behaviorInfo.BlinkTime * 2;
				renderer.color = Color.Lerp(startingColor, Color.red, lerpModifier);
				if (lerpModifier <= 0)
				{
					moveTowardsStartingColor = true;
				}
			}

			yield return null;
		}
	}
}
