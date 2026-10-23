using UnityEngine;
using UnityEngine.EventSystems;

public class CargoSlot : MonoBehaviour, IDropHandler
{
	[SerializeField]
	private NavalUnit ship;

	[SerializeField]
	private int holdId;

	[SerializeField]
	private CargoDrag cargoDrag;
	public CargoDrag CargoDrag { get { return cargoDrag; } set { cargoDrag = value; } }

	[SerializeField]
	private UIManager uiMgr;

	void Awake()
	{
		uiMgr = UIManager.instance;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SlotInit(NavalUnit ship, int holdId)
	{
		this.ship = ship;
		this.holdId = holdId;
	}

	public void OnDrop(PointerEventData eventData)
	{
		//Debug.Log($"Drop on Slot:{holdId}");

		if (ship.CargoList.Count > holdId)//there's something in this hold
		{
			if (ship.CargoList[holdId].Quantity >= 100)
				return;
		}
		GameObject obj = eventData.pointerDrag;
		StockDrag stockDrag = obj.GetComponent<StockDrag>();

		if (stockDrag == null)
			return;

		/*cargoDrag.IconParent = transform;
		cargoObj.transform.position = transform.position;*/

		int cargoLeft = ship.AddCargo(holdId, stockDrag.Cargo);
		stockDrag.Cargo.Quantity = cargoLeft;

		uiMgr.UpdateCargoSlots(ship);
	}
}
