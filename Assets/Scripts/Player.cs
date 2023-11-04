using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public Movement Movement { get; private set; }
    [field: SerializeField] public InputReader Input { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }
    [field: SerializeField] public Camera Camera { get; private set; }
}
