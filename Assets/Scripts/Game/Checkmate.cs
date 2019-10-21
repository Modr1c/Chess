/****************************************************
    文件：Checkmate.cs
    作者：Modr1c
    邮箱：984564258@qq.com
    日期：2019/10/18 16:1:29
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 检测是否将军
/// </summary>

public class Checkmate
{
    private GameManager gameManager;
    private UIManager uiManager;
    public Checkmate()
    {
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
    }

    private int B_KingX, B_KingY, R_KingX, R_KingY; 

    /// <summary>
    /// 检测是否将军
    /// </summary>
    public void JudgeIfCheckmate()
    {
        GetKingPosition();
        if (gameManager.chessBoard[B_KingX, B_KingY] != 1)
        {
            uiManager.ShowTip("红色方胜利");
            gameManager.gameOver = true;
            return;
        }
        else if(gameManager.chessBoard[R_KingX,R_KingY]!=8)
        {
            uiManager.ShowTip("黑色方胜利");
            gameManager.gameOver = true;
            return;
        }
        bool ifCheckmate;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                switch (gameManager.chessBoard[i,j])
                {
                    case 2:
                        ifCheckmate = gameManager.rules.IsValidMove(gameManager.chessBoard, i, j, R_KingX, R_KingY);
                        if (ifCheckmate)
                        {
                            //AudioSouceManager.Instance.PlaySound(4);
                            uiManager.ShowTip("帅被車将军了");
                        }
                        break;
                    case 3:
                        ifCheckmate = gameManager.rules.IsValidMove(gameManager.chessBoard, i, j, R_KingX, R_KingY);
                        if (ifCheckmate)
                        {
                            //AudioSouceManager.Instance.PlaySound(4);
                            uiManager.ShowTip("帅被马将军了");
                        }
                        break;
                    case 4:
                        ifCheckmate = gameManager.rules.IsValidMove(gameManager.chessBoard, i, j, R_KingX, R_KingY);
                        if (ifCheckmate)
                        {
                            //AudioSouceManager.Instance.PlaySound(4);
                            uiManager.ShowTip("帅被炮将军了");
                        }
                        break;
                    case 7:
                        ifCheckmate = gameManager.rules.IsValidMove(gameManager.chessBoard, i, j, R_KingX, R_KingY);
                        if (ifCheckmate)
                        {
                            //AudioSouceManager.Instance.PlaySound(4);
                            uiManager.ShowTip("帅被卒将军了");
                        }
                        break;
                    case 9:
                        ifCheckmate = gameManager.rules.IsValidMove(gameManager.chessBoard, i, j, B_KingX, B_KingY);
                        if (ifCheckmate)
                        {
                            //AudioSouceManager.Instance.PlaySound(4);
                            uiManager.ShowTip("将被車将军了");
                        }
                        break;
                    case 10:
                        ifCheckmate = gameManager.rules.IsValidMove(gameManager.chessBoard, i, j, B_KingX, B_KingY);
                        if (ifCheckmate)
                        {
                            //AudioSouceManager.Instance.PlaySound(4);
                            uiManager.ShowTip("将被马将军了");
                        }
                        break;
                    case 11:
                        ifCheckmate = gameManager.rules.IsValidMove(gameManager.chessBoard, i, j, B_KingX, B_KingY);
                        if (ifCheckmate)
                        {
                            //AudioSouceManager.Instance.PlaySound(4);
                            uiManager.ShowTip("将被炮将军了");
                        }
                        break;
                    case 14:
                        ifCheckmate = gameManager.rules.IsValidMove(gameManager.chessBoard, i, j, B_KingX, B_KingY);
                        if (ifCheckmate)
                        {
                            //AudioSouceManager.Instance.PlaySound(4);
                            uiManager.ShowTip("将被兵将军了");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 获取将帅位置
    /// </summary>
    private void GetKingPosition()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 3; j < 6; j++)
            {
                if( gameManager.chessBoard[i,j]==1)
                {
                    B_KingX = i;
                    B_KingY = j;
                }
            }
        }

        for (int i = 7; i < 10; i++)
        {
            for (int j = 3; j < 6; j++)
            {
                if (gameManager.chessBoard[i, j] == 8)
                {
                    R_KingX = i;
                    R_KingY = j;
                }
            }
        }
    }
}
