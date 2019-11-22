using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakableItemScript : MonoBehaviour
{
    public GameObject explosion; // drag your explosion prefab here

    private float _maxHealth;
    public float itemHealth = 100f;
    public float dropDmg = 10f;
    public float wallDmg = 0.2f;
    public Mesh stage2Mesh;
    public Mesh stage3Mesh;
    public int currentStage = 1;
    public AudioClip stage2Sound;
    public AudioClip stage3Sound;
    public AudioClip destroySound;

    public bool endGame;


    public float magnitude = 2;


    private void Start()
    {
        GetComponent<AudioSource> ().playOnAwake = false;
        _maxHealth = itemHealth;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("magntiude: " + collision.relativeVelocity.magnitude);

        if (collision.relativeVelocity.magnitude > magnitude)
        {
            itemHealth -= dropDmg;
            ChangeItemBasedOnHealth();
            StartControllerVibrate(.5f);

        }




    }
    void OnTriggerStay(Collider collider)
    {
        if(collider.CompareTag("WallTrigger"))
        {
            Debug.Log("taking constantly " + wallDmg + "damage");
            itemHealth -= wallDmg;
            ChangeItemBasedOnHealth();
            //StartControllerVibrate(.5f);
        }

    }

    private void ChangeItemBasedOnHealth()
    {
        if (itemHealth > (_maxHealth / 3)&& itemHealth <= (_maxHealth / 3 * 2))
        {
            if (stage2Sound != null)
            {
                GetComponent<AudioSource>().clip = stage2Sound;
                GetComponent<AudioSource>().Play();
            }

            ChangeStage(2);

        } else if (itemHealth > 0 && itemHealth <= (_maxHealth / 3))
        {
            if (stage2Sound != null)
            {
                GetComponent<AudioSource>().clip = stage3Sound;
                GetComponent<AudioSource>().Play();
            }

            ChangeStage(3);
        } else if (CheckIfCanDestroyItem())
        {
            Explode(gameObject);
            if (endGame)
            {
                StartCoroutine(RestartSceneAfterDuration(1f));

            }
        }
        else
        {
            GetComponent<AudioSource> ().clip = stage2Sound;
            GetComponent<AudioSource> ().Play ();
        }

    }

    private void ChangeStage(int stage)
    {
        currentStage = stage;
        switch (stage)
        {
            case 2:
            {
                if (stage2Mesh != null)
                    GetComponent<MeshFilter>().mesh = stage2Mesh;
                break;
            }
            case 3:
            {
                if (stage3Mesh != null)
                    GetComponent<MeshFilter>().mesh = stage3Mesh;
                break;
            }
        }
    }

    //Max 2 seconds
    private void StartControllerVibrate(float duration)
    {
        OVRInput.SetControllerVibration(0.3f, 1, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0.3f, 1, OVRInput.Controller.LTouch);
        StartCoroutine(StopVibrate(duration));
    }

    private IEnumerator RestartSceneAfterDuration(float duration)
    {
        Debug.Log("restarting");

        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("SampleScene");
    }
    private IEnumerator StopVibrate(float duration)
    {
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }

    private bool CheckIfCanDestroyItem()
    {
        return itemHealth <= 0;
    }


    private void Explode(GameObject gObj)
    {
        GetComponent<AudioSource> ().Play ();
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
        expl.GetComponent<AudioSource>().clip = destroySound;
        expl.GetComponent<AudioSource> ().Play();
        Destroy(gObj); // destroy the grenade
        Destroy(expl, 1.5f); // delete the explosion after 3 seconds
    }

}
