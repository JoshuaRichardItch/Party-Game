using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    
    public GameObject spinWheel;
    private Spinner spinner;
    //player id
    public int playerID;
    //board spaces the player will jump to
    private float[,] positions =
    {
        //x
        {-3.521f, -4.992f, -6.503f, -8.06f, -9.8f, -11.77f, -13.92f, -16.35f, -18.92f, -21.2f, -23.4f, -25.69f, -27.91f, -30.05f, -23.78f, -22.17f, -20.33f, -18.39f, -16.11f},
        //y (may not be implemented)
        //z
        {-9.858f, -9.858f, -9.858f, -9.858f, -9.858f, -9.858f, -9.858f, -9.858f, -9.858f, -9.858f, -9.858f, -9.858f, -9.858f, -9.858f, -11.6f, -11.6f, -11.6f, -11.6f, -11.6f}
    };
    private string[] spaceTypes =
    {
        "item", "dice", "chance", "curse", "friend", "bank", "event", "lucky", "duel", "unlucky", "debt", "rewind", "zoom", "ffa", "buff", "property", "jail", "railroad", "promotion"
    };
    private int targetSpace = 0;
    private bool activateSpace = false;
    //player property
    public TextMeshProUGUI scoreText;
    private int dubloons = 20;
    private int bones = 2;
    private int level = 1;
    private int exp = 50;
    private int turnips = 5;
    public string[] items = { "", "", "" };

    // Start is called before the first frame update
    void Start()
    {
        spinner = spinWheel.GetComponent<Spinner>();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activateSpace && !spinner.currentlyRotating && !spinner.returnReady)
        {
            CallSpinner(1);
        } 
        else if (spinner.returnReady && !activateSpace)
        {
            spinner.returnReady = false;
            MoveSpaces(spinner.returnValue);
        }
        else if (activateSpace)
        {
            //item space (play an item minigame)
            if (spaceTypes[targetSpace] == "item")
            {
                
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    items[0] = "mushroomTicket";
                    activateSpace = false;
                }
                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    items[1] = "mushroomTicket";
                    activateSpace = false;
                }
                if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    items[2] = "mushroomTicket";
                    activateSpace = false;

                }
            }
            //dice space (get a random dice item)
            else if (spaceTypes[targetSpace] == "dice")
            {

                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    items[0] = "doubleDice";
                    activateSpace = false;
                }
                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    items[1] = "doubleDice";
                    activateSpace = false;
                }
                if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    items[2] = "doubleDice";
                    activateSpace = false;

                }
            }
            //chance time space (mario party chance time)
            else if (spaceTypes[targetSpace] == "chance")
            {
                activateSpace = false;
            }
            //cursed space (very likely to lose a star, bowser space)
            else if (spaceTypes[targetSpace] == "curse")
            {
                activateSpace = false;
            }
            //friendship space (share 5 coins with another player)
            else if (spaceTypes[targetSpace] == "friend")
            {
                activateSpace = false;
            }
            //bank space (collect all coins in the bank if you land on the space)
            else if (spaceTypes[targetSpace] == "bank")
            {
                activateSpace = false;
            }
            //event space (special event based on the location happens)
            else if (spaceTypes[targetSpace] == "event")
            {
                activateSpace = false;
            }
            //lucky space (random positive event happens)
            else if (spaceTypes[targetSpace] == "lucky")
            {
                activateSpace = false;
            }
            //duel space (play a minigame with 1 other player for dubloons/bones)
            else if (spaceTypes[targetSpace] == "duel")
            {
                activateSpace = false;
            }
            //unlucky space (unlikely to lose a star, likely to lose coins)
            else if (spaceTypes[targetSpace] == "unlucky")
            {
                activateSpace = false;
            }
            //debt space (lose 3 coins)
            else if (spaceTypes[targetSpace] == "debt")
            {
                dubloons -= 3;
                UpdateScore();
                activateSpace = false;
            }
            //rewind space (roll again in the opposite direction)
            else if (spaceTypes[targetSpace] == "rewind")
            {
                CallSpinner(-1);
                activateSpace = false;
            }
            //zoom space (roll again)
            else if (spaceTypes[targetSpace] == "zoom")
            {
                CallSpinner(1);
                activateSpace = false;
            }
            //free for all space (play a minigame with all players with coins on the line)
            else if (spaceTypes[targetSpace] == "ffa")
            {
                activateSpace = false;
            }
            //buff space (gain 100 exp and level up)
            else if (spaceTypes[targetSpace] == "buff")
            {
                exp += 100;
                UpdateScore();
                activateSpace = false;
            }
            //property space (buy a property if you want to, then charge others rent for landing on it)
            else if (spaceTypes[targetSpace] == "property")
            {
                activateSpace = false;
            }
            //jail space (does nothing, traps players if they have less than 0 coins)
            else if (spaceTypes[targetSpace] == "jail")
            {
                activateSpace = false;
            }
            //railroad space (travel to the next railroad)
            else if (spaceTypes[targetSpace] == "railroad")
            {
                activateSpace = false;
            }
            //promotion space (75% chance to become a queen, 25% chance to become a horse)
            else if (spaceTypes[targetSpace] == "promotion")
            {
                activateSpace = false;
            }
            //break out of the if statement
            else
            {
                activateSpace = false;
            }
        }
    }
    //updates the scoreboard for the player
    void UpdateScore()
    {
        if (exp >= 100) { exp -= 100; level++; }
        scoreText.text = "Dubloons: " + dubloons + "\r\nBones: " + bones + "\r\nLevel: " + level + "(" + exp + "/100)\r\nTurnips: " + turnips;
    }
    //wheel spin function
    void CallSpinner(int direction)
    {
        spinner.speed = 2000 + Random.Range(0, 200);
        spinner.direction = direction;
        spinner.rotationTime = true;
        /*if (Input.GetKeyUp(KeyCode.W))
        {
            //determine roll
            diceRoll = Random.Range(0, 13);
            Debug.Log(diceRoll);
            activateSpace = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            //roll = 1 (debug)
            diceRoll = 1;
            Debug.Log(diceRoll);
            activateSpace = true;
        }*/
    }
    void MoveSpaces(int spinValue)
    {
        //jump to the next space
        activateSpace = true;
        targetSpace += (spinValue/* * direction */);
        if (targetSpace > 18)
        {
            targetSpace -= 19;
        }
        else if (targetSpace < 0)
        {
            targetSpace += 19;
        }
        transform.position = new Vector3(positions[0, targetSpace] + (24.05697f - 3.521f), transform.position.y, positions[1, targetSpace] + (20.20824f - 9.858f));
    }
}
