using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameObject lootGO = Instantiate(_droppedLootPrefab, transform.position, Quaternion.identity);
            lootGO.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            float dropforce = 2f;
            Vector2 dropDirection = Vector2.up;
            lootGO.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropforce, ForceMode2D.Impulse);
        }
    }
}