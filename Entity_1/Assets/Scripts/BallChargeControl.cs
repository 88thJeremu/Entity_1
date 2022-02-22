using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChargeControl : MonoBehaviour
{
    private ChargedObject charge;
    private Rigidbody rb;

    private void Start()
    {
        charge = gameObject.GetComponent<ChargedObject>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && charge != null)
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
}