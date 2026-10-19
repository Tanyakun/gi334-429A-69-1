using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectNation : MonoBehaviour
{
	[Header("Leader Data - All arrays use the same order")]
	[SerializeField] private Sprite[] leaderSprites;
	[SerializeField] private string[] leaderNames;
	[SerializeField] private string[] leaderSubtitles;
	[TextArea(4, 12)]
	[SerializeField] private string[] historicalTexts;
	[SerializeField] private Sprite[] flagSprites;

	[Header("Ability 1")]
	[SerializeField] private Sprite[] ability1Icons;
	[SerializeField] private string[] ability1Names;
	[TextArea(2, 4)]
	[SerializeField] private string[] ability1Descriptions;

	[Header("Ability 2")]
	[SerializeField] private Sprite[] ability2Icons;
	[SerializeField] private string[] ability2Names;
	[TextArea(2, 4)]
	[SerializeField] private string[] ability2Descriptions;

	[Header("Special Unit")]
	[SerializeField] private Sprite[] specialUnitIcons;
	[SerializeField] private string[] specialUnitNames;
	[TextArea(2, 4)]
	[SerializeField] private string[] specialUnitDescriptions;

	[Header("Center UI")]
	[SerializeField] private Image leaderImage;
	[SerializeField] private TMP_Text leaderNameText;
	[SerializeField] private TMP_Text leaderSubtitleText;

	[Header("Historical Background UI")]
	[SerializeField] private TMP_Text historicalText;

	[Header("National Flag UI")]
	[SerializeField] private Image flagImage;

	[Header("Ability 1 UI")]
	[SerializeField] private Image ability1Image;
	[SerializeField] private TMP_Text ability1NameText;
	[SerializeField] private TMP_Text ability1DescriptionText;

	[Header("Ability 2 UI")]
	[SerializeField] private Image ability2Image;
	[SerializeField] private TMP_Text ability2NameText;
	[SerializeField] private TMP_Text ability2DescriptionText;

	[Header("Special Unit UI")]
	[SerializeField] private Image specialUnitImage;
	[SerializeField] private TMP_Text specialUnitNameText;
	[SerializeField] private TMP_Text specialUnitDescriptionText;

	[SerializeField]
	private string nextScene = "Map01";

	[SerializeField]
	private string BackScene = "MainMenu";

	[SerializeField]
	private int index = 0;

	private void Start()
	{
		if (leaderSprites == null || leaderSprites.Length == 0)
		{
			Debug.LogError("SelectNation: No leader sprites assigned.");
			return;
		}

		index = Mathf.Clamp(index, 0, leaderSprites.Length - 1);
		UpdateLeaderUI();
	}

	// Called by LeaderCarousel when the selected leader changes
	public void SetSelectedLeader(int i)
	{
		SelectEuropeNation(i);
	}

	public void SelectEuropeNation(int i)
	{
		if (leaderSprites == null ||
			i < 0 || i >= leaderSprites.Length)
			return;

		index = i;
		Settings.playerNationId = index;

		UpdateLeaderUI();
	}

	private void UpdateLeaderUI()
	{
		SetSprite(leaderImage, leaderSprites);

		SetText(leaderNameText, leaderNames);
		SetText(leaderSubtitleText, leaderSubtitles);
		SetText(historicalText, historicalTexts);

		SetSprite(flagImage, flagSprites);

		SetSprite(ability1Image, ability1Icons);
		SetText(ability1NameText, ability1Names);
		SetText(ability1DescriptionText, ability1Descriptions);

		SetSprite(ability2Image, ability2Icons);
		SetText(ability2NameText, ability2Names);
		SetText(ability2DescriptionText, ability2Descriptions);

		SetSprite(specialUnitImage, specialUnitIcons);
		SetText(specialUnitNameText, specialUnitNames);
		SetText(specialUnitDescriptionText, specialUnitDescriptions);
	}

	private void SetText(TMP_Text target, string[] data)
	{
		if (target == null) return;

		if (data != null && index < data.Length)
			target.text = data[index];
		else
			target.text = "";
	}

	private void SetSprite(Image target, Sprite[] data)
	{
		if (target == null) return;

		if (data != null && index < data.Length)
		{
			target.sprite = data[index];
			target.enabled = data[index] != null;
		}
		else
		{
			target.enabled = false;
		}
	}

	public void StartGame()
	{
		Settings.playerNationId = index;
		SceneManager.LoadScene(nextScene);
	}
}
