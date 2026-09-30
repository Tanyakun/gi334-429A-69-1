using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectNation : MonoBehaviour
{
	[SerializeField]
	private Sprite[] leaderSprites;

	[SerializeField]
	private Image leaderImage;

	[SerializeField]
	private string nextScene = "Map01";

	[SerializeField]
	private int index = 0;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SelectEuropeNation(int i)
	{
		index = i;
		Settings.playerNationId = index;

		if (leaderSprites[index] != null)
			leaderImage.sprite = leaderSprites[index];
	}

	public void StartGame()
	{
		SceneManager.LoadScene(nextScene);
	}
}
