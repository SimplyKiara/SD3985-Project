using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.m_Lens.OrthographicSize = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
