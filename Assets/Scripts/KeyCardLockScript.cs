using System;
using System.Collections;
using UnityEngine;

public class KeyCardLockScript : MonoBehaviour
{
    public GameObject door;
    private GameObject _indicator;

    public int keyCode = 1234;

    public AudioClip accessGranted;
    public AudioClip accessDenied;


    public LockStates LockState = LockStates.Idle;
    public enum LockStates
    {
        Granted,
        Idle,
        Denied

    }

    private HingeJoint _hingeJoint;
    // Start is called before the first frame update
    void Start()
    {
        if (door == null) return;
        _hingeJoint = door.GetComponent<HingeJoint>();
        _indicator = transform.GetChild(0).gameObject;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger fired");

        if (!other.gameObject.CompareTag("KeyCard")) return;
        GameObject keyCard = other.gameObject;
        var keyCardScript = keyCard.GetComponent<KeyCardScript>();

        Debug.Log(keyCardScript.keyCode == keyCode ? "Code is same!!!" : "Nope, code is not same");
        if (keyCardScript.keyCode == keyCode)
        {
            AccessGranted();
        }
        else
        {
            AccessDenied();
        }
    }

    private void AccessGranted()
    {
        StartCoroutine(_openDoorAfterDelay(.4f));
        ChangeIndicator(LockStates.Granted);

    }

    private void AccessDenied()
    {
        StartCoroutine(_setIndicatorToIdle());
        ChangeIndicator(LockStates.Denied);
    }

    private IEnumerator _setIndicatorToIdle()
    {
        yield return new WaitForSeconds(1);
        ChangeIndicator(LockStates.Idle);
    }

    private IEnumerator _openDoorAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OpenDoor();
    }

    private void OpenDoor()
    {
        if (door == null) return;
        var motor = _hingeJoint.motor;
        motor.force = 40;
        motor.targetVelocity = 100;
        motor.freeSpin = false;
        _hingeJoint.motor = motor;
        _hingeJoint.useMotor = true;
        LockState = LockStates.Granted;
    }

    private void ChangeIndicator(LockStates status)
    {
        var rend = _indicator.GetComponent<Renderer>();
        var audioSource = GetComponent<AudioSource>();
        switch (status)
        {
            case LockStates.Granted:
                rend.material.SetColor("_Color", Color.green);
                audioSource.clip = accessGranted;
                audioSource.Play();
                break;
            case LockStates.Idle:
                rend.material.SetColor("_Color", Color.yellow);
                break;
            case LockStates.Denied:
                rend.material.SetColor("_Color", Color.red);
                audioSource.clip = accessDenied;
                audioSource.Play();
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
