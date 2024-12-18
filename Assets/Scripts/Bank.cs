using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    private int heldMoney = 0;

    //stores money into bank when you pass the space
    public int OnPassing(int leftovers)
    {
        if (leftovers - 5 < 0)
        {
            heldMoney += leftovers;
            return 0;
        }
        heldMoney += 5;
        return leftovers - 5;
    }

    //gives you the bank's money when you land on the space
    public int OnLanding()
    {
        int givenMoney = heldMoney;
        heldMoney = 0;
        return givenMoney;
    }
}
