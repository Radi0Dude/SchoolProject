using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiNavmash : MonoBehaviour
{
	[SerializeField]
	private NavMeshAgent agent;

	private void Start()
	{
		
	}

	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Input.GetMouseButtonDown(0))
		{

			if(Physics.Raycast(ray, out hit))
			{
				agent.SetDestination(hit.point);
			}
		}

	}
}
