using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);

        if (gameObject.CompareTag("Enemy Pick Up"))
        {
            transform.Rotate (new Vector3 (5, 50, 10) * Time.deltaTime);
        }
    }
}
