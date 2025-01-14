using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using Photon.Pun;
using Photon.Realtime;

public class RecyclableScrollerDemo : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    private RecyclableScrollRect _recyclableScrollRect;

    private List<ContactInfo> _contactList = new List<ContactInfo>();

    private void Awake()
    {
        UpdateContactList();

        _recyclableScrollRect.DataSource = this;
        
        StartCoroutine(AutoUpdateContactList());
    }

    public void UpdateContactList()
    {
        _contactList.Clear();

        foreach (var player in PhotonNetwork.PlayerList)
        {
            string score = player.CustomProperties.ContainsKey("Score")
                ? player.CustomProperties["Score"].ToString()
                : "0";

            _contactList.Add(new ContactInfo
            {
                Name = player.NickName,
                Score = score,
                id = player.ActorNumber.ToString()
            });
        }

        _recyclableScrollRect.ReloadData();
    }

    private IEnumerator AutoUpdateContactList()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            UpdateContactList();
        }
    }

    public int GetItemCount()
    {
        return _contactList.Count;
    }

    public void SetCell(ICell cell, int index)
    {
        var item = cell as DemoCell;
        item.ConfigureCell(_contactList[index], index);
    }
}

public class ContactInfo
{
    public string Name;
    public string Score;
    public string id;

    public ContactInfo() {}
}
