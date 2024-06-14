using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class RBCharacterController : MonoBehaviour
{
	Rigidbody rb;
	[SerializeField]
	LayerMask ground;

	[SerializeField] bool isItemObject;

	[SerializeField]
	float springHeight;
	float modifiedSpringHeight;
	[SerializeField]
	float springStrenght;
	[SerializeField] float springDamper;

	[SerializeField]
	float springLenght;
	Vector3 downDir = new Vector3(0, -1, 0);

	bool isGrounded;
	RaycastHit rayHit;

	Vector2 dir;

	int checkMovementStyle;

	[SerializeField]
	float waterDrag;

	[Layer]
	[SerializeField]
	string waterLayer;

	//Movement
	[SerializeField]
	float playerSpeed;

	[SerializeField]
	float stoppingForce;

	[SerializeField]
	private float jumpPower;

	float swimUp;

	Vector3 rotation;

	[SerializeField]
	Transform cam;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		modifiedSpringHeight = springHeight;
		checkMovementStyle = 0;
	}

	private void FixedUpdate()
	{
		if(checkMovementStyle == 0)
		{
			SpringController();
			Movement();      
			if (isGrounded && Input.GetKeyDown(KeyCode.Space))
			{
				Jump();
			}
		}
		else if (checkMovementStyle == 1)
        {
			Swimming();
        }
		Quaternion pRot = transform.rotation;

		rotation = pRot.eulerAngles;
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

		isGrounded = Vector3.Distance(hit.point, transform.position) < springHeight && !hit.collider.isTrigger;





		if (isGrounded)
		{
			rayHit = hit;
			Vector3 velocity = rb.velocity;
			Vector3 rayDir = transform.TransformDirection(downDir);

			Vector3 otherVel = Vector3.zero;
			Rigidbody hitBody = hit.rigidbody;

			if(hitBody != null)
			{
				otherVel = hitBody.velocity;
			}

			float rayDirVel = Vector3.Dot(rayDir, velocity);
			float otherDirVel = Vector3.Dot(rayDir, otherVel);

			float relativeVel = rayDirVel - otherDirVel;

			float x = hit.distance - modifiedSpringHeight;

			float springForce = (x * springStrenght) - (relativeVel * springDamper);

			rb.AddForce(rayDir * springForce);

			if(hitBody != null)
			{
				hitBody.AddForceAtPosition(rayDir * springForce, hit.point);
				Debug.Log("helo");
			}
			else
			{
				rayHit = new RaycastHit();
			}
		}

		
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer(waterLayer))
		{
			checkMovementStyle = 1;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer(waterLayer))
		{
			checkMovementStyle = 0;
		}
	}

	public void OnMovement(InputAction.CallbackContext context)
	{
		dir = context.ReadValue<Vector2>();
		
	}

	private void Movement()
	{
		if(dir != Vector2.zero)
		{
			transform.localEulerAngles = new Vector3(0, cam.transform.localEulerAngles.y, 0);
			Debug.Log("Ejjjjj");
		}
        if (!rb.useGravity)
        {
			rb.drag = 0;
			swimUp = 0;
            rb.useGravity = true;
			
        }
        Vector2 groundedMovement = new Vector2(rb.velocity.x, rb.velocity.z);
		if(isGrounded)
		{
			if(rb.drag == 0)
			{
				rb.drag = 2;
			}
			rb.AddRelativeForce(dir.x * playerSpeed, 0, dir.y * playerSpeed);
		}
		else
		{
			if (rb.drag != 0)
			{
				rb.drag = 0;
			}
		}


		
		if(dir == Vector2.zero && rb.velocity != Vector3.zero)
		{
			if(isGrounded)
			{
				rb.AddForce(-rb.velocity.x * stoppingForce, 0, -rb.velocity.z * stoppingForce);			
			}

		}
	}

	private void Jump()
	{
		rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
	}

	private void Swimming()
	{
		if (dir != Vector2.zero)
		{
			transform.localEulerAngles = new Vector3(0, cam.transform.localEulerAngles.y, 0);
			Debug.Log("Ejjjjj");
		}
		Debug.Log("you be swimming");
		if (rb.useGravity)
		{			
			rb.drag = waterDrag;
			rb.useGravity = false;
		}
		if (Input.GetKey(KeyCode.Space))
		{
			swimUp = 1;
		}
		else if (Input.GetKey(KeyCode.LeftControl))
		{
			swimUp = -1;
		}
		else if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl))
		{
			swimUp = 0;
		}
		else if(Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftControl))
		{
			swimUp = 0;
		}
		
		
		rb.AddRelativeForce(dir.x * playerSpeed, swimUp * playerSpeed, dir.y * playerSpeed);
		
	}
}
