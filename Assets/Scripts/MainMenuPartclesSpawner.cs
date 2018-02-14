using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPartclesSpawner : MonoBehaviour 
{

	public ObjectPool particlesPool;
	public float timeToSpawnParticles;
	public float randomPositionMultiplier;
	public float angleDispersion;
	public float speed;
	public float timeToReturnToPool;


	private Vector3 screenCenterPosition;
	private float halfScreenHeight;
	private float halfScreenWidth;
	private float timeToSpawnCounter;


	void Start()
	{
		Camera mainCamera = Camera.main;
		screenCenterPosition = mainCamera.transform.position;
		screenCenterPosition.z = 0;

		halfScreenHeight = mainCamera.orthographicSize;
		halfScreenWidth = halfScreenHeight * mainCamera.aspect;

		timeToSpawnCounter = 0.0f;
	}


	void Update()
	{
		if (timeToSpawnCounter <= 0)
		{
			GameObject particleSystem = particlesPool.GetObject();
			particleSystem.transform.position = GetRandomPosition();
			particleSystem.transform.rotation = GetRotationTowardsCenter(particleSystem.transform.position);
			particleSystem.SetActive(true);
			particleSystem.GetComponent<Rigidbody2D>().velocity = particleSystem.transform.right * speed;
			StartCoroutine(ReturnToPool(particleSystem));
			timeToSpawnCounter = timeToSpawnParticles;
		}
		else
		{
			timeToSpawnCounter -= Time.deltaTime;
		}
	}


	Vector3 GetRandomPosition()
	{
		bool vertical = (GetRandomSign() == 1);
		Vector3 position = Vector3.zero;
		if (vertical)
		{
			position.x = Random.Range(-halfScreenWidth, halfScreenWidth);
			position.y = Random.Range(halfScreenHeight, halfScreenHeight * randomPositionMultiplier);
			position.y *= GetRandomSign();
		}
		else
		{
			position.x = Random.Range(halfScreenWidth, halfScreenWidth * randomPositionMultiplier);
			position.y = Random.Range(-halfScreenHeight, halfScreenHeight);
			position.x *= GetRandomSign();
		}

		return position;
	}

	
	int GetRandomSign()
	{
		return (Random.Range(0, 2) == 0) ? 1 : -1;
	}


	Quaternion GetRotationTowardsCenter(Vector3 position)
	{
		Vector3 direction = screenCenterPosition - position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		angle += Random.Range(-angleDispersion / 2, angleDispersion / 2);
		return Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));
	}


	IEnumerator ReturnToPool(GameObject objectToReturn)
	{
		yield return new WaitForSeconds(timeToReturnToPool);
		objectToReturn.GetComponent<PoolSignature>().ReturnToPool();
	}
}
