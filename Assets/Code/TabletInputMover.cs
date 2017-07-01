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
    //public Text m_text;
    public float m_gravity;

    private Rigidbody m_rb;
    private GroundChecker m_groundChecker;
    private bool m_canJump = true;
    private PersistentData m_pData;

    #endregion

    void Awake()
    {
        if (!FindObjectOfType<PersistentData>())
        {
            Instantiate(Resources.Load("PersistentDataGO"));
        }
        m_pData = FindObjectOfType<PersistentData>();
    }

    void Start ()
    {
        m_rb = GetComponent<Rigidbody>();
        m_groundChecker = GetComponentInChildren<GroundChecker>();
	}

    void Update()
    {
        //m_text.text = Input.acceleration.x.ToString() + ", " + Input.acceleration.y.ToString() + ", " + Input.acceleration.z.ToString();
        if (m_pData.m_state == GameState.Playing)
        {
            Vector3 input = Input.acceleration;
            input.y += Input.GetAxis("Horizontal") * -1;
            input.x += Input.GetAxis("Vertical");

            //move if not in dead zone
            Vector3 vel = m_rb.velocity;
            vel.z = 0;
            vel.x = 0;

            if (Mathf.Abs(input.x) > 0.1)
                vel += Vector3.forward * -m_speed * Mathf.Sign(input.x) * -1;

            if (Mathf.Abs(input.y) > 0.1)
                vel += Vector3.right * -m_speed * Mathf.Sign(input.y);

            if (Mathf.Abs(input.x) <= 0.1 && Mathf.Abs(input.y) <= 0.1)
            {
                //calculate and add drag
                vel.z = 0;
                vel.x = 0;
            }
            m_rb.velocity = vel;

            //jump
            if ((input.z > 0.6 || Input.GetKeyDown(KeyCode.Space)) && m_canJump)
            {
                m_canJump = false;
                m_rb.AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
            }

            //reset jump and apply gravity
            if (m_groundChecker.m_isOnGround)
            {

            }
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
            else
                m_canJump = true;
        }
        else
            m_rb.velocity = Vector3.zero;
    }
}
