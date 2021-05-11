using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemManager : View
{

    public override string Name
    {
        get { return Const.V_ItemManager; }
    }


    public static Dictionary<int, Item> ItemList = new Dictionary<int, Item>();
    public GridPanelUI GridPanelUI;

    public DragItemUI DragItemUI;
    public GameObject ItemPanel;
    GameModel m_GameModel;

    private bool isDrag = false;
    private List<Item> storedItems = new List<Item>();
    protected void Awake()
    {
        //base.Awake();
        Load();
        m_GameModel = GetModel<GameModel>() as GameModel;

        // m_GameModel = GetModel<GameModel>() as GameModel;
        GridUI.OnLeftBeginDrag += GridUI_OnLeftBeginDrag;
        GridUI.OnLeftEndDrag += GridUI_OnLeftEndDrag;
        GridUI.OnLeftClick += GridUI_OnLeftClick;
    }

    private void OnDisable()
    {
        GridUI.OnLeftBeginDrag -= GridUI_OnLeftBeginDrag;
        GridUI.OnLeftEndDrag -= GridUI_OnLeftEndDrag;
        GridUI.OnLeftClick -= GridUI_OnLeftClick;
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
        Item I00 = new Item(0, "录音笔", "一支有录音功能的笔，似乎有些损坏", "Item/recordPen", new List<string>{ "LadyBag" });
        Item I01 = new Item(1, "电子时钟","一个可设置时间的电子时钟",  "Item/DigitClock", null);
        Item I02 = new Item(2, "piece01","piece01_info", "Item/Piece1", null);
        Item I03 = new Item(3, "电池", "一块电量充足的电池",  "Item/battery", new List<string> { "EnergyRepair1","EnergyRepair3" });
        Item I04 = new Item(4, "piece03", "piece03_info", "Item/Piece3", null);
        Item I05 = new Item(5, "扳手", "可以用来修复", "Item/Tool", new List<string> { "Day2WaterPungCanRepair" });
        Item I06 = new Item(6, "piece02", "piece02_info", "Item/Piece2", null);

        Item I07 = new Item(7, "piece04", "piece04_info", "Item/Piece2", null);
        Item I08 = new Item(8, "piece05", "piece05_info", "Item/Piece2", null);

        Item I09 = new Item(9, "蓝色脑图", "放置在摄像头上可以产生特殊效果","Item/BlueMap", new List<string> { "BlueMapEffect" });
        Item I10 = new Item(10, "SD卡", "相机SD卡",  "Item/SDCard", null);
        Item I11 = new Item(11, "piece06", "piece06_info", "Item/Piece6", null);
        Item I12 = new Item(12, "piece07", "piece07_info", "Item/Piece7", null);
        Item I13 = new Item(13, "medicine", "medecine", "Item/Medecine", null);


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
        ItemList.Add(I13.ID, I13);

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
        //保存已获得的物品
        m_GameModel.collectedItems_Temp.Add(ItemList[itemID]);

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
                if (!m_GameModel.HandleItem(item, TargetName))
                {
                    CreateNewItem(item, prevTransform);
                }
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
