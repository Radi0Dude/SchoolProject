using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PreasurePlater : MonoBehaviour
{

	public delegate void PlatePressed();
	public static event PlatePressed OnPressed;



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


	[SerializeField]
	bool doOnce;
	private void Start()
	{
		//Just For futureproof
		openDoor = connectedBody.GetComponent<OpenDoor>();
	}
	private void OnTriggerStay(Collider other)
	{
        if (!doOnce)
        {
			if (multipleDoors)
			{
				openDoor.DoorOpener();
			}

			else 
			{ 
				if(other.gameObject.tag == interactableTag)
				{	
					if(OnPressed != null)
					{
						OnPressed();

					}

				}	
		}
        }


	}

	private void OnTriggerEnter(Collider other)
	{
		if (doOnce)
		{
			if (other.gameObject.tag == interactableTag)
			{
				if (OnPressed != null)
				{
					OnPressed();

				}

			}
		}
	}

}
