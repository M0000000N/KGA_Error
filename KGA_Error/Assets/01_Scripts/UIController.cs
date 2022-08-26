using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonBehaviour<UIController>
{
    public void StartFadeInCRT()
    {
        StartCoroutine("FadeInStart");
        Debug.Log("���̵���");
    }
    public void StartFadeOutCRT()
    {
        StartCoroutine("FadeOutStart");
        Debug.Log("���̵�ƿ�");
    }

    //���̵� �ƿ�
    public IEnumerator FadeInStart()
    {
        gameObject.SetActive(true);
        for (float f = 1f; f > 0; f -= 0.02f)
        {
            Color color = gameObject.GetComponent<Image>().color;
            color.a = f;
            gameObject.GetComponent<Image>().color = color;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    //���̵� ��
    public IEnumerator FadeOutStart()
    {
        gameObject.SetActive(true);
        for (float f = 0f; f < 1; f += 0.02f)
        {
            Color color = gameObject.GetComponent<Image>().color;
            color.a = f;
            gameObject.GetComponent<Image>().color = color;
            yield return null;
        }
    }
}
