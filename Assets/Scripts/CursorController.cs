using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour 
{

	public static CursorController instance;


	public Texture2D shootCursor;
	public Vector2 shootCursotHotspot;

	public Texture2D defaultCursor;
	public Vector2 defaultCursorHotspot;


	public void SetDefaultCursor()
	{
		Cursor.SetCursor(defaultCursor, defaultCursorHotspot, CursorMode.Auto);
	}


	public void SetShootCursor()
	{
		Cursor.SetCursor(shootCursor, shootCursotHotspot, CursorMode.Auto);
	}


	void Awake () 
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
}
