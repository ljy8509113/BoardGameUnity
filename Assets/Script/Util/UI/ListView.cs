using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListView : MonoBehaviour
{
    public GameObject roomItem;
    public GameObject contentView;
    List<ResponseRoomList.Room> list = new List<ResponseRoomList.Room>();
    public RectTransform prefab;
    public ScrollRect scrollView;
    public RectTransform content;

    [SerializeField]
    public List<GameObject> listItem;

    void Awake()
    {
        
    }

    void Start()
    {
        for(int i=0; i<Common.LIST_COUNT; i++)
        {
            GameObject item = Instantiate(roomItem) as GameObject;
            listItem.Add(item);
            content.transform.parent = item.transform ;

        }
    }

    void Update()
    {
        
    }
    

    public void setData(ResponseBase res)
    {
        switch (res.identifier)
        {
            case Common.IDENTIFIER_GAME_ROOM_LIST:
                {
                    ResponseRoomList resRoomList = (ResponseRoomList)res;
                    int current = resRoomList.current;
                    int max = resRoomList.max;

                    if (resRoomList.list != null && resRoomList.list.Count > 0)
                    {
                        foreach (ResponseRoomList.Room room in resRoomList.list)
                        {

                        }
                    }
                    else
                    {
                        //목록없음
                    }
                }
                
                break;
        }
    }
}

