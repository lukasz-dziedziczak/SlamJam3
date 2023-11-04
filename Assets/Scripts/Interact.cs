using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Interact : MonoBehaviour
{
    Player player;
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
    [SerializeField] float rayLength = 5f;
    [SerializeField] LayerMask rayHitable;

    bool canInteract;
    Switch hitSwitch;
    Door hitDoor;

    private void Awake()
    {
        if (player == null) player = GetComponent<Player>();
    }

    private void Start()
    {
        player.Input.Interact += OnInteract;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = player.Camera.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength, rayHitable))
        {
            canInteract = true;
            UI.UpdatePointer(true);

            if (hit.collider.TryGetComponent<Switch>(out Switch _switch))
            {
                hitSwitch = _switch;
            }
            else
            {
                Door _door = hit.collider.GetComponentInParent<Door>();
                if (_door != null) hitDoor = _door;
            }
        }

        else
        {
            canInteract = false;
            UI.UpdatePointer(false);
            hitSwitch = null;
            hitDoor = null;
        }
    }

    private void OnInteract()
    {
        if (hitSwitch != null) hitSwitch.Interact();
        else if (hitDoor != null) hitDoor.Interact();
    }
}
