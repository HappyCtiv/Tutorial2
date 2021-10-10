using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
    }


    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rb2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if ((Input.GetKey(KeyCode.Space))) //make a comment
            {
                rb2d.AddForce(new Vector2(0, 2),ForceMode2D.Impulse);
            }
        }
    }
}
