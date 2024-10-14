using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _endGameText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _healthText;
    private float _startTime;
    private float _currentTime;
    private bool _isPlaying = false;
    [SerializeField] private float _fallThreshold = -10f;

    [SerializeField] private PlayerController _playerController;

    void Start()
    {
        Time.timeScale = 1;
        _endGameText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _startTime = Time.time;
    }

    void Update()
    {
        _healthText.text = "Health: " + _playerController.GetHealth().ToString();
        if (_isPlaying)
        {
            _currentTime = Time.time - _startTime;
            _timerText.text = "Time: " + _currentTime.ToString("F2");

            if (_playerController.GetHealth() <= 0)
            {
                Defeat();
            }
            if (_playerController.transform.position.y < _fallThreshold)
            {
                Defeat();
            }
        }
    }

    public void PlayerCrossedFinishLine()
    {
        Victory();
    }
    public void PlayerCrossedStartLine()
    {
        _isPlaying = true;
    }
    public void PlayerFellOff()
    {
        Defeat();
    }


    void Victory()
    {
        _isPlaying = false;
        _endGameText.gameObject.SetActive(true);
        _endGameText.text = "Victory!";
        _timerText.gameObject.SetActive(true);
        _timerText.text = "Time: " + _currentTime.ToString("F2");
        _restartButton.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    void Defeat()
    {
        _isPlaying = false;
        _endGameText.gameObject.SetActive(true);
        _endGameText.text = "Defeat!";
        _timerText.gameObject.SetActive(true);
        _timerText.text = "Time: " + _currentTime.ToString("F2");
        _restartButton.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
