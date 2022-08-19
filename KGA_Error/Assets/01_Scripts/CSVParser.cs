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
    private Dictionary<int, DataTable> dataTable = new Dictionary<int, DataTable>();
    private void Awake()
    {
        // 1. ���ҽ� �������� csv �ε�
        TextAsset csvTextAsset = Resources.Load<TextAsset>("CSV/DataTable");

        // 2. csv���� ���� - CsvReader�� �Ű����� Configuration�� �� ����
        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|", // �÷�����
            NewLine = Environment.NewLine // ���๮�� ����
        };

        // �̰��� ���������� �� �� ������ ����
        using (StringReader cswString = new StringReader(csvTextAsset.text)) // �Ľ��� csv�� �о�´�.
        {
            using (CsvReader csv = new CsvReader(cswString, config))
            {
                IEnumerable<DataTable> records = csv.GetRecords<DataTable>();
                foreach (DataTable record in records)
                {
                    if (false == dataTable.ContainsKey(record.Index))
                    {
                        dataTable[record.Index] = record;

                        // HACK : index�� 1���� �����ϱ⿡ 0�� ��Ҹ� �� ������ ä��
                        dataTable[record.Index] = null;
                    }
                    dataTable[record.Index] = record;
                }
            }
        }
    }
    public DataTable GetCsvData(int _index) // �ε����� ���ϴ� �����͸� ã�´�.
    {
        return dataTable[_index];
    }
}