using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    //here is where i will make new booleans to simplify movement and player code
    private bool myTurn = false;
    private bool directionNotYetAdded = false;
    //game objects and scripts
    public GameObject spinWheel;
    private Spinner spinner;
    public GameObject bankSpace;
    private Bank bank;
    public GameObject turnKeeper;
    private Turns turns;
    public GameObject chanceSpace;
    private MoneyManager moneyManager;
    //player id
    public int playerID;
    //board spaces the player will jump to
    private float[,] positions =
    {
        //x
        {-12.07f, -12.84f, -13.79f, -14.72f, -16f, -17.29f, -18.77f, -20.12f, -21.39f, -22.71f, -24.04f, -25.42f, -26.81f, -27.18f, -27.45f, -27.99f, -28.88f, -30.09f, -31.43f, -32.93f, -34.56f, -34.87f, -35.15f, -35f, -34.36f, -33.37f, -32.38f, -31.2f, -29.98f, -28.59f, -27.1f, -25.48f, -24.25f, -22.97f, -21.73f, -20.41f, -18.79f, -17.01f, -15.37f, -13.99f, -12.48f, -10.88f, -8.77f, -7.13f, -5.59f, -4.5f, -4.08f, -3.86f, -4.08f, -4.49f, -4.99f, -5.73f, -6.44f, -7.18f, -7.84f, -8.86f},
        //y (may not be implemented)
        //z
        {-13.69f, -12.51f, -11.46f, -10.45f, -9.98f, -9.87f, -9.9f, -9.85f, -9.82f, -9.76f, -9.76f, -9.82f, -9.53f, -10.91f, -12.58f, -14.02f, -15.32f, -16.33f, -17.08f, -17.45f, -17.68f, -18.96f, -20.21f, -21.78f, -23.11f, -24.29f, -25.35f, -26.09f, -26.83f, -27.45f, -28.1f, -28.62f, -29.29f, -29.75f, -29.98f, -30.29f, -30.26f, -30.32f, -30.34f, -30.26f, -30.01f, -29.75f, -29.29f, -28.69f, -27.85f, -26.7f, -25.28f, -23.8f, -22.35f, -21.02f, -19.59f, -18.18f, -16.78f, -15.52f, -14.23f, -13.23f}
    };
    private float xModifier = 24.05697f - 3.521f;
    private float zModifier = 20.20824f - 9.858f;
    //length of the board space positions list + 1
    private string[] spaceTypes =
    {
        "dice", "friend", "debt", "unevent", "unevent", "friend", "lucky", "debt", "debt", "unevent", "friend", "rewind", "unevent", "friend", "unevent", "item", "bank", "dice", "unevent", "unevent", "item", "unevent", "zoom", "friend", "debt", "debt", "unlucky", "debt", "dice", "unlucky", "friend", "zoom", "unevent", "unevent", "debt", "item", "friend", "lucky", "lucky", "unevent", "curse", "curse", "zoom", "rewind", "bank", "unlucky", "lucky", "rewind", "lucky", "zoom", "friend", "unevent", "dice", "item", "lucky", "friend"
        //"item", "dice", "chance", "curse", "friend", "bank", "unevent", "lucky", "duel", "unlucky", "debt", "rewind", "zoom", "ffa", "buff", "property", "jail", "railroad", "promotion"
    };
    private int targetSpace = 0;
    private bool activateSpace = false;
    private Vector3 target;
    //player property
    public TextMeshProUGUI scoreText;
    private int doubloons = 10;
    private int bones = 2;
    private int level = 1;
    private int exp = 50;
    private int turnips = 5;
    private string[] items = { "", "", "" };
    private float speed = 10.0f;
    private int spacesToMove = 0;

    // Start is called before the first frame update
    void Start()
    {
        spinner = spinWheel.GetComponent<Spinner>();
        bank = bankSpace.GetComponent<Bank>();
        turns = turnKeeper.GetComponent<Turns>();
        moneyManager = chanceSpace.GetComponent<MoneyManager>();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (spinner.playerToMove == playerID) { myTurn = true; } else { myTurn = false; }
        if (!spinner.wheelIsSpinning && spinner.wheelCanBeSpun && Input.GetKeyDown(KeyCode.W) && myTurn)
        {
            CallSpinner(1);
        }
        if (!spinner.wheelIsSpinning && spinner.returnNow && myTurn) {
            spinner.returnNow = false;
            spacesToMove = spinner.returnValue;
        }
        if (spacesToMove != 0 && myTurn)
        {
            MoveSpaces(spinner.direction);
        }
        if (activateSpace && myTurn)
        {
            //item space (play an item minigame)
            if (spaceTypes[targetSpace] == "item")
            {

                if (Input.GetKeyUp(KeyCode.A))
                {
                    items[0] = "mushroomTicket";
                    UpdateTurn();
                }
                if (Input.GetKeyUp(KeyCode.S))
                {
                    items[1] = "mushroomTicket";
                    UpdateTurn();
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    items[2] = "mushroomTicket";
                    UpdateTurn();
                }
            }
            //dice space (get a random dice item)
            else if (spaceTypes[targetSpace] == "dice")
            {

                if (Input.GetKeyUp(KeyCode.A))
                {
                    items[0] = "doubleDice";
                    UpdateTurn();
                }
                if (Input.GetKeyUp(KeyCode.S))
                {
                    items[1] = "doubleDice";
                    UpdateTurn();
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    items[2] = "doubleDice";
                    UpdateTurn();
                }
            }
            //chance time space (mario party chance time)
            else if (spaceTypes[targetSpace] == "chance")
            {
                if (moneyManager.ChanceTime(playerID))
                {
                    UpdateTurn();
                }
            }
            //cursed space (very likely to lose a star, bowser space)
            else if (spaceTypes[targetSpace] == "curse")
            {
                int curse = Random.Range(1, 6);
                if (curse > 2 && bones > 0) { bones -= 1; } else { doubloons /= 20; }
                UpdateScore();
                UpdateTurn();
            }
            //friendship space (share 5 coins with another player)
            else if (spaceTypes[targetSpace] == "friend")
            {
                if (moneyManager.Friendship(playerID))
                {
                    UpdateTurn();
                }
            }
            //bank space (collect all coins in the bank if you land on the space)
            else if (spaceTypes[targetSpace] == "bank")
            {
                doubloons += bank.OnLanding();
                UpdateScore();
                UpdateTurn();
            }
            //uneventful space (does nothing)
            else if (spaceTypes[targetSpace] == "unevent")
            {
                UpdateTurn();
            }
            //lucky space (random positive event happens)
            else if (spaceTypes[targetSpace] == "lucky")
            {
                int luck = Random.Range(1, 6);
                if (luck < 4) { doubloons += (2 * luck + 5); } else if (luck == 4) {
                    //recieve item
                } else if (luck == 5)
                {
                    doubloons += bank.OnLanding();
                }
                Debug.Log(luck);
                UpdateScore();
                UpdateTurn();
            }
            //duel space (play a minigame with 1 other player for dubloons/bones)
            else if (spaceTypes[targetSpace] == "duel")
            {
                UpdateTurn();
            }
            //unlucky space (unlikely to lose a star, likely to lose coins)
            else if (spaceTypes[targetSpace] == "unlucky")
            {
                int unluck = Random.Range(1, 6);
                if (unluck == 6 && bones > 0) { bones -= 1; } else if (unluck < 5 && doubloons > 10) { doubloons -= 10; } else if (unluck == 5 && doubloons > 20) { doubloons -= 20; } else { doubloons += (unluck * 2); }
                UpdateScore();
                UpdateTurn();
            }
            //debt space (lose 3 coins)
            else if (spaceTypes[targetSpace] == "debt")
            {
                moneyManager.Debt(playerID);
                UpdateTurn();
            }
            //rewind space (roll again in the opposite direction)
            else if (spaceTypes[targetSpace] == "rewind")
            {
                directionNotYetAdded = true;
                CallSpinner(-1);
                activateSpace = false;
            }
            //zoom space (roll again)
            else if (spaceTypes[targetSpace] == "zoom")
            {
                directionNotYetAdded = true;
                CallSpinner(1);
                activateSpace = false;
            }
            //free for all space (play a minigame with all players with coins on the line)
            else if (spaceTypes[targetSpace] == "ffa")
            {
                UpdateTurn();
            }
            //buff space (gain 100 exp and level up)
            else if (spaceTypes[targetSpace] == "buff")
            {
                exp += 100;
                UpdateScore();
                UpdateTurn();
            }
            //property space (buy a property if you want to, then charge others rent for landing on it)
            else if (spaceTypes[targetSpace] == "property")
            {
                UpdateTurn();
            }
            //jail space (does nothing, traps players if they have less than 0 coins)
            else if (spaceTypes[targetSpace] == "jail")
            {
                UpdateTurn();
            }
            //railroad space (travel to the next railroad)
            else if (spaceTypes[targetSpace] == "railroad")
            {
                UpdateTurn();
            }
            //promotion space (75% chance to become a queen, 25% chance to become a horse)
            else if (spaceTypes[targetSpace] == "promotion")
            {
                UpdateTurn();
            }
            //break out of the if statement
            else
            {
                UpdateTurn();
            }
        }
        //}
    }
    //updates the scoreboard for the player
    void UpdateScore()
    {
        if (exp >= 100) { exp -= 100; level++; }
        scoreText.text = "Doubloons: " + doubloons + "\r\nBones: " + bones /*+ "\r\nLevel: " + level + "(" + exp + "/100)\r\nTurnips: " + turnips*/;
    }
    //wheel spin function
    void CallSpinner(int direction)
    {
        spinner.speed = 2000 + Random.Range(0, 200);
        spinner.direction = direction;
        spinner.wheelCanBeSpun = false;
        spinner.wheelIsSpinning = true;
    }
    //move number of spaces
    void MoveSpaces(int direction)
    {
        if (directionNotYetAdded)
        {
            directionNotYetAdded = false;
            spacesToMove+=direction;
        }
        target = new Vector3 (positions[0, targetSpace] + xModifier, transform.position.y, positions[1, targetSpace] + zModifier);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.z == target.z)
        {
            spacesToMove -= direction;
            if (spaceTypes[targetSpace] == "bank" && spacesToMove != 0)
            {
                doubloons = bank.OnPassing(doubloons);
                UpdateScore();
            }
        }
        if (spacesToMove == 0)
        {
            activateSpace = true;
            return;
        }
        if (transform.position.x == target.x && transform.position.z == target.z)
        {
            targetSpace += direction;
            if (targetSpace > 55)
            {
                targetSpace -= (56);
            }
            /*18else if (targetSpace < 0)
            {
                targetSpace += 19;
            }*/
        }
    }
    //end/next turn
    void UpdateTurn()
    {
        activateSpace = false;
        spinner.playerToMove++;
        if (spinner.playerToMove > 4)
        {
            spinner.playerToMove = 1;
        }
        spinner.wheelCanBeSpun = true;
        directionNotYetAdded = true;
    }
    //set doubloons
    public void SetDoubloons(int increment)
    {
        doubloons += increment;
        if (doubloons < 0) { doubloons = 0; }
        UpdateScore();
    }
    //set bones
    public void SetBones(int increment)
    {
        bones += increment;
        if (bones < 0) { bones = 0; }
        UpdateScore();
    }
    //set level
    public void SetExp(int increment)
    {
        exp += increment;
        UpdateScore();
    }
    //set turnips
    public void SetTurnips(int increment)
    {
        turnips += increment;
        UpdateScore();
    }
    //get doubloons
    public int GetDoubloons()
    {
        return doubloons;
    }
    //get bones
    public int GetBones()
    {
        return bones;
    }
}
