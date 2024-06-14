using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
	[SerializeField]
	Transform preasurePlatre;

	private NavMeshAgent agent;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		agent.SetDestination(preasurePlatre.position);
	}
}
