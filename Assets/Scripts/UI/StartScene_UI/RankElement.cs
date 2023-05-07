using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankElement : MonoBehaviour
{
    [SerializeField] private Sprite[] rankSprites;

    [SerializeField] private RectTransform rectTransform = default;
    [SerializeField] private Image rankImage = default;
    [SerializeField] private TextMeshProUGUI rankText = default;
    [SerializeField] private TextMeshProUGUI userNameText = default;
    [SerializeField] private TextMeshProUGUI scoreText = default;

    public float Height => rectTransform.rect.size.y;

    private void OnDisable()
    {
        rankText.gameObject.SetActive(false);
        rankImage.gameObject.SetActive(false);  
    }

    public void SetRank(int rank, string id, string score)
    {
        if (rank <= 3)
        {
            rankImage.gameObject.SetActive(true);
            rankImage.sprite = rankSprites[(int)rank - 1];
        }

        rankText.text = rank.ToString();
        rankText.gameObject.SetActive(true);

        if (score == null)
        {
            userNameText.enabled = false;
            scoreText.text = "000000";
            return;
        }

        userNameText.text = id;
        scoreText.text = score;
    }
}
