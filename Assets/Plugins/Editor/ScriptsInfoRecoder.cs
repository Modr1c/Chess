using System;
using System.IO;

public class ScriptsInfoRecoder : UnityEditor.AssetModificationProcessor {
    private static void OnWillCreateAsset(string path) {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs")) {
            string str = File.ReadAllText(path);
            str = str.Replace("#CreateAuthor#", Environment.UserName).Replace(
                              "#CreateTime#", string.Concat(DateTime.Now.Year, "/", DateTime.Now.Month, "/",
                                DateTime.Now.Day, " ", DateTime.Now.Hour, ":", DateTime.Now.Minute, ":", DateTime.Now.Second));
            File.WriteAllText(path, str);
        }
    }
}