using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    public Text portalText;
    public Text diamondText;

    public int portals = 10;
    int diamonds = 0;

    private void Awake() 
    { 
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        portalText.text = "Portals created: " + portals.ToString();
        diamondText.text = "Diamonds collected: " + diamonds.ToString();
    }

    public void AddDiamonds()
    {
        diamonds++;
        diamondText.text = "Diamonds collected: " + diamonds.ToString();
    }

    public void ReducePortals()
    {
        portals--;
        portalText.text = "Portals created: " + portals.ToString();
    }
}
