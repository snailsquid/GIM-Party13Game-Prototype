using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public float maxX = 250f;
    public float minX = -250f;
    public float moveSpeed = 250f;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            moveHook(1);
        }
        else
        {
            moveHook(-1);
        }
    }
    private void moveHook(float moveInput)
    {
        Vector3 movement = Vector3.right * moveInput * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + movement;
        newPosition.x = Mathf.Clamp(newPosition.x,minX,maxX);
        transform.localPosition = newPosition;
    }
}