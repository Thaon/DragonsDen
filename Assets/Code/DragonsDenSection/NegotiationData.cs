using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class NegotiationData : MonoBehaviour 
{

	public Flowchart flowchart;

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

	public Text productName;
	int productWords = 0;

	public GameObject buttonPanel;
	public GameObject toasterPanel;
	public GameObject chooseDragonPanel;
	public GameObject negotiationPanel;
	public GameObject productDesignPanel;

	public Text finalDeal;
	public GameObject dealPanel;

	/* 0 = Duncan
	 * 1 = Peter
	 * 2 = Deborah
	 */

	public Image[] dragonFace;
	public Sprite[] dragonFaceSprites;


	// Use this for initialization
	void Start () 
	{
		persData = FindObjectOfType <PersistentData> ();
		currentMoney = persData.GetTotalItamsValue ();

		moneyWanted = (int)Mathf.Round (currentMoney * moneySlider.value * 1.5f);
		moneyWantedText.text = (moneyWanted * 50000).ToString ();
		percentageAvailable = (int)Mathf.Round ( percentageSlider.value * 51);
		percentageText.text = percentageAvailable.ToString ();

		Setup ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		moneyWanted = (int)Mathf.Round (currentMoney * moneySlider.value * 3);
		moneyWantedText.text = (moneyWanted * 50000).ToString ();
		percentageAvailable = (int)Mathf.Round (percentageSlider.value * 51);
		percentageText.text = percentageAvailable.ToString ();
	}

	void Setup ()
	{
		shares [0] = Random.Range (0.9f, 1.2f);
		shares [1] = Random.Range (0.9f, 1.2f);
		shares [2] = Random.Range (0.9f, 1.2f);
		shares [3] = Random.Range (0.9f, 1.2f);

		pickupCount [0] = persData.GetItemNumber ("Cattle");
		pickupCount [1] = persData.GetItemNumber ("Gems");
		pickupCount [2] = persData.GetItemNumber ("Share");
		pickupCount [3] = 6;

		print (pickupCount[0]);
		print ("hello");

		dragonPref[0, (int)pickups.cattle] = 1.5f;
		dragonPref[0, (int)pickups.gems] = 1;
		dragonPref[0, (int)pickups.shares] = 0.6f;

		dragonPref[1, (int)pickups.cattle] = 0.6f;
		dragonPref[1, (int)pickups.gems] = 1.5f;
		dragonPref[1, (int)pickups.shares] = 1;

		dragonPref[2, (int)pickups.cattle] = 1;
		dragonPref[2, (int)pickups.gems] = 0.6f;
		dragonPref[2, (int)pickups.shares] = 1.5f;

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
		descriptions[3, 1] = "Toilet";
		descriptions[3, 2] = "Washer dryer combo";


		for (int i = 0; i < 3; i++)
		{

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

		print ("asset: " + assetValue);
		print ("deal: " + dealValue);

		if (dealValue >= assetValue + 7)
		{
			flowchart.ExecuteBlock ("BadDealPlayer");
			dragonFace[chosenDragon].sprite = dragonFaceSprites[2 + chosenDragon * 3];
		}

		if (dealValue <= assetValue - 7)
		{
			flowchart.ExecuteBlock ("BadDealDragon");
			dragonFace[chosenDragon].sprite = dragonFaceSprites[0  + chosenDragon * 3];
		}

		if (dealValue <= assetValue + 7 && dealValue >= assetValue - 7)
		{
			flowchart.ExecuteBlock ("Deal");
			dragonFace[chosenDragon].sprite = dragonFaceSprites[2  + chosenDragon * 3];
		}
			
	}

	public void ProposeCounterOffer ()
	{

	}

    public void EndDeal()
    {
        int assetValue = 0;
        for (int i = 0; i < 4; i++)
        {
            assetValue += Mathf.RoundToInt(pickupCount[i] * shares[i] * (1 + currentMultiplier[i]) * dragonPref[chosenDragon, i]);
        }
        persData.SetDealResults(percentageAvailable, moneyWanted, assetValue);
        persData.ChangeSceneTo("EndGame");
    }

	public void CreateProduct (int val)
	{
		currentMultiplier[val] += maxMultiplier[val];

		if (maxMultiplier[val] == 0.5f)
		{
			maxMultiplier[val] = 0.2f;
			buttonsText[val].text = descriptions[val, 1];

			productName.text += descriptions[val, 2];
		}
		else if (maxMultiplier[val] == 0.2f)
		{
			maxMultiplier[val] = 0.0f;
			buttonsText[val].text = descriptions[val, 0];

			productName.text += descriptions[val, 1];
		}
		else
			productName.text += descriptions[val, 0];

		productName.text += ", ";
		productWords++;

		if (productWords == 3)
		{
			buttonPanel.SetActive (false);
			toasterPanel.SetActive (true);
		}
			
	}

	public void PickToaster (int val)
	{
		productName.text += descriptions[3, val];

		flowchart.ExecuteBlock ("ChooseDragon");

		productDesignPanel.SetActive (false);
	}

	public void ChooseDragon (int val)
	{
		print (val);
		chosenDragon = val;

		if (val == 0)
			flowchart.ExecuteBlock ("DuncanBannatyne");
		else if (val == 1)
			flowchart.ExecuteBlock ("PeterJones");
		else 
			flowchart.ExecuteBlock ("DeborahMeaden");

		chooseDragonPanel.SetActive (false);
	}
		
}
