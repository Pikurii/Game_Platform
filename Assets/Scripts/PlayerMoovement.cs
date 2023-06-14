using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoovement : MonoBehaviour
{
   public Rigidbody2D rb;
   public float accelerationSpeed;
   public float maxSpeed;

   PlatformMovement platform;
   float movement = 0;
   int jump = 0;

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

         if (Input.GetKeyDown("space") && jump < 2 ) {
            //Debug.Log("lompat");
            rb.AddForce(new Vector2(0, 350f));
            jump++;
        }
    }

    void FixedUpdate() {
        if (movement != 0)
            rb.AddForce(new Vector2(movement * accelerationSpeed, 0.5f));

        float _velocityx = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(_velocityx, rb.velocity.y);

        if (platform != null) {
            transform.position = transform.position + new Vector3(platform.speed * platform.facing, 0, 0);
        }
    }

     void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "MovingPlatform" )
        {
            jump = 0;
        }

        if (collision.gameObject.tag == "MovingPlatform") {
            platform = collision.gameObject.GetComponent<PlatformMovement>();
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "MovingPlatform") {
            platform = null;
        }
    }
}
