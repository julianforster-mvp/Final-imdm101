using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
    }

  
}
