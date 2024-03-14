using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System;

public class profileManager : MonoBehaviour
{
    public TMP_Text headingPlayername;
    public TMP_Text pandaTalk;
    [Header("Overview Statistic")]
    public RectTransform learnedWords;
    public RectTransform todoWords;
    public RectTransform graphContainer;
    [Header("Word Phases Statistic")]
    public RectTransform[] phases;

    // Start is called before the first frame update
    void Start()
    {
        headingPlayername.text = UIManager.player.getName();
        pandaTalk.text = "Welcome " +UIManager.player.getName()+ "! I am happy to see you here :)";
        PlayerPrefs.SetFloat("dueWords", 0);
        getAllDueWords();
        //get count of words in phases
        for(int i=0; i < 7; i++)
        {
            PlayerPrefs.SetFloat("phase" + i.ToString(), 0);
            getWordCountPhase(i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            updateOverview();
            updatePhases();
        }
        //PlayerPrefs.DeleteAll();
    }
    void getAllDueWords()
    {
        string path = Application.persistentDataPath + "/";
        string[] files=Directory.GetFiles(path, "*.txt");
        for(int i =0; i< files.Length; i++)
        {
            string[] data=File.ReadAllLines(files[i]);
            foreach(string datainfo in data)
            {
                Word w = Word.stringToWord(datainfo);
                if (DateTime.Compare(w.lastTrained, w.dueTime)>=0)
                {
                    PlayerPrefs.SetFloat("dueWords", PlayerPrefs.GetFloat("dueWords",0) + 1);
                }
            }
        }
        
    }
    void getWordCountPhase(int phase)
    {
        string path = Application.persistentDataPath + "/";
        string[] files = Directory.GetFiles(path, "*.txt");
        for (int i = 0; i < files.Length; i++)
        {
            string[] data = File.ReadAllLines(files[i]);
            foreach (string datainfo in data)
            {
                Word w = Word.stringToWord(datainfo);
                if (w.phase == phase)
                {
                    PlayerPrefs.SetFloat("phase"+phase.ToString(), PlayerPrefs.GetFloat("phase"+phase.ToString(), 0) + 1);
                }
            }
        }
    }

    void resizePhase(int phase)
    {
        float maxHeight = Mathf.Abs(graphContainer.rect.y);
        float phaseCount = PlayerPrefs.GetFloat("phase" + phase.ToString());
        float phaseHeight = 0f;

        if(phaseCount*50 > maxHeight)
        {
            phaseHeight = (phaseCount * 50) / (maxHeight / 100);
        }
        else
        {
            phaseHeight = phaseCount * 50;
        }
        phases[phase].sizeDelta = new Vector2(phases[phase].sizeDelta.x, phaseHeight);
    }
    void updatePhases()
    {
        for(int i =0; i< phases.Length; i++)
        {
            resizePhase(i);
        }
    }
    void updateOverview()
    {
        //change the height according to words that exist => relativer anteil von maxHeight graphContainter
        float maxHeight = Mathf.Abs(graphContainer.rect.y);
        float countWords = PlayerPrefs.GetFloat("wordCount");
        float dueWords = PlayerPrefs.GetFloat("dueWords");
        float heightLearnedWords = 0f; 
        float heightDueWords = 0f;
        if((countWords-dueWords)*50 > maxHeight || dueWords*50 > maxHeight)
        {
            heightLearnedWords = ((countWords - dueWords) * 50) / (maxHeight/100);
            heightDueWords = (dueWords * 50) / (maxHeight/100);
        }
        else
        {
            heightLearnedWords = (countWords - dueWords) * 50;
            heightDueWords = dueWords * 50;
        }
        learnedWords.sizeDelta = new Vector2(learnedWords.sizeDelta.x, heightLearnedWords);
        todoWords.sizeDelta = new Vector2(todoWords.sizeDelta.x, heightDueWords);
    }
    
}
