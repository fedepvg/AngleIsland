using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    char ActualAxis;
    Quaternion DestRot;

    void Awake()
    {
        rigi = GetComponentInParent<Rigidbody>();
        SpeedZ = 20f;
        XRotationRate = 70f;
        ZRotationRate = -70f;
        DestRot = Quaternion.identity;
    }

    private void FixedUpdate()
    {        
        rigi.AddRelativeForce(new Vector3(0, 0, 10 * SpeedZ),ForceMode.Force);

        Debug.Log(rigi.velocity.sqrMagnitude);
        Debug.DrawRay(transform.position, transform.forward*10);
    }

    private void Update()
    {
        GetInput(ref InputX, ref InputZ);
        transform.Rotate(InputX * XRotationRate * Time.deltaTime, 0f, InputZ * ZRotationRate * Time.deltaTime);
        if(Input.GetKey(KeyCode.X))
        {
            Rotate(90, 'x');
        }
        else if(Input.GetKey(KeyCode.Y))
        {
            Rotate(90, 'y');
        }
        else if(Input.GetKey(KeyCode.Z))
        {
            Rotate(90, 'z');
        }
        else
        {
            Rotate();
        }
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

    public void Rotate(int rot, char axis)
    {
        //DestRot=Quaternion.identity;
        if (axis != ActualAxis)
        {
            switch (axis)
            {
                case 'x':
                    DestRot *= Quaternion.AngleAxis(rot, transform.right);
                    break;
                case 'y':
                    DestRot *= Quaternion.AngleAxis(rot, transform.up);
                    break;
                case 'z':
                    DestRot *= Quaternion.AngleAxis(rot, transform.forward);
                    break;
            }
        }
        DestRot = DestRot * transform.rotation;
    }

    public void Rotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, DestRot, RotSpeed * Time.deltaTime);
        if (transform.rotation == DestRot)
        {
            DestRot = Quaternion.identity;
        }
    }
}
