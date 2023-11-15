using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static Grid;

public class Grid : MonoBehaviour
{
    public static Grid Instance { get; private set; }
    [field: SerializeField] public Room[] Rooms {  get; private set; }
    [SerializeField] Layout[] layouts;
    [SerializeField] SFX_GameSounds gameSounds;
    bool gameComplete;
    float timer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        //SetAllOff();
        //SetRandomOn();
        SetRandomLayout();
        gameSounds.PlayOpeningSound();
    }

    private void Update()
    {
        if (!gameComplete) timer += Time.deltaTime;
    }

    private void RandomOnOff()
    {
        foreach (Room room in Rooms)
        {
            int random = UnityEngine.Random.Range(0, 2);

            if (random == 0) room.Set(false);
            else if (random == 1) room.Set(true);
        }
    }

    public static void Toggle(Cord cord)
    {
        List<Cord> cords = new List<Cord>();
        cords.Add(cord);
        cords.Add(new Cord(cord.Column - 1, cord.Row));
        cords.Add(new Cord(cord.Column + 1, cord.Row));
        cords.Add(new Cord(cord.Column, cord.Row - 1));
        cords.Add(new Cord(cord.Column, cord.Row + 1));

        foreach (Room room in Instance.Rooms)
        {
            if (cords.Contains(room.Cords))
            {
                room.Toggle();
            }
        }
    }

    public static void Set(Cord cord)
    {
        List<Cord> cords = new List<Cord>();
        cords.Add(cord);
        cords.Add(new Cord(cord.Column - 1, cord.Row));
        cords.Add(new Cord(cord.Column + 1, cord.Row));
        cords.Add(new Cord(cord.Column, cord.Row - 1));
        cords.Add(new Cord(cord.Column, cord.Row + 1));

        foreach (Room room in Instance.Rooms)
        {
            if (cords.Contains(room.Cords))
            {
                room.Set(true);
            }
        }
    }

    public static void SetAllOff()
    {
        foreach (Room room in Instance.Rooms)
        {
            room.Set(false);
        }
    }

    [Serializable]
    public struct Cord : IEquatable<Cord>
    {
        [field: SerializeField] public int Column { get; private set; }
        [field: SerializeField] public int Row { get; private set; }

        public Cord(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public bool Equals(Cord other)
        {
            return Column == other.Column && Row == other.Row;
        }
    }

    public static bool AllOn
    {
        get
        {
            foreach (Room room in Instance.Rooms)
            {
                if (!room.IsOn) return false;
            }
            return true;
        }
    }

    public static int OnCount
    {
        get
        {
            int count = 0;
            foreach (Room room in Instance.Rooms)
            {
                if (room.IsOn) count++;
            }
            return count;
        }
    }

    private void SetRandomOn(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            Rooms[UnityEngine.Random.Range(0, Rooms.Length)].Set(true);
        }
        
    }

    [Serializable]
    public class Layout
    {
        public bool[] Cords;
        public Cord[] Solution;

        public Layout()
        {
            Cords = new bool[25];
        }

        public void Set(Room[] rooms)
        {
            for(int i = 0;i < 25;i++)
            {
                rooms[i].Set(Cords[i]);
            }
        }
    }

    public void SetRandomLayout()
    {
        layouts[UnityEngine.Random.Range(0, layouts.Length)].Set(Rooms);

    }

    public static void GameComplete()
    {
        Instance.gameSounds.PlayCompleteSound();
        UI.Instance.Blackscreen.StartFadeOut();
        Instance.gameComplete = true;
        UI.Instance.Blackscreen.FadeOutComplete += UI.ShowWinScreen;
    }

    public static string TimeString
    {
        get
        {
            int minutes = Mathf.FloorToInt(Instance.timer / 60);
            int seconds = Mathf.FloorToInt(Instance.timer % 60);
            return minutes + ":" + seconds;
        }
    }
}
