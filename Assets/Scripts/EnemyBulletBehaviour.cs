using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    float time;
    public GameObject EnemyBullet;
    Transform RespawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        this.time = 0.0f;
        RespawnPoint = GameObject.Find("Field/StartPoint/Player1SpawnPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 3秒後に消滅
        this.time += Time.deltaTime;
        if (this.time > 3.0f)
        {
            Destroy(EnemyBullet);
        }
    }

    private void OnTriggerEnter(Collider other) {
        //壁に当たったら弾だけ消滅
        if (other.gameObject.tag == "Wall") {
            Destroy(EnemyBullet);
        }
        if (other.gameObject.tag == "Player") {
            Destroy(EnemyBullet);
            other.gameObject.transform.position = RespawnPoint.position;
        }
    } 
}
