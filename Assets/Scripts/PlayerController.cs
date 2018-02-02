using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

	public Transform gunHolder;
	public Transform positionToSpawnBullets;
	public WeaponStats stats;
	public Texture2D cursor;
	public float moveSpeed;


	private Camera mainCamera;
	[SerializeField] private ObjectPool currentProjectilePool;
	private Vector2 maxPosition;
	private Vector2 minPosition;
	private float timeBetweenShotsCounter;



	void Start () 
	{
		Cursor.SetCursor(cursor, new Vector2(16, 16), CursorMode.Auto);
		mainCamera = Camera.main;
		CircleCollider2D collider = GetComponent<CircleCollider2D>();
		maxPosition = ActivePlayerZone.MaxPoint - new Vector2(collider.radius, collider.radius);
		minPosition = ActivePlayerZone.MinPoint + new Vector2(collider.radius, collider.radius);
		timeBetweenShotsCounter = 0;
	}
	

	void Update () 
	{
		Move();
		RotateGunTowardsCursor();
		if (Input.GetButton("Fire1") && timeBetweenShotsCounter <= 0.0f)
		{
			Shoot();
			timeBetweenShotsCounter = stats.timeBetweenShots;
		}
		else
		{
			timeBetweenShotsCounter -= Time.deltaTime;
		}
	}


	void Move()
	{
		Vector2 newPosition = (new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))).normalized 
			* moveSpeed * Time.deltaTime + transform.position;
		newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
		newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);
		transform.position = newPosition;
	}


	void RotateGunTowardsCursor()
	{
		Vector2 direction = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		gunHolder.eulerAngles = new Vector3(0.0f, 0.0f, angle);
	}


	void Shoot()
	{
		GameObject projectile = currentProjectilePool.GetObject();
		projectile.transform.rotation = gunHolder.rotation;
		projectile.transform.position = positionToSpawnBullets.position;
		projectile.SetActive(true);
	}
}
