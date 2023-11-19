using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Ball : MonoBehaviour
{
    public float jumpSpeed = 5;
    public GameObject deadPiece;

    Rigidbody2D rb;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(hor, 0));

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity += Vector2.up * jumpSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        if(collision.gameObject.name.Contains("Spike"))
        {
            for(int i = 0; i < 5; i++)
            {
                var offset = Random.insideUnitSphere;
                //deadPiece.GetComponent<SpriteRenderer>().color
                Instantiate(deadPiece, transform.position + offset, transform.rotation);
            }
            Destroy(gameObject);
            GameManager.instance.Lose();         
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.Win();
    }
}