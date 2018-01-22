using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private float h, v;
    
    public float speed = 1.5f;

    public float sensitivity = 5f; // чувствительность мыши
    private float rotationY;

    public float headMinY = -40f; // ограничение угла для головы
    public float headMaxY = 40f;
    
    private Vector3 direction;

    public float jumpTime = 0.5f;
    private float _jumpTime;

    public Transform _camera;

    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical")    ;

        if (_jumpTime <= jumpTime)
            _jumpTime += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (jumpTime >= 0.1)
                jumpTime -= 0.1f;
        }
        
        
        float rotationX = _camera.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp (rotationY, headMinY, headMaxY);
        _camera.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        
        direction = new Vector3(h, 0, v);
        direction = _camera.TransformDirection(direction);
        direction = new Vector3(direction.x, 0, direction.z);
    }

    private void FixedUpdate()
    {
        
        if (Input.GetButton("Jump") && _jumpTime >= jumpTime)
        {
            _jumpTime = 0;
            _rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
        
        
       
        
        _rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);
		
        // Ограничение скорости, иначе объект будет постоянно ускоряться
        if(Mathf.Abs(_rigidbody.velocity.x) > speed)
        {
            _rigidbody.velocity = new Vector3(Mathf.Sign(_rigidbody.velocity.x) * speed, _rigidbody.velocity.y, _rigidbody.velocity.z);
        }
        if(Mathf.Abs(_rigidbody.velocity.z) > speed)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, Mathf.Sign(_rigidbody.velocity.z) * speed);
        }
    }
}