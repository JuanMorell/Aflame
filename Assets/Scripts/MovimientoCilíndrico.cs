using UnityEngine;

public class MovimientoCilíndrico : MonoBehaviour
{
    public Transform centro; // El centro del cilindro
    public float radio = 5f; // Distancia al centro
    public float velocidadAngular = 10f; // Velocidad angular de movimiento
    public float altura = 0f; // Mantener la altura constante en el eje Y

    private Rigidbody rb;

    void Start()
    {
        radio = Vector3.Distance(centro.position, transform.position);
        // Asegurarse de que el Rigidbody esté presente
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento angular (rotación alrededor del centro)
        float angulo = velocidadAngular * Time.deltaTime;
        Vector3 direccionMovimiento = new Vector3(Mathf.Sin(angulo), 0f, Mathf.Cos(angulo));

        // Calcular la nueva posición en coordenadas cilíndricas
        Vector3 nuevaPosicion = centro.position + new Vector3(direccionMovimiento.x * radio, altura, direccionMovimiento.z * radio);

        // Aplicar la nueva posición en el Rigidbody
        if (rb != null)
        {
            rb.MovePosition(nuevaPosicion);
        }
        else
        {
            transform.position = nuevaPosicion;
        }
    }
}