using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] Transform knob;
    [SerializeField] float knobOffRot = 38.058f;
    [SerializeField] float knobOnRot;
    [field: SerializeField] public bool IsOn { get; private set; }
    [SerializeField] Room room;
    [SerializeField] float lerpTime = 0.5f;
    [SerializeField] AudioSource audioSource;

    bool turningOff;
    bool turningOn;
    float timer;
    float t => timer / lerpTime;

    float angle => knob.transform.localEulerAngles.x;

    private void Awake()
    {
        if (room == null) room = GetComponentInParent<Room>();
    }

    private void Update()
    {
        if (timer < lerpTime)
        {
            timer += Time.deltaTime;
            if (timer > lerpTime) timer = lerpTime;
        }

        if (turningOn)
        {
            float newAngle = Mathf.LerpAngle(knobOffRot, knobOnRot, t);
            knob.transform.localEulerAngles = new Vector3(newAngle, 0, 0);

            if (t == 1)
            {
                turningOn = false;
                IsOn = true;
                room.UpdateLight();
            }
        }

        else if (turningOff)
        {
            float newAngle = Mathf.LerpAngle(knobOnRot, knobOffRot, t);
            knob.transform.localEulerAngles = new Vector3(newAngle, 0, 0);

            if (t == 1)
            {
                turningOff = false;
            }
        }
    }

    public void SetKnobPosOn()
    {
        knob.transform.localEulerAngles = new Vector3(knobOnRot, 0, 0);
        IsOn = true;
        room.UpdateLight();
    }

    public void SetKnobPosOff()
    {
        knob.transform.localEulerAngles = new Vector3(knobOffRot, 0, 0);
        IsOn = false;
        room.UpdateLight();
    }

    public void Interact()
    {
        if (turningOn || turningOff) return;
        Grid.Toggle(Cords);
        audioSource.Play();
    }

    public void StartLerp()
    {
        if (IsOn)
        {
            IsOn = false;
            turningOff = true;
            room.UpdateLight();
        }
        else turningOn = true;
        timer = 0;
    }

    public Grid.Cord Cords => room.Cords;
}
