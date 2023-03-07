using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private bool _timerIsOn;
    [SerializeField] private float _timerValue;
    [SerializeField] private Text _timerView;

    [Header("Objects")]
    [SerializeField] private Player _player;
    [SerializeField] private Exit _exitFromLevel;

    public Transform Goal;

    public GameObject TextYouWon;
    public GameObject TextYouLose;
    public GameObject ButtonRetry;

    public GameObject DialogPanel;
    public GameObject PanelWon;
    public Text txt1;

    private float _timer = 0;
    private bool _gameIsEnded = false;

    private void Awake()
    {
        _timer = _timerValue;
        
    }

    private void Start()
    {
        
        TextYouWon.SetActive(false);
        TextYouLose.SetActive(false);
        ButtonRetry.SetActive(false);
        DialogPanel.SetActive(false);
        PanelWon.SetActive(false);
        _exitFromLevel.Close();
    }

    private void Update()
    {
        if(_gameIsEnded)
            return;
        
        TimerTick();
        LookAtPlayerHealth();
        LookAtPlayerInventory();
        TryCompleteLevel();
        txt1.text = $"Поздравляем, вы прошли уровень за {Time.timeSinceLevelLoad:F1}";
    }

    private void TimerTick()
    {
        if(_timerIsOn == false)
            return;
        
        _timer -= Time.deltaTime;
        _timerView.text = $"{_timer:F1}";
        
        if(_timer <= 0)
            Lose();
    }

    private void TryCompleteLevel()
    {
        if(_exitFromLevel.IsOpen == false)
            return;

        
        if(_player.transform.position.x > Goal.transform.position.x)
        {
            if(_player.transform.position.z > Goal.transform.position.z)
            {
                Victory();
            }
        }
            
    }

    private void LookAtPlayerHealth()
    {
        if(_player.IsAlive)
            return;

        Lose();
        Destroy(_player.gameObject);
    }

    private void LookAtPlayerInventory()
    {
        if(_player.HasKey)
            _exitFromLevel.Open();
    }

    public void Victory()
    {
        
        _gameIsEnded = true;
        _player.Disable();
        TextYouWon.SetActive(true);
        ButtonRetry.SetActive(true);
        PanelWon.SetActive(true);     
    }

    public void Lose()
    {
        _gameIsEnded = true;
        _player.Disable();
        TextYouLose.SetActive(true);
        ButtonRetry.SetActive(true);
        DialogPanel.SetActive(true);
    }
    public void RestartLevel(int numberScene)
    {
        SceneManager.LoadScene(numberScene);
    }
}
