/****************************************************
    文件：ChessRegreting.cs
    作者：Modr1c
    邮箱：984564258@qq.com
    日期：2019/10/21 14:21:19
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 悔棋
/// </summary>

public class ChessRegreting
{
    private readonly GameManager gameManager;
    public ChessRegreting()
    {
        gameManager = GameManager.Instance;
    }
    /// <summary>
    /// 步数计数器
    /// </summary>
    public int regretCount;
    /// <summary>
    /// 悔棋数组，记录每一步棋
    /// </summary>
    public Chess[] chessSteps;

    /// <summary>
    /// 记录每一步棋子
    /// </summary>
    public struct Chess
    {
        public ChessSteps from;
        public ChessSteps to;
        public GameObject gridOne;
        public GameObject gridTwo;
        public GameObject chessOne;
        public GameObject chessTwo;
        public int chessOneID;
        public int chessTwoID;

    }

    /// <summary>
    /// 棋子位置
    /// </summary>
    public struct ChessSteps
    {
        public int x;
        public int y;
    }

    /// <summary>
    /// 悔棋
    /// </summary>
    public void RegretChess()
    {
        gameManager.HideLastPositionUI();
        gameManager.HideClickUI();
        gameManager.HideCanEatUI();
        if (gameManager.chessPeople == 1)
        {

        }
        else if (gameManager.chessPeople == 2)
        {
            if (regretCount <= 0)
            {
                return;
            }
            int f = regretCount - 1;
            int oneID = chessSteps[f].chessOneID;//棋子原来位置的ID
            int twoID = chessSteps[f].chessTwoID;//棋子移动到位置的ID
            GameObject gridOne, gridTwo, chessOne, chessTwo;
            gridOne = chessSteps[f].gridOne;
            gridTwo = chessSteps[f].gridTwo;
            chessOne = chessSteps[f].chessOne;
            chessTwo = chessSteps[f].chessTwo;
            //吃子
            if (chessTwo != null)
            {
                chessOne.transform.SetParent(gridOne.transform);
                chessTwo.transform.SetParent(gridTwo.transform);
                MTools.SetLocalPositionZero(chessOne);
                MTools.SetLocalPositionZero(chessTwo);
                gameManager.chessBoard[chessSteps[f].from.x, chessSteps[f].from.y] = oneID;
                gameManager.chessBoard[chessSteps[f].to.x, chessSteps[f].to.y] = twoID;
            }
            //移动
            else
            {
                chessOne.transform.SetParent(gridOne.transform);
                MTools.SetLocalPositionZero(chessOne);
                gameManager.chessBoard[chessSteps[f].from.x, chessSteps[f].from.y] = oneID;
                gameManager.chessBoard[chessSteps[f].to.x, chessSteps[f].to.y] = 0;
            }

            if (gameManager.chessMove == false)
            {
                UIManager.Instance.ShowTip("红方走");
                gameManager.chessMove = true;
            }
            else
            {
                UIManager.Instance.ShowTip("黑方走");
                gameManager.chessMove = false;
            }
            regretCount--;
            chessSteps[f] = new Chess();
        }
    }

    /// <summary>
    /// 添加悔棋步数
    /// </summary>
    /// <param name="regretStepNum"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    /// <param name="toX"></param>
    /// <param name="toY"></param>
    /// <param name="ID1"></param>
    /// <param name="ID2"></param>
    public void AddChess(int regretStepNum, int fromX, int fromY, int toX, int toY, int ID1, int ID2)
    {
        GameObject item1 = gameManager.boardGrid[fromX, fromY];
        GameObject item2 = gameManager.boardGrid[toX, toY];
        chessSteps[regretStepNum].from.x = fromX;
        chessSteps[regretStepNum].from.y = fromY;
        chessSteps[regretStepNum].to.x = toX;
        chessSteps[regretStepNum].to.y = toY;
        chessSteps[regretStepNum].gridOne = item1;
        chessSteps[regretStepNum].gridTwo = item2;
        gameManager.HideCanEatUI();
        gameManager.HideClickUI();
        GameObject firstChess = item1.transform.GetChild(0).gameObject;
        chessSteps[regretStepNum].chessOne = firstChess;
        chessSteps[regretStepNum].chessOneID = ID1;
        chessSteps[regretStepNum].chessTwoID = ID2;
        if (item2.transform.childCount != 0)
        {
            GameObject secondChess = item2.transform.GetChild(0).gameObject;
            chessSteps[regretStepNum].chessTwo = secondChess;
        }
        regretCount++;
    }
}
