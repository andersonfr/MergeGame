using MergeGame.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MergeGame.Items
{
    public class ItemController : MonoBehaviour
    {

        Dragable dragableInterface;
        RectTransform myArea;

        public void SetData(RectTransform dragableArea, Vector3 pos)
        {
            GetComponent<RectTransform>().localPosition = pos;

            myArea = dragableArea;
            dragableInterface = GetComponent<Dragable>();
            if (dragableInterface)
                dragableInterface.SetData(myArea);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

