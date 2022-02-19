using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChargeControl : MonoBehaviour
{
    private ChargedObject charge;

    private void Start()
    {
        charge = gameObject.GetComponent<ChargedObject>();
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
}