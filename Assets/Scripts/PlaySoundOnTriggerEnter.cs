using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTriggerEnter : MonoBehaviour
{
    public AudioClip soundToPlay;

    private void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
            return;
        GetComponent<AudioSource> ().clip = soundToPlay;
        GetComponent<AudioSource> ().Play ();


        OVRInput.SetControllerVibration(.5f, .5f,OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(.8f, .5f,OVRInput.Controller.LTouch);

        StartCoroutine(StopVibrate(.5f, true));
        StartCoroutine(StopVibrate(.8f, false));
    }
    private IEnumerator StopVibrate(float duration, bool isLeft)
    {
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, isLeft ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch);
    }


}
