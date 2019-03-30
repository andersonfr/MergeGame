
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;
    public float rate;
    public int level;
    public Sprite itemSprite;

    public void SetData(int level, string name,Sprite sprite, float rate)
    {
        this.level = level;
        this.itemName = name;
        this.itemSprite = sprite;
        this.rate = rate;

        GetComponent<Image>().sprite = itemSprite;
    }
}