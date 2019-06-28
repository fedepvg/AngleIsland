using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

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
    Transform [] positions;
    [SerializeField]
    private InputField InputText;
    public Text BAckgroundText;
    public float RotSpeed;
    Quaternion DestRot;
    Quaternion OriginRotation;
    float LerpMultiplier;

    void Awake()
    {
        rigi = GetComponentInParent<Rigidbody>();
        XRotationRate = 70f;
        ZRotationRate = -70f;
        DestRot = Quaternion.identity;
    }

    private void Start()
    {
        CommandTerminal.Terminal.Shell.AddCommand("rx", RotateOnX, 1, 1, "Rotate on an axis 'n' euler angles");
        CommandTerminal.Terminal.Shell.AddCommand("ry", RotateOnY, 1, 1, "Rotate on Y axis 'n' euler angles");
        CommandTerminal.Terminal.Shell.AddCommand("rz", RotateOnZ, 1, 1, "Rotate on Z axis 'n' euler angles");
    }

    private void FixedUpdate()
    {        
        rigi.AddRelativeForce(new Vector3(0, 0, 10 * SpeedZ), ForceMode.Force);
    }

    private void Update()
    {
        Rotate();
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

    public void RotateOnX(CommandTerminal.CommandArg[] args)
    {
        float rot = args[0].Int;

        DestRot *= Quaternion.AngleAxis(rot, -transform.right);

        LerpMultiplier = 0;
        OriginRotation = transform.rotation;
    }

    public void RotateOnY(CommandTerminal.CommandArg[] args)
    {
        float rot = args[0].Int;

        DestRot *= Quaternion.AngleAxis(rot, transform.up);

        LerpMultiplier = 0;
        OriginRotation = transform.rotation;
    }

    public void RotateOnZ(CommandTerminal.CommandArg[] args)
    {
        float rot = args[0].Int;

        DestRot *= Quaternion.AngleAxis(rot, -transform.forward);

        LerpMultiplier = 0;
        OriginRotation = transform.rotation;
    }

    public void Rotate()
    {
        LerpMultiplier += RotSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(OriginRotation, DestRot, LerpMultiplier);
        if (LerpMultiplier >= 1)
        {
            LerpMultiplier = 0;
            OriginRotation = transform.rotation;
        }
        //if (transform.rotation == DestRot)
        //{
        //    DestRot = Quaternion.identity;
        //}
    }
}
