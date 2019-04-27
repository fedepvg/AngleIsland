using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    private Rigidbody rigi;
    [SerializeField]
    private float SpeedZ;
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
        rigi = GetComponentInParent<Rigidbody>();
        SpeedZ = 20f;
        XRotationRate = 70f;
        ZRotationRate = -70f;
    }

    private void FixedUpdate()
    {
        GetInput(ref InputX,ref InputZ);
        

        rigi.AddRelativeTorque(InputX * XRotationRate * Time.deltaTime, 0f, InputZ * ZRotationRate * Time.deltaTime, ForceMode.Force);

        rigi.AddRelativeForce(new Vector3(0, 0, 10 * SpeedZ));

        Debug.Log(rigi.velocity.sqrMagnitude);
        Debug.DrawRay(transform.position, transform.forward*10);
    }

    void GetInput(ref float InputX,ref float InputZ)
    {
        InputX = Input.GetAxis("Vertical");
        InputZ = Input.GetAxis("Horizontal");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer)=="Floor")
        {
            rigi.useGravity = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Floor")
        {
            rigi.useGravity = true;
        }
    }
}
