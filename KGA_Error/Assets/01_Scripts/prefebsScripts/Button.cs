using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Button : MonoBehaviour
{
    public enum ButtonInfo  // ��ư ����
    {
        OneButton,
        TwoButton_L,
        TwoButton_R,
    };
    public ButtonInfo ButtonState;

    public bool IsPush = false; // ���ȴ��� üũ

    private RoomController roomController;
    private ButtonController buttonController;
    private AudioSource buttonPlayer;   // ��ư ������ ���� �Ҹ�

    private Animator buttonAmim;    // ��ư ������ �ִϸ��̼�
    private string ButtonText;      // CSV���� �������� �ؽ�Ʈ
    private TextMeshPro ButtonTMP;  // ��ư �ٷ� ���� �ߴ� �ؽ�Ʈ

    private float buttonBlend;      // �ִϸ��̼� ���尪
    private float timer;

    private void Awake()
    {
        roomController = GetComponentInParent<RoomController>();
        buttonController = GetComponentInParent<ButtonController>();
        buttonPlayer = GetComponentInParent<AudioSource>();
        buttonAmim = GetComponent<Animator>();
        ButtonTMP = GetComponentInChildren<TextMeshPro>();
    }

    private void Start()
    {
        // ��ư �ؽ�Ʈ �Ľ�
        switch ((int)ButtonState)
        {
            case 0: // �Ѱ���ư
                ButtonText = CSVParser.Instance.GetCsvOneButton((int)roomController.roomInfo);
                buttonController.checkingText.enabled = false;
                break;
            case 1: // ���ʹ�ư
                ButtonText = CSVParser.Instance.GetCsvTwoButton_L((int)roomController.roomInfo);
                break;
            case 2: // ������ ��ư
                ButtonText = CSVParser.Instance.GetCsvTwoButton_R((int)roomController.roomInfo);
                break;
        }
        ButtonTMP.text = ButtonText;
        ButtonTMP.enabled = false;

        buttonBlend = 0f;
    }
    private void Update()
    {
        if (IsPush && buttonController.doNotPush == false)
        {
            buttonPlayer.Play(); // ������ �Ҹ�
            StartCoroutine("PushDown");

            while (buttonBlend <= 1) // ���尪 0 -> 1
            {
                buttonBlend += Time.deltaTime;
            }
        }

        // Player ray����
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
        buttonAmim.SetFloat("Blend", buttonBlend);

        if (ButtonState == 0)   // ��ư �ϳ��� ���� checking�ؽ�Ʈ ����
        {
            buttonController.checkingText.enabled = true;
        }

        if (buttonBlend >= 1)
        {
            buttonController.doNotPush = true;
            buttonBlend = 0f;
            IsPush = false; // �ִϸ��̼��� ������ ������ ���� ����
            yield return new WaitForSeconds(4f);
            if (ButtonState == 0)
            {
                GameManager.Instance.CurrentScene++; // ���Ѿ
                SceneManagement.Instance.ChangeScene();
            }
        }
    }
    public void ButtonTextEnable()
    {
        ButtonTMP.enabled = true;
    }
    public void ButtonTextDisable()
    {
        ButtonTMP.enabled = false;
    }
}
