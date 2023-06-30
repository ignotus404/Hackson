using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("壁をまとめたオブジェクト")]
    [SerializeField] GameObject MazeGround; //迷路の壁全部をまとめるオブジェクト

    int MazeX;
    int MazeY;
    int[,] Maze; //迷路のデータを格納する配列

    //マップ生成
    void Awake()
    {
        MazeX = MazeGround.transform.childCount;
        MazeY = MazeGround.transform.GetChild(0).childCount;

        GameObject[,] MazeObject = new GameObject[MazeX, MazeY];

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
