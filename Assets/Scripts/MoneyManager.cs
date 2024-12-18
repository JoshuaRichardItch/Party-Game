using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public class MoneyManager : MonoBehaviour
{
    //players
    public GameObject player1;
    private PlayerController player1con;
    public GameObject player2;
    private PlayerController player2con;
    public GameObject player3;
    private PlayerController player3con;
    public GameObject player4;
    private PlayerController player4con;
    //variables
    private int friendDoubloons = 5;
    // Start is called before the first frame update
    void Start()
    {
        player1con = player1.GetComponent<PlayerController>();
        player2con = player2.GetComponent<PlayerController>();
        player3con = player3.GetComponent<PlayerController>();
        player4con = player4.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Share 5 coins with a friend
    public bool Friendship(int ID)
    {
        if (ID == 1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                player1con.SetDoubloons(friendDoubloons);
                player2con.SetDoubloons(friendDoubloons);
                return true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                player1con.SetDoubloons(friendDoubloons);
                player3con.SetDoubloons(friendDoubloons);
                return true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                player1con.SetDoubloons(friendDoubloons);
                player4con.SetDoubloons(friendDoubloons);
                return true;
            }
        }
        else if (ID == 2)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                player2con.SetDoubloons(friendDoubloons);
                player1con.SetDoubloons(friendDoubloons);
                return true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                player2con.SetDoubloons(friendDoubloons);
                player3con.SetDoubloons(friendDoubloons);
                return true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                player2con.SetDoubloons(friendDoubloons);
                player4con.SetDoubloons(friendDoubloons);
                return true;
            }
        }
        else if (ID == 3)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                player3con.SetDoubloons(friendDoubloons);
                player1con.SetDoubloons(friendDoubloons);
                return true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                player3con.SetDoubloons(friendDoubloons);
                player2con.SetDoubloons(friendDoubloons);
                return true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                player3con.SetDoubloons(friendDoubloons);
                player4con.SetDoubloons(friendDoubloons);
                return true;
            }
        }
        else if (ID == 4)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                player4con.SetDoubloons(friendDoubloons);
                player1con.SetDoubloons(friendDoubloons);
                return true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                player4con.SetDoubloons(friendDoubloons);
                player2con.SetDoubloons(friendDoubloons);
                return true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                player4con.SetDoubloons(friendDoubloons);
                player3con.SetDoubloons(friendDoubloons);
                return true;
            }
        }
        return false;
    }

    // debt space
    public void Debt(int ID)
    {
        if (ID == 1) { player1con.SetDoubloons(-3); }
        else if (ID == 2) { player2con.SetDoubloons(-3); }
        else if (ID == 3) { player3con.SetDoubloons(-3); }
        else if (ID == 4) { player4con.SetDoubloons(-3); }
    }

    // chance time
    public bool ChanceTime(int ID)
    {
        int temp1 = 0;
        int temp2 = 0;
        int target1 = Random.Range(1, 5);
        int target2 = Random.Range(1, 5);
        if (target1 == target2) { target2++; }
        if (target2 > 4) { target2 = 1; }
        int chanceEffect = Random.Range(0, 4);
        Debug.Log(target1 + " & " + target2 + ", " + chanceEffect);
        // doubloon transfer
        if (chanceEffect == 0)
        {
            if (target1 == 1)
            {
                player1con.SetDoubloons(-10);
            }
            if (target1 == 2)
            {
                player2con.SetDoubloons(-10);
            }
            if (target1 == 3)
            {
                player3con.SetDoubloons(-10);
            }
            if (target1 == 4)
            {
                player4con.SetDoubloons(-10);
            }

            if (target2 == 1)
            {
                player1con.SetDoubloons(10);
            }
            if (target2 == 2)
            {
                player2con.SetDoubloons(10);
            }
            if (target2 == 3)
            {
                player3con.SetDoubloons(10);
            }
            if (target2 == 4)
            {
                player4con.SetDoubloons(10);
            }
        }
        // bone transfer
        else if (chanceEffect == 1)
        {
            if (target1 == 1)
            {
                player1con.SetBones(-1);
            }
            if (target1 == 2)
            {
                player2con.SetBones(-1);
            }
            if (target1 == 3)
            {
                player3con.SetBones(-1);
            }
            if (target1 == 4)
            {
                player4con.SetBones(-1);
            }

            if (target2 == 1)
            {
                player1con.SetBones(1);
            }
            if (target2 == 2)
            {
                player2con.SetBones(1);
            }
            if (target2 == 3)
            {
                player3con.SetBones(1);
            }
            if (target2 == 4)
            {
                player4con.SetBones(1);
            }
        }
        // doubloon swap
        else if (chanceEffect == 2)
        {
            // target1 == player 1
            if (target1 == 1 && target2 == 2)
            {
                temp1 = player1con.GetDoubloons();
                temp2 = player2con.GetDoubloons();
                player1con.SetDoubloons(-temp1);
                player2con.SetDoubloons(-temp2);
                player1con.SetDoubloons(temp2);
                player2con.SetDoubloons(temp1);
            }
            if (target1 == 1 && target2 == 3)
            {
                temp1 = player1con.GetDoubloons();
                temp2 = player3con.GetDoubloons();
                player1con.SetDoubloons(-temp1);
                player3con.SetDoubloons(-temp2);
                player1con.SetDoubloons(temp2);
                player3con.SetDoubloons(temp1);
            }
            if (target1 == 1 && target2 == 4)
            {
                temp1 = player1con.GetDoubloons();
                temp2 = player4con.GetDoubloons();
                player1con.SetDoubloons(-temp1);
                player4con.SetDoubloons(-temp2);
                player1con.SetDoubloons(temp2);
                player4con.SetDoubloons(temp1);
            }
            // target1 == player 2
            if (target1 == 2 && target2 == 1)
            {
                temp1 = player2con.GetDoubloons();
                temp2 = player1con.GetDoubloons();
                player2con.SetDoubloons(-temp1);
                player1con.SetDoubloons(-temp2);
                player2con.SetDoubloons(temp2);
                player1con.SetDoubloons(temp1);
            }
            if (target1 == 2 && target2 == 3)
            {
                temp1 = player2con.GetDoubloons();
                temp2 = player3con.GetDoubloons();
                player2con.SetDoubloons(-temp1);
                player3con.SetDoubloons(-temp2);
                player2con.SetDoubloons(temp2);
                player3con.SetDoubloons(temp1);
            }
            if (target1 == 2 && target2 == 4)
            {
                temp1 = player2con.GetDoubloons();
                temp2 = player4con.GetDoubloons();
                player2con.SetDoubloons(-temp1);
                player4con.SetDoubloons(-temp2);
                player2con.SetDoubloons(temp2);
                player4con.SetDoubloons(temp1);
            }
            // target1 == player 3
            if (target1 == 3 && target2 == 1)
            {
                temp1 = player3con.GetDoubloons();
                temp2 = player1con.GetDoubloons();
                player3con.SetDoubloons(-temp1);
                player1con.SetDoubloons(-temp2);
                player3con.SetDoubloons(temp2);
                player1con.SetDoubloons(temp1);
            }
            if (target1 == 3 && target2 == 2)
            {
                temp1 = player3con.GetDoubloons();
                temp2 = player2con.GetDoubloons();
                player3con.SetDoubloons(-temp1);
                player2con.SetDoubloons(-temp2);
                player3con.SetDoubloons(temp2);
                player2con.SetDoubloons(temp1);
            }
            if (target1 == 3 && target2 == 4)
            {
                temp1 = player3con.GetDoubloons();
                temp2 = player4con.GetDoubloons();
                player3con.SetDoubloons(-temp1);
                player4con.SetDoubloons(-temp2);
                player3con.SetDoubloons(temp2);
                player4con.SetDoubloons(temp1);
            }
            // target1 == player 4
            if (target1 == 4 && target2 == 1)
            {
                temp1 = player4con.GetDoubloons();
                temp2 = player1con.GetDoubloons();
                player4con.SetDoubloons(-temp1);
                player1con.SetDoubloons(-temp2);
                player4con.SetDoubloons(temp2);
                player1con.SetDoubloons(temp1);
            }
            if (target1 == 4 && target2 == 2)
            {
                temp1 = player4con.GetDoubloons();
                temp2 = player2con.GetDoubloons();
                player4con.SetDoubloons(-temp1);
                player2con.SetDoubloons(-temp2);
                player4con.SetDoubloons(temp2);
                player2con.SetDoubloons(temp1);
            }
            if (target1 == 4 && target2 == 3)
            {
                temp1 = player4con.GetDoubloons();
                temp2 = player3con.GetDoubloons();
                player4con.SetDoubloons(-temp1);
                player3con.SetDoubloons(-temp2);
                player4con.SetDoubloons(temp2);
                player3con.SetDoubloons(temp1);
            }
        }
        // bone swap
        else if (chanceEffect == 3)
        {
            // target1 == player 1
            if (target1 == 1 && target2 == 2)
            {
                temp1 = player1con.GetBones();
                temp2 = player2con.GetBones();
                player1con.SetBones(-temp1);
                player2con.SetBones(-temp2);
                player1con.SetBones(temp2);
                player2con.SetBones(temp1);
            }
            if (target1 == 1 && target2 == 3)
            {
                temp1 = player1con.GetBones();
                temp2 = player3con.GetBones();
                player1con.SetBones(-temp1);
                player3con.SetBones(-temp2);
                player1con.SetBones(temp2);
                player3con.SetBones(temp1);
            }
            if (target1 == 1 && target2 == 4)
            {
                temp1 = player1con.GetBones();
                temp2 = player4con.GetBones();
                player1con.SetBones(-temp1);
                player4con.SetBones(-temp2);
                player1con.SetBones(temp2);
                player4con.SetBones(temp1);
            }
            // target1 == player 2
            if (target1 == 2 && target2 == 1)
            {
                temp1 = player2con.GetBones();
                temp2 = player1con.GetBones();
                player2con.SetBones(-temp1);
                player1con.SetBones(-temp2);
                player2con.SetBones(temp2);
                player1con.SetBones(temp1);
            }
            if (target1 == 2 && target2 == 3)
            {
                temp1 = player2con.GetBones();
                temp2 = player3con.GetBones();
                player2con.SetBones(-temp1);
                player3con.SetBones(-temp2);
                player2con.SetBones(temp2);
                player3con.SetBones(temp1);
            }
            if (target1 == 2 && target2 == 4)
            {
                temp1 = player2con.GetBones();
                temp2 = player4con.GetBones();
                player2con.SetBones(-temp1);
                player4con.SetBones(-temp2);
                player2con.SetBones(temp2);
                player4con.SetBones(temp1);
            }
            // target1 == player 3
            if (target1 == 3 && target2 == 1)
            {
                temp1 = player3con.GetBones();
                temp2 = player1con.GetBones();
                player3con.SetBones(-temp1);
                player1con.SetBones(-temp2);
                player3con.SetBones(temp2);
                player1con.SetBones(temp1);
            }
            if (target1 == 3 && target2 == 2)
            {
                temp1 = player3con.GetBones();
                temp2 = player2con.GetBones();
                player3con.SetBones(-temp1);
                player2con.SetBones(-temp2);
                player3con.SetBones(temp2);
                player2con.SetBones(temp1);
            }
            if (target1 == 3 && target2 == 4)
            {
                temp1 = player3con.GetBones();
                temp2 = player4con.GetBones();
                player3con.SetBones(-temp1);
                player4con.SetBones(-temp2);
                player3con.SetBones(temp2);
                player4con.SetBones(temp1);
            }
            // target1 == player 4
            if (target1 == 4 && target2 == 1)
            {
                temp1 = player4con.GetBones();
                temp2 = player1con.GetBones();
                player4con.SetBones(-temp1);
                player1con.SetBones(-temp2);
                player4con.SetBones(temp2);
                player1con.SetBones(temp1);
            }
            if (target1 == 4 && target2 == 2)
            {
                temp1 = player4con.GetBones();
                temp2 = player2con.GetBones();
                player4con.SetBones(-temp1);
                player2con.SetBones(-temp2);
                player4con.SetBones(temp2);
                player2con.SetBones(temp1);
            }
            if (target1 == 4 && target2 == 3)
            {
                temp1 = player4con.GetBones();
                temp2 = player3con.GetBones();
                player4con.SetBones(-temp1);
                player3con.SetBones(-temp2);
                player4con.SetBones(temp2);
                player3con.SetBones(temp1);
            }
        }
        return true;
    }

}
