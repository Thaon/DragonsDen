using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Increase, Decrease };

public class ItemInteractor : MonoBehaviour {

    #region member variables

    private Collider m_coll;

    #endregion


    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Item>())
            other.GetComponent<Item>().Activate();
    }
}
