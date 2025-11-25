using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{

    public float lifetime = 0.20f;    // how long until it destroyed

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);     //destroy game object after lifetime become zero
    }
}

