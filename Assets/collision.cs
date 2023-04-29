using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("BAMMMMMMMM");
        Destroy(col.gameObject);
    }
}
