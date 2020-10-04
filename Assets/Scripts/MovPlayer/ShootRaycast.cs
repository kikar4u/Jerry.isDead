using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
   
        ReloadingGun();

        ShootKill();
    }

    public void LazerDectection ()
    {
        Debug.DrawRay(transform.position, Vector3.forward * 10, Color.red);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.forward);

        if (Physics.Raycast(ray, out hit))
        {
            print(hit.transform.name + "traverse le rayon");
        }
    }

    void ReloadingGun()
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

    public void ShootKill ()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(_currentAmmo > 0)
            {
                Fire();
                _currentAmmo--;
            }
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(m_BulletPref);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), m_BulletSpawn.parent.parent.GetComponent<Collider>());

        bullet.transform.position = m_BulletSpawn.position;
        Vector3 rotation = bullet.transform.rotation.eulerAngles;

        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        bullet.GetComponent<Rigidbody>().AddForce(m_BulletSpawn.forward * m_BulletSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulleAfterTime(bullet, m_LifeTime));

    }

    private IEnumerator DestroyBulleAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(bullet);
    }

    #region Private Members
    private int _currentAmmo;
    private bool _isReloading = false;
    #endregion
}
