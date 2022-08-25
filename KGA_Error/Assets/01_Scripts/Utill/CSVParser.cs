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
    public string TwoButton_L { get; set; }
    public string OneButton { get; set; }
    public string TwoButton_R { get; set; }
    public string Image { get; set; }
    public string BGM { get; set; }
    public string Turn1 { get; set; }
    public string Turn2 { get; set; }
    public string Turn3 { get; set; }
    public string Turn4 { get; set; }
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
                        // dataTable[record.Index] = null;
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

    public string GetCsvScript(int _number)
    {
        int _index = GameManager.Instance.CurrentScene * 1000 + _number;
        return GetCsvData(_index).Script;
    }
    public string GetCsvDoorName(int _number)
    {
        int _index = GameManager.Instance.CurrentScene * 1000 + _number;
        return GetCsvData(_index).DoorName;
    }
    public string GetCsvOneButton(int _number)
    {
        int _index = GameManager.Instance.CurrentScene * 1000 + _number;
        return GetCsvData(_index).OneButton;
    }
    public string GetCsvTwoButton_L(int _number)
    {
        int _index = GameManager.Instance.CurrentScene * 1000 + _number;
        return GetCsvData(_index).TwoButton_L;
    }
    public string GetCsvTwoButton_R(int _number)
    {
        int _index = GameManager.Instance.CurrentScene * 1000 + _number;
        return GetCsvData(_index).TwoButton_R;
    }
    public int GetCsvImage(int _number)
    {
        int _index = GameManager.Instance.CurrentScene * 1000 + _number;
        int imageIndex = Int32.Parse(GetCsvData(_index).Image);
        return imageIndex;
    }
    public int GetCsvBGM(int _number)
    {
        int _index = GameManager.Instance.CurrentScene * 1000 + _number;
        int audioIndex = Int32.Parse(GetCsvData(_index).BGM);
        return audioIndex;
    }
    public int[] GetCsvTurn()
    {
        int _index = GameManager.Instance.CurrentScene * 1000;

        int[] myTurn = { 
            Int32.Parse(GetCsvData(_index).Turn1),
            Int32.Parse(GetCsvData(_index).Turn2),
            Int32.Parse(GetCsvData(_index).Turn3),
            Int32.Parse(GetCsvData(_index).Turn4)
        };        
        return myTurn;
    }
}