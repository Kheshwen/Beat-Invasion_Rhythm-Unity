using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingEffect : MonoBehaviour
{
    public Text gameover;
    public Color blinkEffect;

    // Start is called before the first frame update
    void Start()
    {
        blinkEffect = gameover.color;
    }

    // Update is called once per frame
    void Update()
    {
        blinkEffect.a = Mathf.Round(Mathf.PingPong(Time.unscaledTime * 2.0f, 1));      //make sure the text is always blinking
        gameover.color = blinkEffect;
    }
}
