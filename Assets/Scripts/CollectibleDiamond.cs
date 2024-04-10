using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDiamond : MonoBehaviour
{
    public AudioSource collectAudio;

    // called when other objects enter trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController controller = collision.GetComponent<PlayerController>();
            controller.CollectibleAmount();
            Destroy(this.gameObject);
            collectAudio.enabled = true;
            collectAudio.Play();
            //Debug.Log("Player health: " + controller.HP);
        }
    }
}
