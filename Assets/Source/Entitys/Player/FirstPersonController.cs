using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require a character controller to be attached to the same game object
[AddComponentMenu("PersonController/ FirstPersonController")]
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour 
{
    public bool isControllable = true;
    public float moveSpeed = 6.0f;
    public float gravity = 20.0f;
    static float runSpeed = 0;
    public float jumpHeight = 0.5f;

    public float sensitivityX = 15F;

    public bool canJump = true;

    public float inAirControlAcceleration = 3.0f;

    enum CharacterState
    {
        Idle = 0,
        Walking = 1,
        Trotting = 2,
        Running = 3,
        Jumping = 4,
    }
    private CharacterState _characterState;
    private Vector3 inAirVelocity = Vector3.zero;
    // The current move direction in x-z
    private Vector3 moveDirection = Vector3.zero;
    // The current vertical speed
    private float verticalSpeed = 0.0f;
    CharacterController controller;
    // The last collision flags returned from controller.Move
    private CollisionFlags collisionFlags;
    private float rotationX = 0F;
    // Last time the jump button was clicked down
    private float lastJumpButtonTime = -10.0f;
    // Last time we performed a jump
    private float lastJumpTime = -1.0f;
    
    private float jumpRepeatTime = 0.05f;
    private float jumpTimeout = 0.15f;
    private float groundedTimeout = 0.25f;
    // Are we jumping? (Initiated with jump button and not grounded yet)
    private bool jumping = false;
    private bool jumpingReachedApex = false;
    // the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
    private float lastJumpStartHeight = 0.0f;

    void Start()
    { 
      
    }

    void Update()
    {
        if (!isControllable)
        {
            // kill all inputs if not controllable.
            Input.ResetInputAxes();
        }
        ApplyJumping();

        ApplyGravity();

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        
        // Calculate actual motion
        var movement = moveDirection * moveSpeed + new Vector3(0, verticalSpeed, 0) + inAirVelocity;
        movement *= Time.deltaTime;
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        // Move the controller
        CharacterController controller = GetComponent<CharacterController>();
        transform.eulerAngles = new Vector3(0, rotationX, 0);
        collisionFlags = controller.Move(movement);
    }

    void ApplyJumping()
    {
        // Prevent jumping too fast after each other
        if (lastJumpTime + jumpRepeatTime > Time.time)
            return;

        if (IsGrounded())
        {
            // Jump
            // - Only when pressing the button down
            // - With a timeout so you can press the button slightly before landing		
            if (canJump && Time.time < lastJumpButtonTime + jumpTimeout)
            {
                verticalSpeed = CalculateJumpVerticalSpeed(jumpHeight);
                SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void DidJump()
    {
        jumping = true;
        jumpingReachedApex = false;
        lastJumpTime = Time.time;
        lastJumpStartHeight = transform.position.y;
        lastJumpButtonTime = -10;

        _characterState = CharacterState.Jumping;
    }

    void ApplyGravity()
    {
        if (isControllable)	// don't move player at all if not controllable.
        {
            // Apply gravity
            var jumpButton = Input.GetButton("Jump");


            // When we reach the apex of the jump we send out a message
            if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0)
            {
                jumpingReachedApex = true;
                SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
            }

            if (IsGrounded())
                verticalSpeed = 0.0f;
            else
                verticalSpeed -= gravity * Time.deltaTime;
        }
    }

    float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * targetJumpHeight * gravity);
    }

    bool IsGrounded()
    {
        return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
    }

    // приседание
    void Crouch(bool arg) 
    {
        if(arg == true) 
        {
            controller.height -= 1 * Time.deltaTime;
            //transform.position = NewPos;  // Загружаем позицию
        }
        else
         {
             controller.height += 1 * Time.deltaTime; ; // Тут добавляем к значению "height" 2 единицы
             //transform.position = NewPos;  // Загружаем позицию
        }
    }

}
