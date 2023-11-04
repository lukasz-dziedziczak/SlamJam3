using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform door;
    [SerializeField] float closedAngle;
    [SerializeField] float openAngle;
    [SerializeField] float lerpTime = 0.4f;
    [SerializeField] AudioSource audioSource;

    bool isOpen;
    bool isOpening;
    bool isClosing;
    bool isMoving => isOpening || isClosing;
    float timer;
    float angle => door.transform.localEulerAngles.y;

    private void Start()
    {
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (timer < lerpTime)
        {
            timer += Time.deltaTime;
            if (timer > lerpTime) timer = lerpTime;
        }

        if (isOpening)
        {
            float newAngle = Mathf.LerpAngle(closedAngle, openAngle, timer/lerpTime);
            door.transform.localEulerAngles = new Vector3(0.0f, newAngle, 0.0f);
            if (timer / lerpTime == 1)
            {
                isOpening = false;
                isOpen = true;
            }
        }
        else if(isClosing)
        {
            float newAngle = Mathf.LerpAngle(openAngle, closedAngle, timer / lerpTime);
            door.transform.localEulerAngles = new Vector3(0.0f, newAngle, 0.0f);
            if (timer / lerpTime == 1)
            {
                isClosing = false;
                isOpen = false;
            }
        }
    }

    public void Interact()
    {
        if (isMoving) return;

        if (isOpen) isClosing = true;
        else isOpening = true;
        timer = 0;

        audioSource.Play();
    }

    public void Set(bool open)
    {
        if (open)
        {
            door.transform.localEulerAngles = new Vector3 (0.0f, openAngle, 0.0f);
        }
        else
        {
            door.transform.localEulerAngles = new Vector3(0.0f, closedAngle, 0.0f);
        }
        isOpen = open;
        //print("door set " + open.ToString());
    }
}
