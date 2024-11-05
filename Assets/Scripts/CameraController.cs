using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraController : MonoBehaviour
{
    private int currentLevel = 0;
    private float[] limit = {0,0};
    ConstraintSource source;
    private float height = 0;
    public GameObject player;
    RotationConstraint rotationConstraint;
    // Start is called before the first frame update
    void Start()
    {
        rotationConstraint = gameObject.GetComponent<RotationConstraint>();
        source = rotationConstraint.GetSource(0);
        switch (currentLevel)
        {
            case 0:
                SetCameraLimits(30, 10, 0);
                break;
            case 1:
                SetCameraLimits(-10, -50, 25);
                break;
            case 2:
                SetCameraLimits(-70, -110, 50);
                break;
            case 3:
                SetCameraLimits(-130, -170, 75);
                break;
            case 4:
                SetCameraLimits(-190, -230, 100);
                break;
            case 5:
                SetCameraLimits(-250, -290, 125);
                break;
        }
        //Debug.Log(limit[0] + " " + limit[1]);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.transform.eulerAngles);
        if (
            player.transform.eulerAngles.y <= 30 && player.transform.eulerAngles.y >= 10
            //true
            )
        {
            transform.rotation = player.transform.rotation;
        }
    }

    void SetCameraLimits(float l, float r, float h)
    {
        height = h;
        limit[0] = l;
        limit[1] = r;
    }
}
