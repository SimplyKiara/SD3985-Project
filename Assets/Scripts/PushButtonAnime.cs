using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonAnime : MonoBehaviour
{
    private Vector3 posi = new Vector3(0f, 0f, 0f);

    public void OnButton()
    { 
        if (posi == new Vector3(0f, 0f, 0f))
        {
            posi = this.transform.localPosition; 
        }
        this.transform.localPosition = posi + new Vector3(5, -5, 0);　
    }

    public void OutButton()
    {　
        this.transform.localPosition = posi;
    }
}