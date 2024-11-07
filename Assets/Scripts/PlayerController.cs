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
    public float jumpSpeedDivisionFactor = 3;
    bool jump = false;
    bool onGround = true;

    LayerMask layerMask;
    public Transform bodyTransform;
    Rigidbody rb;

    void Start()
    {
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
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(bodyTransform.position, bodyTransform.TransformDirection(Vector3.down));

        onGround = Physics.Raycast(ray, out hit, rayLength, layerMask);

        if (onGround && Input.GetKeyDown(KeyCode.Space) && !jump) jump = true;

        //Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red, 1000f);
        //print(Physics.Raycast(ray, out hit, rayLength, layerMask));

        if (Input.GetAxis("Horizontal") == 0) currentSpeed = 0;

        currentSpeed = onGround ? currentSpeed + 0.03f : currentSpeed - 0.06f;
        currentSpeed = Mathf.Clamp(currentSpeed, speed / jumpSpeedDivisionFactor, speed);

        transform.Rotate(new Vector3(0, -currentSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0));
    }
}
