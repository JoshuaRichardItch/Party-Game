using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    //here is where i will make new booleans to simplify movement and player code
    public bool wheelCanBeSpun = true;
    public bool wheelIsSpinning = false;
    public bool returnNow = false;

    public int playerToMove = 1;
    public float spinTimer = 2.0f;
    public int direction = 1;
    public GameObject center;
    public int speed = 2000;
    private float timer = 0.25f;
    public int returnValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wheelIsSpinning)
        {
            SpinTheWheel();
        }
    }
    void SpinTheWheel()
    {
        this.transform.RotateAround(center.transform.position, Vector3.down, speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            speed -= 200 + Random.Range(0, 75);
            timer = 0.25f;
        }
        if (speed <= 0)
        {
            wheelIsSpinning = false;
            StartCoroutine(ReturnReward());
        }
    }
    IEnumerator ReturnReward()
    {
        yield return new WaitForSeconds(0.35f);
        if(transform.eulerAngles.y < 36)
        {
            returnValue = 1;
            Debug.Log(returnValue);
        }
        else if (transform.eulerAngles.y < 72)
        {
            returnValue = 2;
            Debug.Log(returnValue);
        }
        else if (transform.eulerAngles.y < 108)
        {
            returnValue = 3;
            Debug.Log(returnValue);
        }
        else if (transform.eulerAngles.y < 144)
        {
            returnValue = 4;
            Debug.Log(returnValue);
        }
        else if (transform.eulerAngles.y < 180)
        {
            returnValue = 5;
            Debug.Log(returnValue);
        }
        else if (transform.eulerAngles.y < 216)
        {
            returnValue = 6;
            Debug.Log(returnValue);
        }
        else if (transform.eulerAngles.y < 252)
        {
            returnValue = 7;
            Debug.Log(returnValue);
        }
        else if (transform.eulerAngles.y < 288)
        {
            returnValue = 8;
            Debug.Log(returnValue);
        }
        else if (transform.eulerAngles.y < 324)
        {
            returnValue = 9;
            Debug.Log(returnValue);
        }
        else if (transform.eulerAngles.y < 360)
        {
            returnValue = 10;
            Debug.Log(returnValue);
        }
        returnValue *= direction;
        returnNow = true;
    }
}
