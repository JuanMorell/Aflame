using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    float currentSpeed = 0;

    public float rayLength = 1;
    public float jumpStrength = 10.0f;
    float initZAxisPosition;
    //public float jumpSpeedDivisionFactor = 3;
    public float gravityAccelerationFactor = 20;
    bool jump = false;
    bool onGround = true;

    LayerMask layerMask;
    public Transform bodyTransform;
    Rigidbody rb;

    void Start()
    {
        initZAxisPosition = bodyTransform.localPosition.z;
        layerMask = LayerMask.GetMask("Terrain");
        rb = transform.GetChild(0).GetComponent<Rigidbody>();
        print(rb.gameObject.name);
    }

    void FixedUpdate()
    {
        if (jump)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            jump = false;
        }

        
        if (!onGround 
            //&& rb.velocity.y <= 0
            ) 
        { 
            //print(rb.velocity.y);
            rb.AddForce(Vector3.down*100, ForceMode.Acceleration);
        }
    }

    void Update()
    {
        if (bodyTransform.localPosition.z != initZAxisPosition)
            bodyTransform.localPosition = new Vector3(bodyTransform.localPosition.x, bodyTransform.localPosition.y, initZAxisPosition);


        RaycastHit hit;
        Ray ray = new Ray(bodyTransform.position, bodyTransform.TransformDirection(Vector3.down));

        onGround = Physics.Raycast(ray, out hit, rayLength, layerMask);

        if (onGround && Input.GetKeyDown(KeyCode.Space) && !jump) jump = true;

        //Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red, 1000f);
        //print(Physics.Raycast(ray, out hit, rayLength, layerMask));

        //if (Input.GetAxis("Horizontal") == 0) currentSpeed = 0;

        //currentSpeed = onGround ? currentSpeed + 0.03f : currentSpeed - 0.06f;
        //currentSpeed = Mathf.Clamp(currentSpeed, speed / jumpSpeedDivisionFactor, speed);

        transform.Rotate(new Vector3(0, -speed * Time.deltaTime * Input.GetAxis("Horizontal"), 0));
        //transform.Rotate(new Vector3(0, -currentSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0));

    }
}
