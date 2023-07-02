using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using StarterAssets;

public class PlayerBehavior : MonoBehaviour
{
    Transform RespawnPoint;
    public GameObject bulletPrefab;
    public GameObject HummerPrefab;
    public Transform Camera;

    public GameObject MiniMap;
    public GameObject HPText;
    int HP = 100;
    public GameObject ItemRunText;
    public int ItemRun = 2;
    public GameObject ItemMapText;
    public int ItemMap = 2;
    public GameObject ItemBreakText;
    public int ItemBreak = 2;
    public GameObject ItemBulletText;
    public int ItemBullet = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        MiniMap = GameObject.Find("UI/MiniMap");
        RespawnPoint = GameObject.Find("Field/StartPoint/Player1SpawnPoint").transform;
        MiniMap.SetActive(false);
        ItemBreakText = GameObject.Find("UI/ItemBreak");
        ItemRunText = GameObject.Find("UI/ItemRun");
        ItemMapText = GameObject.Find("UI/ItemMap");
        ItemBulletText = GameObject.Find("UI/ItemBullet");
    }

    // Update is called once per frame
    void Update()
    {

        //アイテムを表示
        ItemRunText.GetComponent<TextMeshProUGUI>().text = "" + ItemRun;
        ItemMapText.GetComponent<TextMeshProUGUI>().text = "" + ItemMap;
        ItemBreakText.GetComponent<TextMeshProUGUI>().text = "" + ItemBreak;

        //アイテムを使う
        if(Keyboard.current.zKey.wasPressedThisFrame) 
        {
            if(ItemRun > 0) //アイテムがあるとき
            {
                ItemRun--;
                GetComponent<FirstPersonController>().MoveSpeed = GetComponent<FirstPersonController>().MoveSpeed * 2;
            }
        }

        if(Keyboard.current.xKey.wasPressedThisFrame) 
        {
            if(ItemMap > 0) //アイテムがあるとき
            {
                ItemMap--;
                MiniMap.SetActive(true);
            }
        }

        if(Keyboard.current.cKey.wasPressedThisFrame) 
        {
            if(ItemBreak > 0) //アイテムがあるとき
            {
                ItemBreak--;
                GameObject hummer = Instantiate(HummerPrefab) as GameObject;
                hummer.transform.position = Camera.transform.position + Camera.transform.forward - Camera.transform.up;
                hummer.transform.forward = Camera.transform.forward + Camera.transform.up*0.1f;
            }
        }

        //弾を表示
        ItemBulletText.GetComponent<TextMeshProUGUI>().text = "" + ItemBullet;

        //弾を撃つ
        if(Mouse.current.leftButton.wasPressedThisFrame) {
            if(ItemBullet > 0) //弾があるとき
            {
                ItemBullet--;
                GameObject bullet = Instantiate(bulletPrefab) as GameObject;
                bullet.transform.position = Camera.transform.position + Camera.transform.forward - Camera.transform.up;
                bullet.transform.forward = Camera.transform.forward + Camera.transform.up*0.1f;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        //敵に触れたらリスポーン
        if (hit.gameObject.tag == "Enemy") {
            transform.position = RespawnPoint.position;
        }
        //敵の攻撃に触れたらダメージ
        if (hit.gameObject.tag == "EnemyAttack"){
            Debug.Log("Hit");
            HP -= 20;
            Destroy(hit.gameObject);
        }
        //アイテムに触れたらアイテムを取得
        if (hit.gameObject.tag == "ItemRun"){
            ItemRun++;
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.tag == "ItemMap"){
            ItemMap++;
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.tag == "ItemBreak"){
            ItemBreak++;
            Destroy(hit.gameObject);
        }
    }
}
