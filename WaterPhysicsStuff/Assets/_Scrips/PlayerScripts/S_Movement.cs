using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class S_Movement : MonoBehaviour
{
	Rigidbody rb;

	Vector2 dir;

	[SerializeField]
	float springHeight;

	[SerializeField]
	float damper;

	[SerializeField]
	float raylenght;

	Vector3 bounds;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		bounds = gameObject.GetComponent<MeshRenderer>().bounds.size;
	}
	public void GetButtonPressed(InputAction.CallbackContext context)
	{
		dir = context.ReadValue<Vector2>();

	}

	

	public void JumpPressed(InputAction.CallbackContext context)
	{
		if(context.started)
		{
			Jump();
		}
	}
	
	private bool Grounded()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, raylenght))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void Jump()
	{

	}
	private void FixedUpdate()
	{
		if(Grounded())
		{
			KeepCharacterAfloat();
		}
		Debug.Log(Grounded());

	}
	private void KeepCharacterAfloat()
	{
		RaycastHit hit;

		

		if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, raylenght))
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * raylenght, Color.green);
			Vector3 hitPoint = hit.point;

			float diffrence = hitPoint.y + springHeight;

			if(transform.position.y - bounds.y / 2 - hitPoint.y < diffrence)
			{
				rb.AddForce(Vector3.up * (-Physics.gravity.y / 2 + diffrence), ForceMode.Force);
			}
			else if(transform.position.y - bounds.y / 2 - hitPoint.y > diffrence)
			{
				rb.AddForce(Vector3.up * (-Physics.gravity.y / 2 ), ForceMode.Force);
			}
			else if(transform.position.y - bounds.y / 2 - hitPoint.y == diffrence)
			{
				rb.AddForce(Vector3.up * (-Physics.gravity.y / 2));
			}
			
			
		}

		Vector3 charVel = rb.velocity;

	}

}
