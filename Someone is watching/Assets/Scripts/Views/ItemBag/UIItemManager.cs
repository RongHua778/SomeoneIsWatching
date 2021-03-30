using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemManager : View
{

    public override string Name
    {
        get { return Const.V_ItemManager; }
    }


    private Dictionary<int, Item> ItemList = new Dictionary<int, Item>();
    public GridPanelUI GridPanelUI;

    public DragItemUI DragItemUI;
    public GameObject ItemPanel;
    GameModel m_GameModel;

    private bool isDrag = false;
    protected void Awake()
    {
        //base.Awake();
        Load();
        
       // m_GameModel = GetModel<GameModel>() as GameModel;
        GridUI.OnLeftBeginDrag += GridUI_OnLeftBeginDrag;
        GridUI.OnLeftEndDrag += GridUI_OnLeftEndDrag;
        GridUI.OnLeftClick += GridUI_OnLeftClick;
    }

    private void Start()
    {
        m_GameModel = GetModel<GameModel>() as GameModel;
    }
    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").transform as RectTransform, Input.mousePosition, Camera.main, out position);

        if (isDrag)
        {
            DragItemUI.Show();
            DragItemUI.SetLocalPosition(position);
        }
    }

    public void Show()
    {
        ItemPanel.SetActive(true);
    }

    public void Hide()
    {
        ItemPanel.SetActive(false);
    }

    private void Load()
    {
        //List<string[]> items = CSVReader.ReadCSV(Resources.Load<TextAsset>("Translation/items"));
        ItemList = new Dictionary<int, Item>();
        Item I00 = new Item(0, "录音笔", "一支有录音功能的笔，似乎有些损坏","", "Item/recordPen", new List<string>{ "LadyBag" });
        Item I01 = new Item(1, "电子时钟","一个可设置时间的电子时钟", "", "Item/DigitClock", null);
        Item I02 = new Item(2, "MemoryPiece#1", 
            "医生的话让我很难过，真的只能这样了吗。我哪里也不想去，打车回家已经是晚上9点了，只想早点休息。",
            "The doctor's words make me very sad.I didn't want to go anywhere. It was already 9 p.m and I just wanted to get an early night.",
            "Item/Piece1", null);
        Item I03 = new Item(3, "电池", "一块电量充足的电池", "", "Item/battery", new List<string> { "EnergyRepair1","EnergyRepair3" });
        Item I04 = new Item(4, "记忆碎片#3", "记忆碎片#3", "", "Item/Piece3", null);
        Item I05 = new Item(5, "扳手", "可以用来修复", "", "Item/Tool", new List<string> { "Day2WaterPungCanRepair" });
        Item I06 = new Item(6, "记忆碎片#2", "记忆碎片#2", "", "Item/Piece2", null);

        Item I07 = new Item(7, "记忆残片#4", "记忆残片#4", "", "Item/Piece2", null);
        Item I08 = new Item(8, "记忆残片#5", "记忆残片#5", "", "Item/Piece2", null);

        Item I09 = new Item(9, "蓝色脑图", "放置在摄像头上可以产生特殊效果", "", "Item/BlueMap", new List<string> { "BlueMapEffect" });
        Item I10 = new Item(10, "SD卡", "相机SD卡", "", "Item/SDCard", null);
        Item I11 = new Item(11, "记忆残片#6", "记忆残片#6", "", "Item/Piece6", null);
        Item I12 = new Item(12, "记忆残片#7", "记忆残片#7", "", "Item/Piece7", null);


        ItemList.Add(I00.ID, I00);
        ItemList.Add(I01.ID, I01);
        ItemList.Add(I02.ID, I02);
        ItemList.Add(I03.ID, I03);
        ItemList.Add(I04.ID, I04);
        ItemList.Add(I05.ID, I05);
        ItemList.Add(I06.ID, I06);
        ItemList.Add(I07.ID, I07);
        ItemList.Add(I08.ID, I08);
        ItemList.Add(I09.ID, I09);
        ItemList.Add(I10.ID, I10);
        ItemList.Add(I11.ID, I11);
        ItemList.Add(I12.ID, I12);



    }

    public void StoreItem(int itemID)
    {
        if (!ItemList.ContainsKey(itemID))
            return;

        Transform emptyGrid = GridPanelUI.FindEmptyGrid();
        if (emptyGrid == null)
        {
            Debug.LogWarning("背包已满！");
            return;
        }
        Item temp = ItemList[itemID];
        CreateNewItem(temp, emptyGrid);

        
    }

    #region 事件回调
    private void GridUI_OnLeftBeginDrag(Transform gridTransform)
    {
        if (gridTransform.childCount == 0)
            return;
        else
        {
            Item item = ItemModel.GetItem(gridTransform.name);
            DragItemUI.UpdateImage(item.Icon);
            Destroy(gridTransform.GetChild(0).gameObject);
            isDrag = true;
                
        }
    }

    private void GridUI_OnLeftEndDrag(Transform prevTransform,Transform endTransform)
    {
        isDrag = false;
        DragItemUI.Hide();
        Item item = ItemModel.GetItem(prevTransform.name);

        
        if (endTransform == null)//拖动到没UI的地方
        {
            CreateNewItem(item, prevTransform);
        }
        //拖动到指定的监视器里
        else if (endTransform.tag == "ItemArea")
        {
            string TargetName = endTransform.name;
            //Debug.Log(TargetName);
            if (item.AttentionStates.Contains(TargetName))
            {
                m_GameModel.HandleItem(item,TargetName);
            }
            else
            {
                CreateNewItem(item, prevTransform);
            }


        }
        else//拖动到无效的UI地方
        {
            //Debug.Log(endTransform.name);
            CreateNewItem(item, prevTransform);
        }
        
    }

    private void GridUI_OnLeftClick(Transform gridTransform)
    {
        if (gridTransform.childCount == 0)
            return;
        else
        {
            Item item = ItemModel.GetItem(gridTransform.name);
            SendEvent(Const.E_ItemGrid,item);
        }
    }
    #endregion

    private void CreateNewItem(Item item,Transform parent)
    {
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
        itemPrefab.GetComponent<ItemUI>().UpdateImage(item.Icon);
        GameObject itemGO = GameObject.Instantiate(itemPrefab);
        itemGO.transform.SetParent(parent);
        itemGO.transform.localPosition = Vector3.zero;
        itemGO.transform.localScale = Vector3.one;

      
        //save data
        ItemModel.StoreItem(parent.name, item);
    }


    public override void RegisterEvents()
    {
        AttentionEvents.Add(Const.E_GetItem);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Const.E_GetItem:
                int id = (int)obj;
                StoreItem(id);
                Sound.Instance.PlayEffect("SoundEffect/Sound_Take");
                break;
        }

    }
}
