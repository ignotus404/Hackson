using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XSkillbulletBehavior : MonoBehaviour
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
        // 15秒後に消滅
        this.time += Time.deltaTime;
        if (this.time > 15.0f)
        {
            Destroy(Bullet);
        }
    }

    private void OnTriggerEnter(Collider other) {
        //敵に当たったら敵ごと消滅
        if (other.gameObject.tag == "Enemy") {
            Destroy(other.gameObject);
            Destroy(Bullet);
        }
        //壁に当たったら壁も消滅
        else if (other.gameObject.tag == "Wall") {
            Destroy(Bullet);
            Destroy(other.gameObject);
        }
        //壁に当たったら弾だけ消滅
        else if(other.gameObject.tag == "UndestroyableWall")
        {
            Destroy(Bullet);
        }
    } 
}
