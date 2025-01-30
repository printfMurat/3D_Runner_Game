using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private float forwardSpeed = 750f;//Sabit ileri hýz
    private float lateralSpeed = 300f;//Yön hýzý

    private float maxLateralRange;



    public GameObject finishPanel;




    public GameObject plartform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
       CalculateTransformBounds();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ilero doðru sabit haraket
        rb.AddForce(Vector3.forward * Time.fixedDeltaTime * forwardSpeed);

        float horizantalMove = Input.GetAxis("Horizontal");

        rb.AddForce(Vector3.right * horizantalMove * lateralSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        //Haraketi sýnýrlandýr
        RestrictLateralMovement();
    }

    public void CalculateTransformBounds()
    {
        if (plartform != null)
        {
            float plartformWidth = plartform.GetComponent<Renderer>().bounds.size.x;
            maxLateralRange = (plartformWidth / 2f) + 0.1f;
        }
        else
        {
            Debug.LogError("Plarform Bulunamadý");
        }
    }
    void RestrictLateralMovement()
    {
        Vector3 position = rb.position;
        position.x = Mathf.Clamp(position.x, -maxLateralRange, maxLateralRange);
        rb.position = position;

        // Yan hareket hýzýný sýfýrla
        Vector3 velocity = rb.linearVelocity;
        velocity.x = 0f;
        rb.linearVelocity = velocity;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Time.timeScale = 0;
            finishPanel.SetActive(true);       
        }

                
    }

}
