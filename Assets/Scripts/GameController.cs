using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public PlayerController player;

    public bool isMouseHeld = false;
    private bool cancelshot = false;
    public float largestCameraSize;   //8.45f
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.m_Lens.OrthographicSize = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        // shooting
        if (Input.GetMouseButtonDown(0) && player.bulletNumber < 2 && !EventSystem.current.IsPointerOverGameObject())
        {
            isMouseHeld = true;
        }
        if (Input.GetMouseButtonUp(0) && player.bulletNumber < 2 && !EventSystem.current.IsPointerOverGameObject())
        {
            CancelInvoke("ZoomOut");
            isMouseHeld = false;
            virtualCamera.m_Lens.OrthographicSize = 5f;
            if (cancelshot == false)
            {
                player.Launch();
            }
            cancelshot = false;
        }


        // zoom out cancellation
        if (isMouseHeld == true && Input.GetMouseButtonUp(1))
        {
            virtualCamera.m_Lens.OrthographicSize = 5f;
            isMouseHeld = false;
            cancelshot = true;
        }

        // portal cancellation
        if (isMouseHeld == false && Input.GetMouseButtonDown(1) && player.portal1 != null && player.portal2 == null && Time.timeScale != 0)
        {
            Destroy(player.portal1);
            player.DiminishmentBulletNumber();
        }
        if (isMouseHeld == false && Input.GetMouseButtonDown(1) && player.portal2 != null && Time.timeScale != 0)
        {
            Destroy(player.portal1);
            Destroy(player.portal2);
            player.ClearBulletNumber();
        }
    }

    private void FixedUpdate()
    {
        // movement
        player.horizontal = Input.GetAxis("Horizontal");
        if (isMouseHeld == false)
        {
            player.rb.velocity = new Vector2(player.horizontal * player.moveSpeed, player.rb.velocity.y);
        }   
        
        //zoom out
        if (isMouseHeld == true && virtualCamera.m_Lens.OrthographicSize <= largestCameraSize)
        {
            Invoke("ZoomOut", 0.1f);
        }

    }
    void ZoomOut()
    {
        virtualCamera.m_Lens.OrthographicSize += 0.25f;
    }
}
