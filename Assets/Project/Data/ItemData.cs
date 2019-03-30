using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MergeGame/Item",fileName ="Item") ]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public int level;
    public float rate;
}
