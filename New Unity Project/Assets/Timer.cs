using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {


    public float min = 0;
    public float second = 0;
    private float time;
    public Text CountText;
    // Use this for initialization
    void Start () {
        time = min * 60 + second;

        /*we are NOT making a hour-level*/
        if(min > 60)
        {
            min = 60;
        }
    }
	
	// Update is called once per frame
	void Update () {
        /*update time*/
        time -= Time.deltaTime;

        second = Mathf.Floor(time) % 60;
        min = (Mathf.Floor(time) - second) / 60;

        if(min < 2)
        {
            /*this is bad but whatever*/
            CountText.color = Color.red;
        }

        /*show the value on the screen*/
        string buffer = "";
        if(second <10)
        {
            buffer = "0";
        }

        CountText.text = "Time : \n" + min.ToString() + ":" + buffer + second.ToString();
    }
}
