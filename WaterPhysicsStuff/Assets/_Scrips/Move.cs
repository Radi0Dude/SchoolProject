using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    Transform startPoint;
    [SerializeField]
    Transform endPoint;

    int timer;

	private void Update()
	{

        timer = (int)Time.time;
        if (timer % 2 == 0)
        {
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, Time.time - timer);
        }
        else 
        {
			transform.position = Vector3.Lerp(endPoint.position, startPoint.position, Time.time - timer);
		}
	}
}
