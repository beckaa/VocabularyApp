using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;
using UnityEngine.UI;
public class training : MonoBehaviour
{
    [Header("Management")]
    public TMP_Text foreign_language;
    public TMP_Text native_language;
    private string[] words;
    public TMP_Text question;
    public TMP_Text answer;
    private string solution;
    Word askedWord;
    public Toggle due_words;
    public Button Submit;

    [Header("warning message")]
    public GameObject warningMessage;

    //use Application.persistentDatapath for build
    private string path;
    // private string path="./Assets/Vocabs/";

    [Header("Panda Feedback UI")]
    public GameObject startPanda;
    public GameObject correctAnswer;
    public GameObject falseAnswer;
    public List<Sprite> happyPanda;
    public List<Sprite> sadPanda;
    void Start()
    {
        path = Application.persistentDataPath + "/";
        //path = "./Assets/Vocabs/";
        askWord(due_words);
    }

    private void Update()
    {
        if (UploadWord.newWordadded)
        {
            askWord(due_words);
            Submit.interactable = true;
            UploadWord.newWordadded = false;
            warningMessage.SetActive(false);
        }
       
    }

    int randomListIndex(List<Sprite> list)
    {
        return UnityEngine.Random.Range(0, list.Count);
    }
    //TODO: use different sprites e.g. random happy sprites and emotes
    private void resetPandaFeedback()
    {
        if (!startPanda.activeSelf)
        {
            
            startPanda.SetActive(true);
        }
        if (correctAnswer.activeSelf)
        {
            correctAnswer.SetActive(false);
        }
        if (falseAnswer.activeSelf)
        {
            falseAnswer.SetActive(false);
        }
    }
    public void askWord(Toggle due)
    {
        resetPandaFeedback();
        if (due.isOn)
        {
            words = loadDueWords();
        }
        else
        {
            words = loadAllWords();
        }
        if (words.Length > 0)
        {
            int currentIndex = randomIndex();
            askedWord = Word.stringToWord(words[currentIndex]);
            int language = UnityEngine.Random.Range(0, 2);
            if (language == 1)
            {
                question.text = "Translate: " + askedWord.translation;
                solution = askedWord.word;
            }
            else
            {
                question.text = "Translate: " + askedWord.word;
                solution = askedWord.translation;
            }
        }
        else
        {
            warningMessage.SetActive(true);
            Submit.interactable = false;
            question.text = "Uuups !? There are no words left.. ";
        }
            
        }

    public void checkWord()
    {
        //ignore casing?
        string ans = answer.text.ToLower();
        if (ans.Equals(solution.ToLower()))
        {
            int random = randomListIndex(happyPanda);
            correctAnswer.transform.GetChild(0).GetComponent<Image>().sprite = happyPanda[random];
            correctAnswer.SetActive(true);
            if (due_words.isOn)
            {
                UploadWord.editWordPhase(askedWord.word, askedWord.phase + 1, native_language, foreign_language);
            }
            else
            {
                //do not change the phase if not in the mode to check due_words
                UploadWord.editWordPhase(askedWord.word, askedWord.phase, native_language, foreign_language);
            }
            
            if (falseAnswer.activeSelf)
            {
                falseAnswer.SetActive(false);
            }
        }
        else
        {
            int random = randomListIndex(sadPanda);
            falseAnswer.transform.GetChild(0).GetComponent<Image>().sprite = sadPanda[random];
            falseAnswer.SetActive(true);
            if (due_words.isOn)
            {
                UploadWord.editWordPhase(askedWord.word, 0, native_language, foreign_language);
            }
            else
            {
                //do not change the phase if not in the mode to check due_words
                UploadWord.editWordPhase(askedWord.word, askedWord.phase, native_language, foreign_language);
            }
            
            if (correctAnswer.activeSelf)
            {
                correctAnswer.SetActive(false);
            }
        }
        if (startPanda.activeSelf)
        {
            startPanda.SetActive(false);
        }
        //Refresh only for editor
        //UnityEditor.AssetDatabase.Refresh();
        
    }

    int randomIndex()
    {
        return UnityEngine.Random.Range(0, words.Length - 1);
    }

    string[] loadAllWords()
    {
        string filename = native_language.text + foreign_language.text;
        if (File.Exists(path + filename + ".txt"))
        {
            return File.ReadAllLines(path + filename + ".txt");
        }
        else if (!File.Exists(filename))
        {
            filename = foreign_language.text + native_language.text;
            if (File.Exists(path + filename + ".txt"))
            {
                return words = File.ReadAllLines(path + filename + ".txt");
            }
            else
            {
                Debug.LogError("File does not exist!!");
            }
        }
        return null;

    }
    string[] loadDueWords()
    {
        string filename = native_language.text + foreign_language.text;
        if (File.Exists(path + filename + ".txt"))
        {
            return getDueWords();
        }
        else if (!File.Exists(filename))
        {
            filename = foreign_language.text + native_language.text;
            if (File.Exists(path + filename + ".txt"))
            {
                return getDueWords();
            }
            else
            {
                Debug.LogError("File does not exist!!");
            }
        }
        return null;

    }
    string[] getDueWords()
    {
        string filename = native_language.text + foreign_language.text;
        List<String> listOfWords = new List<String>();
        string[] data = File.ReadAllLines(path + filename + ".txt");
        foreach (string words in data)
        {
            Word w = Word.stringToWord(words);
            int compare = DateTime.Compare(DateTime.Today, w.dueTime);
            if (compare >= 0)
            {
                listOfWords.Add(w.toString());
            }
        }
        return listOfWords.ToArray();
    }
}
