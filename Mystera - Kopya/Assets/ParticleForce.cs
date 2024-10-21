using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleForce : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]bool isTriger;
    [SerializeField] Vector3 kuvvet;
    Animator anim;
    private void OnTriggerEnter(Collider other)
    { 
        
        if (other.tag == "Player")
        {
            anim = other.transform.GetChild(0).GetComponent<Animator>();
            rb = other.GetComponent<Rigidbody>();
            isTriger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTriger = false;
        }
    }

    void Update()
    {
        StartCoroutine(bekle());
    }
    IEnumerator bekle()
    {
        if (isTriger && rb != null)
        {
            yield return new WaitForSeconds(0.1f);
            if (!anim.GetBool("fly"))
            {
                anim.SetBool("fly", true);
            }  
            kuvvet = new Vector3(rb.velocity.x, Mathf.Sqrt(1 * 2f * Physics.gravity.magnitude), rb.velocity.z);
            rb.velocity = kuvvet;

        }
        else if (!isTriger && rb != null)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * Time.deltaTime;
            if (rb.gameObject.GetComponent<CharacterCont>().isGrounded)
            {
                rb = null;
                anim = null;
            }
            else
            {
                anim.SetBool("fly", false);
            }
            
        }
    }
}
