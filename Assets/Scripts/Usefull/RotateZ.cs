using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateZ : MonoBehaviour
{

    private Vector3 startRotation;
    private float actualRotation = 0.0f;
    public float rotateSpeed = 4.0f;
    // Use this for initialization
    void Start()
    {
        startRotation = new Vector3(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("start : " + startRotation);
        actualRotation += rotateSpeed;
        transform.rotation = Quaternion.Euler(actualRotation, startRotation.y, startRotation.z);
    }
}
