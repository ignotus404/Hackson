using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    public bool EnemyGoalFlag;
    public bool PlayerGoalFlag;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ゴール");
            MazeGenerator.PlayerGoalFlag = true;
        }

        else if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("敵ゴール");
            MazeGenerator.EnemyGoalFlag = true;
        }
    }
}
