using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
[SerializeField] private Transform Target;

    private void FixedUpdate()
    {
        transform.LookAt(Target);
    }
}
