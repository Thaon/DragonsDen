using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragdaq : MonoBehaviour 
{

	int[] assetValue = new int[3];
	PersistentData persData;
	public Text[] assetValueText;

	// Use this for initialization
	void Start () 
	{
		persData = FindObjectOfType <PersistentData> ();
		assetValue[0] = persData.m_itemValuesByIndex[0];
		assetValue[1] = persData.m_itemValuesByIndex[1];
		assetValue[2] = persData.m_itemValuesByIndex[2];

		assetValueText[0].text += assetValue[0].ToString ();
		assetValueText[1].text += assetValue[1].ToString ();
		assetValueText[2].text += assetValue[2].ToString ();

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

}
