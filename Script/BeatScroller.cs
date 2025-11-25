using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;

    public Text presstostart;
    public Color blinkColor;


    void Start()
    {
        beatTempo = beatTempo / 60f;
        blinkColor = presstostart.color;

    }

    void Update()
    {
        if(!hasStarted)              //while we don't click anything , the text will infinitely blink
        {
            blinkColor.a = Mathf.Round(Mathf.PingPong(Time.unscaledTime * 2.0f, 1));
            presstostart.color = blinkColor;

            if (Input.anyKeyDown)            //when we click any key , the text will disappear
            {
                hasStarted = true;
                presstostart.text = "";
            }
        }
        else       //every game objects assigned with this script will move downward at the same time
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f); // only moving at y axis.

        }
    }
}
