using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScraperMover : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 startPos;
    public float endPosZ;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (transform.position.z <= endPosZ)
        {
            transform.position = startPos;
        }
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
