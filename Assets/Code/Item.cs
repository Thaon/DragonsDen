using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    #region member variables

    public string m_name;
    public ItemType m_type;

    #endregion

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void Activate()
    {
        if (m_type == ItemType.Increase)
        {
            FindObjectOfType<PersistentData>().ModifyItemsValue(m_name, m_type);
            Destroy(this.gameObject);
        }
        else
        {
            FindObjectOfType<PersistentData>().ModifyItemsValue(m_name, m_type);

            foreach (Item it in transform.parent.GetComponentsInChildren<Item>())
            {
                if (it.m_type == ItemType.Decrease)
                {
                    it.enabled = false;
                }
            }
        }
    }
}
