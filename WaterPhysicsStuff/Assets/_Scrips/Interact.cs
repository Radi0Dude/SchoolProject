using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField]
    [Layer]
    string interactLayer;

	GameObject currentInteractable;
	GetModelVertex floatingScript;


	bool hasChanged;

	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer(interactLayer))
		{
			currentInteractable = other.gameObject;
			if(floatingScript == null)
			{
				floatingScript = currentInteractable.GetComponent<GetModelVertex>();
			}
			

		}
	}

	private void Update()
	{
		if(floatingScript != null)
			{
				Debug.Log("axhooo");
				if (Input.GetKeyDown(KeyCode.E) && !hasChanged)
				{
					floatingScript.objectDensity *= 3;
					hasChanged = true;
					Debug.Log("Hello");
				}
				else if(Input.GetKeyDown(KeyCode.E) && hasChanged)
				{
					floatingScript.objectDensity /= 3;
					hasChanged = false;
					Debug.Log("Not hello");
				}
			}
	}

	private void OnTriggerExit(Collider other)
	{
		if (currentInteractable != null)
		{
			currentInteractable = null;	
		}
	}
}
