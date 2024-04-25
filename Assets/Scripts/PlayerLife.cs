using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public AudioSource deathAudio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //debug death
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player dead");
        deathAudio.Play();
        rb.bodyType = RigidbodyType2D.Static;
        //anim.SetTrigger("death");
        RestartScene(3.0f);
    }

    private void RestartScene(float delay)
    {
        Invoke("RestartLevel", delay);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
