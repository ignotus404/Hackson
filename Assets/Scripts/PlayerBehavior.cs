using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    Transform RespawnPoint;
    public GameObject bulletPrefab;
    public Transform Camera;
    // Start is called before the first frame update
    void Start()
    {
        RespawnPoint = GameObject.Find("Field/StartPoint/Player1SpawnPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //弾を撃つ
        if(Mouse.current.leftButton.wasPressedThisFrame) {
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;
            bullet.transform.position = Camera.transform.position + Camera.transform.forward - Camera.transform.up;
            bullet.transform.forward = Camera.transform.forward + Camera.transform.up*0.1f;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        //敵に触れたらor敵に撃たれたらリスポーン
        if (hit.gameObject.tag == "Enemy" || hit.gameObject.tag == "EnemyAttack") {
            transform.position = RespawnPoint.position;
        }
    }
}
