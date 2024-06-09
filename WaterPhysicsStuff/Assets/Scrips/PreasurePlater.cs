using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PreasurePlater : MonoBehaviour
{

	public delegate void PlatePressed();
	public static event PlatePressed OnPressed;

	public delegate void PlateReleased();
	public static event PlateReleased OnReleased;

	[Tag]
	[SerializeField]
	string playerTag;
	[Tag]
	[SerializeField]
	string interactableTag;

	[SerializeField]
	GameObject connectedBody;

	[SerializeField]
	bool multipleDoors;

	OpenDoor openDoor;

	private void Start()
	{
		//Just For futureproof
		openDoor = connectedBody.GetComponent<OpenDoor>();
	}
	private void OnTriggerStay(Collider other)
	{
		if (multipleDoors)
		{
			openDoor.DoorOpener();
		}

		else 
		{ 
			if(other.gameObject.tag == playerTag || other.gameObject.tag == interactableTag)
			{	
				if(OnPressed != null)
				{
					OnPressed();

				}

			}	
		}

	}
	private void OnTriggerExit(Collider other)
	{
		if (multipleDoors)
		{
			openDoor.DoorOpener();
		}

		else
		{
			if (other.gameObject.tag == playerTag || other.gameObject.tag == interactableTag)
			{
				if (OnReleased != null)
				{
					OnReleased();

				}

			}
		}
	}


}
