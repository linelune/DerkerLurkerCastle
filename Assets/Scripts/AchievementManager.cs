using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Achievement manager https://youtu.be/8cFCyIP2LYM
public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements;
    public TextMeshProUGUI achievementText;
    private bool gameStart;
    private bool derkerKill;
    private bool knightKill;

    private void Start()
    {
        InitializeAchievements();
        gameStart = derkerKill = knightKill = false;
        DontDestroyOnLoad(gameObject);
        achievementText.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckAchievementCompletion();
    }

    private void CheckAchievementCompletion()
    {
        if (achievements == null) return;

        foreach(var achievement in achievements)
        {
            achievement.UpdateCompletion();
        }
    }

    private void InitializeAchievements()
    {
        if (achievements != null) return;

        achievements = new List<Achievement>();
        achievements.Add(new Achievement("Start the Game", "You started the game", (object o)=>gameStart == true));
        achievements.Add(new Achievement("Tis' But a Scratch!", "You killed the Knight!", (object o) => knightKill));
        achievements.Add(new Achievement("Derker's Dead", "You defeated Derker Lurker.", (object o) => derkerKill));
    }

    private void KnightKilled()
    {
        knightKill = true;
    }

    private void DerkerKilled()
    {
        derkerKill = true;
    }

    private void gameStarted()
    {
        gameStart = true;
    }

    private void DisplayAchievement(string title, string description)
    {
        StartCoroutine(AchievementTimer(title, description));
    }

    private void OnEnable()
    {
        ExitLightScript.OnGameStart += gameStarted;
        Knight.OnDeath += KnightKilled;
        BossDerker.OnDeath += DerkerKilled;
        Achievement.OnGain += DisplayAchievement;
    }

    private void OnDisable()
    {
        ExitLightScript.OnGameStart -= gameStarted;
        Knight.OnDeath -= KnightKilled;
        BossDerker.OnDeath -= DerkerKilled;
        Achievement.OnGain -= DisplayAchievement;
    }

    IEnumerator AchievementTimer(string title, string description)
    {
        achievementText.gameObject.SetActive(true);
        achievementText.text = "Achievement got:\n" + title + "\n" + description;
        yield return new WaitForSeconds(3);
        achievementText.gameObject.SetActive(false);
    }
}

public class Achievement
{
    public Achievement(string title, string description, Predicate<object> requirement)
    {
        this.title = title;
        this.description = description;
        this.requirement = requirement;
    }

    public string title;
    public string description;
    public Predicate<object> requirement;
    public bool achieved;
    public delegate void Gain(string title, string description);
    public static event Gain OnGain;

    public void UpdateCompletion()
    {
        if (achieved)
            return;

        if (RequirementsMet())
        {
            Debug.Log("Achievement got: '" + title + " " + description + "'");
            OnGain?.Invoke(title, description);
            achieved = true;
        }
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }
}
