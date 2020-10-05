using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootRaycast : MonoBehaviour
{
    #region Public And Protected Members
    public GameObject m_BulletPref;
    public Transform m_BulletSpawn;
    public float m_BulletSpeed = 30;
    public float m_LifeTime = 3;
    public float m_ReloadTime = 1f;
    public int m_MaxAmmo = 10;
    #endregion

    #region Private Members
    private int _currentAmmo;
    private bool _isReloading = false;
    #endregion

    void Start()
    {
       _currentAmmo = m_MaxAmmo;
        
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            SpawnBullet(Bullet.directionBullet.left);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            SpawnBullet(Bullet.directionBullet.center);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            SpawnBullet(Bullet.directionBullet.right);
        }

        LazerDectection();
    }

    public void LazerDectection ()
    {
        Debug.DrawRay(transform.position, Vector3.forward * 10, Color.red);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.forward);

        if (Physics.Raycast(ray, out hit))
        {
            //print(hit.transform.name + "traverse le rayon");
        }
    }

    public void ReloadingGun()
    {
        if(_currentAmmo < m_MaxAmmo)

        if (Input.GetKeyDown(KeyCode.R) && !_isReloading)
        {   
              print("je recharge");
              StartCoroutine(Reload());
              return;       
        }
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        Debug.Log("j'ai Recharge");

        yield return new WaitForSeconds(m_ReloadTime);

        _currentAmmo = m_MaxAmmo;
        _isReloading = false;
    }

    public void ShootKill (InventaireHandler.AlgoActionEnum direction)
    {
        if(_currentAmmo > 0)
        {
            Fire(direction);
            _currentAmmo--;
        }
        
    }

    private void Fire(InventaireHandler.AlgoActionEnum direction)
    {
        GameObject jerrysHead = PlayerMov.Instance.jerrysHead;
        if (direction == InventaireHandler.AlgoActionEnum.Left)
        {
            jerrysHead.transform.DORotate(new Vector3(jerrysHead.transform.eulerAngles.x, -45f, jerrysHead.transform.eulerAngles.z), 0.3f).OnComplete(() =>
            {
                SpawnBullet(Bullet.directionBullet.left);
                jerrysHead.transform.DORotate(new Vector3(0f, 0f, 0f), 0.5f);
            });
        }
            
        if(direction == InventaireHandler.AlgoActionEnum.Right)
        {
            jerrysHead.transform.DORotate(new Vector3(jerrysHead.transform.eulerAngles.x, 45f, jerrysHead.transform.eulerAngles.z), 0.3f).OnComplete(() =>
            {
                SpawnBullet(Bullet.directionBullet.right);
                jerrysHead.transform.DORotate(new Vector3(0f, 0f, 0f), 0.5f);
            });
        }
            
        if(direction == InventaireHandler.AlgoActionEnum.Up)
        {
           SpawnBullet(Bullet.directionBullet.center);
        }
    }

    void SpawnBullet(Bullet.directionBullet direction)
    {
        GameObject bullet = Instantiate(m_BulletPref);
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), m_BulletSpawn.parent.parent.GetComponent<Collider>());

        bullet.transform.position = m_BulletSpawn.transform.position;
        bullet.transform.Rotate(new Vector3(0, 90, 0));
        bullet?.GetComponent<Bullet>().Fire(direction);
    }

   

}
