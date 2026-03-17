using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float openDistance = 2f;
    public float speed = 2f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;

    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;

    private bool open = false;

    void Start()
    {
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;


        leftOpenPos = leftClosedPos + leftDoor.forward * openDistance;
        rightOpenPos = rightClosedPos - rightDoor.forward * openDistance;
    }

    void Update()
    {
        if (open)
        {
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftOpenPos, Time.deltaTime * speed);
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightOpenPos, Time.deltaTime * speed);
        }
        else
        {
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftClosedPos, Time.deltaTime * speed);
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightClosedPos, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            open = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            open = false;
    }
}
