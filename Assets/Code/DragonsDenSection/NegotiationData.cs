using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NegotiationData : MonoBehaviour 
{

	public Text moneyWantedText;
	public Slider moneySlider;
	public Text percentageText;
	public Slider percentageSlider;

	int currentMoney = 10;
	int moneyWanted;
	int percentageAvailable;


	public PersistentData persData;

	// Use this for initialization
	void Start () 
	{
		//persData = FindObjectOfType <PersistentData> ();
		//currentMoney = persData.GetTotalItamsValue ();

		moneyWanted = (int)Mathf.Round (currentMoney + currentMoney * moneySlider.value * 1.5f);
		moneyWantedText.text = moneyWanted.ToString ();
		percentageAvailable = (int)Mathf.Round ( percentageSlider.value * 51);
		percentageText.text = percentageAvailable.ToString ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		moneyWanted = (int)Mathf.Round (currentMoney + currentMoney * moneySlider.value * 1.5f);
		moneyWantedText.text = moneyWanted.ToString ();
		percentageAvailable = (int)Mathf.Round ( percentageSlider.value * 51);
		percentageText.text = percentageAvailable.ToString ();
	}

	public void SubmitOffer ()
	{
		int dealValue = currentMoney * (1 + percentageAvailable / 10) - moneyWanted;
		if (dealValue > currentMoney)
			print ("hello");
			
	}

}
