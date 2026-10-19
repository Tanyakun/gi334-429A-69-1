
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LeaderCarousel : MonoBehaviour
{
	[Header("All 5 Leaders - in selection order")]
	[SerializeField] private Sprite[] leaderSprites;

	[Header("The 3 visible portrait Images")]
	[SerializeField] private Image leftPortrait;
	[SerializeField] private Image centerPortrait;
	[SerializeField] private Image rightPortrait;

	[Header("Position Settings")]
	[SerializeField] private float sideX = 100f;
	[SerializeField] private float centerX = 0f;
	[SerializeField] private float centerY = 0f;
	[SerializeField] private float sideScale = 0.7f;
	[SerializeField] private float centerScale = 1f;

	[Header("Animation")]
	[SerializeField] private float animationDuration = 0.3f;

	[Header("Selection")]
	[SerializeField] private int selectedIndex = 0;

	[Header("Connect SelectNation")]
	[SerializeField] private SelectNation selectNation;

	private bool isAnimating;

	private RectTransform LeftRT => leftPortrait.rectTransform;
	private RectTransform CenterRT => centerPortrait.rectTransform;
	private RectTransform RightRT => rightPortrait.rectTransform;

	private void Start()
	{
		if (leaderSprites == null || leaderSprites.Length < 3 ||
			leftPortrait == null ||
			centerPortrait == null ||
			rightPortrait == null)
		{
			Debug.LogError(
				"LeaderCarousel: Assign 3 portraits and at least 3 leader sprites."
			);
			enabled = false;
			return;
		}

		selectedIndex = Mathf.Clamp(
			selectedIndex, 0, leaderSprites.Length - 1
		);

		SetInitialLayout();
		NotifySelectedLeader();
	}

	private void SetInitialLayout()
	{
		leftPortrait.sprite = GetSprite(selectedIndex - 1);
		centerPortrait.sprite = GetSprite(selectedIndex);
		rightPortrait.sprite = GetSprite(selectedIndex + 1);

		LeftRT.anchoredPosition = new Vector2(-sideX, centerY);
		CenterRT.anchoredPosition = new Vector2(centerX, centerY);
		RightRT.anchoredPosition = new Vector2(sideX, centerY);

		LeftRT.localScale = Vector3.one * sideScale;
		CenterRT.localScale = Vector3.one * centerScale;
		RightRT.localScale = Vector3.one * sideScale;
	}

	private Sprite GetSprite(int index)
	{
		index = (index % leaderSprites.Length
				 + leaderSprites.Length)
				 % leaderSprites.Length;

		return leaderSprites[index];
	}

	public void Next()
	{
		if (isAnimating) return;

		int nextIndex = (selectedIndex + 1) % leaderSprites.Length;

		StartCoroutine(AnimateNext(nextIndex));
	}

	public void Previous()
	{
		if (isAnimating) return;

		int previousIndex =
			(selectedIndex - 1 + leaderSprites.Length)
			% leaderSprites.Length;

		StartCoroutine(AnimatePrevious(previousIndex));
	}

	private IEnumerator AnimateNext(int nextIndex)
	{
		isAnimating = true;

		Image incoming = leftPortrait;

		// เตรียมรูปใหม่ไว้นอกจอทางขวา
		incoming.sprite = GetSprite(nextIndex + 1);
		incoming.rectTransform.anchoredPosition =
			new Vector2(sideX + 100f, centerY);
		incoming.rectTransform.localScale =
			Vector3.one * sideScale;

		Vector2 leftTarget = new Vector2(-sideX, centerY);
		Vector2 centerTarget = new Vector2(centerX, centerY);
		Vector2 rightTarget = new Vector2(sideX, centerY);

		yield return AnimateThree(
			centerPortrait.rectTransform, leftTarget, sideScale,
			rightPortrait.rectTransform, centerTarget, centerScale,
			incoming.rectTransform, rightTarget, sideScale
		);

		// สลับหน้าที่ของ Image ทั้งสาม
		Image oldLeft = leftPortrait;
		leftPortrait = centerPortrait;
		centerPortrait = rightPortrait;
		rightPortrait = oldLeft;

		selectedIndex = nextIndex;
		NotifySelectedLeader();

		isAnimating = false;
	}

	private IEnumerator AnimatePrevious(int previousIndex)
	{
		isAnimating = true;

		Image incoming = rightPortrait;

		// เตรียมรูปใหม่ไว้นอกจอทางซ้าย
		incoming.sprite = GetSprite(previousIndex - 1);
		incoming.rectTransform.anchoredPosition =
			new Vector2(-sideX - 100f, centerY);
		incoming.rectTransform.localScale =
			Vector3.one * sideScale;

		Vector2 leftTarget = new Vector2(-sideX, centerY);
		Vector2 centerTarget = new Vector2(centerX, centerY);
		Vector2 rightTarget = new Vector2(sideX, centerY);

		yield return AnimateThree(
			incoming.rectTransform, leftTarget, sideScale,
			leftPortrait.rectTransform, centerTarget, centerScale,
			centerPortrait.rectTransform, rightTarget, sideScale
		);

		// สลับหน้าที่ของ Image ทั้งสาม
		Image oldRight = rightPortrait;
		rightPortrait = centerPortrait;
		centerPortrait = leftPortrait;
		leftPortrait = oldRight;

		selectedIndex = previousIndex;
		NotifySelectedLeader();

		isAnimating = false;
	}

	private IEnumerator AnimateThree(
		RectTransform a, Vector2 posA, float scaleA,
		RectTransform b, Vector2 posB, float scaleB,
		RectTransform c, Vector2 posC, float scaleC)
	{
		Vector2 startA = a.anchoredPosition;
		Vector2 startB = b.anchoredPosition;
		Vector2 startC = c.anchoredPosition;

		Vector3 startScaleA = a.localScale;
		Vector3 startScaleB = b.localScale;
		Vector3 startScaleC = c.localScale;

		float elapsed = 0f;
		float duration = Mathf.Max(0.01f, animationDuration);

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;

			float t = Mathf.Clamp01(elapsed / duration);

			// SmoothStep ทำให้การเคลื่อนไหวดูนุ่มนวล
			t = t * t * (3f - 2f * t);

			a.anchoredPosition = Vector2.Lerp(startA, posA, t);
			b.anchoredPosition = Vector2.Lerp(startB, posB, t);
			c.anchoredPosition = Vector2.Lerp(startC, posC, t);

			a.localScale = Vector3.Lerp(
				startScaleA, Vector3.one * scaleA, t
			);
			b.localScale = Vector3.Lerp(
				startScaleB, Vector3.one * scaleB, t
			);
			c.localScale = Vector3.Lerp(
				startScaleC, Vector3.one * scaleC, t
			);

			yield return null;
		}

		a.anchoredPosition = posA;
		b.anchoredPosition = posB;
		c.anchoredPosition = posC;

		a.localScale = Vector3.one * scaleA;
		b.localScale = Vector3.one * scaleB;
		c.localScale = Vector3.one * scaleC;
	}

	private void NotifySelectedLeader()
	{
		if (selectNation != null)
		{
			selectNation.SetSelectedLeader(selectedIndex);
		}
	}
}
