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
    private float DashSpeed;
    float InputX;
    float InputZ;
    Transform[] positions;
    [SerializeField]
    private InputField InputText;
    public Text BAckgroundText;
    public float RotSpeed;
    Quaternion DestRot;
    Quaternion OriginRotation;
    float LerpMultiplier;
    public GameObject Explosion;
    public GameObject Camera;

    void Awake()
    {
        rigi = GetComponentInParent<Rigidbody>();
        DestRot = Quaternion.identity;
    }

    private void Start()
    {
        CommandTerminal.Terminal.Shell.AddCommand("rx", RotateOnX, 1, 1, "Rotate on an axis 'n' euler angles");
        CommandTerminal.Terminal.Shell.AddCommand("ry", RotateOnY, 1, 1, "Rotate on Y axis 'n' euler angles");
        CommandTerminal.Terminal.Shell.AddCommand("rz", RotateOnZ, 1, 1, "Rotate on Z axis 'n' euler angles");

        CommandTerminal.Terminal.Shell.AddCommand("dz", DashOnZ, 1, 1, "Dash on Z axis with 'n' speed");
        CommandTerminal.Terminal.Shell.AddCommand("dx", DashOnX, 1, 1, "Dash on X axis with 'n' speed");
    }

    private void FixedUpdate()
    {        
        rigi.AddRelativeForce(Vector3.forward * SpeedZ, ForceMode.Force);
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

    public void RotateOnX(CommandTerminal.CommandArg[] args)
    {
        float rot = args[0].Int;

        rigi.velocity = Vector3.zero;

        DestRot *= Quaternion.AngleAxis(rot, -transform.right);

        LerpMultiplier = 0;
        OriginRotation = transform.rotation;
    }

    public void RotateOnY(CommandTerminal.CommandArg[] args)
    {
        float rot = args[0].Int;

        rigi.velocity = Vector3.zero;

        DestRot *= Quaternion.AngleAxis(rot, transform.up);

        LerpMultiplier = 0;
        OriginRotation = transform.rotation;
    }

    public void RotateOnZ(CommandTerminal.CommandArg[] args)
    {
        float rot = args[0].Int;

        rigi.velocity = Vector3.zero;

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
    }

    public void DashOnZ(CommandTerminal.CommandArg[] args)
    {
        float speed = args[0].Int;

        rigi.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    public void DashOnX(CommandTerminal.CommandArg[] args)
    {
        float speed = args[0].Int;

        rigi.AddRelativeForce(Vector3.right * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rigi.useGravity = false;
        rigi.velocity = Vector3.zero;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
        Explosion.SetActive(true);
        Camera.transform.position -= transform.forward * 40;
    }
}
