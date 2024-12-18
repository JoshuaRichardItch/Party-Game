using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    //keeps track of the last logged player id
    private int prevLogID = 0;
    private int turns = 0;
    //if this player id is smaller than the previous one, turns++
    public void nextTurnPlease(int ID)
    {
        if (prevLogID > ID)
        {
            turns++;
        }
        prevLogID = ID;
    }
}
