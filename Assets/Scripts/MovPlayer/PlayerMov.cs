using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    #region Public 
    public float m_Speed;
    public float m_PositionMove_x;
    public float m_MaxHeight;
    public float m_MinHeight;
    public float m_JumpForce;
    public float m_RayonColision = 0.9f;

    public LayerMask m_groundLayers;  
    #endregion


    private void Awake()
    {
        m_Rb = GetComponentInChildren<Rigidbody>();
        m_Col = GetComponentInChildren<CapsuleCollider>();
    }

    

    void Update()
    {
        MovePlayer();
        Jump();
    }

    public void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, m_Speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > m_MaxHeight)
        {
            _targetPosition = new Vector3(transform.position.x  - m_PositionMove_x, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < m_MinHeight)
        {
            _targetPosition = new Vector3(transform.position.x + m_PositionMove_x, transform.position.y, transform.position.z);
        }
    }

    public void Jump ()
    {
        if(IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            m_Rb.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            Debug.Log("Go Jump plz");
        }
    }

    private bool IsGrounded()
    {
       return Physics.CheckCapsule(m_Col.bounds.center, new Vector3(m_Col.bounds.center.x, m_Col.bounds.min.y, m_Col.bounds.center.z),
            m_Col.radius * m_RayonColision, m_groundLayers);      
    }


    #region Private
    private Vector3 _targetPosition;
    private Rigidbody m_Rb;
    private CapsuleCollider m_Col;
    #endregion


}
