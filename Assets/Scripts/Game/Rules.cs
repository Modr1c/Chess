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
    public bool IsValidMove(int[,] position,int FromX, int FromY, int ToX, int ToY)
    {
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
        return x >= 8 && x < 15 ? true : false;
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
            case 1://黑将
                if (ToX > 2 || ToY > 5 || ToY < 3)
                {
                    return false;
                }
                if ((Mathf.Abs(ToX - FromX) + Mathf.Abs(ToY - FromY)) > 1)
                {
                    return false;
                }
                break;
            case 8://红帅
                if (ToX < 7 || ToY > 5 || ToY < 3)
                {
                    return false;
                }
                if ((Mathf.Abs(ToX - FromX) + Mathf.Abs(ToY - FromY)) > 1)
                {
                    return false;
                }
                break;
            case 5://黑士
                if (ToX > 2 || ToY > 5 || ToY < 3)
                {
                    return false;
                }
                if (Mathf.Abs(FromX - ToX) != 1 || Mathf.Abs(FromY - ToY) != 1)
                {
                    return false;
                }
                break;
            case 12://红仕
                if (ToX < 7 || ToY > 5 || ToY < 3)
                {
                    return false;
                }
                if (Mathf.Abs(FromX - ToX) != 1 || Mathf.Abs(FromY - ToY) != 1)
                {
                    return false;
                }
                break;
            case 6://黑象
                if (ToX > 4)
                {
                    return false;
                }
                if (Mathf.Abs(FromX - ToX) != 2 || Mathf.Abs(FromY - ToY) != 2)
                {
                    return false;
                }
                if (position[(FromX + ToX) / 2, (FromY + ToY) / 2] != 0)
                {
                    return false;
                }
                break;
            case 13://红相
                if (ToX < 5)
                {
                    return false;
                }
                if (Mathf.Abs(FromX - ToX) != 2 || Mathf.Abs(FromY - ToY) != 2)
                {
                    return false;
                }
                if (position[(FromX + ToX) / 2, (FromY + ToY) / 2] != 0)
                {
                    return false;
                }
                break;
            case 7://黑卒
                if (ToX < FromX)
                {
                    return false;
                }
                if (FromX < 5 && FromX == ToX)
                {
                    return false;
                }
                if (ToX - FromX + Mathf.Abs(ToY - FromY) > 1)
                {
                    return false;
                }
                break;
            case 14://红兵
                if (ToX > FromX)
                {
                    return false;
                }
                if (FromX > 4 && FromX == ToX)
                {
                    return false;
                }
                if (FromX - ToX + Mathf.Abs(ToY - FromY) > 1)
                {
                    return false;
                }
                break;
            case 2://红黑車
            case 9:
                //車走直线
                if (FromY != ToY && FromX != ToX)
                {
                    return false;
                }
                //判断当前移动路径上是否有其他棋子
                if (FromX == ToX)//走横线
                {
                    if (FromY < ToY)//右走
                    {
                        for (i = FromY + 1; i < ToY; i++)
                        {
                            if (position[FromX, i] != 0)//代表移动路径上有棋子
                            {
                                return false;
                            }
                        }
                    }
                    else//左走
                    {
                        for (i = ToY + 1; i < FromY; i++)
                        {
                            if (position[FromX, i] != 0)
                            {
                                return false;
                            }
                        }
                    }
                }
                else//走竖线
                {
                    if (FromX < ToX)//下走
                    {
                        for (j = FromX + 1; j < ToX; j++)
                        {
                            if (position[j, FromY] != 0)
                            {
                                return false;
                            }
                        }
                    }
                    else//上走
                    {
                        for (j = ToX + 1; j < FromX; j++)
                        {
                            if (position[j, FromY] != 0)
                            {
                                return false;
                            }
                        }
                    }
                }
                break;
            case 3:
            case 10://红黑马
                //马走日字
                //竖日                                                
                if (!((Mathf.Abs(ToY - FromY) == 1 && Mathf.Abs(ToX - FromX) == 2) ||
                //横日    
                    (Mathf.Abs(ToY - FromY) == 2 && Mathf.Abs(ToX - FromX) == 1)))
                {
                    return false;
                }
                //马别腿
                if (ToY - FromY == 2)//右横日
                {
                    i = FromY + 1;
                    j = FromX;
                }
                else if (FromY - ToY == 2)//左横日
                {
                    i = FromY - 1;
                    j = FromX;
                }
                else if (ToX - FromX == 2)//下竖日
                {
                    i = FromY;
                    j = FromX + 1;
                }
                else if (FromX - ToX == 2)//上竖日
                {
                    i = FromY;
                    j = FromX - 1;
                }
                if (position[j, i] != 0)
                {
                    return false;
                }
                break;
            case 4:
            case 11://红黑炮
                //炮走直线
                if (FromY != ToY && FromX != ToX)
                {
                    return false;
                }
                //炮是走棋还是翻山吃子
                //炮移动
                if (position[ToX, ToY] == 0)
                {
                    if (FromX == ToX)//炮走横线
                    {
                        if (FromY < ToY)//右走
                        {
                            for (i = FromY + 1; i < ToY; i++)
                            {
                                if (position[FromX, i] != 0)
                                {
                                    return false;
                                }
                            }
                        }
                        else//左走
                        {
                            for (i = ToY + 1; i < FromY; i++)
                            {
                                if (position[FromX, i] != 0)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    else//炮走竖线
                    {
                        if (FromX < ToX)//下走
                        {
                            for (j = FromX + 1; j < ToX; j++)
                            {
                                if (position[j, FromY] != 0)
                                {
                                    return false;
                                }
                            }
                        }
                        else//上走
                        {
                            for (j = ToX + 1; j < FromX; j++)
                            {
                                if (position[j, FromY] != 0)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                //炮翻山吃子
                else
                {
                    int count = 0;
                    if (FromX == ToX)//走横线
                    {
                        if (FromY < ToY)//右走
                        {
                            for (i = FromY + 1; i < ToY; i++)
                            {
                                if (position[FromX, i] != 0)
                                {
                                    count++;
                                }
                            }
                            if (count != 1)
                            {
                                return false;
                            }
                        }
                        else//左走
                        {
                            for (i = ToY + 1; i < FromY; i++)
                            {
                                if (position[FromX, i] != 0)
                                {
                                    count++;
                                }
                            }
                            if (count != 1)
                            {
                                return false;
                            }
                        }
                    }
                    else//走竖线
                    {
                        if (FromX < ToX)//下走
                        {
                            for (j = FromX + 1; j < ToX; j++)
                            {
                                if (position[j, FromY] != 0)
                                {
                                    count++;
                                }
                            }
                            if (count != 1)
                            {
                                return false;
                            }
                        }
                        else//上走
                        {
                            for (j = ToX + 1; j < FromX; j++)
                            {
                                if (position[j, FromY] != 0)
                                {
                                    count++;
                                }
                            }
                            if (count != 1)
                            {
                                return false;
                            }
                        }
                    }
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
        int[,] mPosition = new int[10, 9];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
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
