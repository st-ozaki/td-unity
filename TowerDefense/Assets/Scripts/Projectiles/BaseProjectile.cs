using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public float m_lifeTime = 1.0f;
    public float m_damage = 20.0f;

    protected GameObject m_target;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //decrement the life time of the projectile. When the life time reaches 0 destroy the game object
        m_lifeTime -= Time.deltaTime;
        if (m_lifeTime <= 0.0f )
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        m_target = target;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //If the projectile hit an enemy then have the enemy take damage and then destroy the projectile
        if( other.gameObject.tag == "Enemy" )
        {
            other.gameObject.GetComponent<BaseEnemy>().TakeDamage(m_damage);
            Destroy(gameObject);
        }
    }
}
