using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
	[SerializeField]
	GameObject dropObjectPrefab;

	Transform spawnPoint;

	private void Start()
	{
		spawnPoint = transform.GetChild(0);
	}

	private void OnEnable()
	{
		PreasurePlater.OnPressed += DropObject;
	}

	void DropObject()
	{
		GameObject go = Instantiate(dropObjectPrefab, spawnPoint.position, Quaternion.identity);
	}
}
