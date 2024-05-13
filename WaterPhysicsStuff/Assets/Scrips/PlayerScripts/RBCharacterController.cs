using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBCharacterController : MonoBehaviour
{
	Rigidbody rb;
	LayerMask ground;

	[SerializeField] bool isItemObject;

	[SerializeField]
	float springHeight;
	float modifiedSpringHeight;

	[SerializeField]
	float springLenght;
	Vector3 downDir;

	bool isGrounded;
	RaycastHit rayHit;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		downDir = -Vector3.up;
	}

	private void FixedUpdate()
	{
		SpringController();
	}

	private void SpringController()
	{
		Ray ray = new Ray(transform.position, downDir);
		RaycastHit hit;
		isGrounded = false;

		bool rayDidHit = Physics.Raycast(ray, out hit, springLenght, ground);
		if (!rayDidHit)
		{
			return;
		}

		isGrounded = Vector3.Distance(rayHit.point, transform.position) < springHeight && !rayHit.collider.isTrigger;

		if (isGrounded)
		{
			rayHit = hit;
			Vector3 velocity = rb.velocity;
			Vector3 rayDir = transform.TransformDirection(downDir);
		}
	}
}
