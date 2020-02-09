//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public bool lockCursore;
    public bool showCursor;
    public bool showCroshair;
    [Space]
    public bool oldRendering;
    public RawImage texture;
    [Space]
    public GameObject PlayerPrefab;
    [Space]
    public SceneMusic sceneMusic;
    [Space]
    public MainPanel mainMenuPanel;
    public SimplePanel gameMenuPanel;
    public SimplePanel optionsPanel;
    [Space]
    public LoadScreen loadScreenPanel;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public SimplePanel deathScreen;

    public StatisticePanel statisticeScreen;

    private bool paused = false;
    private AsyncOperation progres;
    private AudioSource audSo;
    private int qurentScene = 0;
    [HideInInspector]
    public Statistic statistic = new Statistic();
    private RectTransform rectTRf;

    [System.Serializable]
    public class Statistic 
    {
        public float t = 0;
        public float sec = 0;
        public float min = 0;
        public float hor = 0;

        public string time = "";
        public int enamy = 0;
        public int item = 0;
        public int secrets = 0;
    }

    //
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        } else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        audSo = GetComponent<AudioSource>();
    }
    //
    private void Start()
    {
        PlaySceneMusic(qurentScene);
        mainMenuPanel.panel.SetActive(true);
        //mainMenuPanel.mainText.SetActive(true);
        mainMenuPanel.selectedText.Select();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Screen.lockCursor = lockCursore;
        //Cursor.visible = showCursor;
        //Cursor.lockState = CursorLockMode.Locked;

    }
    // Выполняется каждый кадр
    private void Update()
    {
        qurentScene = SceneManager.GetActiveScene().buildIndex;

        if (UIDynamicCrosshair.Instance != null)
            UIDynamicCrosshair.Instance.gameObject.SetActive(showCroshair);

        if (sceneMusic.volumeSlider != null)
            audSo.volume = sceneMusic.volumeSlider.value;

        if (qurentScene > 0)
        {
            if (oldRendering && texture) 
            {
                texture.gameObject.SetActive(true);
                Camera.main.targetTexture = (RenderTexture)texture.texture;
            }
            else
            {
                texture.gameObject.SetActive(false);
            }
            if (Input.GetButtonDown("Cancel"))
            {
                if (!paused)
                {
                    GamePause(true);
                }
                else
                {
                    GamePause(false);
                }
            }
            if (paused != true)
            {
                statisticeScreen.time.text = GetGamePlayingTime();
                statisticeScreen.enemy.text = statistic.enamy.ToString();
                statisticeScreen.secrets.text = statistic.secrets.ToString();
            }
        }
        if (progres != null && progres.isDone)
        {
            loadScreenPanel.panel.active = false;
        }
    }
    // Проиграть Фоновую музыку
    private void PlaySceneMusic(int lvl, bool loop = true)
    {
        audSo.clip = sceneMusic.levelMusic[lvl];
        audSo.loop = loop;
        audSo.Play();
    }
    private void PlayTrack(AudioClip clip) 
    {
        audSo.clip = clip;
        audSo.loop = true;
        audSo.Play();
    }
    // Получить игровое время
    private string GetGamePlayingTime()
    {
        statistic.t += Time.deltaTime;
        statistic.sec = Mathf.FloorToInt(statistic.t % 60);
        statistic.min = Mathf.Floor(statistic.t / 60);
        statistic.hor = Mathf.Floor(statistic.t / 3600); 
        return statistic.hor + ":" + statistic.min + ":" + statistic.sec;
    }
    // Асинхронній процес загрузки
    IEnumerator LoadAsync(int nextScene)
    {
        loadScreenPanel.panel.active = true;

        progres = SceneManager.LoadSceneAsync(nextScene);

        while (!progres.isDone)
        {
            float p = Mathf.Clamp01(progres.progress / 0.9f);
            loadScreenPanel.progressBar.fillAmount = p;
            loadScreenPanel.procentBar.text = (p * 100).ToString("##.##") + "%";
            yield return null;
        }
    }
    // ВЫполняется при загрузке сцены
    private void OnLevelWasLoaded(int level)
    {
        if (level != null)
        PlaySceneMusic(level);
        if (level > 0)
            SpawnPlayer();
        if (level == 0)
        {
            mainMenuPanel.panel.SetActive(true);
            //mainMenuPanel.mainText.SetActive(true);
            mainMenuPanel.selectedText.Select();

            gameMenuPanel.panel.active = false;
        }
    }
    //Спавним игрока
    private void SpawnPlayer()
    {
        StartPoint[] respawns = GameObject.FindObjectsOfType<StartPoint>();
        foreach (StartPoint respawn in respawns)
        {
            if (respawn.thisPoint == true)
            {
                GameObject player = (GameObject)Instantiate(PlayerPrefab, respawn.transform.position, respawn.transform.rotation);
            }
        }
    }
    // Счетчик убитых врагов
    public void AddEnemyFrag(int val, bool inc = true) 
    {
        if (inc == true) 
        {
            statistic.enamy += val;
        }
        Debug.Log("ENEMYS: "+ statistic.enamy);
    }
    //---------------------Публичние методы навигации в меню------------------------------------
    // Грузить уровень номер ...
    public void GameStartLevel(int nextScene)
    {
        mainMenuPanel.panel.SetActive(false);
        //mainMenuPanel.mainText.SetActive(false);

        gameMenuPanel.panel.SetActive(false);

        deathScreen.panel.SetActive(false);
        statisticeScreen.panel.SetActive(false);

        //choiceLevelPanel.panel.active = false;

        Debug.Log("Loding: " + nextScene);
        StartCoroutine(LoadAsync(nextScene));
    }
    // Грузить следующий уровень ...
    public void GameNextLevel()
    {
        mainMenuPanel.panel.SetActive(false);
        //mainMenuPanel.mainText.SetActive(false);

        gameMenuPanel.panel.SetActive(false);

        deathScreen.panel.SetActive(false);
        statisticeScreen.panel.SetActive(false);

        //choiceLevelPanel.panel.active = false;

        Debug.Log("Loding: " + qurentScene+1);
        StartCoroutine(LoadAsync(qurentScene + 1));
    }
    //Показать панель опций
    public void OptionsShow(bool arg)
    {
        optionsPanel.panel.active = arg;
        if (arg == true)
        {
            optionsPanel.selectedText.Select();
            if (qurentScene == 0)
            {
                mainMenuPanel.panel.SetActive(false);
            }
            else
            {
                gameMenuPanel.panel.SetActive(false);
            }
        }
        else 
        {
            if (qurentScene == 0)
            {
                mainMenuPanel.panel.SetActive(true);
                mainMenuPanel.selectedText.Select();
            }
            else
            {
                gameMenuPanel.panel.SetActive(true);
                gameMenuPanel.selectedText.Select();
            }
        }

    }
    // Поставить на паузу
    public void GamePause(bool arg)
    {
        paused = arg;
        //Screen.lockCursor = arg;
        //Cursor.visible = !arg;
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        FirstPersonController fpsc = player.GetComponent<FirstPersonController>();
        if (arg == true)
        {
            //Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            fpsc.enabled = false;

            gameMenuPanel.panel.SetActive(true);
            gameMenuPanel.selectedText.Select();
        }
        else 
        {
           // Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1.0f;
            fpsc.enabled = true;

            gameMenuPanel.panel.SetActive(false);
        }
    }
    // Перезапустить уровень
    public void GameRestart() 
    {
        statistic.t = 0;
        deathScreen.panel.SetActive(false);
        StartCoroutine(LoadAsync(qurentScene));
    }
    // Возврат в игру
    public void ReturnGame() 
    {
        GamePause(false);
    }
    // Выход из игры
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    // Конец игры с проигришем
    public void GameOver() 
    {
        deathScreen.panel.SetActive(true);
        deathScreen.selectedText.Select();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Screen.lockCursor = lockCursore;
        //Cursor.visible = showCursor;
        //Cursor.lockState = CursorLockMode.Locked;

        audSo.Stop();
    }
    // Конец игры с выигришем
    public void GameEnd() 
    {
        GamePause(true);

        statisticeScreen.panel.SetActive(true);
        statisticeScreen.selectedText.Select();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Screen.lockCursor = lockCursore;
        //Cursor.visible = showCursor;
        //Cursor.lockState = CursorLockMode.Locked;

        PlayTrack(sceneMusic.complateMusic);

        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<PlayerAudioController>().enabled = false;
    }
    //
    public void ToMainMenu() 
    {
        GameStartLevel(0);
    }
//---------------------------------Полноекранный режим---------------------------------------
    public void FullScreen(bool arg) 
    {
        Screen.fullScreen = arg;
    }
    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    //--------------------------------------------------------------------------------------
    public void SetResolution()
    {
        // Переключение на 640 х 480 полный экран на 60 герц
        Screen.SetResolution(640, 480, true, 60);
    }
}

[System.Serializable]
public struct LoadScreen
{
    public GameObject panel;
    public Image progressBar;
    public Text procentBar;
}

[System.Serializable]
public class MainPanel
{
    public GameObject panel;
    public GameObject mainText;
    public Button selectedText;
}

[System.Serializable]
public class SimplePanel
{
    public GameObject panel;
    public Button selectedText;
}

[System.Serializable]
public class StatisticePanel
{
    public GameObject panel;
    public Button selectedText;
    public Text time;
    public Text enemy;
    public Text secrets;
}

[System.Serializable]
public class SceneMusic
{
    public Slider volumeSlider;
    public AudioClip[] levelMusic;
    public AudioClip complateMusic;
}