using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

    public bool m_isOnGround = true;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
            m_isOnGround = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
            m_isOnGround = false;
    }
}
