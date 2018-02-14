using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

	public Transform gunHolder;
	public Transform positionToSpawnBullets;
	public float moveSpeed;
	public float hurtSlowdownTime;
	public float hurtSlowdownMultiplier;


	private AudioSource audioSource;
	private Camera mainCamera;
	private WeaponStats stats;
	private ObjectPool currentProjectilePool;
	private Vector2 maxPosition;
	private Vector2 minPosition;
	private float timeBetweenShotsCounter;
	private float hurtSlowdownCounter;
	private bool isSlownDown;


	public void SetWeapon(WeaponStats newWeapon, ObjectPool newPool)
	{
		stats = newWeapon;
		currentProjectilePool = newPool;
	}


	void Start () 
	{
		CursorController.instance.SetShootCursor();
		mainCamera = Camera.main;
		audioSource = GetComponent<AudioSource>();
		CircleCollider2D collider = GetComponent<CircleCollider2D>();
		maxPosition = ActivePlayerZone.MaxPoint - new Vector2(collider.radius, collider.radius);
		minPosition = ActivePlayerZone.MinPoint + new Vector2(collider.radius, collider.radius);
		timeBetweenShotsCounter = 0;
		PlayerHealthManager.onPlayerHurt += SlowDownOnHurt;
	}
	

	void Update () 
	{
		Move();
		if (Time.timeScale != 0.0f)
		{
			RotateGunTowardsCursor();
		}
		if (Input.GetButton("Fire1") && timeBetweenShotsCounter <= 0.0f && Time.timeScale != 0.0f)
		{
			Shoot();
			timeBetweenShotsCounter = stats.TimeBetweenShots;
		}
		else
		{
			timeBetweenShotsCounter -= Time.deltaTime;
		}

		if (isSlownDown)
		{
			if (hurtSlowdownCounter <= 0.0f)
			{
				isSlownDown = false;
				moveSpeed /= hurtSlowdownMultiplier;
			}
			else
			{
				hurtSlowdownCounter -= Time.deltaTime;
			}
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
		if (stats.Clip != null)
		{
			audioSource.PlayOneShot(stats.Clip);
		}
		projectile.SetActive(true);
	}


	void SlowDownOnHurt()
	{
		if (!isSlownDown)
		{
			isSlownDown = true;
			moveSpeed *= hurtSlowdownMultiplier;
		}
		hurtSlowdownCounter = hurtSlowdownTime;
	}


	void OnDestroy()
	{
		PlayerHealthManager.onPlayerHurt -= SlowDownOnHurt;
	}
}
