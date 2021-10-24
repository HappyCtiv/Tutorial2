using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    Animator anim;
    private bool facingRight = true;
    public float speed;
    public float jumpForce;
    public Text scoreText;
    public Text winText;
    public Text livesText;

    public AudioSource musicSource;
    public AudioClip musicBackground;
    public AudioClip musicWin;
    
    private int score = 0;
    private int lives = 3;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;


    private float hozMovement;
    private float vertMovement;

    void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scoreText.text = "Coins: " + score.ToString();
        LivesCountText();
        winTexting();
        winText.text ="";
 
    }


    void Update()
    {      
        musicSource.clip = musicBackground;
        musicSource.Play();
        musicSource.loop = true;

        if (score==8)
        {
        musicSource.Stop();
        musicSource.clip = musicWin;
        musicSource.Play();
        }
        


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        hozMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical");



        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        rb2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }

    void LateUpdate()
    {


        if (isOnGround == false)
        {
            anim.SetInteger("State", 2);
        }    
        if (isOnGround == true && hozMovement==0)
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }  

    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            score += 1;
            scoreText.text = "Coins: " +  score.ToString();
            Destroy(collision.collider.gameObject);
            winTexting();
        }

        if(collision.collider.tag == "Enemy")
        {
            lives -=1;
            Destroy(collision.collider.gameObject);
            LivesCountText();
            winTexting();
            
        }
    }
  
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.Space)) //make a comment that I use Space button
            {
                rb2d.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
            }
        }
    }
    void winTexting()
    {   
        if (score==4)
        {
           
            lives = 3;
            LivesCountText();
            transform.position = new Vector2(76.0f, 0.03f);
        }
        if (score>=8)
        {
            winText.text = "You win! Game created by Sheyko Zakhar";
 
        }
        if (lives==0)
        {
            winText.text = "You Lose";
            Destroy(this.gameObject);
        }
    }

    void LivesCountText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    void Flip()
    {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
    }
}
