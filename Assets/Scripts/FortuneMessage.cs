using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class FortuneMessage
{
    private List<string> textList;
    private System.Random rand;
    
    public FortuneMessage()
    {
        this.textList = new List<string>();
        this.rand = new System.Random();
        var filePath = Path.Combine(Application.streamingAssetsPath, "test.txt");
        try
        {
            using (var fs = new FileStream(filePath, FileMode.Open))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        this.textList.Add(line);
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        if (this.textList.Count == 0)
        {
            this.textList.Add("No Message");
        }
    }
        
    public string GetMessage()
    {
        return this.textList[this.rand.Next() % this.textList.Count];
    }
}
