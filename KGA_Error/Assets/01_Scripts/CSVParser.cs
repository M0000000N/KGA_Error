using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
public class ScriptData // ���� �����Ϳ� �̸��� ���ƾ��Ѵ�.
{
    public string phase { get; set; }
    public int index { get; set; } 
    public string script { get; set; }
}
public class CSVParser : MonoBehaviour
{
    private void Start()
    {
        // 1. ���ҽ� �������� csv �ε�
        TextAsset scriptTextAsset = Resources.Load<TextAsset>("CSV/Error - Script"); 

        // 
        //CsvReader�� �ι�° �Ű������� Configuration�� �� �Ű�����
        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|", // �÷�����
            NewLine = Environment.NewLine // ���๮�� ����
        };

        // �̰��� ���������� �� �� ������ ����
        using (StringReader cswString = new StringReader(scriptTextAsset.text)) // �Ľ��� csv�� �о�´�.
        {
            using (CsvReader csv = new CsvReader(cswString, config))
            {
                IEnumerable<ScriptData> records = csv.GetRecords<ScriptData>();
                foreach (ScriptData record in records)
                {
                    Debug.Log($"{record.phase} : {record.index} : {record.script}");
                }
            }
        }
        // cswString.Dispose(); // ������ �������� �� ������ �ݾ������
        // csv.Dispose();
        // �ٵ� �̰� ������ �� �ֱ� ������ �ڵ����� Dispose�� ȣ�����ִ� using������ ����Ѵ�.
    }
}