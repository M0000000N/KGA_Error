//#define WINDOW 
#define OCULUS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OVR;

public class PlayerInput : MonoBehaviour
{
    private float xRotate = 0.0f; // ���� ����� X�� ȸ������ ���� ���� ( ī�޶� �� �Ʒ� ���� )

    public float turnSpeed = 2.5f; // ���콺 ȸ�� �ӵ�
    public float moveSpeed = 3f; // �̵� �ӵ�
    public Transform CameraTransform;
    private Transform PlayerTransform;

    private void Awake()
    {
        PlayerTransform = GetComponent<Transform>();
    }

    void Update()
    {
#if WINDOW
        MouseRotation();
        Raycast();
#endif
        PlayerMove();
    }

    /// <summary>
    /// ���콺 ȸ������ �þߺ���
    /// </summary>
    void MouseRotation()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        float yRotate = CameraTransform.eulerAngles.y + yRotateSize;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);  // ȸ���� ����(X��, �ϴù���, �ٴڹ���)

        CameraTransform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }

    /// <summary>
    /// �̵��� �̵��ӵ�
    /// </summary>
    void PlayerMove()
    {
#if OCULUS
        Vector2 mov2d = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        Vector3 dir = new Vector3(mov2d.x, 0, mov2d.y).normalized;
#endif

#if WINDOW
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= 1.5f;
        }
#endif

        dir = CameraTransform.TransformDirection(dir);// ������Ʈ�� �ٶ󺸴� �չ������� �̵����� ������ ����

        dir.y = 0f; // �׷��� y�� �ö󰡸� �ȵ�

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// ȭ�� ����� Ray���
    /// </summary>
    void Raycast()
    {
        RaycastHit hit; // �浹����
        float maxDistance = 20f; // �˻� �ִ� �Ÿ�

        if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out hit, maxDistance))
        {
            if (hit.transform.tag == "Button") // ��ư �����ϸ�
            {
                hit.transform.GetComponent<Buttonss>()?.ButtonTextEnable(); // ��ư�ؽ�Ʈ ���
                if (Input.GetMouseButton(0) && !hit.transform.GetComponentInParent<ButtonController>().doNotPush) // Ŭ���ϸ�
                {
                    hit.transform.GetComponent<Buttonss>().IsPush = true; // �����ٰ� �˷��ֱ�
                }
            }
        }
    }
}