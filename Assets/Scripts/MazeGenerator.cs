using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    #region MazeGenerator
    [Header("壁をまとめたオブジェクト")]
    [SerializeField] GameObject MazeGround; //迷路の壁全部をまとめるオブジェクト

    int MazeX;
    int MazeY;
    int[,] Maze; //迷路のデータを格納する配列
    GameObject[,] MazeObject; //迷路のオブジェクトを格納する配列 
    #endregion
    
    #region GameManager
    [Header("プレイヤー1"),SerializeField]GameObject Player1;
    [Header("プレイヤー1スポーン地点"),SerializeField]Transform Player1Spawn;
    [Header("プレイヤー2"),SerializeField]GameObject Player2;
    [Header("プレイヤー2スポーン地点"),SerializeField]Transform Player2Spawn;
    [Header("プレイヤー3"),SerializeField]GameObject Player3;
    [Header("プレイヤー3スポーン地点"),SerializeField]Transform Player3Spawn;
    [Header("プレイヤー4"),SerializeField]GameObject Player4;
    [Header("プレイヤー4スポーン地点"),SerializeField]Transform Player4Spawn;
    [Header("環境敵"),SerializeField]GameObject Enemy;
    [Header("環境敵スポーン数"),SerializeField]int EnemySpawnNum;
    [Header("ゴール地点"),SerializeField]GameObject GoalPoint;
    [Header("ゴールフラグ"),SerializeField]static bool GoalFlag;

    Vector3 Player1SpawnPoint;
    Vector3 Player2SpawnPoint;
    Vector3 Player3SpawnPoint;
    Vector3 Player4SpawnPoint;
    #endregion

    //マップ生成
    void Awake()
    {
        MazeX = MazeGround.transform.childCount;
        MazeY = MazeGround.transform.GetChild(0).childCount;

        MazeObject = new GameObject[MazeX, MazeY];

        Debug.Log(MazeX);
        Debug.Log(MazeY);

        for (int i = 0; i < MazeX; i++)
        {
            for (int j = 0; j < MazeY; j++)
            {
                MazeObject[i, j] = MazeGround.transform.GetChild(i).GetChild(j).gameObject;
            }
        }

        Maze = new int[MazeX, MazeY];
        InitializeMaze();

        Random.InitState(System.DateTime.Now.Millisecond); //乱数の初期化

        //倒すための棒をつくる
        for (int i = 1; i < MazeX; i += 2)
        {
            for (int j = 1; j < MazeY; j += 2)
            {
                Maze[i, j] = 1; //奇数のマスに棒を倒す
                CarvePath(i, j);
            }
        }

        //ゴール地点にある壁を取り除く
        for(int i = (MazeX/2) - 1; i < (MazeX/2) + 2; i++)
        {
            for(int j = (MazeY/2) - 1; j < (MazeY/2) + 2; j++)
            {
                Maze[i, j] = 0;
            }
        }

        //配列どおりにマップを生成する
        for (int i = 0; i < MazeX; i++)
        {
            for (int j = 0; j < MazeY; j++)
            {
                if (Maze[i, j] == 1)
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

    void Start()
    {
        //プレイヤーのスポーン地点を取得
        Player1SpawnPoint = Player1Spawn.position;
        Player2SpawnPoint = Player2Spawn.position;
        Player3SpawnPoint = Player3Spawn.position;
        Player4SpawnPoint = Player4Spawn.position;

        //プレイヤーをスポーン
        Instantiate(Player1, Player1SpawnPoint, Quaternion.identity);
        Instantiate(Player2, Player2SpawnPoint, Quaternion.identity);
        Instantiate(Player3, Player3SpawnPoint, Quaternion.identity);
        Instantiate(Player4, Player4SpawnPoint, Quaternion.identity);

        //環境敵をスポーン
        for (int i = 0; i < EnemySpawnNum; i++)
        {
            int x = Random.Range(0, MazeX);
            int y = Random.Range(0, MazeY);

            if (Maze[x, y] == 0)
            {
                Debug.Log("敵スポーン");
                Vector3 EnemySpawnPoint = MazeObject[x, y].transform.position;
                Instantiate(Enemy, EnemySpawnPoint, Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
    }

    void InitializeMaze()
    {
        for (int i = 0; i < MazeX; i++)
        {
            for (int j = 0; j < MazeY; j++)
            {
                Maze[i, j] = 0; //全てのマスを道にする
            }
        }
    }

    void CarvePath(int x, int y)
    {
        int[] directions = { 0, 1, 2, 3 };
        ShuffleArray(directions);

        foreach (int direction in directions)
        {
            int nextX = x;
            int nextY = y;

            switch (direction)
            {
                case 0: // 上
                    //1行目のみ上に道を作る
                    if(y == 1)
                    {
                        nextY -= 1;
                    }
                    break;
                case 1: // 下
                    nextY += 1;
                    break;
                case 2: // 左
                    nextX -= 1;
                    break;
                case 3: // 右
                    nextX += 1;
                    break;
            }

            if (IsInBounds(nextX, nextY) && Maze[nextX, nextY] == 0)
            {
                Maze[nextX, nextY] = 1; //道にする
                return;
            }
        }
    }

    void ShuffleArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int temp = array[i];
            int randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < MazeX && y >= 0 && y < MazeY;
    }
}