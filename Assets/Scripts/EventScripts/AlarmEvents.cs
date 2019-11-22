using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventScripts
{
    public class AlarmEvents : MonoBehaviour
    {
        public void EnableAlarmLights()
        {
            GameObject[] lights = GameObject.FindGameObjectsWithTag("AlarmLight");

            foreach (var alarm in lights)
            {
                alarm.GetComponent<Light>().intensity = .5f;
            }
        }

        public void PlayAlarmSound()
        {
            GameObject[] lights = GameObject.FindGameObjectsWithTag("AlarmLight");

            foreach (var alarm in lights)
            {
                alarm.GetComponent<AudioSource>().Play();
            }
        }

    }
}
