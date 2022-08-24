using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInput : MonoBehaviour
{
    private float xRotate = 0.0f; // ���� ����� X�� ȸ������ ���� ���� ( ī�޶� �� �Ʒ� ���� )

    public float turnSpeed = 3f; // ���콺 ȸ�� �ӵ�
    public float moveSpeed = 2f; // �̵� �ӵ�
                                 // public bool IsPush = false;
    public Transform CameraTransform;

    void Update()
    {
        MouseRotation();
        KeyboardMove();
        Raycast();
    }

    /// <summary>
    /// ���콺 ȸ������ �þߺ���
    /// </summary>
    void MouseRotation()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);  // ȸ���� ����(X��, �ϴù���, �ٴڹ���)

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }

    /// <summary>
    /// �̵��� �̵��ӵ�
    /// </summary>
    void KeyboardMove()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0f;
        transform.position += (dir * moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 3f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= 3f;
        }
    }

    /// <summary>
    /// ȭ�� ����� Ray���
    /// </summary>
    void Raycast()
    {
        RaycastHit hit; // �浹����
        float maxDistance = 15f; // �˻� �ִ� �Ÿ�

        // Debug.DrawRay(CameraTransform.position, CameraTransform.forward * maxDistance, Color.blue); // ���̽��

        if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out hit, maxDistance))
        {
            if (hit.transform.tag == "Button") // ��ư �����ϸ�
            {
                hit.transform.GetComponent<Button>()?.ButtonTextEnable(); // ��ư�ؽ�Ʈ ���
                if (Input.GetMouseButton(0)) // Ŭ���ϸ�
                {
                    hit.transform.GetComponent<Button>().IsPush = true; // �����ٰ� �˷��ֱ�
                }
            }
        }
    }
}