using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScriptController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private TextMeshPro scriptTMP;
    private string scriptText;

    public int Index;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        scriptTMP = GetComponent<TextMeshPro>();
    }
    private void Start()
    {
        scriptText = CSVParser.Instance.GetCsvScript(Index); // CSV에서 스크립트 가져옴
        scriptTMP.text = scriptText;
        meshRenderer.enabled = false;
    }

    // 가까이 가면 나타나는 텍스트
    void OnTriggerEnter(Collider _other)
    {
        meshRenderer.enabled = true;
        TTS.Instance.TTSPlay(scriptText);
    }
    void OnTriggerExit(Collider _other)
    {
        meshRenderer.enabled = false;
    }
}
