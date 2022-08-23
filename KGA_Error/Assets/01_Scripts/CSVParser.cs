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
    public string DoorName { get; set; }
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
    private DataTable GetCsvData(int _index) // �ε����� ���ϴ� �����͸� ã�´�.
    {
        return dataTable[_index];
    }

    public string GetCsvScript(int _scene, int _number)
    {
        int _index = _scene * 1000 + _number;
        return GetCsvData(_index).Script;
    }
    public string GetCsvDoorName(int _scene, int _number)
    {
        int _index = _scene * 1000 + _number;
        return GetCsvData(_index).DoorName;
    }
    public string GetCsvB1(int _scene, int _number)
    {
        int _index = _scene * 1000 + _number;
        return GetCsvData(_index).B1;
    }
    public string GetCsvB2(int _scene, int _number)
    {
        int _index = _scene * 1000 + _number;
        return GetCsvData(_index).B2;
    }
    public string GetCsvImage(int _scene, int _number)
    {
        int _index = _scene * 1000 + _number;
        return GetCsvData(_index).Image;
    }
    public string GetCsvBGM(int _scene, int _number)
    {
        int _index = _scene * 1000 + _number;
        return GetCsvData(_index).BGM;
    }
}