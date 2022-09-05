//#define WINDOW 
#define OCULUS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OVR;

public class PlayerInput : MonoBehaviour
{
    private float xRotate = 0.0f; // 내부 사용할 X축 회전량은 별도 정의 ( 카메라 위 아래 방향 )

    public float turnSpeed = 2.5f; // 마우스 회전 속도
    public float moveSpeed = 3f; // 이동 속도
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
    /// 마우스 회전으로 시야변경
    /// </summary>
    void MouseRotation()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        float yRotate = CameraTransform.eulerAngles.y + yRotateSize;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);  // 회전량 제한(X축, 하늘방향, 바닥방향)

        CameraTransform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }

    /// <summary>
    /// 이동과 이동속도
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

        dir = CameraTransform.TransformDirection(dir);// 오브젝트가 바라보는 앞방향으로 이동방향 돌려서 조정

        dir.y = 0f; // 그래도 y는 올라가면 안됨

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 화면 가운데에 Ray쏘기
    /// </summary>
    void Raycast()
    {
        RaycastHit hit; // 충돌정보
        float maxDistance = 20f; // 검사 최대 거리

        if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out hit, maxDistance))
        {
            if (hit.transform.tag == "Button") // 버튼 감지하면
            {
                hit.transform.GetComponent<Buttonss>()?.ButtonTextEnable(); // 버튼텍스트 출력
                if (Input.GetMouseButton(0) && !hit.transform.GetComponentInParent<ButtonController>().doNotPush) // 클릭하면
                {
                    hit.transform.GetComponent<Buttonss>().IsPush = true; // 눌렀다고 알려주기
                }
            }
        }
    }
}