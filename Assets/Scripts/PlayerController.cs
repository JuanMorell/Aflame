using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    float horizontal;

    public float rayLength = 1;
    public float jumpStrength = 10.0f;
    float initZAxisPosition;
    public float jumpSpeedDivisionFactor = 4;
    public float gravityAccelerationFactor = 20;
    bool jump = false;
    bool onGround = true;
    bool againstWall = false;

    LayerMask layerMask;
    public Transform bodyTransform;
    public Transform centralTransform;
    Rigidbody rb;

    void Start()
    {
        initZAxisPosition = bodyTransform.localPosition.z;
        layerMask = LayerMask.GetMask("Terrain");
        rb = GetComponent<Rigidbody>();
        //rb = transform.GetChild(0).GetComponent<Rigidbody>();
        //print(rb.gameObject.name);
    }

    void FixedUpdate()
    {
        if (jump)
        {
            rb.AddForce(Vector3.up * jumpStrength * 100, ForceMode.Impulse);
            jump = false;
        }

        
        if (!onGround) 
            rb.AddForce(Vector3.down * (jumpStrength / jumpSpeedDivisionFactor) * 100, ForceMode.Acceleration);
        

        rb.AddForce(speed * horizontal * bodyTransform.TransformDirection(Vector3.right), ForceMode.VelocityChange);
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (bodyTransform.localPosition.z != initZAxisPosition)
            bodyTransform.localPosition = new Vector3(bodyTransform.localPosition.x, bodyTransform.localPosition.y, initZAxisPosition);

        RaycastHit hit;
        Ray ray = new Ray(bodyTransform.position, bodyTransform.TransformDirection(Vector3.down));

        onGround = Physics.Raycast(ray, out hit, rayLength, layerMask);

        if (onGround && Input.GetKeyDown(KeyCode.Space) && !jump) jump = true;

        bodyTransform.LookAt(new Vector3(250, bodyTransform.position.y, 250));
    }

    //void OnCollisionStay(Collision other)
    //{
    //    onGround = other.gameObject.CompareTag("Terrain");
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    onGround = !onGround;
    //}
}
