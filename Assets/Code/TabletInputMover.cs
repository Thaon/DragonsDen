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
    public float m_gravity;

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
        //m_text.text = Input.acceleration.x.ToString() + ", " + Input.acceleration.y.ToString() + ", " + Input.acceleration.z.ToString();

        Vector3 input = Input.acceleration;
        input.x += Input.GetAxis("Horizontal");
        input.y += Input.GetAxis("Vertical");

        //move if not in dead zone
        if (Mathf.Abs(input.x) > 0.1)
            m_rb.AddForce(Vector3.right * -m_speed * Mathf.Sign(input.x), ForceMode.Force);

        if (Mathf.Abs(input.y) > 0.1)
            m_rb.AddForce(Vector3.forward * -m_speed * Mathf.Sign(input.y), ForceMode.Force);

        //jump
        if ((input.z > 0.6 || Input.GetKeyDown(KeyCode.Space)) && m_canJump)
        {
            m_canJump = false;
            m_rb.AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
        }

        //reset jump and apply gravity
        if (m_groundChecker.m_isOnGround)
            m_canJump = true;
        else
            m_rb.AddForce(Vector3.up * -m_gravity, ForceMode.VelocityChange);

        //make dragon float a bit off the ground
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.up * -8, Color.red);
        if (Physics.Raycast(transform.position, Vector3.up * -1, out hit, 8))
        {
            if (hit.collider.tag == "Ground")
                if (Vector3.Distance(hit.point, transform.position) < 5.3f)
                    transform.position += new Vector3(0, .1f, 0);
        }
    }
}
