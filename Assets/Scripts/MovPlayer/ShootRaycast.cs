using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRaycast : MonoBehaviour
{
    public GameObject m_BulletPref;
    public Transform m_BulletSpawn;
    public float m_BulletSpeed = 30;
    public float m_LifeTime = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LazerDectection();
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

    public void ShootKill ()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Fire();
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


}
