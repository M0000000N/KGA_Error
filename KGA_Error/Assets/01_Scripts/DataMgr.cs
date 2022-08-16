using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptData
{
    public string phase;
    public int index;
    public string script;
    public ScriptData(string phase, int index, string script)
    {
        this.phase = phase;
        this.index = index;
        this.script = script;
    }
}

public class DataMgr : MonoBehaviour
{
    static GameObject container;
    static GameObject Container { get => container; }

    static DataMgr instance;
    public static DataMgr Instance // �̰� �Ƹ� ������Ƽ ����...?
    {
        get
        {
            if (instance == null)
            {
                container = new GameObject();
                container.name = "DataMgr";
                instance = container.AddComponent(typeof(DataMgr)) as DataMgr;

                instance.SetScriptDatFromCSV();

                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    public string ScriptDataFileName = ".json"; // �̰Ź���
    [Header("���� ���� DB")]
    [SerializeField] TextAsset scriptDB; // �ܺ� �ڵ忡�� ������ ���������� ������ �Ұ�

    public Dictionary<int, ScriptData> ScriptDataDict { get; set; }

    private void SetScriptDatFromCSV()
    {
        scriptDB = Resources.Load<TextAsset>("CSV/GameData - Monster");

        if (scriptDB == null)
        {
            Debug.LogError("���Ͼ���");
            return;
        }

        if (ScriptDataDict == null)
        {
            ScriptDataDict = new Dictionary<int, ScriptData>(); // �̰� ������
        }

        string[] lines = scriptDB.text.Substring(0, scriptDB.text.Length).Split('\n'); // ���γ���
        for (int i = 1; i < lines.Length; i++) // ������ ����
        {
            string[] row = lines[i].Split(',');
            ScriptDataDict.Add(int.Parse(row[0]), new ScriptData(
                string.Parse(row[0]),  // phase
                int.Parse(row[1]),             // name
                float.Parse(row[2]),  // moveSpeed
                float.Parse(row[3]),  // rotationSpeed
                row[4]              // description
                ));
        }
    }

    public ScriptData GetScriptData(int index)
    {
        if (ScriptDataDict.ContainsKey(index))
        {
            return ScriptDataDict[index];
        }
        Debug.LogWarning("�ε��� ����");
        return null;

    }
}