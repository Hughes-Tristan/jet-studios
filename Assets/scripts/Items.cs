
// Developer(s): Tristan Hughes
// Last Updated: 11-16-24
// Items Class

// the intended purpose of this class is to represent an item

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is a representation of an item
// you can use it create an item by using the create menu in unity
// make sure to assign a sprite and name
[CreateAssetMenu(fileName = "newItem", menuName = "ItemCreator/New Item")]
public class Items : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
}
