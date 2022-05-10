using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class playercontroller : MonoBehaviour
{

    //1- get the camera movement with the mouse
    [SerializeField] Transform playerCamera = null;

    //5-
    // regarding the rotational speed of the player => give inital value to the mouse sensitivity 
    [SerializeField] float mouseSensitivity = 3.5f; //can be adjusted in the editor based on your convenience 

    //7- regarding the camera pitch: cameraPitch => keeps track of the camera's current X rotation
    float cameraPitch = 0.0f; //means it begins looking directly forward 

    //11- 
    [SerializeField] bool lockCursor = true ;  //true by default in order to lock the cursor once the play mode is started


    //player speed
    [SerializeField] float playerSpeed = 2.0f;

    //player jump distance
    [SerializeField] float jumpHeight = 1.0f;

    //player Gravity
    [SerializeField] float gravityValue = 9.8f;

    private float verticalVelocity;
    private float groundedTimer;        // to allow jumping when going down ramps


    //bool isGrounded;
    //private Vector3 velocity;

    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {         
        controller = GetComponent<CharacterController>();

        //12- check if the lockCursor boolean is true
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked; //locks the cursor to the center of the screen
            Cursor.visible = false; //invisible cursor 
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    //2- 
    //regarding all the mouse look functionality
    void UpdateMouseLook()
    {
        //3-
        //retrieving the horizontal & vertical axes of the mouse 
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));     

        //4- 
        //connecting the mouse horizontal motion with the camera(player) movement 
        // up => (0,1,0)
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity); //6- * by the mouse sensitivity value to adjust the rotation speed

        //8- we want the mouse delta to influence the camera pitch
        cameraPitch -= mouseDelta.y * mouseSensitivity; // subtract in order to have the inverse of the delta

        //9- clamping the value of cameraPitch between 90 & -90 only to prevent the upside down flipping
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        //10- setting the camera's euler angles to rotate around the x axis by cameraPitch value
        playerCamera.localEulerAngles = Vector3.right * cameraPitch; // right => (1,0,0)

    }

    /* Task: a- Movement of the player => walking around with [WASD] keys 
             b- Gravity Control => overcoming the floating issue
             c- Jumping functionality */
    

    void UpdateMovement()
    {


     bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            // cooldown interval to allow reliable jumping even when coming down ramps
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }
 
        // slam into the ground
        if (groundedPlayer && verticalVelocity < 0)
        {
            // hit ground
            verticalVelocity = 0f;
        }
 
        // apply gravity always, to let us track down ramps properly
        verticalVelocity -= gravityValue * Time.deltaTime;
 
        // gets keyboard input
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical"));
        
        // allow jump as long as the player is on the ground
        if (Input.GetKey("right ctrl")|| Input.GetKey("left ctrl"))
        {
        // scale by speed *2 (running)
        move *= playerSpeed*2;
        }else{
        // scale by speed (walking)
        move *= playerSpeed;
        }
 
        // allow jump as long as the player is on the ground
        if (Input.GetKey("space"))
        {
            // must have been grounded recently to allow jump
            if (groundedTimer > 0)
            {
                // no more until we recontact ground
                groundedTimer = 0;
 
                // Physics dynamics formula for calculating jump up velocity based on height and gravity
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
            }
        }
        
        // inject Y velocity before we use it
        move.y = verticalVelocity;
        
 
        // call .Move() once only (muliply with camera to make characther move in the camera direction)
        controller.Move(Quaternion.Euler (0, playerCamera.transform.eulerAngles.y, 0)*move * Time.deltaTime);


//##################
//V2 fails to jump and move in the same time (due to calling move twice)
        /*
        if(controller.isGrounded){velocity.y=0;}
        // for jump if characther on ground and space is clicked
        if (Input.GetKey("space")&& controller.isGrounded)
        {
        //sets velocity y to the jump velocity
        velocity.y = jumpVelocity;
        }
        else
        {
        //decreases the velocity of y if postive and goes to - (to go to the ground)
        velocity.y += Gravity * Time.deltaTime;
        }
        
        controller.Move(velocity * Time.deltaTime);


        // for movement
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float vertical = Input.GetAxis("Vertical") *moveSpeed;
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);
        */

        
        //##################
        //V1 had problems with jumping due to not using characther controller
/*
        if (Input.GetKey("w"))
        {
            //Debug.Log("forward");
            //forward (0, 0, 1).
            transform.Translate(Vector3.forward * moveSpeed* Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            //Debug.Log("back");
            //back (0, 0, -1).
            transform.Translate(Vector3.back * moveSpeed* Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            //Debug.Log("right");
            //right (1, 0, 0).
            transform.Translate(Vector3.right * moveSpeed* Time.deltaTime);
        }
        
        if (Input.GetKey("a"))
        {
            //Debug.Log("left");
            //left (-1, 0, 0).
            transform.Translate(Vector3.left * moveSpeed* Time.deltaTime);
        }
*/
  
    }
}
