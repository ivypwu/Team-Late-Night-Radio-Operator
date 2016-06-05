using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {


    public float Starthour = 0;

    public float Endhour = 0;


    private float time;
    private float Endtime;
    public Text CountText;

    // Use this for initialization
    void Start () {
        /*game 1 min = real world 1 second*/
        float hour = Endhour - Starthour;
        Endtime = hour * 60;

    }
	
	// Update is called once per frame
	void Update () {
        /*update time*/
        time += Time.deltaTime;

        float min = Mathf.Floor(time) % 60;
        float hour = (Mathf.Floor(time) - min) / 60;

        if(time > Endtime * 0.8)
        {
            /*this is bad but whatever*/
            CountText.color = Color.red;
        }

        /*show the value on the screen*/
        string buffer = "";
        if(min < 10)
        {
            buffer = "0";
        }

        CountText.text = "Time : \n" + hour.ToString() + ":" + buffer + min.ToString();
    }
}
