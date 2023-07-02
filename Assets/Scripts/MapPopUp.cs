using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPopUp : MonoBehaviour
{
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        
        map.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Map")){
            map.SetActive(true);
        }

        else
        {
            map.SetActive(false);   
        }
    }
}
