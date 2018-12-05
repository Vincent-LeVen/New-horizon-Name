using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{

    private Vector3 startRotation;
    private float actualRotationZ = 0.0f;
    public float rotateSpeedZ = 4.0f;
    // Use this for initialization
    void Start()
    {
        startRotation = new Vector3(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actualRotationZ += rotateSpeedZ;
        transform.rotation = Quaternion.Euler(startRotation.x, actualRotationZ, startRotation.z);
    }
}
