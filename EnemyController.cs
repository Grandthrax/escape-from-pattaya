using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    Rigidbody2D rbplayer;
    SpriteRenderer Srend;
    Animator anim;
   
    public int zombieViewDistance = 5;

    GameObject player;

    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;

    DialogueScript dialogueScript;
    bool dialogueTriggered;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       

        Srend = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
         rbplayer = player.GetComponent<Rigidbody2D>();

       
        dialogueScript = GetComponent<DialogueScript>();
        dialogueTriggered = false;
        
    }

    // Update is called once per frame
    void Update()
    {   

        //need this to make the animation move
        anim.SetBool("IsOnGround", true);

        //look at the player
        var playerPos = player.transform.position;
        var playerX = playerPos.x;
        var playerY = playerPos.y;

        var enemyPos = transform.position;
        var enemyX = enemyPos.x;
        var enemyY = enemyPos.y;
    

        if(Vector2.Distance(transform.position, player.transform.position) <= zombieViewDistance)
        {
            if(!dialogueTriggered)
            {
                dialogueScript.DialogueOn();
                dialogueTriggered = true;
            }
            

            var step = speed*Time.deltaTime;

            //distance between enemy and player on each axis. Divided by speed gives number of seconds to reach. time deltatime gives us the fraction of a second per frame 
            //var newX = enemyX + (((playerX - enemyX)/speed) *Time.deltaTime);
            //var newY = enemyY + (((playerY - enemyY) / speed) * Time.deltaTime);

            //here's my new solution, seems to work ...
            var newVec = new Vector3(playerX - enemyX,playerY - enemyY,0).normalized;            

            newVec = newVec * speed * Time.deltaTime;

            transform.position = transform.position + newVec;
            //always moves at same speed
            anim.SetFloat("MoveX", speed);

            if(enemyX < playerX){
                //look right
                Srend.flipX = false;
            }
            else{
                //look left
                Srend.flipX = true;
            }
             //need this to make the animation move
            anim.SetBool("Idle", false);

        }else{
            //idle
            anim.SetBool("Idle", true);
            anim.SetFloat("MoveX", 0);

            if(dialogueTriggered)
            {
                dialogueScript.DialogueOff();
                dialogueTriggered = false;
            }
        }
       
    }


    private void OnCollisionEnter2D(Collision2D other) {
        PlayerControls player = other.gameObject.GetComponent<PlayerControls>();

        if(player != null){
            anim.Play("CyberPunk_Blonde_ATTACK");
            player.ChangeHealth(-1);
        }
    }

}
