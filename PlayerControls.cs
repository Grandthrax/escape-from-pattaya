using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    SpriteRenderer render;
    Vector2 lookDirection = new Vector2(1, 0);
    Rigidbody2D rigidbody2d;

    bool dead = false;

    bool invincible = false;
    float invincibleTimer = 0;
    float deathTimer = 2.0f;
    public float invincibleLength = 1.0f;

   // public int maxHealth = 4;
    
    public float speed = 3.0f;
    void Start()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>(); 
        rigidbody2d = GetComponent<Rigidbody2D>();

       // currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //don't do normal stuff if dead
        if (dead)
        {

            if (deathTimer < 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                deathTimer -= Time.deltaTime;
            }



        }

        //check invincible
        if (invincible)
        {
            if (invincibleTimer <= 0)
            {
                invincible = false;
            }
            else
            {
                invincibleTimer -= Time.deltaTime;
            }
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical).normalized;


        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;


        position = position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);

    }

    public void ChangeHealth(int amount = -1)
    {
        if (!invincible)
        {
            // never go below 0
            LevelManager.currentHealth = Mathf.Clamp(LevelManager.currentHealth + amount, 0, LevelManager.maxHealth);

            //give the player a break while injured
            if (amount < 1)
            {

                StartCoroutine("Flasher", Color.red);
                

                invincible = true;
                invincibleTimer = invincibleLength;
            }
            else
            {
                StartCoroutine("Flasher",Color.green);
            }

            if (LevelManager.currentHealth == 0)
            {

                animator.Play("Death");
                dead = true;
            }
        }

    }

     IEnumerator Flasher(Color col) 
         {
             for (int i = 0; i < 5; i++)
             {
              render.color = col;
              yield return new WaitForSeconds(.1f);
              render.color = Color.white; 
              yield return new WaitForSeconds(.1f);
             }
          }

}
