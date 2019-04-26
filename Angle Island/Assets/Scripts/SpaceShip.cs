using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    private Rigidbody rigi;
    [SerializeField]
    private float SpeedZ;
    [SerializeField]
    private float SpeedLimit;
    [SerializeField]
    private float XRotationRate;
    [SerializeField]
    private float ZRotationRate;
    float InputX;
    float InputZ;
    Vector2 MoveInput;
    Vector3 MoveDir=Vector3.zero;
    void Awake()
    {
        rigi=GetComponentInParent<Rigidbody>();
        SpeedZ = 0.1f;
        SpeedLimit = 50f;
        XRotationRate = 45f;
        ZRotationRate = 45f;
    }

    private void FixedUpdate()
    {
        GetInput(ref MoveInput);
        Vector3 DesiredMove =transform.forward * InputX + transform.up + transform.right * InputZ;
        //MoveDir.x = DesiredMove.x * SpeedZ;
        //MoveDir.z = DesiredMove.z * SpeedZ;
        if (rigi.velocity.z < SpeedLimit)
            rigi.AddForce(DesiredMove, ForceMode.Impulse);
        Debug.Log(rigi.velocity.z);
        Debug.DrawRay(transform.position, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-InputX * XRotationRate * Time.deltaTime, 0f, InputZ * ZRotationRate * Time.deltaTime);
    }

    void GetInput(ref Vector2 moveInput)
    {
        InputX = Input.GetAxis("Vertical");
        InputZ = Input.GetAxis("Horizontal");
    }
}
