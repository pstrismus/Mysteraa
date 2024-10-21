using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterCont : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpHeight = 5f;

    float horizontal;
    float vertical;

    Rigidbody rb;

    public bool isGrounded;
    [SerializeField] float groundCheckDistance = 0.3f; 
    [SerializeField] Transform groundCheck; 
    [SerializeField] LayerMask groundMask; 
    Vector3 kuvvet;

    Animator anim;

    bool isjump;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGrounded();
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("jump", true);
        }    
    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;

        // Karakterin hareket hýzýný (hem yatay hem dikey) animasyona baðlayalým
        float movementMagnitude = new Vector2(horizontal, vertical).magnitude;
        anim.SetFloat("Blend", movementMagnitude);

        // Karakterin Rigidbody'sini hareket ettiriyoruz
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);

        // Eðer hareket ediyorsa (horizontal veya vertical bir girdi varsa)
        if (moveDirection != Vector3.zero)
        {
            // Karakterin modeli dönecek
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            gameObject.transform.GetChild(0).rotation = Quaternion.Slerp(gameObject.transform.GetChild(0).rotation, targetRotation, Time.deltaTime * 10f);
        }
    }



    public void Jump()
    {
        kuvvet = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * 2f * Physics.gravity.magnitude), rb.velocity.z);
        rb.velocity = kuvvet;
        isjump = true;
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
        
        if (!isGrounded)
        {
            if (!anim.GetBool("fly"))
            {
                anim.SetBool("jumpdown", true);
            }

            rb.velocity += Vector3.up * Physics.gravity.y * Time.deltaTime;
        }
        else if (isjump && isGrounded)
        {
            anim.SetBool("jump", false);
            isjump = false;
        }
        else if (isGrounded)
        {
            anim.SetBool("jumpdown", false);
        }
    }
}
