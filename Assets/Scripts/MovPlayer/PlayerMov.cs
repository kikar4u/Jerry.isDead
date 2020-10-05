using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerMov : MonoBehaviour
{
    static private PlayerMov thisOne;

    static public PlayerMov Instance
    {
        get
        {
            if (!thisOne) thisOne = FindObjectOfType<PlayerMov>();
            return thisOne;
        }
    }

    #region Public And Protected Members 
    public GameObject jerrysHead;
    [SerializeField] private ShootRaycast shoot;
    public float m_Speed;
    public float m_PositionMove_x;
    [SerializeField] private float m_MaxWidth;
    [SerializeField] private float m_MinWidth;
    public float m_JumpForce;
    [SerializeField] private float jumpHegth = 1.5f;
    [SerializeField] private int rangeJump = 2; 
    public float m_RayonColision = 0.9f;

    public LayerMask m_groundLayers;
    #endregion

    private enum PositionPlayer { left, center, right}
    PositionPlayer positionPlayer = PositionPlayer.center;



    #region Private
    private Vector3 _targetPosition;
    //private Rigidbody m_Rb;
    //private CapsuleCollider m_Col;
    #endregion


    private void Awake()
    {
        //m_Rb = GetComponentInChildren<Rigidbody>();
        //m_Col = GetComponentInChildren<CapsuleCollider>();
    }

    

    public void MovePlayer(InventaireHandler.AlgoActionEnum direction)
    {
        Debug.Log("Move Player");
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, m_Speed * Time.deltaTime);

        if (direction == InventaireHandler.AlgoActionEnum.Left)
        {
            if(positionPlayer == PositionPlayer.center || positionPlayer == PositionPlayer.right)
            {
                _targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + m_PositionMove_x);
                transform.DOMove(_targetPosition, 0.5f);

                if (positionPlayer == PositionPlayer.center) positionPlayer = PositionPlayer.left;
                else if (positionPlayer == PositionPlayer.right) positionPlayer = PositionPlayer.center;
            }
        }
        else if (direction == InventaireHandler.AlgoActionEnum.Right && transform.position.x < m_MinWidth)
        {
            if (positionPlayer == PositionPlayer.center || positionPlayer == PositionPlayer.left)
            {
                _targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - m_PositionMove_x);
                transform.DOMove(_targetPosition, 0.5f);

                if (positionPlayer == PositionPlayer.center) positionPlayer = PositionPlayer.right;
                else if (positionPlayer == PositionPlayer.left) positionPlayer = PositionPlayer.center;
            }
        }
    }

    public void Jump()
    {
        //float i =  SpaceWheel.Instance.levelToLoad.sequenceDuration * 1.5f;
        //transform.DOJump(_targetPosition, m_JumpForce, 1, i);

        StartCoroutine(RoutineJump());
    }

    public IEnumerator RoutineJump()
    {
        transform.DOMoveY(transform.position.y + jumpHegth, 0.5f);

        bool sequencePassed = false;

        SpaceWheel.Instance.eventSequenceEnds.AddListener(() => sequencePassed = true);

        for (int i = 0; i < rangeJump; i++)
        {
            yield return new WaitUntil(() => sequencePassed);

            sequencePassed = false;
        }

        transform.DOMoveY(transform.position.y - jumpHegth, 0.5f);
    }

    public void Init()
    {
        
    }




    //public void Jump ()
    //{
    //    if(IsGrounded())
    //    {
    //        m_Rb.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
    //        Debug.Log("Go Jump plz");
    //    }
    //}

    //private bool IsGrounded()
    //{
    //   return Physics.CheckCapsule(m_Col.bounds.center, new Vector3(m_Col.bounds.center.x, m_Col.bounds.min.y, m_Col.bounds.center.z),
    //        m_Col.radius * m_RayonColision, m_groundLayers);      
    //}




}
