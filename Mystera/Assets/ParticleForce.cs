using System.Collections;
using UnityEngine;

public class ParticleForce : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] bool isTrigger;
    [SerializeField] Vector3 kuvvet;
    Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim = other.transform.GetChild(0).GetComponent<Animator>();
            rb = other.GetComponent<Rigidbody>();
            isTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isTrigger && anim != null && rb != null)
        {
            anim.SetBool("fly", true);

            
            kuvvet = new Vector3(rb.velocity.x, Mathf.Sqrt(1 * 2f * Physics.gravity.magnitude), rb.velocity.z);
            rb.velocity = kuvvet;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && rb != null && anim != null)
        {
            anim.SetBool("fly", false);

            
            if (other.GetComponent<CharacterCont>().isGrounded)
            {
                rb = null;
                anim = null;
            }
        }
    }
}
