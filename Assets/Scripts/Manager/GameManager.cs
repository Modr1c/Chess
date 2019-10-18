/****************************************************
    文件：GameManager.cs
    作者：Modr1c
    邮箱：984564258@qq.com
    日期：2019/10/18 10:6:52
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 存贮游戏数据，游戏引用，游戏资源，模式切换与控制
/// </summary>

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Instance = this;
    }

    public static GameManager Instance { get; private set; }


    #region 数据

    /// <summary>
    /// 当前游戏人数 当前对战人数，PVE 1 PVP 2 联网 3
    /// </summary>
    public int chessPeople;
    /// <summary>
    /// 当前难度  1.简单 2.一般 3.困难
    /// </summary>
    public int currentLevel;
    /// <summary>
    /// 当前棋盘状况
    /// </summary>
    public int[,] chessBoard;
    /// <summary>
    /// 棋盘上的所有格子
    /// </summary>
    public GameObject[,] boardGrid;


    #endregion

    #region 开关
    /// <summary>
    /// true-红 false-黑
    /// </summary>
    public bool chessMove;
    public bool gameOver;
    #endregion

    #region 资源
    /// <summary>
    /// 格子
    /// </summary>
    public GameObject gridGo;
    /// <summary>
    /// 所有棋子的sprite
    /// </summary>
    public Sprite[] sprites;
    /// <summary>
    /// 棋子
    /// </summary>
    public GameObject chessGo;
    /// <summary>
    /// 可以移动的位置显示
    /// </summary>
    public GameObject canMovePosUIGo;
    #endregion

    #region 引用
    [HideInInspector]
    /// <summary>
    /// 棋盘
    /// </summary>
    public GameObject boardGo;
    /// <summary>
    /// 0.单机 1.联网
    /// </summary>
    public GameObject[] boardGos;
    [HideInInspector]
    /// <summary>
    /// 上一次点击的对象
    /// </summary>
    public ChessOrGrid lastChessOrGrid;
    public Rules rules;
    public MovingOfChess movingOfChess;
    public Checkmate checkmate;
    /// <summary>
    /// 被吃掉的棋子池
    /// </summary>
    public GameObject eatChessPool;
    /// <summary>
    /// 当前选中棋子显示
    /// </summary>
    public GameObject clickChessUIGo;
    /// <summary>
    /// 棋子移动前的位置显示
    /// </summary>
    public GameObject lastPosUIGo;
    /// <summary>
    /// 可以吃掉的棋子显示
    /// </summary>
    public GameObject canEatPosUIGo;
    /// <summary>
    /// 移动位置存储显示栈
    /// </summary>
    private Stack<GameObject> canMoveUIStack;
    /// <summary>
    /// 当前移动位置已显示栈
    /// </summary>
    private Stack<GameObject> currentCanMoveUIStack;
    #endregion

    private void Start()
    {
        //Test
        chessPeople = 1;
        ResetGame();
    }

    /// <summary>
    /// 重置游戏
    /// </summary>
    public void ResetGame()
    {
        chessMove = true;
        chessBoard = new int[10, 9]
        {
            {2,3,6,5,1,5,6,3,2},
            {0,0,0,0,0,0,0,0,0},
            {0,4,0,0,0,0,0,4,0},
            {7,0,7,0,7,0,7,0,7},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {14,0,14,0,14,0,14,0,14},
            {0,11,0,0,0,0,0,11,0},
            {0,0,0,0,0,0,0,0,0},
            {9,10,13,12,8,12,13,10,9}
        };

        boardGrid = new GameObject[Constants.boardHeight, Constants.boardWidth];
        if (chessPeople == 1)
        {
            boardGo = boardGos[0];
        }
        else
        {
            boardGo = boardGos[1];
        }
        InitGrid();
        InitChess();
        rules = new Rules();
        movingOfChess = new MovingOfChess(this);
        checkmate = new Checkmate();
        canMoveUIStack = new Stack<GameObject>();
        for (int i = 0; i < Constants.canMoveUIStackLength; i++)
        {
            canMoveUIStack.Push(Instantiate(canMovePosUIGo));
        }
        currentCanMoveUIStack = new Stack<GameObject>();
    }

    /// <summary>
    /// 实例化格子
    /// </summary>
    private void InitGrid()
    {
        float posX = 0, posY = 0;
        for (int i = 0; i < Constants.boardHeight; i++)
        {
            for (int j = 0; j < Constants.boardWidth; j++)
            {
                GameObject itemGo = Instantiate(gridGo);
                itemGo.transform.SetParent(boardGo.transform);
                itemGo.name = "Item[" + i.ToString() + "," + j.ToString() + "]";
                itemGo.transform.localPosition = new Vector3(posX, posY, 0);
                posX += Constants.gridWidth;
                if (posX >= Constants.gridWidth * Constants.boardWidth)
                {
                    posY -= Constants.gridHeight;
                    posX = 0;
                }
                itemGo.GetComponent<ChessOrGrid>().xIndex = i;
                itemGo.GetComponent<ChessOrGrid>().yIndex = j;
                boardGrid[i, j] = itemGo;
            }
        }
    }

    /// <summary>
    /// 实例化棋子
    /// </summary>
    private void InitChess()
    {
        for (int i = 0; i < Constants.boardHeight; i++)
        {
            for (int j = 0; j < Constants.boardWidth; j++)
            {
                GameObject item = boardGrid[i, j];
                switch (chessBoard[i, j])
                {
                    case 1:
                        CreateChess(item, "b_King", sprites[0], false);
                        break;
                    case 2:
                        CreateChess(item, "b_Rook", sprites[1], false);
                        break;
                    case 3:
                        CreateChess(item, "b_Knight", sprites[2], false);
                        break;
                    case 4:
                        CreateChess(item, "b_Cannon", sprites[3], false);
                        break;
                    case 5:
                        CreateChess(item, "b_Guard", sprites[4], false);
                        break;
                    case 6:
                        CreateChess(item, "b_Minister", sprites[5], false);
                        break;
                    case 7:
                        CreateChess(item, "b_Capture", sprites[6], false);
                        break;
                    case 8:
                        CreateChess(item, "r_King", sprites[7]);
                        break;
                    case 9:
                        CreateChess(item, "r_Rook", sprites[8]);
                        break;
                    case 10:
                        CreateChess(item, "r_Knight", sprites[9]);
                        break;
                    case 11:
                        CreateChess(item, "r_Cannon", sprites[10]);
                        break;
                    case 12:
                        CreateChess(item, "r_Guard", sprites[11]);
                        break;
                    case 13:
                        CreateChess(item, "r_Minister", sprites[12]);
                        break;
                    case 14:
                        CreateChess(item, "r_Capture", sprites[13]);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 生成棋子游戏物体
    /// </summary>
    /// <param name="gridItem">作为父对象的格子</param>
    /// <param name="name">棋子名称</param>
    /// <param name="chessIcon">棋子标志样式</param>
    /// <param name="ifRed">是否为红色棋子</param>
    private void CreateChess(GameObject gridItem, string name, Sprite chessIcon, bool ifRed = true)
    {
        GameObject item = Instantiate(chessGo);
        item.transform.SetParent(gridItem.transform);
        item.name = name;
        item.GetComponent<Image>().sprite = chessIcon;
        MTools.SetLocalPositionZero(item);
        item.transform.localScale = Vector3.one;
        item.GetComponent<ChessOrGrid>().isRed = ifRed;
    }

    /// <summary>
    /// 被吃掉的棋子处理
    /// </summary>
    /// <param name="itemGo"></param>
    public void BeEat(GameObject itemGo)
    {
        itemGo.transform.SetParent(eatChessPool.transform);
        MTools.SetLocalPositionZero(itemGo);
    }

    #region 游戏中UI显示隐藏处理
    public void ShowClickUI(Transform targetTrans)
    {
        clickChessUIGo.transform.SetParent(targetTrans);
        MTools.SetLocalPositionZero(clickChessUIGo);
    }

    public void HideClickUI()
    {
        clickChessUIGo.transform.SetParent(eatChessPool.transform);
        MTools.SetLocalPositionZero(clickChessUIGo);
    }

    public void ShowLastPositionUI(Vector3 showPosition)
    {
        lastPosUIGo.transform.position = showPosition;
    }

    public void HideLastPositionUI()
    {
        lastPosUIGo.transform.localPosition = new Vector3(100, 100, 100);
    }

    public void HideCanEatUI()
    {
        canEatPosUIGo.transform.SetParent(eatChessPool.transform);
        canEatPosUIGo.transform.localPosition = Vector3.zero;
    }

    public GameObject PopCanMoveUI()
    {
        GameObject itemGo = canMoveUIStack.Pop();
        currentCanMoveUIStack.Push(itemGo);
        itemGo.SetActive(true);
        return itemGo;
    }
    public void PushCanMoveUI(GameObject itemGo)
    {
        canMoveUIStack.Push(itemGo);
        itemGo.transform.SetParent(eatChessPool.transform);
        itemGo.SetActive(false);
    }

    public void ClearCurrentCanMoveUIStack()
    {
        while (currentCanMoveUIStack.Count > 0)
        {
            PushCanMoveUI(currentCanMoveUIStack.Pop());
        }
    }
    #endregion
}
