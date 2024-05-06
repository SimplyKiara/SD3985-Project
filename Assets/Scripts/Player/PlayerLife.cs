using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    bool isDead = false;

    public AudioSource deathAudio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDead && gameObject.transform.position.y < -7)
        {
            isDead = true;
            Die();
        }

        //debug death
        //if (!isDead && Input.GetKeyDown(KeyCode.Space))
        //{
        //    isDead = true;
        //    Die();
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player dead");
        deathAudio.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        Invoke("RestartLevel", 3.0f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
