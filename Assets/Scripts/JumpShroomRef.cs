using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpShroomRef : MonoBehaviour
{
    Vector3 ogScale;
    Color ogColor;
    [HideInInspector]
    public float moisture = 1;
    public GameObject boletus;
    public GetItemRef getItemRef;
    public RainDetection rainDetection;

    // Start is called before the first frame update
    void Start()
    {
        ogScale = transform.localScale;
        ogColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(rainDetection.underRain && moisture < 1.5f)
        {
            moisture += Time.deltaTime;
        }

        transform.localScale = ogScale * moisture;
        Color newColor = new Color(ogColor.r + (moisture / 2.55f), ogColor.g, ogColor.b);
        GetComponent<Renderer>().material.color = newColor;
        boletus.GetComponent<Renderer>().material.color = newColor;
    }

    void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = false;

    }
}
