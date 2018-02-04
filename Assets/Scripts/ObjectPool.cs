using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{

	public GameObject objectPrefab;


	private Queue<GameObject> objects;


	public GameObject GetObject()
	{
		if (objects.Count > 0)
		{
			return objects.Dequeue();
		}
		else
		{
			GameObject newObject = Instantiate(objectPrefab, transform);
			newObject.SetActive(false);
			PoolSignature signature = newObject.AddComponent<PoolSignature>();
			signature.originalPool = this;
			return newObject;
		}
	}


	public void ReturnObject(GameObject returnedObject)
	{
		//Debug.Log("Return");
		returnedObject.SetActive(false);
		objects.Enqueue(returnedObject);
	}


	void Start()
	{
		objects = new Queue<GameObject>();
	}
}
