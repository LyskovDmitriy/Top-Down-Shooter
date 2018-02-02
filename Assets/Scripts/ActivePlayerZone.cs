using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ActivePlayerZone : MonoBehaviour 
{

	public static Vector2 MaxPoint { get; private set; }
	public static Vector2 MinPoint { get; private set; }


	private static ActivePlayerZone activeZone;


	private BoxCollider2D boxCollider;


	void Awake () 
	{
		if (activeZone == null)
		{
			activeZone = this;
		}
		else if (activeZone != this)
		{
			Destroy(gameObject);
		}

		boxCollider = GetComponent<BoxCollider2D>();
		MaxPoint = boxCollider.bounds.max;
		MinPoint = boxCollider.bounds.min;
	}
}
