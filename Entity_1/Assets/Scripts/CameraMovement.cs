using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject ball;
    void Update()
    {
        transform.position = ball.transform.position + new Vector3(0, 2.0f, -7.5f);
    }
}
