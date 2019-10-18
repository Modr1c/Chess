/****************************************************
    文件：Rules.cs
    作者：Modr1c
    邮箱：984564258@qq.com
    日期：2019/10/18 15:40:34
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 象棋规则类
/// </summary>

public class Rules
{
    /// <summary>
    /// 检测当前移动是否合法
    /// </summary>
    /// <param name="position"></param>
    /// <param name="FromX"></param>
    /// <param name="FromY"></param>
    /// <param name="ToX"></param>
    /// <param name="ToY"></param>
    /// <returns></returns>
    public bool IsValidMove(int FromX, int FromY, int ToX, int ToY)
    {
        int[,] position = GameManager.Instance.chessBoard;
        int moveChessID, targetID;
        moveChessID = position[FromX, FromY];
        targetID = position[ToX, ToY];
        if (IsSameSide(moveChessID, targetID))
        {
            return false;
        }
        return IsValid(moveChessID, position, FromX, FromY, ToX, ToY);
    }

    /// <summary>
    /// 判断选中的两个物体类型是否相同
    /// </summary>
    /// <returns></returns>
    public bool IsSameSide(int x, int y)
    {
        return (IsBlack(x) && IsBlack(y)) || (IsRed(x) && IsRed(y));
    }

    public bool IsBlack(int x)
    {
        return x > 0 && x < 8 ? true : false;
    }

    public bool IsRed(int x)
    {
        return x > 8 && x < 15 ? true : false;
    }

    public bool IsValid(int moveChessID, int[,] position, int FromX, int FromY, int ToX, int ToY)
    {
        if (FromX == ToX && FromY == ToY)
        {
            return false;
        }
        if (!KingKill(position, FromX, FromY, ToX, ToY))
        {
            return false;
        }
        int i = 0, j = 0;
        switch (moveChessID)
        {
            case 1:
                if (ToX > 2 || ToY > 5 || ToY < 3)
                {
                    return false;
                }
                if ((Mathf.Abs(ToX - FromX) + Mathf.Abs(ToY - FromY)) > 1)
                {
                    return false;
                }

                break;
            default:
                break;
        }
        return true;
    }

    /// <summary>
    /// 判断将帅是否在同一直线
    /// </summary>
    /// <param name="position"></param>
    /// <param name="FromX"></param>
    /// <param name="FromY"></param>
    /// <param name="ToX"></param>
    /// <param name="ToY"></param>
    /// <returns></returns>
    public bool KingKill(int[,] position, int FromX, int FromY, int ToX, int ToY)
    {
        int b_KingX = 0, b_KingY = 0, r_KingX = 0, r_KingY = 0;
        int count = 0;
        //假想的棋盘
        int[,] mPosition = new int[Constants.boardHeight, Constants.boardWidth];
        for (int i = 0; i < Constants.boardHeight; i++)
        {
            for (int j = 0; j < Constants.boardWidth; j++)
            {
                mPosition[i, j] = position[i, j];
            }
        }
        mPosition[ToX, ToY] = mPosition[FromX, FromY];
        mPosition[FromX, FromY] = 0;
        //将的位置
        for (int i = 0; i < 3; i++)
        {
            for (int j = 3; j < 6; j++)
            {
                if (mPosition[i, j] == 1)
                {
                    b_KingX = i;
                    b_KingY = j;
                }
            }
        }
        //帅的位置
        for (int i = 7; i < 10; i++)
        {
            for (int j = 3; j < 6; j++)
            {
                if (mPosition[i, j] == 8)
                {
                    r_KingX = i;
                    r_KingY = j;
                }
            }
        }
        //将帅同一直线
        if (b_KingY == r_KingY)
        {
            for (int i = b_KingX + 1; i < r_KingX; i++)
            {
                if (position[i, b_KingY] != 0)
                {
                    count++;
                }
            }
        }
        //将帅不同直线
        else
        {
            count = -1;
        }
        return count == 0 ? false : true;
    }
}
