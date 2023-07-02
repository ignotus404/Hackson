using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCbehavior : MonoBehaviour
{
    public float speed = 3.0f;
    int num;
    float time;

    public SphereCollider sC;

    void Start()
    {
        this.time = 0.0f;
    }

    void Update()
    {
        // 3秒後に前進 開始時の処理落ち中に壁を抜けるバグがあったため遅延をかけた
        this.time += Time.deltaTime;
        if (this.time > 3.0f)
        {
            // 前進
            this.transform.position += sC.gameObject.transform.forward * speed * Time.deltaTime;
        }

        // 90度回転
        if (sC.gameObject.GetComponent<ChildOnCollider>().isWall)
        {
            //回転方向のランダム化
            int num = Random.Range(0, 3);
            switch (num)
            {
                default:
                    break;

                case 0:
                    this.transform.Rotate(0, 90, 0);
                    break;

                case 1:
                    this.transform.Rotate(0, -90, 0);
                    break;

                case 2:
                    this.transform.Rotate(0, 180, 0);
                    break;
            }

            // 壁に当たったフラグを戻す
            sC.gameObject.GetComponent<ChildOnCollider>().isWall = false;
        }
    }
}
