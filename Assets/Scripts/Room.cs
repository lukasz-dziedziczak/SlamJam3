using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [field: SerializeField] public int Column { get; private set; }
    [field: SerializeField] public int Row {  get; private set; }
    [SerializeField] NeonLight[] roomLights;
    [SerializeField] Switch roomSwitch;
    [SerializeField] Door roomDoor;

    Grid grid;

    private void Awake()
    {
        if (roomLights.Length == 0)
        {
            roomLights = GetComponentsInChildren<NeonLight>();
        }
        
        if (roomSwitch == null) roomSwitch = GetComponentInChildren<Switch>();

        grid = GetComponentInParent<Grid>();
    }

    private void Start()
    {
    }

    public void UpdateLight()
    {
        foreach (NeonLight neonlight in roomLights)
        {
            neonlight.Set(roomSwitch.IsOn);
        }

        if (Grid.AllOn) Debug.LogWarning("Game Complete!");
    }

    public void Set(bool isOn)
    {
        if (roomSwitch == null) return;

        if (isOn)
        {
            roomSwitch.SetKnobPosOn();
        }
        else
        {
            roomSwitch.SetKnobPosOff();
        }
    }

    public void Toggle()
    {
        if (roomSwitch == null)
        {
            Debug.LogError(name + " missing roomSwitch referance");
            return;
        }

        roomSwitch.StartLerp();

        /*if (IsOn)
        {
            roomSwitch.SetKnobPosOff();
        }
        else
        {
            roomSwitch.SetKnobPosOn();
        }*/
    }

    public bool IsOn => roomSwitch.IsOn;

    public Grid.Cord Cords => new Grid.Cord(Column, Row);
}
