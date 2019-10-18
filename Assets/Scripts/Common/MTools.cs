/****************************************************
    文件：MTools.cs
    作者：Modr1c
    邮箱：984564258@qq.com
    日期：2019/10/18 10:29:41
*****************************************************/
using UnityEngine;

/// <summary>
/// 工具类
/// </summary>

public class MTools
{
    /// <summary>
    /// 根据组件名字递归寻找子物体
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="goName"></param>
    /// <returns></returns>
    public static Transform FindChild(Transform trans, string goName)
    {
        Transform child = trans.Find(goName);
        if (child != null)
            return child;

        Transform go;
        for (int i = 0; i < trans.childCount; i++)
        {
            child = trans.GetChild(i);
            go = FindChild(child, goName);
            if (go != null)
                return go;
        }
        return null;
    }

    /// <summary>
    /// 设置localPoisition为(0,0,0)
    /// </summary>
    /// <param name="go"></param>
    public static void SetLocalPositionZero(GameObject go)
    {
        go.transform.localPosition = Vector3.zero;
    }
}
