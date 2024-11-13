using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private Transform moveDirectionOffset;

    public float speed = 6.0f;
    public float rayLength = 1;
    public float jumpStrength = 10.0f;
    public float jumpSpeedDivisionFactor = 4;

    float horizontal;
    float initZAxisPosition;
    float radius;

    Vector3 initPos;

    bool jump = false;
    bool onGround = true;
    bool againstWall = false;

    public Transform bodyTransform;
    //public Vector2 worldCenter;
    LayerMask layerMask;
    Rigidbody rb;

    void Start()
    {
        radius = Vector3.Distance(transform.position, new Vector3(center.position.x, transform.position.y, transform.position.z));
        initPos = transform.position;
        initZAxisPosition = transform.localPosition.z;
        layerMask = LayerMask.GetMask("Terrain");
        rb = transform.GetComponent<Rigidbody>();
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

        rb.AddRelativeForce(speed * horizontal * Vector3.right, ForceMode.VelocityChange);
        //rb.AddForce(speed * horizontal * transform.TransformDirection(Vector3.right), ForceMode.VelocityChange);

    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        //if (transform.localPosition.z != initZAxisPosition)
        //    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, initZAxisPosition);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        onGround = Physics.Raycast(ray, out hit, rayLength, layerMask);

        if (onGround && Input.GetKeyDown(KeyCode.Space) && !jump) jump = true;

        transform.LookAt(new Vector3(center.position.x, transform.position.y, center.position.z));
        bodyTransform.localEulerAngles = horizontal == 0 ? bodyTransform.localEulerAngles : horizontal > 0 ? Vector3.zero : new Vector3(0, 180, 0);

        var allowedPos = transform.position - initPos;
        allowedPos = Vector3.ClampMagnitude(allowedPos, radius);
        transform.position = initPos + allowedPos;
    }

    void LateUpdate()
    {


    }
}
