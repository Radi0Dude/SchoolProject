using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	Vector3 startPos;
	Vector3 endPos;

	[SerializeField]
	float endPosAmountUp;

	
	float timer;
	float startTime;

	[SerializeField]
	float secondsForDoorOpen;

	Vector3 curPos;
	bool firstEnable = false;

	bool isReady;

	bool closingDoor;
	private void Start()
	{
		startPos = transform.localPosition;
		endPos = new Vector3(transform.localPosition.x, transform.localPosition.y + endPosAmountUp, transform.localPosition.z);
	}

	private void OnEnable()
	{
		PreasurePlater.OnPressed += DoorOpener;
		

	}

	private void OnDisable()
	{
		PreasurePlater.OnPressed -= DoorOpener;

		firstEnable = true;
	}
	public void DoorOpener()
	{
		if(startTime == 0)
		{
			startTime = Time.time;
		}
		timer = (Time.time - startTime) / secondsForDoorOpen;

		transform.localPosition = Vector3.Lerp(startPos, endPos, timer);
	
	}

	

	
}
