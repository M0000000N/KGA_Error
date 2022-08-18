using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class DataTable // ���� �����Ϳ� �̸��� ���ƾ��Ѵ�.
{
    public int Index { get; set; }
    public string Script { get; set; }
    public string B1 { get; set; }
    public string B2 { get; set; }
    public string Image { get; set; }
    public string BGM { get; set; }
}
public class CSVParser : SingletonBehaviour<CSVParser>
{
    private Dictionary<int, DataTable> scriptTable = new Dictionary<int, DataTable>();
    private void Awake()
    {
        // 1. ���ҽ� �������� csv �ε�
        TextAsset scriptTextAsset = Resources.Load<TextAsset>("CSV/Error - DataTable");

        // 2. csv���� ���� - CsvReader�� �Ű����� Configuration�� �� ����
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
                IEnumerable<DataTable> records = csv.GetRecords<DataTable>();
                foreach (DataTable record in records)
                {
                    if (false == scriptTable.ContainsKey(record.Index))
                    {
                        scriptTable[record.Index] = record;

                        // HACK : index�� 1���� �����ϱ⿡ 0�� ��Ҹ� �� ������ ä��
                        scriptTable[record.Index] = null;
                    }
                    scriptTable[record.Index] = record;
                }
            }
        }
    }
    public DataTable GetScriptData(int _index)
    {
        return scriptTable[_index];
    }
}