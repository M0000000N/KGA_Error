using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float turnSpeed = 3f; // ���콺 ȸ�� �ӵ�
    public float moveSpeed = 2f; // �̵� �ӵ�

    private float xRotate = 0.0f; // ���� ����� X�� ȸ������ ���� ���� ( ī�޶� �� �Ʒ� ���� )

    private int _index=1;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Update ()
    {
        MouseRotation();
        KeyboardMove();
    }
    
    void MouseRotation()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;        
        float yRotate = transform.eulerAngles.y + yRotateSize;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);  // ȸ���� ����(X��, �ϴù���, �ٴڹ���)

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }
    
    void KeyboardMove()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0f;
        transform.position += (dir * moveSpeed * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 3f;
            //Debug.Log($"{DataMgr.Instance.GetScriptData("Intro").script}");
            _index++;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= 3f;
        }
    }
}