using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootBag : MonoBehaviour
{
    [SerializeField] private GameObject _droppedLootPrefab;
    [SerializeField] List<Loot> lootList = new List<Loot>();
    // Update is called once per frame
    
    Loot GetDroppedLootItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if(possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;
    }
    public void InstantiateLoot()
    {
        Loot droppedItem = GetDroppedLootItem();
        if(droppedItem != null)
        {
            GameObject lootGO = Instantiate(_droppedLootPrefab);
            lootGO.transform.position = transform.position;
            var lootIcon = lootGO.GetComponentInChildren<SpriteRenderer>();
            if(lootIcon != null)
                lootIcon.sprite = droppedItem.data.bulletIcon;
            var lootData = lootGO.GetComponentInChildren<DropLootAddToList>();
            if(lootData != null)
                lootData.SetLootInfo(droppedItem.data);
            //float dropforce = 2f;
            //Vector2 dropDirection = Vector2.up;
            //lootGO.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropforce, ForceMode2D.Impulse);
        }
    }
}