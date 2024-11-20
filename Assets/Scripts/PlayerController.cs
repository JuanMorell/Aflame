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
    public Transform bodyTransform;

    float horizontal;
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
        //SALTAMOS SI SE HA PULSADO EL BOTÓN A TIEMPO
        if (jump)
        {
            jumpTimer = 0;
            coyoteTimer = 0;
            rb.velocity = Vector3.up * jumpStrength;
        }

        //AUMENTAMOS LA GRAVEDAD DURANTE LA CAÍDA
        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (gravityAcceleration - 1) * Time.deltaTime;

        //NOS MOVEMOS CON EL INPUT HORIZONTAL
        rb.AddRelativeForce(speed * horizontal * Vector3.right, ForceMode.VelocityChange);
    }

    void Update()
    {
        //COGEMOS EL INPUT HORIZONTAL
        horizontal = Input.GetAxis("Horizontal");

        //CREAMOS UN RAYCAST PARA DETECTAR EL TERRENO
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        //APLICAMOS UN BUFFER Y TIEMPO DE COYOTE AL SALTO
        jumpTimer = Input.GetKeyDown(KeyCode.Space) ? jumpBuffer : jumpTimer - Time.deltaTime;
        coyoteTimer = Physics.Raycast(ray, out hit, rayLength, layerMask) ? jumpBuffer : coyoteTimer - Time.deltaTime;
        jump = jumpTimer > 0 && coyoteTimer > 0;

        //CAMBIAMOS LA DIRECCIÓN DEL JUGADOR SEGÚN SU DIRECCIÓN DE MOVIMIENTO
        bodyTransform.localEulerAngles = horizontal == 0 ? bodyTransform.localEulerAngles : horizontal > 0 ? Vector3.zero : new Vector3(0, 180, 0);
    }
}
