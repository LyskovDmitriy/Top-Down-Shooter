using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{

	public Transform target;
	public float speed;


	private Vector2 maxPosition;
	private Vector2 minPosition;


	void Start () 
	{
		Camera camera = GetComponent<Camera>();
		float halfCameraHeight = camera.orthographicSize;
		float halfCameraWidth = halfCameraHeight * camera.aspect;
		maxPosition = ActivePlayerZone.MaxPoint - new Vector2(halfCameraWidth, halfCameraHeight);
		minPosition = ActivePlayerZone.MinPoint + new Vector2(halfCameraWidth, halfCameraHeight);
	}


	void LateUpdate()
	{
		Vector3 newPosition = transform.position;
		Vector2 targetPosition = target.position;
		newPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
		newPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
		transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
	}

}
