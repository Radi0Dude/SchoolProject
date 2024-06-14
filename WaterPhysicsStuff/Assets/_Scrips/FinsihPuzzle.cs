using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinsihPuzzle : MonoBehaviour
{
	[Scene]
	[SerializeField]
	string nextSceneName;

	[Tag]
	[SerializeField]
	string playerTag;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == playerTag)
		{
			SceneManager.LoadScene(nextSceneName);
		}
	}
}
