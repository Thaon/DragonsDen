using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    #region member variables

    public string m_name;
    public ItemType m_type;
    public uint m_value;

    #endregion

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void Activate()
    {
        FindObjectOfType<PersistentData>().
    }
}
