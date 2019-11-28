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
                alarm.GetComponent<Light>().intensity = 1f;
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

        public void RestartScene()
        {
            StartCoroutine(RestartSceneAfterDuration(5f));
        }
        private IEnumerator RestartSceneAfterDuration(float duration)
        {
            yield return new WaitForSeconds(duration);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
