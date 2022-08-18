using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public string Phase;
    public int Index;
    public string Script;
    private void Awake()
    {
    }
    private void Start()
    {
    }
    void OnTriggerEnter(Collider _other)
    {
        if (_other.tag == "Player")
        {
            if (gameObject.tag == "Door")
            {
                // ���� �����̴� �ִϸ��̼�
            }
            else if (gameObject.tag == "Finish")
            {
                Application.Quit();
            }
        }
    }
    void OnTriggerExit(Collider _other)
    {
        meshRenderer.enabled = false;
    }
}
