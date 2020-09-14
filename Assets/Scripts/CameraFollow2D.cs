using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;

    public float smoothspeed = 0.125f;

    private void LateUpdate()
    {
        transform.position = target.position;
    }
}
