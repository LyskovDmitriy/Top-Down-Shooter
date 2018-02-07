using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour 
{

	public static PoolsManager instance;


	public GameObject poolPrefab;
	[Tooltip("A projectile pool should be created for each weapon type used in the scene")]
	public ProjectilePoolInfo[] poolInfos;


	public ObjectPool GetPool(WeaponType type)
	{	
		for (int i = 0; i < poolInfos.Length; i++) //Delete all objects in pools in order not to waste memory
		{
			if (poolInfos[i].projectilePool != null)
			{
				poolInfos[i].projectilePool.ClearPool();
			}
		}

		for (int i = 0; i < poolInfos.Length; i++) //Find corresponding poolInfo
		{
			if (type == poolInfos[i].weaponType)
			{
				if (poolInfos[i].projectilePool == null)
				{
					poolInfos[i].projectilePool = Instantiate(poolPrefab, transform).GetComponent<ObjectPool>();
					poolInfos[i].projectilePool.objectPrefab = poolInfos[i].projectilePrefab;
				}
				return poolInfos[i].projectilePool;
			}
		}

		return null;
	}


	void Awake()
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
