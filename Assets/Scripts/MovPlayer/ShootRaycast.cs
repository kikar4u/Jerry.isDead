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

    
    void Start()
    {
       _currentAmmo = m_MaxAmmo;
        
    }

    
    void Update()
    {
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

    IEnumerator Reload()
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
        Vector3 dir = Vector3.forward;
        if(direction == InventaireHandler.AlgoActionEnum.Left)
        {
            dir = Vector3.left;
            GetComponentInChildren<Rigidbody>().DORotate(new Vector3(0f,90f - 45f,0f), 0.5f).OnComplete(() =>
            {
                SpwonBullet(Bullet.directionBullet.left);
                GetComponentInChildren<Rigidbody>().DORotate(new Vector3(0f, 90f, 0f), 0.5f);
            });
        }
            
        if(direction == InventaireHandler.AlgoActionEnum.Right)
        {
            dir = Vector3.right;
            GetComponentInChildren<Rigidbody>().DORotate(new Vector3(0f, 90f + 45f, 0f), 0.5f).OnComplete(() =>
            {
                SpwonBullet(Bullet.directionBullet.right);
                GetComponentInChildren<Rigidbody>().DORotate(new Vector3(0f, 90f, 0f), 0.5f);
            });
        }
            
        if(direction == InventaireHandler.AlgoActionEnum.Up)
        {
           dir = Vector3.forward;
           SpwonBullet(Bullet.directionBullet.center);

        }
    }

    void SpwonBullet(Bullet.directionBullet direction)
    {
        GameObject bullet = Instantiate(m_BulletPref);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), m_BulletSpawn.parent.parent.GetComponent<Collider>());

        bullet.transform.position = m_BulletSpawn.position;
        bullet?.GetComponent<Bullet>().Fire(direction);
    }

   

    #region Private Members
    private int _currentAmmo;
    private bool _isReloading = false;
    
    #endregion
}
