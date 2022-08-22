using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Button : MonoBehaviour
{
    public bool IsPush = false; // ���ȴ��� üũ

    private Animator buttonAmim;    // ��ư ������ �ִϸ��̼�
    private float buttonBlend;      // �ִϸ��̼� ���尪
    private float timer;


    private ButtonController buttonController;

    public TextMeshPro ButtonText;   // ��ư �ٷ� ���� �ߴ� �ؽ�Ʈ


    private void Awake()
    {
        buttonController = GetComponentInParent<ButtonController>();
        buttonAmim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        if (buttonController.ButtonState == 0)
        {
            buttonController.checkingText.enabled = false;
        }
        ButtonText.enabled = false;
        buttonBlend = 0f;
    }
    private void Update()
    {
        if (IsPush)
        {
            StartCoroutine("PushDown");

            buttonBlend += Time.deltaTime; // ���尪 0 -> 1
            if (buttonBlend >= 1)
            {
                buttonBlend = 1f;
                IsPush = false; // �ִϸ��̼��� ������ ������ ���� ����
            }
        }
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            ButtonTextDisable();
            timer = 0f;
        }
    }

    /// <summary>
    /// ������ �ִϸ��̼ǰ� checking �ؽ�Ʈ ����
    /// </summary>
    /// <returns>1��</returns>
    private IEnumerator PushDown()
    {
        if (IsPush == true)
        {
            buttonAmim.SetFloat("Blend", buttonBlend);
            yield return new WaitForSeconds(1f);
            buttonBlend = 0f; // ���尪 �ʱ�ȭ
            if (buttonController.ButtonState == 0)
            {
                buttonController.checkingText.enabled = true; // 1�ʵڿ� checking�ؽ�Ʈ ����
            }
        }
    }

    public void ButtonTextEnable()
    {
        ButtonText.enabled = true;
    }
    public void ButtonTextDisable()
    {
        ButtonText.enabled = false;
    }
}
