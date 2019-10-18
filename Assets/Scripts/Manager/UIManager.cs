/****************************************************
    文件：UIManager.cs
    作者：Modr1c
    邮箱：984564258@qq.com
    日期：2019/10/18 9:56:24
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制页面之间的显示与跳转，按钮的触发方法，在GameManager之后实例化
/// </summary>

public class UIManager : MonoBehaviour
{
    private void Awake()
    {
        Instance = this;

        btn_Standalone.onClick.AddListener(StandaloneMode);
        btn_networking.onClick.AddListener(NetworkingMode);
        btn_Exit.onClick.AddListener(ExitGame);

        btn_PVE.onClick.AddListener(PVEMode);
        btn_PVP.onClick.AddListener(PVPMode);

        btn_Easy.onClick.AddListener(() =>
        {
            LevelOption(1);
        });
        btn_Normal.onClick.AddListener(() =>
        {
            LevelOption(2);
        });
        btn_Hard.onClick.AddListener(() =>
        {
            LevelOption(3);
        });

        btn_Undo.onClick.AddListener(Undo);
        btn_Replay.onClick.AddListener(Replay);
        btn_Return.onClick.AddListener(ReturnToMain);

        btn_Start.onClick.AddListener(StartNetWorkingMode);
        btn_GiveUp.onClick.AddListener(GiveUp);
        btn_ExitGame.onClick.AddListener(ExitGame);
    }

    public static UIManager Instance = null;

    /// <summary>
    /// 0.主菜单 1.单机 2.模式选择 3.难度选择 5.单机游戏 6.联网游戏
    /// </summary>
    public GameObject[] panels;

    private GameManager gameManager;

    #region UI Define
    /// <summary>
    /// 当前需要改变具体文本的显示UI
    /// </summary>
    public Text tipUIText;

    /// <summary>
    /// 两个对应显示UI的引用
    /// </summary>
    public Text[] tipUITexts;

    public Button btn_Standalone;
    public Button btn_networking;
    public Button btn_Exit;

    public Button btn_PVE;
    public Button btn_PVP;

    public Button btn_Easy;
    public Button btn_Normal;
    public Button btn_Hard;

    public Button btn_Undo;
    public Button btn_Replay;
    public Button btn_Return;

    public Button btn_Start;
    public Button btn_GiveUp;
    public Button btn_ExitGame;
    #endregion

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    #region 页面跳转

    /// <summary>
    /// 单机模式
    /// </summary>
    public void StandaloneMode()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
    }

    /// <summary>
    /// 联网模式
    /// </summary>
    public void NetworkingMode()
    {
        panels[0].SetActive(false);
        panels[5].SetActive(true);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// 人机模式
    /// </summary>
    public void PVEMode()
    {
        gameManager.chessPeople = 1;
        panels[2].SetActive(false);
        panels[3].SetActive(true);
    }
    /// <summary>
    /// 双人模式
    /// </summary>
    public void PVPMode()
    {
        tipUIText = tipUITexts[0];
        gameManager.chessPeople = 2;
        LoadGame();
    }

    /// <summary>
    /// 难度选择
    /// </summary>
    /// <param name="Level"></param>
    public void LevelOption(int Level)
    {
        gameManager.currentLevel = Level;
        tipUIText = tipUITexts[0];
        LoadGame();
    }

    #endregion

    #region 游戏加载
    /// <summary>
    /// 加载游戏
    /// </summary>
    private void LoadGame()
    {
        gameManager.ResetGame();
        SetUI();
        panels[4].SetActive(true);
    }

    /// <summary>
    /// 重置UI
    /// </summary>
    public void SetUI()
    {
        panels[2].SetActive(true);
        panels[3].SetActive(false);
        panels[1].SetActive(false);
        panels[0].SetActive(true);

    }
    #endregion

    #region 游戏中方法
    /// <summary>
    /// 悔棋
    /// </summary>
    public void Undo()
    {

    }

    /// <summary>
    /// 重玩
    /// </summary>
    public void Replay()
    {

    }

    /// <summary>
    /// 返回菜单
    /// </summary>
    public void ReturnToMain()
    {

    }

    /// <summary>
    /// 下棋轮次以及信息提示
    /// </summary>
    /// <param name="str"></param>
    public void ShowTip(string str)
    {
        tipUIText.text = str;
    }

    /// <summary>
    /// 开始联网
    /// </summary>
    public void StartNetWorkingMode()
    {

    }

    /// <summary>
    /// 认输
    /// </summary>
    public void GiveUp()
    {

    }
    #endregion
}
