using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Image : MonoBehaviour
{
    public int Index; // ���� ���� ������ ��
    public List<Sprite> ImageList;
    int imageIndex; // CSV���� ��� ����
    void Awake()
    {
        // ��� �̹����� ����Ʈ�� ��´�.
        ImageList = Resources.LoadAll("Image", typeof(Sprite)).OfType<Sprite>().ToList();
        
        // CSV�� ��� �̹��� �ε����� int�� ��ȯ 
        int.TryParse(CSVParser.Instance.GetCsvImage(GameManager.Instance.CurrentScene, Index), out imageIndex);
        
        LoadImage(imageIndex);
    }
    private void LoadImage(int _index)
    {       
        transform.GetChild(0).GetComponent<RawImage>().texture = ImageList[_index].texture;
    }
}
