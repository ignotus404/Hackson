using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("壁をまとめたオブジェクト"),SerializeField]GameObject MazeGround; //迷路の壁全部をまとめるオブジェクト

    int MazeX;
    int MazeY;
    int receiveNum = 0; //棒を倒す方向を決める乱数
    bool Complete = false; //迷路生成完了フラグ
    GameObject[,] MazeObject;
    int[,] Maze; //迷路のデータを格納する配列
   
    
    //マップ生成
    void Awake()
    {
        MazeX = MazeGround.transform.childCount;
        MazeY = MazeGround.transform.GetChild(0).childCount;

        GameObject[,] MazeObject = new GameObject[MazeX, MazeY];
        int[,] Maze = new int[MazeX, MazeY]; //迷路のデータを格納する配列

        Debug.Log(Maze.Length);
        Random.InitState(System.DateTime.Now.Millisecond); //乱数の初期化

        //迷路の壁をまとめるオブジェクトを取得
        for(int i = 0; i < MazeX; i++)
        {
            for(int j = 0; j < MazeY; j++)
            {
                MazeObject[i,j] = MazeGround.transform.GetChild(i).GetChild(j).gameObject;
            }
        }

        //Maze初期化
        for(int i = 0; i < MazeX; i++)
        {
            for(int j = 0; j < MazeY; j++)
            {
                Maze[i, j] = 0; //全てのマスを道にする
            }
        }

        //倒すための棒をつくる
        for(int i = 0; i < MazeX; i++)
        {
            for(int j = 0; j < MazeY; j++)
            {
                if((i%2 == 1) && (j%2 == 1)) 
                {
                    Maze[i, j] = 1; //奇数のマスに棒を倒す
                    while(!Complete)
                    {
                        receiveNum = Random.Range(1, 13) % 4; //1~4の乱数を生成
                        Complete = MazeCreate(i, j, receiveNum, Maze); //棒を倒す
                    }
                }
            }
        }   
        
        //配列どおりにマップを生成する
        for(int i = 0; i < MazeX; i++)
        {
            for(int j = 0; j < MazeY; j++)
            {
                if(Maze[i, j] == 1)
                {
                    MazeObject[i, j].SetActive(true);
                }
                else
                {
                    MazeObject[i, j].SetActive(false);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool MazeCreate(int x, int y,int receiveNum,int[,] Maze)
    {
        Debug.Log(Maze.Length);
        switch (receiveNum)
        {
            case 0:
                //上方向に棒を倒す
                if(y != 1 && Maze[x, y-1] == 0)
                {
                    Maze[x, y-1] = 1;
                    return true;
                }
                else return false;

            case 1:
                //下方向に棒を倒す
                if(Maze[x, y+1] == 0)
                {
                    Maze[x, y+1] = 1;
                    return true;
                }
                else return false;

            case 2:
                //左方向に棒を倒す
                if(x != 1 && Maze[x-1, y] == 0 )
                {
                    Maze[x-1, y] = 1;
                    return true;
                }
                else return false;

            case 3:
                //右方向に棒を倒す
                if(Maze[x+1, y] == 0)
                {
                    Maze[x+1, y] = 1;
                    return true;
                }
                else return false;

            default:
                return false;
        }
    }
}
