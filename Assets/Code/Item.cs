using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    #region member variables

    public string m_name;
    public ItemType m_type;
    public int m_value;

    #endregion

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void Activate()
    {
        FindObjectOfType<PersistentData>().ModifyItemsValue(m_name, m_value, m_type);
        Destroy(this.gameObject);
    }
}
