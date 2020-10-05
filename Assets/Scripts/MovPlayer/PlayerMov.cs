using UnityEngine;
using DG.Tweening;

public class PlayerMov : MonoBehaviour
{
    #region Public And Protected Members 
    public float m_Speed;
    public float m_PositionMove_x;
    public float m_MaxWidth;
    public float m_MinWidth;
    public float m_JumpForce;
    public float m_RayonColision = 0.9f;

    public LayerMask m_groundLayers;  
    #endregion


    private void Awake()
    {
        m_Rb = GetComponentInChildren<Rigidbody>();
        m_Col = GetComponentInChildren<CapsuleCollider>();
    }

    

    public void MovePlayer(InventaireHandler.AlgoActionEnum direction)
    {
        Debug.Log("Move Player");
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, m_Speed * Time.deltaTime);

        if (direction == InventaireHandler.AlgoActionEnum.Left && transform.position.x > m_MaxWidth)
        {
            _targetPosition = new Vector3(transform.position.x , transform.position.y, transform.position.z + m_PositionMove_x);
            transform.DOMove(_targetPosition, 1.0f);
        }
        else if (direction == InventaireHandler.AlgoActionEnum.Right && transform.position.x < m_MinWidth)
        {
            _targetPosition = new Vector3(transform.position.x , transform.position.y, transform.position.z - m_PositionMove_x);
            transform.DOMove(_targetPosition, 1.0f);
        }
    }

    public void Jump()
    {
       float i =  SpaceWheel.Instance.levelToLoad.sequenceDuration * 2;
       transform.DOJump(_targetPosition, m_JumpForce, 1, i);
    }

 

    //public void Jump ()
    //{
    //    if(IsGrounded())
    //    {
    //        m_Rb.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
    //        Debug.Log("Go Jump plz");
    //    }
    //}

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
