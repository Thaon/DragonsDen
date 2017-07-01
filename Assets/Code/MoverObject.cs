using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObject : MonoBehaviour
{
    private PersistentData m_pData;
    private Vector3 m_speed;

    void Start()
    {
        m_pData = FindObjectOfType<PersistentData>();
        m_speed = m_pData.m_speed;
    }

    void Update()
    {
        if (m_pData.m_state == GameState.Playing)
        {
            transform.position += m_speed * Time.deltaTime;
        }
    }

    public void Kill()
    {
        StartCoroutine(KillTimed());
    }

    IEnumerator KillTimed()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
