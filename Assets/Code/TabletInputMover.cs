using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class TabletInputMover : MonoBehaviour {

    #region member variables

    public float m_speed;
    public float m_jumpSpeed;
    [SerializeField]
    public Text m_text;

    private Rigidbody m_rb;
    private GroundChecker m_groundChecker;
    private bool m_canJump = true;

    #endregion

    void Start ()
    {
        m_rb = GetComponent<Rigidbody>();
        m_groundChecker = GetComponentInChildren<GroundChecker>();
	}
	
	void Update ()
    {
        m_text.text = Input.acceleration.x.ToString() + ", " + Input.acceleration.y.ToString() + ", " + Input.acceleration.z.ToString();

        if (Mathf.Abs(Input.acceleration.x) > 0.1)
            m_rb.AddForce(Vector3.right * -m_speed * Mathf.Sign(Input.acceleration.x), ForceMode.Force);

        if (Mathf.Abs(Input.acceleration.y) > 0.1)
            m_rb.AddForce(Vector3.forward * -m_speed * Mathf.Sign(Input.acceleration.y), ForceMode.Force);

        if (Input.acceleration.z > 0.6 && m_canJump)
        {
            m_canJump = false;
            m_rb.AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
        }

        if (m_groundChecker.m_isOnGround)
            m_canJump = true;
        else
            m_rb.AddForce(Vector3.up * -10, ForceMode.VelocityChange);
    }
}
