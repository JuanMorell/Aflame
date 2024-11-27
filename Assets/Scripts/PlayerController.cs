using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float rayLength = 1;
    public float gravityAcceleration = 0;
    public float jumpStrength = 0;
    public float jumpBuffer = 0;
    public Transform body;
    public Transform head;

    float horizontal;
    float vertical;
    float jumpTimer = 0;
    float coyoteTimer = 0;
    bool jump = false;
    LayerMask layerMask;
    Rigidbody rb;

    void Start()
    {
        layerMask = LayerMask.GetMask("Terrain");
        rb = transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //SALTAMOS SI SE HA PULSADO EL BOT�N A TIEMPO
        if (jump)
        {
            jumpTimer = 0;
            coyoteTimer = 0;
            rb.velocity = Vector3.up * jumpStrength;
        }

        //AUMENTAMOS LA GRAVEDAD DURANTE LA CA�DA
        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (gravityAcceleration - 1) * Time.deltaTime;

        //NOS MOVEMOS CON EL INPUT HORIZONTAL
        rb.AddRelativeForce(speed * horizontal * Vector3.right, ForceMode.VelocityChange);
    }

    void Update()
    {
        //COGEMOS EL INPUT
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //CREAMOS UN RAYCAST PARA DETECTAR EL TERRENO
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        //APLICAMOS UN BUFFER Y TIEMPO DE COYOTE AL SALTO
        jumpTimer = Input.GetKeyDown(KeyCode.Space) ? jumpBuffer : jumpTimer - Time.deltaTime;
        coyoteTimer = Physics.Raycast(ray, out hit, rayLength, layerMask) ? jumpBuffer : coyoteTimer - Time.deltaTime;
        jump = jumpTimer > 0 && coyoteTimer > 0;

        //CAMBIAMOS LA DIRECCI�N DEL JUGADOR SEG�N SU DIRECCI�N DE MOVIMIENTO
        body.localEulerAngles = horizontal == 0 ? body.localEulerAngles : horizontal > 0 ? Vector3.zero : new Vector3(0, 180, 0);

        //head.localEulerAngles = vertical >= 0.5f ? new Vector3(0, 0, 50) : Vector3.zero;
        //head.localPosition = vertical <= 0.5f ? new Vector3(1, 0.5f, 1) : Vector3.one;
        //body.localScale = vertical <= 0.5f ? new Vector3 (1, 0.5f, 1) : Vector3.one;

        if (vertical == 0)
        {
            head.localEulerAngles = Vector3.zero;
            head.localPosition = new Vector3(0, 2, 0);
            body.localScale = Vector3.one;
        }
        else if (vertical > 0) {
            head.localEulerAngles = new Vector3(0, 0, 50);
        } else if (vertical < 0) {
            head.localPosition = new Vector3 (0, 1.2f, 0);
            body.localScale = new Vector3(1.25f, 0.5f, 1.25f);
        }
    }
}
