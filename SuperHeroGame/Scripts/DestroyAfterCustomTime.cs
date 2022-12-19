using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Destroys an object after a hardcoded amount of time.
 * This program is only used to destroy the laser beams.
 * Bret Shepard
*/

public class DestroyAfterCustomTime : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }
}
