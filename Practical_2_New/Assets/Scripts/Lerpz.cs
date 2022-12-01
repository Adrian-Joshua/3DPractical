using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerpz : MonoBehaviour
{
    public Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1F)
            anim.CrossFade("run");
        else
            anim.CrossFade("idle");
    }
}
