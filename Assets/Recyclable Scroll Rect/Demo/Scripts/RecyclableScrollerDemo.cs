using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;

/// <summary>
/// Demo controller class for Recyclable Scroll Rect. 
/// A controller class is responsible for providing the scroll rect with datasource. Any class can be a controller class. 
/// The only requirement is to inherit from IRecyclableScrollRectDataSource and implement the interface methods
/// </summary>

//Dummy Data model for demostraion
public struct ContactInfo
{
    public string Name;
    public string Score;
    public string id;
    public string Timestamp;
}

public class RecyclableScrollerDemo : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    private int _dataLength;

    //Dummy data List
    private List<ContactInfo> _contactList = new List<ContactInfo>();

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        InitData();
        _recyclableScrollRect.DataSource = this;
    }

    //Initialising _contactList with dummy data 
    private void InitData()
    {
        var itemList = GameManager.Ins.GetItemList();

        if (_contactList != null) _contactList.Clear();

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].Name == PlayerPrefs.GetString("AccountName"))
            {
                ContactInfo obj = new ContactInfo();
                obj.Name = itemList[i].Name.Substring(0, 8);
                obj.Score = itemList[i].Score.ToString();
                obj.Timestamp = itemList[i].Timestamp;
                obj.id = "No." + i;
                _contactList.Add(obj);
            }
        }
    }

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return _contactList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as DemoCell;
        item.ConfigureCell(_contactList[index], index);
    }

    #endregion
}