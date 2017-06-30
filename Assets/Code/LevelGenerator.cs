using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour 
{

	public GameObject[] tileSet;

	// Use this for initialization
	void Start () 
	{
		LevelGeneration ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void LevelGeneration ()
	{
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 7; j++)
			{
				if (RandomBool (0.9f))
				{
					GameObject tile = Instantiate (tileSet [Random.Range (1, tileSet.Length)], new Vector3 (j * 10, 0, i * 10), Quaternion.identity) as GameObject;
					int rot = Random.Range (0, 4);
					tile.transform.eulerAngles = new Vector3 (0, rot * 90, 0);
				}
				else 
				{
					Instantiate (tileSet [0], new Vector3 (j * 10, 0, i * 10), Quaternion.identity);
				}
		
			}

		}

	}


	public static bool RandomBool (float chance)
	{
		float i = Random.value;
		if (i <= chance)
			return true;
		else
			return false;
		
	}
}
