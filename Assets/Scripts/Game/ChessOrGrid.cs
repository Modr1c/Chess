/****************************************************
    文件：ChessOrGrid.cs
    作者：Modr1c
    邮箱：984564258@qq.com
    日期：2019/10/18 12:38:6
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 棋子或者格子
/// </summary>

public class ChessOrGrid : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ClickCheck);
    }

    //格子索引
    public int xIndex, yIndex;
    public bool isRed;
    public bool isGrid;

    private GameManager gameManager;
    /// <summary>
    /// 棋子父物体
    /// </summary>
    private GameObject gridGo;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gridGo = gameObject;
    }

    /// <summary>
    /// 点击棋子格子时触发的检测方法
    /// </summary>
    public void ClickCheck()
    {
        if (gameManager.gameOver)
        {
            return;
        }
        int itemColorID;
        if (isGrid)
        {
            itemColorID = 0;
        }
        else
        {
            gridGo = transform.parent.gameObject;
            ChessOrGrid chessOrGrid = gridGo.GetComponent<ChessOrGrid>();
            xIndex = chessOrGrid.xIndex;
            yIndex = chessOrGrid.yIndex;
            if (isRed)
            {
                itemColorID = 2;
            }
            else
            {
                itemColorID = 1;
            }
        }
        GridOrChessBehavior(itemColorID, xIndex, yIndex);
    }

    /// <summary>
    /// 格子与棋子的行为逻辑
    /// </summary>
    private void GridOrChessBehavior(int itemColorID, int x, int y)
    {
        gameManager.HideCanEatUI();
        int FromX, FromY, ToX, ToY;
        switch (itemColorID)
        {
            case 0:
                gameManager.ClearCurrentCanMoveUIStack();
                ToX = x;
                ToY = y;
                if (gameManager.lastChessOrGrid == null)
                {
                    gameManager.lastChessOrGrid = this;
                    return;
                }
                if (gameManager.chessMove)
                {
                    if (gameManager.lastChessOrGrid.isGrid)
                    {
                        return;
                    }
                    if (!gameManager.lastChessOrGrid.isRed)
                    {
                        gameManager.lastChessOrGrid = null;
                        return;
                    }
                    FromX = gameManager.lastChessOrGrid.xIndex;
                    FromY = gameManager.lastChessOrGrid.yIndex;
                    bool canMove = gameManager.rules.IsValidMove(gameManager.chessBoard,FromX, FromY, ToX, ToY);
                    if (!canMove)
                    {
                        return;
                    }
                    int chessOneID = gameManager.chessBoard[FromX, FromY];
                    int chessTwoID = gameManager.chessBoard[ToX, ToY];
                    gameManager.chessRegreting.AddChess(gameManager.chessRegreting.regretCount, FromX, FromY, ToX, ToY, chessOneID, chessTwoID);
                    gameManager.movingOfChess.IsMove(gameManager.lastChessOrGrid.gameObject, gridGo, FromX, FromY, ToX, ToY);
                    UIManager.Instance.ShowTip("黑方走");
                    gameManager.checkmate.JudgeIfCheckmate();
                    gameManager.chessMove = false;
                    gameManager.lastChessOrGrid = this;
                    gameManager.HideClickUI();
                }
                else
                {
                    if (gameManager.lastChessOrGrid.isGrid)
                    {
                        return;
                    }
                    if (gameManager.lastChessOrGrid.isRed)
                    {
                        gameManager.lastChessOrGrid = null;
                        return;
                    }
                    FromX = gameManager.lastChessOrGrid.xIndex;
                    FromY = gameManager.lastChessOrGrid.yIndex;
                    bool canMove = gameManager.rules.IsValidMove(gameManager.chessBoard,FromX, FromY, ToX, ToY);
                    if (!canMove)
                    {
                        return;
                    }
                    int chessOneID = gameManager.chessBoard[FromX, FromY];
                    int chessTwoID = gameManager.chessBoard[ToX, ToY];
                    gameManager.chessRegreting.AddChess(gameManager.chessRegreting.regretCount, FromX, FromY, ToX, ToY, chessOneID, chessTwoID);
                    gameManager.movingOfChess.IsMove(gameManager.lastChessOrGrid.gameObject, gridGo, FromX, FromY, ToX, ToY);
                    UIManager.Instance.ShowTip("红方走");
                    gameManager.checkmate.JudgeIfCheckmate();
                    gameManager.chessMove = true;
                    gameManager.lastChessOrGrid = this;
                    gameManager.HideClickUI();
                }
                break;
            case 1:
                gameManager.ClearCurrentCanMoveUIStack();
                if (!gameManager.chessMove)
                {
                    FromX = x;
                    FromY = y;
                    gameManager.movingOfChess.ClickChess(FromX, FromY);
                    gameManager.lastChessOrGrid = this;
                    gameManager.ShowClickUI(transform);
                }
                else
                {
                    if (gameManager.lastChessOrGrid == null)
                    {
                        return;
                    }
                    if (!gameManager.lastChessOrGrid.isRed)
                    {
                        gameManager.lastChessOrGrid = this;
                        return;
                    }
                    FromX = gameManager.lastChessOrGrid.xIndex;
                    FromY = gameManager.lastChessOrGrid.yIndex;
                    ToX = x;
                    ToY = y;
                    bool canMove = gameManager.rules.IsValidMove(gameManager.chessBoard,FromX, FromY, ToX, ToY);
                    if (!canMove)
                    {
                        return;
                    }
                    int chessOneID = gameManager.chessBoard[FromX, FromY];
                    int chessTwoID = gameManager.chessBoard[ToX, ToY];
                    gameManager.chessRegreting.AddChess(gameManager.chessRegreting.regretCount, FromX, FromY, ToX, ToY, chessOneID, chessTwoID);
                    gameManager.movingOfChess.IsEat(gameManager.lastChessOrGrid.gameObject, gameObject, FromX, FromY, ToX, ToY);
                    gameManager.chessMove = false;
                    UIManager.Instance.ShowTip("黑方走");
                    gameManager.lastChessOrGrid = null;
                    gameManager.checkmate.JudgeIfCheckmate();
                    gameManager.HideClickUI();
                }
                break;
            case 2:
                gameManager.ClearCurrentCanMoveUIStack();
                if (gameManager.chessMove)
                {
                    FromX = x;
                    FromY = y;
                    gameManager.movingOfChess.ClickChess(FromX, FromY);
                    gameManager.lastChessOrGrid = this;
                    gameManager.ShowClickUI(transform);
                }
                else
                {
                    if (gameManager.lastChessOrGrid == null)
                    {
                        return;
                    }
                    if (gameManager.lastChessOrGrid.isRed)
                    {
                        gameManager.lastChessOrGrid = this;
                        return;
                    }
                    FromX = gameManager.lastChessOrGrid.xIndex;
                    FromY = gameManager.lastChessOrGrid.yIndex;
                    ToX = x;
                    ToY = y;
                    bool canMove = gameManager.rules.IsValidMove(gameManager.chessBoard,FromX, FromY, ToX, ToY);
                    if (!canMove)
                    {
                        return;
                    }
                    int chessOneID = gameManager.chessBoard[FromX, FromY];
                    int chessTwoID = gameManager.chessBoard[ToX, ToY];
                    gameManager.chessRegreting.AddChess(gameManager.chessRegreting.regretCount, FromX, FromY, ToX, ToY, chessOneID, chessTwoID);
                    gameManager.movingOfChess.IsEat(gameManager.lastChessOrGrid.gameObject, gameObject, FromX, FromY, ToX, ToY);
                    gameManager.chessMove = true;
                    gameManager.lastChessOrGrid = null;
                    UIManager.Instance.ShowTip("红方走");
                    gameManager.checkmate.JudgeIfCheckmate();
                    gameManager.HideClickUI();
                }
                break;
            default:
                break;
        }
    }
}
