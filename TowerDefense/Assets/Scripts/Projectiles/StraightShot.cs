using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shoots in a single direction
/// </summary>
public class StraightShot : BaseProjectile
{
    //The speed of the shot
    public float m_speed = 10.0f;

    //The direction the shot is travelling
    protected Vector3 direction;
    
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        direction = m_target.transform.position - gameObject.transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        //Move the object in the direction of where the target was when we first fired
        gameObject.transform.position += direction * m_speed * Time.deltaTime;
    }
}
