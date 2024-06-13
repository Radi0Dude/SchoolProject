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

	bool isReady;

	bool closingDoor;
	private void Start()
	{
		startPos = transform.position;
		endPos = new Vector3(transform.position.z, transform.position.y + endPosAmountUp, transform.position.z);
	}

	private void OnEnable()
	{
		PreasurePlater.OnPressed += DoorOpener;
		PreasurePlater.OnReleased += DoorClosed;
	}

	private void OnDisable()
	{
		PreasurePlater.OnPressed -= DoorOpener;
		PreasurePlater.OnReleased += DoorClosed;

	}
	public void DoorOpener()
	{
		if(startTime == 0)
		{
			startTime = Time.time;
		}
		timer = (Time.time - startTime) / secondsForDoorOpen;

		transform.position = Vector3.Lerp(startPos, endPos, timer);
		isReady = false;
	}

	private void DoorClosed()
	{
		
		closingDoor = true;
		StartCoroutine(CloseDoor());
	}

	IEnumerator CloseDoor()
	{

		//Fix
		while(closingDoor)
		{
			if (timer > 1 && timer < 0 && !isReady)
			{
				startTime = Time.time + timer;
			}
			if (timer < 1 && !isReady)
			{
				timer = 0;
				startTime = Time.time;
				isReady = true;
			}
			timer = (Time.time - startTime) / secondsForDoorOpen;
			if (isReady)
			{
				Debug.Log("Heu");
				transform.position = Vector3.Lerp(endPos, startPos, timer);
			}
			yield return new WaitForEndOfFrame();
			if(timer > 1)
			{
				closingDoor = false;
			}
		}
	}
}
