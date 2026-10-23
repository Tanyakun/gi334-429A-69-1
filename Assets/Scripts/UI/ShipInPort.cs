using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShipInPort : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private Image unitImage;

	[SerializeField]
	private NavalUnit navalUnit;
	public NavalUnit NavalUnit { get { return navalUnit; } set { navalUnit = value; } }

	[SerializeField]
	private TMP_Text turnText;
	public TMP_Text TurnText { get { return turnText; } set { turnText = value; } }

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

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Click at ship");
	}

	public void UnitInit(NavalUnit navalUnit)
	{
		unitImage.sprite = navalUnit.UnitSprite.sprite;
		this.navalUnit = navalUnit;
	}

	public void UpdateTurnText(int turn)
	{
		turnText.gameObject.SetActive(true);
		turnText.text = ($"{turn}\nTurn(s)");
		turnText.color = Color.yellow;
	}
}
