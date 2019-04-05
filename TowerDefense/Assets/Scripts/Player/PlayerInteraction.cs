using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    protected BaseTower m_tower;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if (m_tower != null)
            {
                Vector3 screenPos = Vector3.zero;
                //if the input touch count is > 0 then we are most likely on mobile so use the first touch. Still need to test mobile
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    screenPos = touch.position;
                }
                else
                {
                    //was most likey a mouse button down trigger so get the mouse position
                    screenPos = Input.mousePosition;
                }

                //Cast a ray from the screen to the world and see if we collide with something. If we did place the tower
                Ray ray = Camera.main.ScreenPointToRay(screenPos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        GameObject tower = GameObject.Instantiate<GameObject>(m_tower.gameObject, hit.point, Quaternion.identity);
                        tower.transform.position += tower.GetComponent<Collider>().bounds.max.y * Vector3.up;
                    }
                }
                m_tower = null;
            }
        }
        //If right clicked then remove the tower
        else if( Input.GetMouseButton(1))
        {
            m_tower = null;
        }
    }
    
    //Gets called from clicking on a tower from the UI
    public void SetTower(BaseTower tower)
    {
        m_tower = tower;
    }
}
