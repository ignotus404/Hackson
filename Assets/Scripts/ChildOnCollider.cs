using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildOnCollider : MonoBehaviour
{
    public bool isWall = false;
 
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Debug.Log("壁に当たった");
            isWall = true;
        }
    }
}
