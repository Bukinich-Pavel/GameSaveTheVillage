using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ImageTimer HarvestTimer; //таймер производства пшеницы
    public ImageTimer EatingTimer; //таймер употребления пшеницы
    public Text resourseText; //вывод кол. ресурсов
    public Image PeasantTimeImg; 
    public Image WarriorTimeImg;
    public Image RaidTimeImg;
    public GameObject GameOverScreen;
    public GameObject WinScreen;

    public AudioSource Click;
    public AudioSource Craft;
    public AudioSource Omnom;
    public AudioSource Mech;
    public AudioSource GaveOverAudio;
    public AudioSource WinGameAudio;

    public Button peasantButton; //кнопка создания крестьянина
    public Button warriorButton; //кнопка создания воин

    public int wheatPerPeasent; //приносит пшен. 1 крестьянин
    public int wheatToWarriors; //съедает пшен. 1 воин

    public int peasantCount; //кол. крестьян
    public int warriorCount; //кол. воинов
    public int wheatCount; //кол. пшен.

    public int peasantCost; //стоим. за 1 крестьянина
    public int warriorCost; //стоим. за 1 воина

    public float peasantCreateTime;
    public float warriorCreateTime;
    public float raidMaxTime;
    public int raidIncrease;
    public int nextRaid;
    public int countEmptyRaid; //кол. ходов до набегов
    private int countWinRaid = -2; //количество прожитых набегов
    private int allRaid = 0;

    public float peasantTimer = -2; //таймер создания крестьянина
    private float warriorTimer = -2; //таймер создания воина
    private float raidTimer;

    public Text sum;

    void Start()
    {
        UpdateText();
        raidTimer = raidMaxTime;
        RaidTimeImg.fillAmount = raidTimer / raidMaxTime;
    }

    void Update()
    {
        raidTimer -= Time.deltaTime;
        RaidTimeImg.fillAmount = raidTimer / raidMaxTime;

        if (raidTimer <= 0 && allRaid >= countEmptyRaid)
        {
            Mech.Play();
            raidTimer = raidMaxTime;
            warriorCount -= nextRaid;
            nextRaid += raidIncrease;
            countWinRaid++;
        }
        else if(raidTimer <= 0)
        {
            raidTimer = raidMaxTime;
            allRaid++;
        }

        if (HarvestTimer.Tick)
        {
            Craft.Play();
            wheatCount += peasantCount * wheatPerPeasent;
        }

        if (EatingTimer.Tick)
        {
            Omnom.Play();
            wheatCount -= warriorCount * wheatToWarriors;
        }


        if (peasantTimer > 0)
        {
            peasantTimer -= Time.deltaTime;
            PeasantTimeImg.fillAmount = peasantTimer / peasantCreateTime;
        }
        else if (peasantTimer > -1)
        {
            PeasantTimeImg.fillAmount = 0;
            peasantButton.interactable = true;
            peasantCount += 1;
            peasantTimer = -2;
        }


        if (warriorTimer > 0)
        {
            warriorTimer -= Time.deltaTime;
            WarriorTimeImg.fillAmount = warriorTimer / warriorCreateTime;
        }
        else if (warriorTimer > -1)
        {
            WarriorTimeImg.fillAmount = 0;
            warriorButton.interactable = true;
            warriorCount += 1;
            warriorTimer = -2;
        }

        if (peasantTimer == -2 && wheatCount > 0)
        {
            peasantButton.interactable = true;
        }
        else
        {
            peasantButton.interactable = false;
        }

        if (warriorTimer == -2 && wheatCount > 0)
        {
            warriorButton.interactable = true;
        }
        else
        {
            warriorButton.interactable = false;
        }

        UpdateText();

        if (warriorCount < 0)
        {
            GaveOverAudio.Play();
            warriorCount = 0; 
            sum.text = $"Прожито набегов: {countWinRaid}";
            GameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }

        if (wheatCount >= 500 || peasantCount >= 50)
        {
            wheatCount = 0; peasantCount = 3;
            WinGameAudio.Play();
            Time.timeScale = 0;
            WinScreen.SetActive(true);
        }


    }


    public void CreatePeasant()
    {
        Click.Play();
        wheatCount -= peasantCost;
        peasantTimer = peasantCreateTime;
        peasantButton.interactable = false;
    }

    public void CreateWarrior()
    {
        Click.Play();
        wheatCount -= warriorCost;
        warriorTimer = warriorCreateTime;
        warriorButton.interactable = false;
    }

    private void UpdateText()
    {
        resourseText.text = peasantCount + "\n" + warriorCount + "\n\n" + wheatCount + "\n\n\n" + nextRaid;
    }
}
