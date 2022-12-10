using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public float x, y;
    float velocity;
    LineRenderer lr;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        if (lr == null)
        {
            Debug.Log("adicionar lineRenderer");
        }   
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y));
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(2 * x, 2* y), ForceMode.Impulse);
            lr.enabled = false;
        }
        velocity = rb.velocity.magnitude;
        if (velocity < 0.01f)
        {
            lr.enabled = true;
        }
        else
        {
            lr.enabled = false;    
        }
    }

}
