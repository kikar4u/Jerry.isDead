using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject prefabSectionDefaut;

    private float DetectionDepth = 3.5f;

    private float DetectionWidth = 10 / 3;
 
    private float DetectionHeigth;

    public enum directionBullet {left, center, right}
    private directionBullet directionShoot;
    [SerializeField] private int rangeInSection;
    [Tooltip("secondes nécessaire pour atteindre la prochaine section")]
    [SerializeField] private float speed;
    private bool isArrived = false;
    private int sectionParsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Troncon SeekForNextTroncon()
    {
        Vector3 directionBox = transform.position + transform.forward * DetectionDepth;
        Debug.DrawLine(transform.position, directionBox, Color.yellow, 5);
        

        switch (directionShoot)
        {
            case directionBullet.left:
                directionBox -= transform.right * DetectionWidth;
                break;
            case directionBullet.right:
                directionBox += transform.right * DetectionWidth;
                break;
        }

        Collider[] checkTroncon = Physics.OverlapBox(directionBox, new Vector3(0.1f, 15, 0.1f));

        foreach (Collider collider in checkTroncon)
        {
            Troncon returnTroncon;
            if(collider.TryGetComponent(out returnTroncon))
            {
                return returnTroncon;
            }
        }

        return null;
    }

    public void Fire(directionBullet direction)
    {
        DetectionHeigth = transform.position.y;
        directionShoot = direction;
        StartCoroutine(MainRoutine());
    }

    private IEnumerator MainRoutine()
    {
        while(sectionParsed < rangeInSection)
        {
            Troncon nextTroncon = SeekForNextTroncon();

            if(nextTroncon)
            {
                
                Vector3 nextDirection = nextTroncon.transform.position + nextTroncon.transform.up * DetectionHeigth;
                isArrived = false;
                Debug.DrawRay(nextTroncon.transform.position, nextTroncon.transform.up * 10, Color.red, 6);
                //Debug.DrawRay(nextTroncon.transform.position, nextDirection, Color.red, 6);
                transform.DOLookAt(nextDirection, speed);
                transform.DOMove(nextDirection, speed).OnComplete(() => isArrived = true);
            }
            else
            {
                DestroyBullet();
            }

            yield return new WaitUntil(() => isArrived);

            sectionParsed++;
        }
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        print("Bullet détuite");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacleCollided;
        if (other.TryGetComponent(out obstacleCollided))
        {
            if (obstacleCollided is Porte || obstacleCollided is Compresseur || obstacleCollided is TourelleAuto)
            {
                DestroyBullet();
            }
            if (obstacleCollided is TourelleAuto)
            {
                TourelleAuto tourelle = (TourelleAuto)obstacleCollided;
                tourelle.DestroyTourelle();
            }
        }
    }
}
