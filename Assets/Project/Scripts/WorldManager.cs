using MergeGame.Items;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private ItemData[] items;

    [SerializeField]
    private WorldTimer worldTimer;

    [SerializeField]
    private ItemController itemObject;

    public RectTransform spawnBox;

    [SerializeField]
    private float coinsRate;

    Factory factory;

    #region properties
    public static WorldManager Instance { get; private set; }
    #endregion
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            factory = new Factory();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initializing World...");
        //SpawnItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SpawnItem();

        Debug.Log(coinsRate);
    }

    public bool CombineEvent(Item a, Item b)
    {
        CombineEvent ce = new CombineEvent(a, b);
        bool isCombination = ce.CheckCombination();
        if (isCombination)
        {
            factory.CreateItem(a.level+1, a.GetComponent<RectTransform>().localPosition);
            Destroy(a.gameObject);
            Destroy(b.gameObject);
        }
        return isCombination;
    }

    void SpawnItem()
    {        
        coinsRate += factory.CreateItem(1);
    }

    private class Factory
    {
        public Factory()
        {
            
        }
        
        public float CreateItem(int level, Vector3 position = default)
        {
            Item i = Instantiate(Instance.itemObject, Instance.spawnBox.transform).GetComponent<Item>();
            if (position.Equals(default))
            {
                
                Vector3 minPosition = Instance.spawnBox.rect.min - i.GetComponent<RectTransform>().rect.min;
                Vector3 maxPosition = Instance.spawnBox.rect.max - i.GetComponent<RectTransform>().rect.max;
                position = new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
            }

            i.SetData(level, Instance.items[level-1].itemName, Instance.items[level - 1].itemSprite, Instance.items[level - 1].rate);
            i.GetComponent<ItemController>().SetData(Instance.spawnBox, position);

            return i.rate;
        }

    }

}
