using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private enum pushedInfo
    {
        First,
        Second,
        OneButton
    };
    private bool pushButton; // ��ư�� ���ȴ��� Ȯ��
    private Animator buttonAmim; //  ��ư ������ �ִϸ��̼�
    private float buttonBlend;

    public AudioSource buttonPlayer; // ��ư ������ ���� �Ҹ�
    public PlayerInput Input; // �÷��̾��� �Է�

    private void Awake()
    {
        buttonPlayer = GetComponent<AudioSource>();
        buttonAmim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.IsPush)
        {
            buttonBlend += Time.deltaTime;
            if (buttonBlend >= 1)
            {
                buttonBlend = 1f;
            }
            PushDown();
        }
        else if (!Input.IsPush)
        {
            buttonBlend -= Time.deltaTime;
            if (buttonBlend >= 0)
            {
                buttonBlend = 0f;
            }
            PushUp();
        }
    }
    private void PushDown()
    {
        if(Input.IsPush == true)
        {
            buttonAmim.SetFloat("Blend", buttonBlend);
        }
    }
    private void PushUp()
    {
        if (Input.IsPush == false)
        {
            buttonAmim.SetFloat("Blend", buttonBlend);
        }
    }

}
