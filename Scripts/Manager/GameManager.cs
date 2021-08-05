using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct SaveData
{
    public int playerHealth;
    public int unicornHorns;
    public int medicine;
    public int Doll;
}

public class GameManager : MonoBehaviour
{
    PlayerMove player;

    //시작 UI 관련
    public GameObject startUI;
    MainMenu start;
    public GameObject startPanel;
    public GameObject howToPlayPanel;
    int startCount = 0;

    //사망 UI 관련
    public GameObject DeathUI;
    DeathMenu death;
    public bool isDeath;

    //InGame UI 관련
    public GameObject inGameUI;
    public GameObject itemUI;
    public Text uniconCount;
    public Image potion;
    public Text potionCount;
    public Image doll;
    public Text dollcount;
    public Image playerHeart;
    public Sprite[] hearts;
    int heartCount;
    public GameObject LoadingPanel;

    //Pause UI 관련
    public GameObject PauseUI;
    PauseMenu pause;
    public GameObject pauseButtonPanel;
    public GameObject howToPlayPanel_PauseUI;
    public bool isPauseOn;

    //Talk UI 관련
    public GameObject talkPanel;
    public Text talkText;
    GameObject scanObject;
    public bool isTextOn = false;
    TalkManager tm;
    int talkIndex;
    public bool hadTalked;

    //아이템 관련
    public int Doll = 0;            //layer 15
    public int unicornHorns = 0;    //layer 16
    public int medicine = 0;        //layer 18

    //스테이지 전환 관련
    public int nowStage;
    MainCamara Camara1;

    //보스 이동 관련
    public GameObject boss;
    BossEnemyMove BEM;

    //저장 불러오기 관련
    bool startSetting = false;
    public GameObject[] stage1Objects;
    public GameObject[] stage2Objects;
    SaveData s;
    Vector2 bossPos;
    public NPCInteraction NPC;


    //엔딩 관련
    public GameObject EndUI;
    public GameObject EndPanel;
    EndMenu end;
    bool returnToStart = false;

    // Start is called before the first frame update
    void Start()
    {
        tm = GameObject.FindObjectOfType<TalkManager>();
        player = GameObject.FindObjectOfType<PlayerMove>();
        start = startUI.GetComponent<MainMenu>();
        death = DeathUI.GetComponent<DeathMenu>();
        pause = PauseUI.GetComponent<PauseMenu>();
        BEM = GameObject.FindObjectOfType<BossEnemyMove>();
        end = EndPanel.GetComponent<EndMenu>();
        Camara1 = GameObject.FindObjectOfType<MainCamara>();

        inGameUI.SetActive(startSetting);
        talkPanel.SetActive(startSetting);
        startPanel.SetActive(startSetting);
        howToPlayPanel.SetActive(startSetting);
        DeathUI.SetActive(startSetting);
        PauseUI.SetActive(startSetting);
        pauseButtonPanel.SetActive(startSetting);
        howToPlayPanel_PauseUI.SetActive(startSetting);
        LoadingPanel.SetActive(startSetting);
        itemUI.SetActive(startSetting);

        isPauseOn = startSetting;
        heartCount = hearts.Length;
        hadTalked = startSetting;
        bossPos = boss.transform.position; 
        isTextOn = startSetting;
        returnToStart = startSetting;
        startCount = 0;
        isDeath = startSetting;

        Time.timeScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 사망
        if (player.currentHp == 0)
        {
            Time.timeScale = 0;
            BEM.speed = 0;
            isDeath = true;
            DeathUI.SetActive(true);

        }

        //###UI관련
        //Start UI;
        startPanel.SetActive(!start.isClickHowToPlay);
        howToPlayPanel.SetActive(start.isClickHowToPlay);
        if (start.isGameStart && startCount == 0)
        {
            GameSave();
            startUI.SetActive(false);
            inGameUI.SetActive(true);
            itemUI.SetActive(true);
            Time.timeScale = 1;
            startCount++;
        }
        //Death UI;
        if (death.isClickRestart)
        {
            switch (nowStage)
            {
                case 0:
                    Stage1Load();
                    break;
                case 1:
                    Stage2Load();
                    break;
                case 2:
                    Stage3Load();
                    break;
            }
            LoadingPanel.SetActive(true);
            Invoke("OutLoadingPanel", 1);
            isDeath = false;
            DeathUI.SetActive(false);
            death.isClickRestart = false;
        }
        else if (death.isQuit)
        {
            GameLoad(0);
            isDeath = false;
            startUI.SetActive(true);
            DeathUI.SetActive(false);
            death.isQuit = false;
            GameOver();
        }
        //Pause UI
        if (Input.GetKeyDown(KeyCode.Escape))   //esc 키 누를시
        {
            isPauseOn = true;
        }
        PauseUI.SetActive(isPauseOn);
        pauseButtonPanel.SetActive(!pause.isClickHowToPlay);
        howToPlayPanel_PauseUI.SetActive(pause.isClickHowToPlay);
        Time.timeScale = isPauseOn ? 0 : 1;
        if (pause.isClickResume)    //일시정지 풀기
        {
            isPauseOn = false;
            pause.isClickResume = false;
        }
        else if (pause.isQuit)
        {
            GameLoad(0);
            startUI.SetActive(true);
            isPauseOn = false;
            pause.isQuit = false;
            GameOver();
        }
        //item UI
        uniconCount.text = "" + unicornHorns + " / 10";
        potion.color = medicine > 0 ? new Color(potion.color.r, potion.color.g, potion.color.b, 1f) : new Color(potion.color.r, potion.color.g, potion.color.b, 0f);
        potionCount.text = medicine > 0 ? "" + medicine : "";
        doll.color = Doll > 0 ? new Color(doll.color.r, doll.color.g, doll.color.b, 1f) : new Color(doll.color.r, doll.color.g, doll.color.b, 0f);
        dollcount.text = Doll > 0 ? "" + Doll : "";
        playerHeart.sprite = player.currentHp > 0 ? hearts[player.currentHp - 1] : null;
        //Ending UI
        if (end.isClickExit)
        {
            GameLoad(0);
            startUI.SetActive(true);
            startPanel.SetActive(true);
            EndUI.SetActive(false);
            end.isClickExit = false;
        }
        //##


        //아이템 사용 관련
        if (Input.GetKeyDown(KeyCode.Alpha1) && medicine > 0 && player.currentHp < player.maxHp)
        {
            medicine--;
            player.currentHp++;
            //물약 사운드
            //audioSource_heal.Play();
        }
    }

    //스테이지 변화
    public void StageChange()
    {
        nowStage++;
        GameSave();
        LoadingPanel.SetActive(true);
        Invoke("OutLoadingPanel", 1);
    }

    //대화창 관련
    public void NPCText(GameObject scanObj)
    {
        scanObject = scanObj;
        InteractionCode objCode = scanObject.GetComponent<InteractionCode>();
        if (objCode.codeNumber == 10000 && unicornHorns > 2)
        {
            StageChange();
            return;
        }
        else if (objCode.codeNumber == 10001)
        {
            StageChange();
            Invoke("BossMoveStart", 3);
            return;
        }
        Talk(objCode.codeNumber, objCode.isNPC, scanObj);

        talkPanel.SetActive(isTextOn);
    }

    void Talk(int id, bool isNPC, GameObject scanObj)
    {
        string talkData = tm.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isTextOn = false;
            talkIndex = 0;
            if (id == 1000)
                hadTalked = true;
            if (!isNPC)
            {
                switch (id)
                {
                    case 100:
                        medicine++;
                        break;
                    case 101:
                        unicornHorns++;
                        break;
                    case 500:
                    case 501:
                        Doll++;
                        break;
                }
                scanObj.SetActive(false);
            }
            return;
        }
        talkText.text = talkData;
        isTextOn = true;
        talkIndex++;
    }

    public void GameOver()
    {
        inGameUI.SetActive(false);
        itemUI.SetActive(false);
    }

    public void GameSave()
    {
        print(player.currentHp);
        s.playerHealth = player.currentHp;
        s.unicornHorns = unicornHorns;
        s.medicine = medicine;
        s.Doll = Doll;
    }

    public void GameLoad(int loadCode)
    {
        //0번: 게임 재시작
        //1번: 1스테이지 불러오기
        //2번: 2스테이지 불러오기
        //3번: 3스테이지 불러오기
        switch (loadCode)
        {
            case 0:
                returnToStart = true;
                GameReset();
                break;
            case 1:
                Stage1Load();
                break;
            case 2:
                Stage2Load();
                break;
            case 3:
                Stage3Load();
                break;
        }
    }
    void GameReset()
    {
        Stage3Load();
        Stage2Load();
        Stage1Load();

        inGameUI.SetActive(startSetting);
        talkPanel.SetActive(startSetting);
        startPanel.SetActive(startSetting);
        howToPlayPanel.SetActive(startSetting);
        DeathUI.SetActive(startSetting);
        PauseUI.SetActive(startSetting);
        pauseButtonPanel.SetActive(startSetting);
        howToPlayPanel_PauseUI.SetActive(startSetting);
        LoadingPanel.SetActive(startSetting);
        itemUI.SetActive(startSetting);
        EndPanel.SetActive(startSetting);

        isPauseOn = startSetting;
        heartCount = hearts.Length;
        hadTalked = startSetting;
        bossPos = boss.transform.position;
        isTextOn = startSetting;
        returnToStart = startSetting;
        startCount = 0;
        isDeath = startSetting;

        Time.timeScale = 0;

        start.isGameStart = startSetting;
        start.isClickHowToPlay = startSetting;
        death.isClickRestart = startSetting;
        death.isQuit = startSetting;
        pause.isClickHowToPlay = startSetting;
        pause.isClickResume = startSetting;
        pause.isQuit = startSetting;
        player.textShowed = startSetting;
    }
    void Stage1Load() 
    {
        //Item, 플레이어 hp 설정
        player.currentHp = player.maxHp;
        unicornHorns = 0;
        Doll = 0;
        medicine = 0;

        //stage설정
        nowStage = 0;
        player.transform.position = player.startingPoint[nowStage];

        //Object(몬스터 아이템 등)
        for (int i = 0; i < stage1Objects.Length; i++)
        {
            stage1Objects[i].SetActive(true);
        }

        //NPC 코드 초기화
        NPC.resetCode();
    }
    void Stage2Load() 
    {
        //Item, 플레이어 hp 설정
        player.currentHp = s.playerHealth;
        unicornHorns = s.unicornHorns;
        Doll = s.Doll;
        medicine = s.medicine;

        //stage설정
        nowStage = 1;
        player.transform.position = player.startingPoint[nowStage];

        //Object(몬스터 아이템 등)
        for (int i = 0; i < stage2Objects.Length; i++)
        {
            stage2Objects[i].SetActive(true);
        }
    }
    void Stage3Load() 
    {
        //Item, 플레이어 hp 설정
        player.currentHp = s.playerHealth;
        unicornHorns = s.unicornHorns;
        Doll = s.Doll;
        medicine = s.medicine;

        //stage설정
        nowStage = 2;
        player.transform.position = player.startingPoint[nowStage];

        //보스 위치 설정
        boss.transform.position = bossPos;
        BEM.speed = 0;
        if(!returnToStart)
            Invoke("BossMoveStart", 3);

    }

    void OutLoadingPanel()
    {
        LoadingPanel.SetActive(false);
    }

    public void BossMoveStart()
    {
        BEM.speed = BEM.RunSpeed;
    }
}
