using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 moveInp { get; private set; }
    public bool jumpInp { get; private set; }
    public bool dashInp { get; private set; }

    private void Update()
    {
        moveInp = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        jumpInp = Input.GetButtonDown("Jump");
        dashInp = Input.GetKeyDown(KeyCode.LeftShift);
    }
}
