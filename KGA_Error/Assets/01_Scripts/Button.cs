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
        if (IsPush)
        {
            StartCoroutine("PushDown");
            float blendSpeed = Time.deltaTime * 1.5f;
            buttonBlend += blendSpeed; // ���尪 0 -> 1
        }

        // �÷��̾� �ֽÿ� ���� ��ư �ؽ�Ʈ ����
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
        buttonController.doNotPush = true;
        buttonAmim.SetFloat("Blend", buttonBlend);

        // ��ư �ϳ��� ���� checking�ؽ�Ʈ ����
        if (ButtonState == 0) { buttonController.checkingText.enabled = true; }

        if (buttonBlend >= 1)
        {
            IsPush = false; // �ִϸ��̼��� ������ ������ ���� ����
            buttonBlend = 0f;

            yield return null;
            buttonController.ButtonSoundPlay();

            yield return new WaitForSeconds(3f);
            if (ButtonState == 0)
            {
                GameManager.Instance.CurrentScene++;
                GameManager.Instance.UpdateScene();
            }
            else
            {
                GameManager.Instance.TurnIndex++;
                roomController.RoomTurnUpdate();
            }
            StopCoroutine("PushDown");
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
