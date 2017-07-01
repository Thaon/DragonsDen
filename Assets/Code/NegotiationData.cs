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

	// Use this for initialization
	void Start () 
	{
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

}
