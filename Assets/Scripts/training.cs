using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
public class training : MonoBehaviour
{
    public TMP_Text foreign_language;
    public TMP_Text native_language;
    private FileStream fs;
    private string[] words;
    public TMP_Text question;
    public TMP_Text answer;
    private string solution;

    //use Application.persistentDatapath for build
    private string path;
    // private string path="./Assets/Vocabs/";

    public GameObject startPanda;
    public GameObject correctAnswer;
    public GameObject falseAnswer;
    void Start()
    {
        path = Application.persistentDataPath + "/";
        askWord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void askWord()
    {
        if (!startPanda.active)
        {
            startPanda.SetActive(true);
        }
        if (correctAnswer.active)
        {
            correctAnswer.SetActive(false);
        }
        if (falseAnswer.active)
        {
            falseAnswer.SetActive(false);
        }
        words= loadWords();
        int index = randomIndex();
        string word = words[index];
        string[] split = word.Split(";");
        int language=Random.Range(0, 1);
        if(language == 1)
        {
            question.text = "Translate: " + split[1];
            solution = split[0];
        }
        else
        {
            question.text = "Translate: " + split[0];
            solution = split[1];
        }
        

    }
    
    public void checkWord()
    {
        //ignore casing?
        string ans = answer.text.ToLower();
        if (ans.Equals(solution.ToLower()))
        {
            correctAnswer.SetActive(true);
            if (falseAnswer.active)
            {
                falseAnswer.SetActive(false);    
            }
        }
        else
        {
            falseAnswer.SetActive(true);
            if (correctAnswer.active)
            {
                correctAnswer.SetActive(false);
            }
        }
        if (startPanda.active)
        {
            startPanda.SetActive(false);
        }
        //use different sprites e.g. random happy sprites
    }

    int randomIndex()
    {
        return Random.Range(0, words.Length-1);
    }

    string[] loadWords()
    {
        string filename = native_language.text + foreign_language.text;
        Debug.Log("first "+filename);
        if (File.Exists(path+filename+".txt"))
        {
            return File.ReadAllLines(path + filename + ".txt");
        }else if (!File.Exists(filename))
        {
            filename = foreign_language.text+native_language.text;
            Debug.Log("second: "+filename);
            if (File.Exists(path+filename+".txt"))
            {
               return words = File.ReadAllLines(path+ filename + ".txt");
            }
            else
            {
                Debug.LogError("File does not exist!!");
            }
        }
        return null;
        
    }
}
