using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public GameObject m_projectile;
    public float m_targetRadius;
    public float m_coolDown = 2.0f;
    public float m_rotateSpeed = 10.0f;
    public Sprite m_spriteImage;

    private float m_coolDownTimer = 0.0f;
    private GameObject m_target;

    // Update is called once per frame
    protected virtual void Update()
    {
        //this will probably need to be better at some point
        //Get the target and rotate the tower to face the target
        m_target = GetTarget();
        if (m_target)
        {
            Vector3 cur = m_target.transform.position - transform.position;
            
            Vector3 newDir = Vector3.RotateTowards(transform.forward, cur, Time.deltaTime * m_rotateSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

        //Check if the tower can shoot. If it can and there is a target then shoot at the target
        if ( CanShoot() )
        {
            if (m_target)
            {
                Shoot(m_target);
            }
        }
        else
        {
            m_coolDownTimer -= Time.deltaTime;
        }

        
    }
    
    /// <summary>
    /// Returns if the tower can shoot
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanShoot()
    {
        return m_coolDownTimer <= 0.0f; ;
    }

    /// <summary>
    /// Get the first target that is returned by overlap sphere.
    /// TODO: Add functionality for finding closest, furthest, lowest/highes health
    /// </summary>
    /// <returns></returns>
    protected virtual GameObject GetTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, m_targetRadius, LayerMask.NameToLayer("Enemy"));
        for( int index = 0; index < colliders.Length; ++index )
        {
            GameObject collider = colliders[index].gameObject;
            if (collider.CompareTag("Enemy"))
            {
                return colliders[index].gameObject;
            }
        }
        return null;
    }


    /// <summary>
    /// Fire the set projectile at the target
    /// </summary>
    /// <param name="target">What the projectile should be aiming at</param>
    protected virtual void Shoot(GameObject target)
    {
        //Create the projectile then set it's target
        GameObject projectile = GameObject.Instantiate<GameObject>(m_projectile, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<BaseProjectile>().SetTarget(target);

        //reset the cooldown
        m_coolDownTimer = m_coolDown;
    }
}
