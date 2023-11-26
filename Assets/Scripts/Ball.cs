using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Ball : MonoBehaviour
{
    public float jumpSpeed = 5;
    public float moveForce;
    public GameObject deadPiece;
    public GameObject gameManager;

    Rigidbody2D rb;
    bool isGrounded;

    void Start()
    {
        Instantiate(gameManager);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(hor, 0) * moveForce * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity += Vector2.up * jumpSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        if(collision.gameObject.CompareTag("Enemy"))
        {
            Die();      
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Teleporter")    
            GameManager.instance.Win();
    }

    void Die()
    {
        for (int i = 0; i < 5; i++)
        {
            var offset = Random.insideUnitSphere;
            //deadPiece.GetComponent<SpriteRenderer>().color
            Instantiate(deadPiece, transform.position + offset, transform.rotation);
        }
        Destroy(gameObject);
        GameManager.instance.Lose();
    }
}