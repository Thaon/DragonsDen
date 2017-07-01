using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    #region member variables

    public bool m_changingScene = false;

    #endregion

    void Awake()
    {
        if (!FindObjectOfType<PersistentData>())
        {
            Instantiate(Resources.Load("PersistentDataGO"));
        }
    }

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
