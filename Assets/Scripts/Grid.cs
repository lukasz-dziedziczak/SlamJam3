using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance { get; private set; }
    [field: SerializeField] public Room[] Rooms {  get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        foreach(Room room in Rooms)
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

    public struct Cord : IEquatable<Cord>
    {
        public int Column { get; private set; }
        public int Row { get; private set; }

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
}
