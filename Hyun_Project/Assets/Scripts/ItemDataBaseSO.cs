using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBaseSo", menuName = "Inventory/DataBase")]
public class ItemDataBaseSO : ScriptableObject
{
    public List<ItemSO> items = new List<ItemSO>();     // ItemSO 리스트로 관리
    
    // 캐싱을 위한 Dictrionary
    private Dictionary<int, ItemSO> itemsByld;          // ID로 아이템 찾기 위한 캐싱
    private Dictionary<string, ItemSO> itemsByName;     // 이름으로 아이템 찾기

    public void Initialze()
    {
        itemsByld = new Dictionary<int, ItemSO>();      // 위에 선언만 했기 때문에 Dictionary 할당
        itemsByName = new Dictionary<string, ItemSO>();

        foreach(var item in items)
        {
            itemsByld[item.id] = item;
            itemsByName[item.itemName] = item;
        }
    }
    
    // ID로 아이템 찾기
    public ItemSO GetItemByld(int id)
    {
        if(itemsByld == null)
        {
            Initialze();        // 캐싱이 되어있는지 확인하고 아니면 초기화 한다.         
        }
        if (itemsByld.TryGetValue(id, out ItemSO item)) return item;
        return null;    
        
    }

    // 이름으로 아이템 찾기
    public ItemSO GetItemByName(string name)
    {
        if(itemsByName == null)
        {
            Initialze();    // 캐싱이 되어있는지 확인하고 아니면 초기화 한다
        }

        if (itemsByName.TryGetValue(name, out ItemSO item)) return item;        // Name 값을 찾아서 ItemSO 리턴한다.
        return null;
    }

    public List<ItemSO> GetItemByType(ItemType type)
    {
        return items.FindAll(item => item.itemType == type);
    }
}
