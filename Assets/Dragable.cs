using UnityEngine;
using UnityEngine.EventSystems;


namespace MergeGame.Input
{
    public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform dragItem;
        private Vector2 originalLocalPointerPosition;
        private Vector3 originalPanelLocalPosition;
        
        //If dragArea == null we get the default canvas in the scene
        public RectTransform DragArea { get; set; }

        //collider to identify possible merge
        private CircleCollider2D mergeCollider;
        private Collider2D other;

        private void Start()
        {
            dragItem = GetComponent<RectTransform>();
            mergeCollider = GetComponent<CircleCollider2D>();
        }

        public void SetData(RectTransform dragArea = null)
        {
            if (dragArea)
                DragArea = dragArea;
            else
            {
                DragArea = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalPanelLocalPosition = dragItem.localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(DragArea, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(DragArea, eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
                dragItem.localPosition = originalPanelLocalPosition + offsetToOriginal;
            }

            //ClampToArea();
        }

        void ClampToArea()
        {
            Vector3 pos = dragItem.localPosition;

            Vector3 minPosition = DragArea.rect.min - dragItem.rect.min;
            Vector3 maxPosition = DragArea.rect.max - dragItem.rect.max;

            pos.x = Mathf.Clamp(dragItem.localPosition.x, minPosition.x, maxPosition.x);
            pos.y = Mathf.Clamp(dragItem.localPosition.y, minPosition.y, maxPosition.y);

            dragItem.localPosition = pos;
        }

        void CheckBounds()
        {
            Vector3 minPosition = DragArea.rect.min - dragItem.rect.min;
            Vector3 maxPosition = DragArea.rect.max - dragItem.rect.max;

            if((dragItem.localPosition.x > maxPosition.x || dragItem.localPosition.x < minPosition.x) ||
                (dragItem.localPosition.y > maxPosition.y || dragItem.localPosition.y < minPosition.y))
            {
                dragItem.localPosition = new Vector2(0, 0);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            CheckBounds();

            //TODO test combining event
            //Debug.Log(Physics2D.OverlapCircle(eventData.position, mergeCollider.radius));
            if (other)
                if (WorldManager.Instance.CombineEvent(GetComponent<Item>(), other.GetComponent<Item>()))
                {
                    //TODO something

                }
                
            
        }

        private void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other);
            this.other = other;
            //Collider2D[] contacts = new Collider2D[10];
            //int contactsNumber = mergeCollider.GetContacts(contacts);

            //if (contactsNumber > 0) Debug.Log(contacts[0]);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            this.other = null;
        }
    }
}

