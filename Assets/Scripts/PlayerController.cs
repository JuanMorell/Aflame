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
    float jumpTimer = 0;
    float horizontal;
    bool jump = false;
    bool onGround = true;
    public Transform bodyTransform;
    LayerMask layerMask;
    Rigidbody rb;

    void Start()
    {
        layerMask = LayerMask.GetMask("Terrain");
        rb = transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Si el jugador ha pulsado el botón de salto durante el buffer de salto, se aplica un vector vertical a la velocidad de Espelma
        if (jump)
        {
            rb.velocity = Vector3.up * jumpStrength;
            jump = false;
        }

        //Una vez Espelma empieza a caer tras el salto, aumentamos la fuerza de gravedad
        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (gravityAcceleration - 1) * Time.deltaTime;

        //Usamos el input horizontal para aplicar un cambio de velocidad a Espelma
        rb.AddRelativeForce(speed * horizontal * Vector3.right, ForceMode.VelocityChange);

        if (rb.velocity.y > 0)
            print("UP");
        else if (rb.velocity.y < 0)
            print("DOWN");
    }

    void Update()
    {
        //Cogiendo el input horizontal
        horizontal = Input.GetAxis("Horizontal");

        //Creando un raycast hacia abajo para detectar si estamos en tierra
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        onGround = Physics.Raycast(ray, out hit, rayLength, layerMask);

        //Aplicamos un buffer al salto para ver si se ha pulsado el botón de salto antes de tocar el sielo
        jumpTimer = Input.GetKeyDown(KeyCode.Space) ? jumpBuffer : jumpTimer - Time.deltaTime;
        jump = onGround && jumpTimer > 0 && !jump;

        //Cambiamos la dirección de Espelma al moverla en la dirección contraria
        bodyTransform.localEulerAngles = horizontal == 0 ? bodyTransform.localEulerAngles : horizontal > 0 ? Vector3.zero : new Vector3(0, 180, 0);
    }
}
        //Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
