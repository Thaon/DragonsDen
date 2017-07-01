using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObject : MonoBehaviour
{

    private Vector3 m_speed;

    void Start()
    {
        m_speed = FindObjectOfType<PersistentData>().m_speed;
    }

    void Update()
    {
        transform.position += m_speed * Time.deltaTime;
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
