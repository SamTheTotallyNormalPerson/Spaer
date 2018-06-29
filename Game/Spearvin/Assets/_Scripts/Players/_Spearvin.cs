using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class _Spearvin : MonoBehaviour
{

    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0, 1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    Animator anim;
    Transform cameraT;
    CharacterController controller;

    public GameObject Punch;

    public float Butt;

    public float bost = 1f;

    public bool IsTalking;

    public int Health;

    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        Punch.SetActive(false);
        IsTalking = true;
       
    }

    void Update()
    {

        if (Health <= 1)
        {
            IsTalking = true;
        }

        if (IsTalking == true)
        {
            // input
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;
            bool running = Input.GetKey(KeyCode.LeftShift);

            Move(inputDir, running);




            if (Input.GetButtonDown("Fire1"))
            {
                Jump();
                FindObjectOfType<AudioManager>().Play("Spearvin noise 3");
            }


            if (Input.GetButtonDown("Fire3") && controller.isGrounded)
            {
                Punch.SetActive(true);
                Invoke("Punchy", Butt);
                controller.transform.position += transform.forward * bost;
                FindObjectOfType<AudioManager>().Play("Swosh");
                FindObjectOfType<AudioManager>().Play("Spearvin noise 1");
                anim.SetTrigger("Punch");

            }

            if (Input.GetButtonDown("Fire3") && !controller.isGrounded)
            {
                Punch.SetActive(true);
                Invoke("Punchy", Butt);
                controller.transform.position += transform.up * bost;
                FindObjectOfType<AudioManager>().Play("Swosh");
                FindObjectOfType<AudioManager>().Play("Spearvin noise 2");
                anim.SetTrigger("AirJump");
            }
        }

        if (Health == 0)
        {
            IsTalking = false;
        }

    }

    void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
            anim.SetBool("Jump", false);
        }

        if (!controller.isGrounded)
        {
            anim.SetBool("Jump", true);
        }

        if (inputDir.sqrMagnitude > 0)
        {


            anim.SetBool("Run", true);

        }
        else
        {
            anim.SetBool("Run", false);
        }

    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }

    void Punchy()
    {
        Punch.SetActive(false);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "NPC" && Input.GetButtonDown("Fire4"))
        {
            IsTalking = false;
        }
    }
}