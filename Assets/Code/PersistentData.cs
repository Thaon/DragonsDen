using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour {

    #region member variables

    public Vector3 m_speed;

    public List<string> m_items;
    public List<uint> m_itemsValues;

    #endregion

    void Start ()
    {
        foreach (string str in m_items)
            m_itemsValues.Add(0);
	}
	
	void Update ()
    {
		
	}

    public void ModifyItemsValue(string name, uint value, ItemType type)
    {
        int itemID = m_items.FindIndex(nm => nm == name);
        m_itemsValues[itemID] = type == ItemType.Increase ? m_itemsValues[itemID] += value : m_itemsValues[itemID] -= value;
    }
}
