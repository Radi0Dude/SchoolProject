using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
	[SerializeField]
	TMP_Text m_Text;

	string m_Name;

	private void Update()
	{
		m_Name =  Mathf.RoundToInt(1 / Time.deltaTime).ToString();

		m_Text.text = m_Name;
	}
	
}
