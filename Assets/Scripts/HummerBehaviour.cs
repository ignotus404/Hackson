using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerBehaviour : MonoBehaviour
{
    float time;
    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        this.time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 6秒後に消滅
        this.time += Time.deltaTime;
        if (this.time > 6.0f)
        {
            Destroy(Bullet);
        }
    }

    private void OnTriggerEnter(Collider other) {
        //敵に当たったら敵ごと消滅
        if (other.gameObject.tag == "Enemy") 
        {
            Destroy(other.gameObject);
            Destroy(Bullet);
        }
        //壁に当たったら壁ごと消滅
        if (other.gameObject.tag == "Wall") {
            Destroy(Bullet);
            Destroy(other.gameObject);
        }
    } 
}
