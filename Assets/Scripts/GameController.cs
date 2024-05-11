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

    int portalsLeft;
    public AudioSource deniedAudio;
    
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.m_Lens.OrthographicSize = 5f;

        portalsLeft = TextManager.instance.portals;
    }

    // Update is called once per frame
    void Update()
    {
        portalsLeft = TextManager.instance.portals;

        // shooting
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            isMouseHeld = true;
        }
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            CancelInvoke("ZoomOut");
            isMouseHeld = false;
            virtualCamera.m_Lens.OrthographicSize = 5f;
            if (cancelshot == false && player.bulletNumber < 2 && portalsLeft > 0)
            {
                player.Launch();
            }
            else
            {
                deniedAudio.Play();
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

        // jump
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timer >= 0.5f)
        {
            player.rb.AddForce(Vector2.up * 300);
            timer = 0f;
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
