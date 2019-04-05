using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    //OnClicked handled in the tower list. There is probably a better way to do this
    public TowerList.OnButtonClickedHandler OnClicked;

    private Image m_image;
    private BaseTower m_tower;

    /// <summary>
    /// Getter and setter for the tower reference
    /// </summary>
    public BaseTower Tower
    {
        set
        {
            //set the tower value and update the image
            m_tower = value;
            UpdateImage();
        }

        get
        {
            return m_tower;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponent<Image>();
        UpdateImage();

        GetComponent<Button>().onClick.AddListener(() => OnClicked(this));
    }
    
    protected void UpdateImage()
    {
        //If the image component and tower have been are valid then set the sprite
        if (m_image && m_tower)
        {
            m_image.sprite = m_tower.m_spriteImage;
        }
    }
}
