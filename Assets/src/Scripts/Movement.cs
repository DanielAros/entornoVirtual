using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float velocidad = 20f;
    public GameObject prota;

    public bool activado = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoH * velocidad * Time.deltaTime, 0.0f, movimientoV * Time.deltaTime * velocidad);

        // rb.AddForce(movimiento);
        transform.Translate(movimiento, Space.Self);

        if(Input.GetKeyDown(KeyCode.LeftControl) && activado){
            prota.transform.position = new Vector3(prota.transform.position.x, prota.transform.position.y - 0.3f, prota.transform.position.z);
            activado = false;
        }else if(Input.GetKeyDown(KeyCode.LeftShift) && (activado == false)){
            prota.transform.position = new Vector3(prota.transform.position.x, prota.transform.position.y + 0.3f, prota.transform.position.z);
            activado = true;
        }
    }
}
