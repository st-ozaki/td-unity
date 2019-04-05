using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    public float m_health = 50.0f;
    
    private Vector3? m_targetLocation;
    private NavMeshAgent m_navMeshAgent;
    private float m_curHealth = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //set the current health
        m_curHealth = m_health;
        
        //get a reference to the nav mesh agent
        m_navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        if( m_navMeshAgent == null )
        {
            Debug.LogWarning("No Nav Mesh Agent attached to " + gameObject.name);
        }

        //attempt go to the set target location
        GoToDestination();
    }

    public void GoToLocation(Vector3 targetLoc)
    {
        //set the location and try and go to the destination
        m_targetLocation = targetLoc;
        GoToDestination();
    }

    public void TakeDamage(float amount)
    {
        //apply the health change, update the text and check if we need to remove the gameobject
        m_curHealth -= amount;
        
        //If the health is less than 0 destroy the game object since it's 'dead'
        if ( m_curHealth < 0.0f )
        {
            Destroy(gameObject);
        }
    }

    //Go to the target location
    private void GoToDestination()
    {
        if (m_navMeshAgent && m_targetLocation.HasValue)
        {
            m_navMeshAgent.destination = m_targetLocation.Value;
        }
    }
}
