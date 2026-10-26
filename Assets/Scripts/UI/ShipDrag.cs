using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ShipDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField]
	private Image shipImage;

	[SerializeField]
	private NavalUnit ship;
	public NavalUnit Ship { get { return ship; } set { ship = value; } }

	[SerializeField]
	private Transform iconParent;
	public Transform IconParent { get { return iconParent; } set { iconParent = value; } }

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Init(NavalUnit ship)
	{
		shipImage.sprite = ship.UnitSprite.sprite;
		this.ship = ship;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		//Debug.Log("Begin Drag");
		iconParent = transform.parent;
		transform.SetParent(transform.root);
		transform.SetAsLastSibling();
		shipImage.raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log("On Drag");
		transform.position = Mouse.current.position.value;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		//Debug.Log("End Drag");
		transform.SetParent(iconParent);
		shipImage.raycastTarget = true;
		transform.position = iconParent.position;
	}
}
