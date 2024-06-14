using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfWrongSpawn : MonoBehaviour
{
	[SerializeField]
	[Layer]
	string groundLayer;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer(groundLayer))
		{
			Destroy(gameObject);
		}
	}
}
