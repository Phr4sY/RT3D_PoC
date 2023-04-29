using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{


    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("BAM");
       // Debug.Log(col.gameObject.name);
    }
}
