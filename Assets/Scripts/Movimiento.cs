using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private new Rigidbody _rigidbody;
    public float movimineto;
    public Vector3 sensibilidad ;
    private bool _floordetectd = false;
    private bool _isJump = false;
    public float jumpfroce = 5.0f;
    public  new Transform camara;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Movimientoactualizado()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 velocity = Vector3.zero ;
        
        if (hor != 0 || ver != 0)
        {
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;
            
            velocity = direction * movimineto;
        }

        velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = velocity;
    }

    private void Movimentomause()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(0, hor*sensibilidad.x, 0);
        }


        if (ver != 0)
        {
            Vector3 rotation = camara.localEulerAngles;
            rotation.x = (rotation.x - ver * sensibilidad.y + 360 ) % 360 ;
            if (rotation.x > 80 && rotation.x < 180)
            {
                rotation.x = 80;
            }else if (rotation.x < 280 && rotation.x > 180)
            {
                rotation.x = 280;
            }

            camara.localEulerAngles = rotation;
        }
    }

    private void Movimientosalto()
    {
        Vector3 floor = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, floor, 1.03f))
        {
            _floordetectd = true;
        }
        else
        {
            _floordetectd = false;
        }

        _isJump = Input.GetButtonDown("Jump");
        
        if (_isJump && _floordetectd)
        {
            _rigidbody.AddForce(new Vector3(0, jumpfroce, 0 ), ForceMode.Impulse);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Movimientoactualizado();
        
        Movimentomause();
        
        Movimientosalto();
    }
}

















