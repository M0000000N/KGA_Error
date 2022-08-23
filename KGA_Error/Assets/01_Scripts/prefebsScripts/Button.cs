using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Button : MonoBehaviour
{
    public enum ButtonInfo // ��ư ���� ����(���� �Ƚ���)
    {
        OneButton,
        TwoButton_L,
        TwoButton_R,
    };
    public ButtonInfo ButtonState;

    public bool IsPush = false; // ���ȴ��� üũ

    private ButtonController buttonController;
    private Animator buttonAmim;    // ��ư ������ �ִϸ��̼�
    private TextMeshPro ButtonTMP;   // ��ư �ٷ� ���� �ߴ� �ؽ�Ʈ
    private string ButtonText;   // ��ư �ٷ� ���� �ߴ� �ؽ�Ʈ

    private float buttonBlend;      // �ִϸ��̼� ���尪
    private float timer;

    private void Awake()
    {
        buttonController = GetComponentInParent<ButtonController>();
        buttonAmim = GetComponent<Animator>();
        ButtonTMP = GetComponentInChildren<TextMeshPro>();
    }
    private void Start()
    {

        switch ((int)ButtonState)
        {
            case 1: // ���ʹ�ư
                ButtonText = CSVParser.Instance.GetCsvB1(GameManager.Instance.CurrentScene, (int)buttonController.roomInfo);
                break;
            case 2: // ������ ��ư
                ButtonText = CSVParser.Instance.GetCsvB2(GameManager.Instance.CurrentScene, (int)buttonController.roomInfo);
                break;
        }
        ButtonTMP.text = ButtonText;
        Debug.Log(gameObject.name + $" - buttonController.roomInfo : {buttonController.roomInfo}, ButtonState : {ButtonState} ButtonText: {ButtonText}, currentScene : {GameManager.Instance.CurrentScene}");

        if (ButtonState == 0)
        {
            buttonController.checkingText.enabled = false;
        }
        ButtonTMP.enabled = false;
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
            if (ButtonState == 0)
            {
                buttonController.checkingText.enabled = true; // checking�ؽ�Ʈ ����
            }
            yield return new WaitForSeconds(4f);
            buttonBlend = 0f; // ���尪 �ʱ�ȭ
            if (ButtonState == 0)
            {
                Debug.Log($"currentScene : {GameManager.Instance.CurrentScene}");
                GameManager.Instance.CurrentScene++; // ���Ѿ
                Debug.Log($"currentScene : {GameManager.Instance.CurrentScene}");
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
