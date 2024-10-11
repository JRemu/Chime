using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;



public class Player : MonoBehaviour
{

    public float HitPoints = 100;
    public GameObject TurretPrefab;

    private readonly float MovementSpeed = 5f;
    private readonly float RotationSpeed = 500f;
    private bool Placeable = false;
    public Transform Destination;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = new();

        // Forwards
        if (Input.GetKey(KeyCode.W))
        {
            movementDirection.z += MovementSpeed;
        }
        // Backwards
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection.z -= MovementSpeed;
        }
        // Left
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection.x -= MovementSpeed;
        }
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection.x += MovementSpeed;
        }
        


        if (Input.GetKey(KeyCode.E))
        {
            this.gameObject.SetActive(false);
            transform.position = Destination.position;
            this.gameObject.SetActive(true);
        }

        if (movementDirection != Vector3.zero)
        {
            transform.Translate(movementDirection * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(movementDirection, Vector3.up),
                RotationSpeed * Time.deltaTime);
        }

        #warning Player is currently hard coded to place turret, change this to place from active inventory slot
        if (Input.GetMouseButtonDown(1))
        {
            TryPlaceTurret();
        }

        if (Input.GetKeyDown(KeyCode.Space)){ 
        TakeDamage(20);                       //Use spacebar to test damage function
        }  
    }

    /*public MemoryOrbDestination GetClosedMemoryOrb()
    {

        return ClosedOrb;
    }*/
    private void TryPlaceTurret()
    {
        if (Placeable)
        {
            Instantiate(TurretPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Placeable"))
        {
            Placeable = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Placeable"))
        {
            Placeable = false;
        }
    }

    // Basic implementation for taking damage, can modify later
    
    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
    }


}