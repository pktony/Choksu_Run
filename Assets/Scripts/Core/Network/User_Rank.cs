using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User_Rank
{
    public string userName;
    public long score;
    public long timeStamp;

    public User_Rank(string userName, long score, long timeStamp)
    {
        this.userName = userName;
        this.score = score;
        this.timeStamp = timeStamp;
    }
}
