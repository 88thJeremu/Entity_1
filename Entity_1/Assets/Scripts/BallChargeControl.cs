using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallChargeControl : MonoBehaviour
{
    private ChargedObject charge;
    private Rigidbody rb;
    private Vector3 checkpoint;
    private void Start()
    {
        charge = gameObject.GetComponent<ChargedObject>();
        rb = gameObject.GetComponent<Rigidbody>();
        checkpoint = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && charge != null)
        {
            charge.charge *= -1;
            charge.UpdateAppearance();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Magnetized"))
        {
            rb.useGravity = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Magnetized"))
        {
            rb.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathBox"))
        {
            transform.position = checkpoint;
            rb.velocity = Vector3.zero;
        }
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkpoint = other.transform.position;
        }
    }
}