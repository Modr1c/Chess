/****************************************************
    文件：MovingOfChess.cs
    作者：Modr1c
    邮箱：984564258@qq.com
    日期：2019/10/18 15:52:40
*****************************************************/
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

    /// <summary>
    /// 显示当前所选棋子可移动路径
    /// </summary>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    public void ClickChess(int fromX, int fromY)
    {
        int chessID = gameManager.chessBoard[fromX, fromY];
        int[,] chessBoard = gameManager.chessBoard;
        switch (chessID)
        {
            case 1://黑将
                GetB_KingMove(chessBoard, fromX, fromY);
                break;
            case 8://红帅
                GetR_KingMove(chessBoard, fromX, fromY);
                break;
            case 2://红黑車
            case 9:
                GetRookMove(chessBoard, fromX, fromY);
                break;
            case 3://红黑马
            case 10:
                GetKnightMove(chessBoard, fromX, fromY);
                break;
            case 4://红黑炮
            case 11:
                GetCannonMove(chessBoard, fromX, fromY);
                break;
            case 5://黑仕
                GetB_GuardMove(chessBoard, fromX, fromY);
                break;
            case 12://红士
                GetR_GuardMove(chessBoard, fromX, fromY);
                break;
            case 6://红黑象
            case 13:
                GetMinisterMove(chessBoard, fromX, fromY);
                break;
            case 7://黑卒
                GetB_CaptureMove(chessBoard, fromX, fromY);
                break;
            case 14://红兵
                GetR_CaptureMove(chessBoard, fromX, fromY);
                break;
            default:
                break;
        }
    }

    #region 得到对应种类的棋子当前可以移动的所有路径
    /// <summary>
    /// 黑将
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    private void GetB_KingMove(int[,] position, int fromX, int fromY)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 3; y < 6; y++)
            {
                if (gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
            }
        }
    }

    /// <summary>
    /// 红帅
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    private void GetR_KingMove(int[,] position, int fromX, int fromY)
    {
        for (int x = 7; x < 10; x++)
        {
            for (int y = 3; y < 6; y++)
            {
                if (gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
            }
        }
    }

    /// <summary>
    /// 红黑車
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    private void GetRookMove(int[,] position, int fromX, int fromY)
    {
        int x, y;
        int chessID;
        chessID = position[fromX, fromY];
        //上
        x = fromX - 1;
        y = fromY;
        while (x >= 0)
        {
            if (position[x, y] == 0)
            {
                GetCanMovePos(position, fromX, fromY, x, y);
            }
            else
            {
                if (!gameManager.rules.IsSameSide(chessID, position[x, y]))
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
                break;
            }
            x--;
        }
        //下
        x = fromX + 1;
        y = fromY;
        while (x < 10)
        {
            if (position[x, y] == 0)
            {
                GetCanMovePos(position, fromX, fromY, x, y);
            }
            else
            {
                if (!gameManager.rules.IsSameSide(chessID, position[x, y]))
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
                break;
            }
            x++;
        }
        //左
        x = fromX;
        y = fromY - 1;
        while (y >= 0)
        {
            if (position[x, y] == 0)
            {
                GetCanMovePos(position, fromX, fromY, x, y);
            }
            else
            {
                if (!gameManager.rules.IsSameSide(chessID, position[x, y]))
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
                break;
            }
            y--;
        }
        //右
        x = fromX;
        y = fromY + 1;
        while (y < 9)
        {
            if (position[x, y] == 0)
            {
                GetCanMovePos(position, fromX, fromY, x, y);
            }
            else
            {
                if (!gameManager.rules.IsSameSide(chessID, position[x, y]))
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
                break;
            }
            y++;
        }
    }

    /// <summary>
    /// 红黑马
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    private void GetKnightMove(int[,] position, int fromX, int fromY)
    {
        int x, y;
        //竖日
        //右下
        x = fromX + 2;
        y = fromY + 1;
        if ((x < 10 && y < 9) && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //右上
        x = fromX - 2;
        y = fromY + 1;
        if ((x >= 0 && y < 9) && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //左下
        x = fromX + 2;
        y = fromY - 1;
        if ((x < 10 && y >= 0) && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //左上
        x = fromX - 2;
        y = fromY - 1;
        if ((x >= 0 && y >= 0) && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }

        //横日
        //右下
        x = fromX + 1;
        y = fromY + 2;
        if ((x < 10 && y < 9) && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //右上
        x = fromX - 1;
        y = fromY + 2;
        if ((x >= 0 && y < 9) && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //左下
        x = fromX + 1;
        y = fromY - 2;
        if ((x < 10 && y >= 0) && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //左上
        x = fromX - 1;
        y = fromY - 2;
        if ((x >= 0 && y >= 0) && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
    }

    /// <summary>
    /// 红黑炮
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    private void GetCannonMove(int[,] position, int fromX, int fromY)
    {
        int x, y;
        bool flag;//是否满足翻山的条件
        int chessID;
        chessID = position[fromX, fromY];
        //右
        x = fromX;
        y = fromY + 1;
        flag = false;
        while (y < 9)
        {
            //是空格子
            if (position[x, y] == 0)
            {
                //在未达成翻山条件前，显示所有可以移动的路径，达成之后
                //不可空翻
                if (!flag)
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
            }
            //是棋子
            else
            {
                //条件未满足时，开启条件的满足，可翻山
                if (!flag)
                {
                    flag = true;
                }
                //已开启，判断当前是否为同一方，如果是，此位置不可以移动
                //如果不是，则此子可吃，即可移动到此位置，则需显示
                //结束当前遍历
                else
                {
                    if (!gameManager.rules.IsSameSide(chessID, position[x, y]))
                    {
                        GetCanMovePos(position, fromX, fromY, x, y);
                    }
                    break;
                }
            }
            y++;
        }
        //左
        y = fromY - 1;
        flag = false;
        while (y >= 0)
        {
            if (position[x, y] == 0)
            {
                if (!flag)
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
            }
            else
            {
                if (!flag)
                {
                    flag = true;
                }
                else
                {
                    if (!gameManager.rules.IsSameSide(chessID, position[x, y]))
                    {
                        GetCanMovePos(position, fromX, fromY, x, y);
                    }
                    break;
                }
            }
            y--;
        }
        //下
        x = fromX + 1;
        y = fromY;
        flag = false;
        while (x < 10)
        {
            //是空格子
            if (position[x, y] == 0)
            {
                //在未达成翻山条件前，显示所有可以移动的路径，达成之后
                //不可空翻
                if (!flag)
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
            }
            //是棋子
            else
            {
                //条件未满足时，开启条件的满足，可翻山
                if (!flag)
                {
                    flag = true;
                }
                //已开启，判断当前是否为同一方，如果是，此位置不可以移动
                //如果不是，则此子可吃，即可移动到此位置，则需显示
                //结束当前遍历
                else
                {
                    if (!gameManager.rules.IsSameSide(chessID, position[x, y]))
                    {
                        GetCanMovePos(position, fromX, fromY, x, y);
                    }
                    break;
                }
            }
            x++;
        }
        //上
        x = fromX - 1;
        flag = false;
        while (x >= 0)
        {
            //是空格子
            if (position[x, y] == 0)
            {
                //在未达成翻山条件前，显示所有可以移动的路径，达成之后
                //不可空翻
                if (!flag)
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
            }
            //是棋子
            else
            {
                //条件未满足时，开启条件的满足，可翻山
                if (!flag)
                {
                    flag = true;
                }
                //已开启，判断当前是否为同一方，如果是，此位置不可以移动
                //如果不是，则此子可吃，即可移动到此位置，则需显示
                //结束当前遍历
                else
                {
                    if (!gameManager.rules.IsSameSide(chessID, position[x, y]))
                    {
                        GetCanMovePos(position, fromX, fromY, x, y);
                    }
                    break;
                }
            }
            x--;
        }
    }

    /// <summary>
    /// 黑仕
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    private void GetB_GuardMove(int[,] position, int fromX, int fromY)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 3; y < 6; y++)
            {
                if (gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
            }
        }
    }

    /// <summary>
    /// 红士
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    private void GetR_GuardMove(int[,] position, int fromX, int fromY)
    {
        for (int x = 7; x < 10; x++)
        {
            for (int y = 3; y < 6; y++)
            {
                if (gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
                {
                    GetCanMovePos(position, fromX, fromY, x, y);
                }
            }
        }
    }

    /// <summary>
    /// 红相黑象
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    private void GetMinisterMove(int[,] position, int fromX, int fromY)
    {
        int x, y;
        //右下走
        x = fromX + 2;
        y = fromY + 2;
        if (x < 10 && y < 9 && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //右上走
        x = fromX - 2;
        y = fromY + 2;
        if (x >= 0 && y < 9 && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //左下走
        x = fromX + 2;
        y = fromY - 2;
        if (x < 10 && y >= 0 && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //左上走
        x = fromX - 2;
        y = fromY - 2;
        if (x >= 0 && y >= 0 && gameManager.rules.IsValidMove(position, fromX, fromY, x, y))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
    }

    /// <summary>
    /// 黑卒
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    private void GetB_CaptureMove(int[,] position, int fromX, int fromY)
    {
        int x, y;
        int chessID;
        chessID = position[fromX, fromY];
        x = fromX + 1;
        y = fromY;
        if (x < 10 && !gameManager.rules.IsSameSide(chessID, position[x, y]))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //过河后
        if (fromX > 4)
        {
            x = fromX;
            y = fromY + 1;//右边
            if (y < 9 && !gameManager.rules.IsSameSide(chessID, position[x, y]))
            {
                GetCanMovePos(position, fromX, fromY, x, y);
            }
            y = fromY - 1;//左边
            if (y >= 0 && !gameManager.rules.IsSameSide(chessID, position[x, y]))
            {
                GetCanMovePos(position, fromX, fromY, x, y);
            }
        }
    }

    /// <summary>
    /// 红兵
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fromX"></param>
    /// <param name="fromY"></param>
    private void GetR_CaptureMove(int[,] position, int fromX, int fromY)
    {
        int x, y;
        int chessID;
        chessID = position[fromX, fromY];
        x = fromX - 1;
        y = fromY;
        if (x > 0 && !gameManager.rules.IsSameSide(chessID, position[x, y]))
        {
            GetCanMovePos(position, fromX, fromY, x, y);
        }
        //过河后
        if (fromX < 5)
        {
            x = fromX;
            y = fromY + 1;//右边
            if (y < 9 && !gameManager.rules.IsSameSide(chessID, position[x, y]))
            {
                GetCanMovePos(position, fromX, fromY, x, y);
            }
            y = fromY - 1;//左边
            if (y >= 0 && !gameManager.rules.IsSameSide(chessID, position[x, y]))
            {
                GetCanMovePos(position, fromX, fromY, x, y);
            }
        }
    }
    #endregion

    private void GetCanMovePos(int[,] position, int fromX, int fromY, int toX, int toY)
    {
        if (!gameManager.rules.KingKill(position, fromX, fromY, toX, toY))
        {
            return;
        }
        GameObject item;
        if (position[toX, toY] == 0)
        {
            item = gameManager.PopCanMoveUI();
        }
        else
        {
            item = gameManager.canEatPosUIGo;
        }
        item.transform.SetParent(gameManager.boardGrid[toX, toY].transform);
        MTools.SetLocalPositionZero(item);
        item.transform.localScale = Vector3.one;
    }
}
