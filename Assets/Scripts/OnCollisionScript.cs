using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OnCollisionScript : MonoBehaviour
{
    public bool endGame;
    public UnityEvent alarm;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("MainCamera"))
        {
            if (endGame)
            {
                alarm.Invoke();
                StartCoroutine(RestartSceneAfterDuration(5f));
            }
        }
    }

    private IEnumerator RestartSceneAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("SampleScene");
    }

}
