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

	/*
	 * 0 = Cattle
	 * 1 = Gems
	 * 2 = Shares
	 * 3 = Magik
	 */

	enum pickups {cattle, gems, shares, magik};

	int[] pickupCount = new int[4];
	float[] shares = new float[4];

	int chosenDragon;

	float[,] dragonPref = new float[3, 4];
	float[] maxMultiplier = new float[4];
	float[] currentMultiplier = new float[4];
	public Text[] buttonsText;
	public string[,] descriptions = new string[4,3];


	// Use this for initialization
	void Start () 
	{
		persData = FindObjectOfType <PersistentData> ();
		//currentMoney = persData.GetTotalItamsValue ();

		moneyWanted = (int)Mathf.Round (currentMoney + currentMoney * moneySlider.value * 1.5f);
		moneyWantedText.text = moneyWanted.ToString ();
		percentageAvailable = (int)Mathf.Round ( percentageSlider.value * 51);
		percentageText.text = percentageAvailable.ToString ();

		Setup ();


	}
	
	// Update is called once per frame
	void Update () 
	{
		moneyWanted = (int)Mathf.Round (currentMoney + currentMoney * moneySlider.value * 1.5f);
		moneyWantedText.text = moneyWanted.ToString ();
		percentageAvailable = (int)Mathf.Round ( percentageSlider.value * 51);
		percentageText.text = percentageAvailable.ToString ();
	}

	void Setup ()
	{
		shares [0] = Random.Range (0.9f, 1.2f);
		shares [1] = Random.Range (0.9f, 1.2f);
		shares [2] = Random.Range (0.9f, 1.2f);
		shares [3] = Random.Range (0.9f, 1.2f);

		pickupCount [0] = 6;
		pickupCount [1] = 6;
		pickupCount [2] = 6;
		pickupCount [3] = 6;

		dragonPref[0, (int)pickups.cattle] = 1.1f;
		dragonPref[0, (int)pickups.gems] = 1;
		dragonPref[0, (int)pickups.shares] = 0.9f;

		dragonPref[1, (int)pickups.cattle] = 0.9f;
		dragonPref[1, (int)pickups.gems] = 1.1f;
		dragonPref[1, (int)pickups.shares] = 1;

		dragonPref[2, (int)pickups.cattle] = 1;
		dragonPref[2, (int)pickups.gems] = 0.9f;
		dragonPref[2, (int)pickups.shares] = 1.1f;

		descriptions[0, 0] = "Somewhat edible";
		descriptions[0, 1] = "Almost gluten free";
		descriptions[0, 2] = "Mega tasty";

		descriptions[1, 0] = "Funny looking";
		descriptions[1, 1] = "Frilly and fancy";
		descriptions[1, 2] = "Blindingly shiny";

		descriptions[2, 0] = "Cheaply Made";
		descriptions[2, 1] = "Bluetooth enabled";
		descriptions[2, 2] = "Super Premium";

		descriptions[3, 0] = "Toaster";
		descriptions[3, 1] = "Volvo xc60";
		descriptions[3, 2] = "Washer dryer combo";


		for (int i = 0; i < 3; i++)
		{
			print (i);

			if (pickupCount[i] >= 6)
			{
				maxMultiplier[i] = 0.5f;
				buttonsText[i].text = descriptions[i, 2];
			}
			else if (pickupCount[i] >=3 && pickupCount[i] < 6)
			{
				maxMultiplier[i] = 0.2f;
				buttonsText[i].text = descriptions[i, 1];
			}
			else
			{
				maxMultiplier[i] = 0f;
				buttonsText[i].text = descriptions[i, 0];
			}


		}

	}

	public void SubmitOffer ()
	{
		int dealValue = currentMoney * (1 + percentageAvailable / 10) - moneyWanted;

		int assetValue = 0;

		for (int i = 0; i < 4; i++)
		{
			assetValue += Mathf.RoundToInt (pickupCount[i] * shares[i] * (1 + currentMultiplier[i]) * dragonPref[chosenDragon, i]);
		}

		if (dealValue > currentMoney)
			print ("hello");
			
	}

	public void CreateProduct (int val)
	{
		currentMultiplier[val] += maxMultiplier[val];

		if (maxMultiplier[val] == 0.5f)
		{
			maxMultiplier[val] = 0.2f;
			buttonsText[val].text = descriptions[val, 1];
		}
		else if (maxMultiplier[val] == 0.2f)
		{
			maxMultiplier[val] = 0.0f;
			buttonsText[val].text = descriptions[val, 0];
		}

	}
		

}
