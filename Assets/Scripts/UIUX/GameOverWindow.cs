using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{
    [SerializeField] private float _duration = 3f;
    [SerializeField] private TextMeshProUGUI[] _statLabels;
    [SerializeField] private TextMeshProUGUI[] _statValues;
    [SerializeField] private TextMeshProUGUI _totalScore;
    [SerializeField] private Button _nextButton;

    private int _index = 0;
    private int _maxLines = 3;
    private int _line = 0;
    private float _timer = 0f;
    private int _scoreTemp;
    private bool _isDone = false;

    private void Awake()
    {
        _nextButton.onClick.AddListener(OnNext);
    }

    private void Update()
    {
        if (_isDone) return;

        _timer += Time.deltaTime;

        if (_index == _statLabels.Length)
        {
            int score = Mathf.RoundToInt(Mathf.Lerp(0, _scoreTemp, _timer / _duration));
            _totalScore.text = $"{score:d9}";
        }

        if (_timer > _duration)
        {
            _timer = 0f;

            if (_index < _statLabels.Length) AddStat();
            else _isDone = true;
        }
    }

    public override void Open()
    {
        base.Open();

        foreach (var label in _statLabels)
        {
            label.text = string.Empty;
        }

        foreach (var value in _statValues)
        {
            value.text = string.Empty;
        }

        _totalScore.text = $"{0:d9}";
        _index = 0;
        _line = 0;
        _timer = 0f;
        _scoreTemp = Random.Range(0, 999999999);
        _isDone = false;
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnNext()
    {
        WindowManager.Open(0);
    }

    private void AddStat()
    {
        _statLabels[_index].text = _statLabels[_index].text + "STAT" + "\n";
        _statValues[_index].text = _statValues[_index].text + $"{Random.Range(0, 999)}" + "\n";
        _line++;

        if (_line == _maxLines)
        {
            _index++;
            _line = 0;
        }
    }
}