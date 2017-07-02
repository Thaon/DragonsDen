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
    public float m_maxDistanceRadius;

    private Rigidbody m_rb;
    private GroundChecker m_groundChecker;
    private bool m_canJump = true;
    private PersistentData m_pData;
    private Vector3 m_startingPosition;
    private float m_inputOffset = 0;

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
        m_startingPosition = transform.position;
        m_startingPosition.y = 0;
    }

    public void GoLeft()
    {
        m_inputOffset = -1;
    }

    public void GoRight()
    {
        m_inputOffset = 1;
    }

    public void Stop()
    {
        m_inputOffset = 0;
    }

    void Update()
    {
        //m_text.text = Input.acceleration.x.ToString() + ", " + Input.acceleration.y.ToString() + ", " + Input.acceleration.z.ToString();
        if (m_pData.m_state == GameState.Playing)
        {
            //Vector3 input = Input.acceleration;
            //input.y += Input.GetAxis("Horizontal") * -1;
            //input.x += Input.GetAxis("Vertical");
            //input.z = 0;

            //move if not in dead zone
            Vector3 vel = m_rb.velocity;
            vel.z = 0;
            vel.x = 0;

            //if (Mathf.Abs(input.x) > 0.1)
            //    vel += Vector3.forward * -m_speed * Mathf.Sign(input.x) * -1;

            //if (Mathf.Abs(input.y) > 0.1)
            //    vel += Vector3.right * -m_speed * Mathf.Sign(input.y);

            //if (Mathf.Abs(input.x) <= 0.1 && Mathf.Abs(input.y) <= 0.1)
            //{
            //    //calculate and add drag
            //    vel.z = 0;
            //    vel.x = 0;
            //}

            //get input from buttons
            if (m_inputOffset != 0)
                vel += Vector3.forward * (-m_speed * Mathf.Sign(m_inputOffset) * -1);

            //calculate distance radius
            Vector3 nextPosition = transform.position + vel;
            nextPosition.y = 0;

            if (Vector3.Distance(nextPosition, m_startingPosition) > m_maxDistanceRadius)
            {
                vel.x = 0;
                vel.z = 0;
            }

            m_rb.velocity = vel;
        }
        else
            m_rb.velocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        //reset jump and apply gravity
        if (m_groundChecker.m_isOnGround)
        {
            m_canJump = true;
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
    }

    public void Jump()
    {
        //jump
        if (m_groundChecker.m_isOnGround)
            if (m_canJump)
            {
                m_canJump = false;
                m_rb.AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
            }
    }
}
