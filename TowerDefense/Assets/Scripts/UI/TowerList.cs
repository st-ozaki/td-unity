using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerList : MonoBehaviour
{
    public delegate void OnButtonClickedHandler(object sender);

    public List<GameObject> m_towers;//Should get a valid list of towers from a tower manager of some sorts
    public GameObject m_towerButton;//Prefab for the tower buttons that get created

    protected GridLayoutGroup m_gridLayoutGroup;
    protected PlayerInteraction m_playerInteraction;

    // Start is called before the first frame update
    void Start()
    {
        m_gridLayoutGroup = GetComponent<GridLayoutGroup>();
        m_playerInteraction = FindObjectOfType<PlayerInteraction>();

        foreach ( GameObject tower in m_towers )
        {
            GameObject towerButton = Instantiate<GameObject>(m_towerButton);
            //Set the tower reference 
            towerButton.GetComponent<TowerButton>().Tower = tower.GetComponent<BaseTower>();
            towerButton.GetComponent<TowerButton>().OnClicked += OnButtonClicked;//set the on click handler
            towerButton.transform.SetParent(transform);
        }
    }
    
    protected void OnButtonClicked(object sender)
    {
        //When the tower button is clicked then set the active tower
        TowerButton towerButton = sender as TowerButton;
        if( towerButton )
        {
            m_playerInteraction.SetTower(towerButton.Tower);
        }
    }
}
