using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour {

    #region member variables

    public Vector3 m_speed;

    public List<string> m_items;

    public List<GameObject> m_slotsArray;

    #endregion

    void Start ()
    {
        m_slotsArray = new List<GameObject>();
        m_items = new List<string>();

        Transform[] arr = GameObject.Find("SlotsArray").GetComponentsInChildren<Transform>();
        foreach(Transform tr in arr)
        {
            if (tr.name != "SlotsArray")
                m_slotsArray.Add(tr.gameObject);
        }

        foreach (GameObject go in m_slotsArray)
        {
            go.SetActive(false);
        }
	}
	
	void Update ()
    {
		
	}

    public void ModifyItemsValue(string name, ItemType type)
    {
        if (type == ItemType.Increase)
        {
            m_items.Add(name);
            m_slotsArray[m_items.Count - 1].SetActive(true);
        }
        else
        {
            if (m_items.Count != 0)
            {
                int itemID = m_items.FindIndex(nm => nm == name);
                m_slotsArray[m_items.Count - 1].SetActive(false);
                m_items.RemoveAt(m_items.Count - 1);
            }
        }
    }
}
