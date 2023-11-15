using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxRotationAngle = 70;
    public bool Enabled;

    private void Awake()
    { 
        if (player == null) player = GetComponent<Player>();
    }

    private void Start()
    {
        player.Camera.transform.localRotation = Quaternion.identity;
        UI.Instance.Blackscreen.FadeInComplete += EnableMovement;
    }

    private void FixedUpdate()
    {
        if (!Enabled) return;

        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        position += 
            transform.forward * (player.Input.Movement.y * movementSpeed * Time.deltaTime)
            + transform.right * (player.Input.Movement.x * movementSpeed * Time.deltaTime);

        Vector3 eulerRot = rotation.eulerAngles;
        eulerRot.y += player.Input.Look.x * rotationSpeed * Time.deltaTime;
        rotation = Quaternion.Euler(eulerRot.x, eulerRot.y, eulerRot.z);

        player.Rigidbody.Move(position, rotation);

        Quaternion camRotation = player.Camera.transform.localRotation;
        Vector3 camRotEuler = camRotation.eulerAngles;
        camRotEuler.x += -player.Input.Look.y * rotationSpeed * Time.deltaTime;
        if (camRotEuler.x >= 180 && camRotEuler.x < 360 - maxRotationAngle)
        {
            camRotEuler.x = 360 - maxRotationAngle;
        }
        else if (camRotEuler.x < 180 && camRotEuler.x > maxRotationAngle)
        {
            camRotEuler.x = maxRotationAngle;
        }
        rotation = Quaternion.Euler(camRotEuler.x, camRotEuler.y, camRotEuler.z);
        player.Camera.transform.localRotation = rotation;
    }

    public void EnableMovement()
    {
        Enabled = true;
    }
}
