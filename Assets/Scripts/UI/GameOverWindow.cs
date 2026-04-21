using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;
    
    public Button nextButton;

    public float statsDelay = 1f;
    public float scoreDuration = 2f;

    private const int totalStats = 6;
    private const int statsPerColumn = 3;

    private int[] statsRolls = new int[totalStats];
    private int finalScore;

    private TextMeshProUGUI[] statsLabels;
    private TextMeshProUGUI[] statsValues;

    private Coroutine routine;

    private void Awake()
    {
        statsLabels = new TextMeshProUGUI[] {leftStatLabel, rightStatLabel};
        statsValues = new TextMeshProUGUI[] {leftStatValue, rightStatValue};

        nextButton.onClick.AddListener(OnNext);
    }

    public override void Open()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }

        base.Open();
        ResetStats();
        routine = StartCoroutine(CoPlayGameOverRoutine());

        //InitText();
        //StartCoroutine(UpdateText());
    }

    public override void Close()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
        ResetStats();

        base.Close();
    }

    public void OnNext()
    {
        windowManager.Open(0);
    }

    private void ResetStats()
    {
        for (int i = 0; i < statsRolls.Length; i++)
        {
            statsRolls[i] = Random.Range(0, 1000);
        }
        finalScore = Random.Range(0, 100000000);

        for (int i = 0; i < statsLabels.Length; i++)
        {
            statsLabels[i].text = string.Empty;
            statsValues[i].text = string.Empty;
        }

        scoreValue.text = $"{0:D8}";
    }

    private IEnumerator CoPlayGameOverRoutine()
    {
        for (int i = 0; i < totalStats; i++)
        {
            yield return new WaitForSeconds(statsDelay);

            int column = i / statsPerColumn;
            var labelText = statsLabels[column];
            var valueText = statsValues[column];
            string newline = (i % statsPerColumn == 0) ? string.Empty : "\n";
            labelText.text = $"{labelText.text}{newline}Stat {i}";
            valueText.text = $"{valueText.text}{newline}{statsRolls[i]:D4}";
        }

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / scoreDuration;
            int current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreValue.text = $"{current:D8}";

            yield return null;
        }

        scoreValue.text = $"{finalScore:D8}";
        routine = null;
    }



    //private IEnumerator UpdateText()
    //{
    //    for (int i = 1; i < 4; i++)
    //    {
    //        yield return new WaitForSeconds(1f);

    //        leftStatLabel.text = string.Concat(leftStatLabel.text, $"Stat {i}\n");
    //        leftStatValue.text = string.Concat(leftStatValue.text, $"{Random.Range(100, 500).ToString()}\n");
    //    }

    //    for (int i = 1; i < 4; i++)
    //    {
    //        yield return new WaitForSeconds(1f);

    //        rightStatLabel.text = string.Concat(rightStatLabel.text, $"Stat {i}\n");
    //        rightStatValue.text = string.Concat(rightStatValue.text, $"{Random.Range(100, 500).ToString()}\n");
    //    }

    //    StartCoroutine(AnimateScore());
    //}

    //private void InitText()
    //{
    //    leftStatLabel.text = "";
    //    leftStatValue.text = "";
    //    rightStatLabel.text = "";
    //    rightStatValue.text = "";
    //}

    //private IEnumerator AnimateScore()
    //{
    //    float elapsed = 0f;
    //    int startScore = 0;
    //    int endScore = Random.Range(10000, 100000);
    //    float duration = 2f;

    //    while (elapsed < duration)
    //    {
    //        elapsed += Time.deltaTime;

    //        float t = Mathf.Clamp01(elapsed / duration);

    //        scoreValue.text = $"{Mathf.Lerp(startScore, endScore, t):00000000}";

    //        yield return null; 
    //    }

    //    scoreValue.text = $"{endScore:00000000}";
    //}
}
