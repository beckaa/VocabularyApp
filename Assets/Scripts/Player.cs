using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    string name;
    int score;
    int level;
    int wordsTrained;
    int maxLevel = 10;
    public void upgradeLevel()
    {
        if(this.score > this.level * 100 && this.level<maxLevel)
        {
            level++;
            PlayerPrefs.SetInt("level", this.level);
        }
    }
    public void initPlayer()
    {
        if (PlayerPrefs.HasKey("playername"))
        {
            this.name = PlayerPrefs.GetString("playername");
        }
        if (PlayerPrefs.HasKey("score"))
        {
            this.score= PlayerPrefs.GetInt("score");
        }
        else
        {
            PlayerPrefs.SetInt("score", 0);
            this.score = 0;
        }
        if (PlayerPrefs.HasKey("level"))
        {
            this.level=PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", 0);
            this.level = 0;
        }
        if (PlayerPrefs.HasKey("wordsTrained"))
        {
            this.wordsTrained=PlayerPrefs.GetInt("wordsTrained");
        }
        else
        {
            PlayerPrefs.SetInt("wordsTrained", 0);
            this.wordsTrained = 0;
        }
    }
   public void increaseWordsTrained()
    {
        this.wordsTrained++;
    }
    public int getWordsTrained()
    {
        return this.wordsTrained;
    }
    public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        PlayerPrefs.SetString("playername", name);
        this.name = name;
    }

    public void increaseScore(int number)
    {
        this.score += number;
        PlayerPrefs.SetInt("score", this.score);
    }
    public void decreaseScore(int number)
    {
        if(this.score - number > 0)
        {
            this.score -= number;
        }
        else
        {
            this.score = 0;
        }
        PlayerPrefs.SetInt("score", this.score);
        
    }

    public int getScore()
    {
        return this.score;
    }

    public int getLevel()
    {
        return this.level;
    }
}
