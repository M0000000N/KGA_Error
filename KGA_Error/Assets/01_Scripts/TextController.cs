using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private TextMesh textMesh;

    public int Index;
    public string Script;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        textMesh = GetComponent<TextMesh>();
    }
    private void Start()
    {
        Script = CSVParser.Instance.GetCsvScript(GameManager.Instance.CurrentScene, Index); // CSV���� ��ũ��Ʈ ������
        textMesh.text = Script;
    }

    // ������ ���� ��Ÿ���� �ؽ�Ʈ
    void OnTriggerEnter(Collider _other)
    {
        meshRenderer.enabled = true;
    }
    void OnTriggerExit(Collider _other)
    {
        meshRenderer.enabled = false;
    }
}
