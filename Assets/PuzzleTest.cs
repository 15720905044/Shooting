using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public int _col;
    public int _row;
    public int _id;
    public GameObject _Obj;
    public Sprite _sprite;

}
public class PuzzleTest : MonoBehaviour
{
    public Sprite[] ShowImage;
    public Item[] itemImg;
    public GameObject panels;
    int length = 0;
    int sqrt = 0;
    private void Start()
    {
        var ResourcesLoadImage = Resources.LoadAll("Image");
        length = transform.childCount;  //length ΪСͼƬ��ĸ���
        sqrt = (int)Mathf.Sqrt(length); // sqrtΪ������

        ShowImage = new Sprite[length];

        for (int i = 0; i < length; i++)
        {
            Debug.Log(i);
            ShowImage[i] = (Sprite)ResourcesLoadImage[i + 1];  //������ͼƬ�ֿ�װ��Sprite[] UIShowImage����
        }
        Debug.Log(length+"22");
        InitItemImg(length);
        Shuffle(ShowImage);
        InitShow();
    }

    //�������
    T[] Shuffle<T>(T[] Array)
    {
        for (int i = 0; i < Array.Length; i++)
        { //���������е�ÿһС��ͼƬ�����������������һ��ͼƬ����λ��
            T temp = Array[i];
            int randomIndex = UnityEngine.Random.Range(0, Array.Length);
            Array[i] = Array[randomIndex];
            Array[randomIndex] = temp;
        }
        return Array;
    }

    //��ʼ��ͼƬid,col,row
    void InitItemImg(int count)
    {
        itemImg = new Item[length];
        for (int i = 0; i < count; i++)
        {
            itemImg[i] = new Item();
            itemImg[i]._id = i;
            itemImg[i]._col = i / sqrt;
            itemImg[i]._row = i % sqrt;
        }
    }
    void InitShow()
    {
        //��ʼ����ʾͼƬ
        for (int i = 0; i < ShowImage.Length; ++i)
        {
            var t = transform.GetChild(i);
            t.GetComponent<Image>().sprite = ShowImage[i];
            //ָ��Sprite
            itemImg[i]._sprite = t.GetComponent<Image>().sprite;
            //ָ��Obj
            itemImg[i]._Obj = t.gameObject;
            var item = itemImg[i];
            itemImg[i]._Obj.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                this.OnClickEvent(item);
            });
        }
        itemImg[length - 1]._Obj.GetComponent<Image>().sprite = null;
        itemImg[length - 1]._sprite = null;
    }

    private void OnClickEvent(Item item)
    {
        //if (canMovie(item))
        //{
            var t = NoneImage();
            t._sprite = item._sprite;
            t._Obj.GetComponent<Image>().sprite = item._Obj.GetComponent<Image>().sprite;
            item._sprite = null;
            item._Obj.GetComponent<Image>().sprite = null;
        //}
        if (Success())
        {
            StartCoroutine(ShowWin());
            Debug.Log("ʤ��");
        }
    }
    Item NoneImage()
    {
        for (int i = 0; i < length; i++)
        {
            if (itemImg[i]._sprite == null)
            {
                return itemImg[i];
            }
        }
        return null;

    }
    //�ж��ܷ��ƶ�
    //bool canMovie(Item item)
    //{
    //    if (item._sprite == null)
    //        return false;
    //    var t = NoneImage();
    //    //�ж���������λ��
    //    if ((t._col == item._col - 1 && t._row == item._row) || (t._col == item._col + 1 && t._row == item._row) || (t._col == item._col && t._row == item._row + 1) || (t._col == item._col && t._row == item._row - 1))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    public void QuitGame()
    {
        Application.Quit();
    }
    // �ж�ʤ��
    bool Success()
    {
        int count=0;
        for (int i = 0; i < length - 1; i++)
        {
            if (itemImg[i]._Obj.GetComponent<Image>().sprite != null)
            {
                if (itemImg[i]._Obj.GetComponent<Image>().sprite.name == "x_" + i.ToString())
                {
                    count++;
                    Debug.Log(count);
                    if (count >= length - 2)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    IEnumerator ShowWin()
    {
        yield return new WaitForSeconds(1f);
        panels.SetActive(true);
    }
}
