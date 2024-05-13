using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetModelVertex : MonoBehaviour
{
	
	MeshFilter meshFilter;

	[SerializeField]
	List<Vector3> vertecies = new List<Vector3>();

	int waterLayer;

	float densityOfWater = 1027;

	float objectVolume;

	int amountOfverticies;

	Rigidbody rb;

	[SerializeField]
	float objectDensity;

	private void Awake()
	{
		waterLayer = LayerMask.NameToLayer("Water");
	}

	float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float v321 = p3.x * p2.y * p1.z;
		float v231 = p2.x * p3.y * p1.z;
		float v312 = p3.x * p1.y * p2.z;
		float v132 = p1.x * p3.y * p2.z;
		float v213 = p2.x * p1.y * p3.z;
		float v123 = p1.x * p2.y * p3.z;
		return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
	}

	float VolumeOfMesh(Mesh mesh)
	{
		float volume = 0;
		Vector3[] vertices = mesh.vertices;
		int[] triangles = mesh.triangles;
		for (int i = 0; i < mesh.triangles.Length; i += 3)
		{
			Vector3 p1 = vertices[triangles[i + 0]];
			Vector3 p2 = vertices[triangles[i + 1]];
			Vector3 p3 = vertices[triangles[i + 2]];
			volume += SignedVolumeOfTriangle(p1, p2, p3);
		}
		return Mathf.Abs(volume);
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		meshFilter = GetComponent<MeshFilter>();
		Matrix4x4 localToWorld = transform.localToWorldMatrix;
		for (int i = 0; i < meshFilter.mesh.vertices.Length; i++)
		{
			vertecies.Add(localToWorld.MultiplyPoint3x4(meshFilter.mesh.vertices[i]));
		}
		objectVolume = VolumeOfMesh(meshFilter.mesh) * transform.localScale.x;

		
	}

	private void getVertecies()
	{
		Matrix4x4 localToWorld = transform.localToWorldMatrix;
		

		for (int i = 0; i < meshFilter.mesh.vertices.Length; i++)
		{
			vertecies[i] = localToWorld.MultiplyPoint3x4(meshFilter.mesh.vertices[i]);
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.layer == waterLayer)
		{
			getVertecies();
			amountOfverticies = 0;
			for(int i = 0; i < vertecies.Count; i++)
			{
				if (vertecies[i].y <= other.gameObject.transform.position.y)
				{
					amountOfverticies++;
				}
			}
			
			float calculateProcentahe = (float)amountOfverticies / vertecies.Count;
			Debug.Log(calculateProcentahe);
			Vector3 calculateForce = CalculateBouency(densityOfWater, objectVolume * calculateProcentahe);
			rb.drag = (1 * calculateProcentahe + .2f) /2;
			rb.AddForce(calculateForce, ForceMode.Force);
			
		}
	}
	private void Update()
	{
		float objectDensityUpdate = objectDensity * objectVolume;
		rb.mass = objectDensityUpdate;
	}

	Vector3 CalculateBouency(float denistyOfLiquid, float volumeOfDisplaecedWater)
	{
		Vector3 buoyancyForce = new Vector3(0, denistyOfLiquid * volumeOfDisplaecedWater * -Physics.gravity.y, 0);

		return buoyancyForce;
	}

	
}
