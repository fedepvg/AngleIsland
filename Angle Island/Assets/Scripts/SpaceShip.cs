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
    public Text BackgroundText;
    public float RotSpeed;
    Quaternion DestRot;
    Quaternion OriginRotation;
    float LerpMultiplier;
    public GameObject Explosion;
    public GameObject Camera;
    public Mesh BondiMesh;
    public Material BondiMaterial;
    int BondiScaleMultiplier = 15;
    public GameObject BondiCamera;
    public GameObject ChoferCamera;

    public delegate void OnGameEnd();
    public static OnGameEnd GameEnd;

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
        CommandTerminal.Terminal.Shell.AddCommand("dy", DashOnX, 1, 1, "Dash on Y axis with 'n' speed");

        CommandTerminal.Terminal.Shell.AddCommand("south", SouthCommand, 0, 0, "DO NOT USE THIS COMMAND");
    }

    private void FixedUpdate()
    {        
        if(rigi)
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
        if (rigi)
        {
            float rot = args[0].Int;

            rigi.velocity = Vector3.zero;

            DestRot *= Quaternion.AngleAxis(rot, -transform.right);

            LerpMultiplier = 0;
            OriginRotation = transform.rotation;
        }
    }

    public void RotateOnY(CommandTerminal.CommandArg[] args)
    {
        if (rigi)
        {
            float rot = args[0].Int;

            rigi.velocity = Vector3.zero;

            DestRot *= Quaternion.AngleAxis(rot, transform.up);

            LerpMultiplier = 0;
            OriginRotation = transform.rotation;
        }
    }

    public void RotateOnZ(CommandTerminal.CommandArg[] args)
    {
        if (rigi)
        {
            float rot = args[0].Int;

            rigi.velocity = Vector3.zero;

            DestRot *= Quaternion.AngleAxis(rot, -transform.forward);

            LerpMultiplier = 0;
            OriginRotation = transform.rotation;
        }
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
        if (rigi)
        {
            float speed = args[0].Int;

            rigi.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
        }
    }

    public void DashOnX(CommandTerminal.CommandArg[] args)
    {
        if (rigi)
        {
            float speed = args[0].Int;

            rigi.AddRelativeForce(Vector3.right * speed, ForceMode.Impulse);
        }
    }

    public void DashOnY(CommandTerminal.CommandArg[] args)
    {
        if (rigi)
        {
            float speed = args[0].Int;

            rigi.AddRelativeForce(Vector3.up * speed, ForceMode.Impulse);
        }
    }

    public void SouthCommand(CommandTerminal.CommandArg[] args)
    {
        MeshFilter mesh = GetComponent<MeshFilter>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        Material mat = GetComponent<Material>();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        
        mesh.mesh = BondiMesh;
        meshCollider.sharedMesh = BondiMesh;
        mr.material = BondiMaterial;
        transform.localScale *= BondiScaleMultiplier;
        Camera.SetActive(false);
        BondiCamera.SetActive(true);
        CommandTerminal.Terminal.Shell.AddCommand("fps", FirstPersonCommand, 0, 0, "Be the driver");
        CommandTerminal.Terminal.Shell.AddCommand("tps", ThirdPersonCommand, 0, 0, "Third person drive");
    }

    public void FirstPersonCommand(CommandTerminal.CommandArg[] args)
    {
        BondiCamera.SetActive(false);
        ChoferCamera.SetActive(true);
    }

    public void ThirdPersonCommand(CommandTerminal.CommandArg[] args)
    {
        BondiCamera.SetActive(true);
        ChoferCamera.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "End")
        {
            rigi.velocity = Vector3.zero;
            Destroy(rigi);
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.enabled = false;
            Explosion.SetActive(true);
            Camera.transform.position = Explosion.transform.position - Explosion.transform.forward * 5;
            StartCoroutine(WaitForGameOver());
        }
        else
        {
            LevelManager.instance.GoToNextLevel();
        }
    }

    IEnumerator WaitForGameOver()
    {
        yield return new WaitForSeconds(2);

        GameEnd();
    }
}
