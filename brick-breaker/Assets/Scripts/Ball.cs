using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ball : MonoBehaviour
{

    public Rigidbody2D body;
    //Used to reset ball position
    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float launchSpeed = 100;

    [Range(0f, 90f)]
    public float deflectionAngle = 90;

   public BlockManager blockmanager;
    void Start()
    {
        startPosition=transform.position;
        LaunchBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {

            Destroy(collision.gameObject);

            blockmanager.blocksDestroyed++;
            Debug.Log("Blocks Broken: " + blockmanager.blocksDestroyed);
        }

         if (collision.gameObject.CompareTag("Player"))
        {
            HitRacket(collision.collider);


        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Void"))
        {

            LaunchBall();
            
        }





    }








    void OnValidate()
    {

        if (body == null) body = GetComponent<Rigidbody2D>();


    }


    void LaunchBall()
    {
       body.MovePosition(startPosition);
     
        body.linearVelocity = Vector2.up * launchSpeed;


    }

    

    void HitRacket(Collider2D racket)
    {
        float relativex = this.transform.position.x - racket.transform.position.x;
        float boundWith = racket.bounds.size.x;
        float boundsWidthHalf = boundWith / 2;

        float percentage = relativex / boundsWidthHalf;

        float angleDegrees = percentage * deflectionAngle;

        float angleRAd = angleDegrees * Mathf.Deg2Rad;


        Vector2 deflectDirection = new Vector2(Mathf.Sin(angleRAd), Mathf.Cos(angleRAd));

        Vector2 direction = body.linearVelocity.normalized * deflectDirection;

        direction.Normalize();

        body.linearVelocity = direction * body.linearVelocity.magnitude;


    }




}
