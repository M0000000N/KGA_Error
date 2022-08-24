using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ImageController : MonoBehaviour
{
    public int RoomIndex; // ���� ���� ������ �� ��ȣ
    public List<Sprite> ImageList;

    private int imageIndex; // CSV���� ��� ����
    void Start()
    {
        // ��� �̹����� ����Ʈ�� ��´�.
        ImageList = Resources.LoadAll("Image", typeof(Sprite)).OfType<Sprite>().ToList();

        imageIndex = CSVParser.Instance.GetCsvImage(RoomIndex);
        LoadImage(imageIndex);        
    }
    private void LoadImage(int _index)
    {       
        transform.GetChild(0).GetComponent<RawImage>().texture = ImageList[_index].texture;
    }
}

