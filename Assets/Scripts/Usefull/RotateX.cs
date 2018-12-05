using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateX : MonoBehaviour
{

    private Vector3 startRotation;
    private float actualRotationX = 0.0f;
    public float rotateSpeedX = 4.0f;
    // Use this for initialization
    void Start()
    {
        startRotation = new Vector3(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actualRotationX += rotateSpeedX;
        transform.rotation = Quaternion.Euler(actualRotationX, startRotation.y, startRotation.z);
    }
}
