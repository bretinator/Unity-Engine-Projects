using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotater : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        float currentYEulerRot = transform.eulerAngles.y;
        float newYEulerRot = currentYEulerRot += rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newYEulerRot, transform.eulerAngles.z);
    }
}
