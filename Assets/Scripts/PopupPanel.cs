using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UIElements;

public class PopupPanel : MonoBehaviour
{
    public GameObject popupPanel;
    private GameObject currentPopupPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopup()
    {
        // Activate the popup panel
        popupPanel.SetActive(true);
        currentPopupPanel = Instantiate(popupPanel, transform);
        currentPopupPanel.transform.position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

    }
}
