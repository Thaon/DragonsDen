using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    #region member variables

    public GameObject[] m_levelSections;
    public GameObject m_spawningPosition;


    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
		{
            GameObject go = Instantiate(m_levelSections[Random.Range(0, m_levelSections.Length)], m_spawningPosition.transform.position + new Vector3(-100, 0, 0), Quaternion.identity);
			go.transform.eulerAngles = new Vector3 (0, (int)Random.Range (0, 2) * 180, 0);
			m_spawningPosition = go;
            other.GetComponent<MoverObject>().Kill();
        }
    }
}
