using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMovement : Photon.MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public Transform lookAt;

    public Camera cam;

    public bool isGrounded;
    public Rigidbody rb;
    private MeshRenderer mr;

    public GameObject[] ChangeInto;
    private bool nearTransform = false;

    void Start()
    {
        cam.enabled = true;
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
    }

    public void FixedUpdate()
    {
       

        if (Input.GetKey(KeyCode.W))
        {
            transform.position -= transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position -= transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 2, 0) * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void Update()
    {
        //Draws the ray from the camera to the crosshair
   
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.6f, 0f));
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            //Debug.Log(hitInfo.transform.gameObject.name);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
        }

        //Gets the distance from the player to all the props that they can change into
        foreach (GameObject Change in ChangeInto)
        {
            if (Vector3.Distance(Change.transform.position, gameObject.transform.position) < 5f)
            {
                nearTransform = true;
                break;
            }
            else
            {
                nearTransform = false;
            }
        }
        //if pressed, the player will change into the prop
        if (Input.GetMouseButtonDown(0) && hitInfo.transform.gameObject.tag == "Transform" && nearTransform == true)
        {
            Debug.Log("You can change");
        }
        
    }
        void LateUpdate()
    {
       transform.LookAt(new Vector3(lookAt.position.x, transform.position.y, lookAt.position.z));
    }
}
