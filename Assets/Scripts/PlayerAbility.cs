using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public GameObject Player;
    public GameObject xSkillObject;
    
    
    WaitForSeconds SpeedUpTime = new WaitForSeconds(8);
    int zSkill;
    int xSkill;
    int cSkill;
    GameObject zSkillObject;
    
    // Start is called before the first frame update
    void Start()
    {
        zSkillObject = GameObject.Find("Map/MapMaskArea");
        zSkill = 0;
        xSkill = 0;
        cSkill = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Skill1"))
        {
            if(zSkill > 0)
            {
                //スキル発動
                int x = Random.Range(0, 4);
                int z = Random.Range(0, 4);

                zSkillObject.transform.GetChild(x).GetChild(z).gameObject.SetActive(false);

                zSkill--;
            }
        }

        if(Input.GetButtonDown("Skill2"))
        {
            if(xSkill > 0)
            {
                //スキル発動
                Instantiate(xSkillObject, transform.position, transform.rotation);
                xSkill--;
            }
        }

        if(Input.GetButtonDown("Skill3"))
        {
            if(cSkill > 0)
            {
                //スキル発動
                StartCoroutine("SpeedUp");
                cSkill--;
            }
        }
    }

    IEnumerator SpeedUp()
    {
        Player.GetComponent<StarterAssets.FirstPersonController>().MoveSpeed = 10;
        yield return SpeedUpTime;
        Player.GetComponent<StarterAssets.FirstPersonController>().MoveSpeed = 5;
    }
}
