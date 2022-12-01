using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerCollision : MonoBehaviour
{
 
    //door collision
    private bool doorIsOpen = false;
    private float doorTimer = 0.0f;
    private GameObject currentDoor;

    public float doorOpenTime = 3.0f;
    public AudioClip doorOpenSound;
    public AudioClip doorShutSound;

    //------------------------------------
    //Battery Collision
    public AudioClip batteryCollect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //to close the door
        if (doorIsOpen)
        {
            doorTimer += Time.deltaTime;

            if (doorTimer > doorOpenTime)
            {
                Door(doorShutSound, false, "DoorShut", currentDoor);
                doorTimer = 0.0f;
            }
        }


        //Player raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5.0f))
        {
            //to check to open door and hide battery gui
            if (hit.collider.gameObject.tag == "outpostDoor" && doorIsOpen == false && BatteryCollect.charge >= 4)
            {
                currentDoor = hit.collider.gameObject;
                Door(doorOpenSound, true, "DoorOpen", currentDoor);
                GameObject.FindWithTag("BatteryGUI").GetComponent<RawImage>().enabled = false;
               
            }
           
            //display battery gui
            else if(hit.collider.gameObject.tag == "outpostDoor" && doorIsOpen == false && BatteryCollect.charge < 4)
            {
                TextHints.message = "The door seems to need more power!";
                TextHints.textOn = true;
                GameObject.FindWithTag("BatteryGUI").GetComponent<RawImage>().enabled = true;
            }



        }






    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
       if(hit.collider == GameObject.Find("mat").GetComponent<Collider>())
        {
            CoconutThrow.canThrow = true;
        }
       else
        {
            CoconutThrow.canThrow = false;
        }
    }


    void Door(AudioClip aClip, bool openCheck, string animName, GameObject thisDoor)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = aClip;
        audio.Play();
        doorIsOpen = openCheck;
        //thisDoor.transform.parent.GetComponent<Animation>().Play(animName);
        thisDoor.transform.parent.GetComponent<Animator>().SetBool("collided", openCheck);


    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "battery")
        {
            AudioSource audio = GetComponent<AudioSource> ();
            audio.clip = batteryCollect;
            audio.Play();
            BatteryCollect.charge++;
            Destroy(other.gameObject);
        }
    }

}
