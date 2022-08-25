using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonBehaviour<UIController>
{
    public GameObject FadePannel;

    //���̵� �ƿ�
    public IEnumerator FadeInStart()
    {
        FadePannel.SetActive(true);
        for (float f = 1f; f > 0; f -= 0.02f)
        {
            Color color = FadePannel.GetComponent<Image>().color;
            color.a = f;
            FadePannel.GetComponent<Image>().color = color;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        FadePannel.SetActive(false);
    }

    //���̵� ��
    public IEnumerator FadeOutStart()
    {
        FadePannel.SetActive(true);
        for (float f = 0f; f < 1; f += 0.02f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
    }
}
