using UnityEngine;

public class Racket : MonoBehaviour
{

    public Rigidbody2D body;
    //How fast we move horizontally in pixels per second
    public float speed = 100; //in px

    public SpriteRenderer racketLeft;
    public SpriteRenderer racketCentre;
    public SpriteRenderer racketRight;
    public BoxCollider2D boxcollider2D;



    void AutoSizeRacket()
    {
        float width = racketLeft.bounds.size.x + racketRight.bounds.size.x + racketCentre.bounds.size.x;
        float height = boxcollider2D.bounds.size.y;
        //Set position of left and right segments

        
        
        //Change size of collider

        boxcollider2D.size = new Vector2(width, height);


        float leftOffsetX = racketCentre.bounds.extents.x + racketLeft.bounds.extents.x;
        float rightOffsetX = racketCentre.bounds.extents.x + racketRight.bounds.extents.x;
        racketLeft.transform.position = racketCentre.transform.position + new Vector3(-leftOffsetX, 0, 0);
        racketRight.transform.position = racketCentre.transform.position + new Vector3(rightOffsetX, 0, 0);



    }

    private void Start()
    {
        
        if (!Input.gyro.enabled)
        {


            Input.gyro.enabled = true;
        }


    }




    void OnValidate()
    {
        AutoSizeRacket();
        //get rigidbody if it is null 
        if (body==null)      body = GetComponent<Rigidbody2D>();
        if (boxcollider2D==null) boxcollider2D= GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Move player horizontally
        float moveX = Input.GetAxis("Horizontal") * speed;
  

        float screenCentre = Screen.width / 2;

        for (int i = 0; i< Input.touchCount; i++)
        {

            Touch touch = Input.GetTouch(i);


            if (touch.position.x <  screenCentre)
            {

                moveX -= speed;


            }
            else
            {


                moveX += speed;

            }





        }

        Quaternion gyroQuat = Input.gyro.attitude;

        Vector3 euler = gyroQuat.eulerAngles;


        float z = euler.z;

        Debug.Log($"Orientation: {euler}");


        if (z > 180)
        {

            z -*+= 360;


        }


        moveX += z / 60f * speed;



        body.linearVelocityX = moveX;

    }
}
