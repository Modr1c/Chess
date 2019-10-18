/****************************************************
    文件：AudioSourceManager.cs
    作者：Modr1c
    邮箱：984564258@qq.com
    日期：2019/10/18 10:7:19
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制音效与背景音乐的播放与切换，在GameManager之后实例化
/// </summary>

public class AudioSourceManager : MonoBehaviour
{
    private void Awake()
    {
        Instance = this;
    }

    public static AudioSourceManager Instance = null;

}
