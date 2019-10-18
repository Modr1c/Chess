/****************************************************
    文件：MovingOfChess.cs
    作者：Modr1c
    邮箱：984564258@qq.com
    日期：2019/10/18 15:52:40
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 棋子的移动
/// </summary>

public class MovingOfChess
{
    public MovingOfChess(GameManager mGameManager)
    {
        gameManager = mGameManager;
    }

    private readonly GameManager gameManager;

    /// <summary>
    /// 棋子移动方法
    /// </summary>
    /// <param name="chessGo"></param>
    /// <param name="targetGrid"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="y1"></param>
    /// <param name="y2"></param>
    public void IsMove(GameObject chessGo, GameObject targetGrid, int FromX, int FromY, int ToX, int ToY)
    {
        gameManager.ShowLastPositionUI(chessGo.transform.position); ;
        chessGo.transform.SetParent(targetGrid.transform);
        MTools.SetLocalPositionZero(chessGo);
        gameManager.chessBoard[ToX, ToY] = gameManager.chessBoard[FromX, FromY];
        gameManager.chessBoard[FromX, FromY] = 0;
    }

    /// <summary>
    /// 吃子
    /// </summary>
    /// <param name="firstChess"></param>
    /// <param name="secondChess"></param>
    /// <param name="FromX"></param>
    /// <param name="FromY"></param>
    /// <param name="ToX"></param>
    /// <param name="ToY"></param>
    public void IsEat(GameObject firstChess, GameObject secondChess, int FromX, int FromY, int ToX, int ToY)
    {
        gameManager.ShowLastPositionUI(firstChess.transform.position);
        GameObject secondChessGrid = secondChess.transform.parent.gameObject;
        firstChess.transform.SetParent(secondChessGrid.transform);
        MTools.SetLocalPositionZero(firstChess);
        gameManager.chessBoard[ToX, ToY] = gameManager.chessBoard[FromX, FromY];
        gameManager.chessBoard[FromX, FromY] = 0;
        gameManager.BeEat(secondChess);
    }


}
