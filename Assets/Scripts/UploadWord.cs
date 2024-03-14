using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEditor;


public class UploadWord : MonoBehaviour
{
    public TMP_Text native_Language;
    public TMP_Text foreign_language;
    public TMP_Text word;
    public TMP_Text translated_word;
    public static bool newWordadded = false;
    public TMP_Text submitMessage;

    //use Application.persistentDatapath for build
    private string path;
    //private static string path="./Assets/Vocabs/";
    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/";
        //path="./Assets/Vocabs/";
    }

    public static void editWordPhase(string askedWord, int phase, TMP_Text native, TMP_Text foreign)
    {
        string path1 = Application.persistentDataPath + "/";
        int index = 0;
        string filename = string.Concat(native.text, foreign.text);
        string[] data = File.ReadAllLines(path1 + filename + ".txt");
        //search for entry
        foreach (string word in data)
        {
            if (word.Contains(askedWord))
            {
                break;
            }
            index++;
        }
        Word editWord = Word.stringToWord(data[index]);
        editWord.setPhase(phase);
        editWord.setLastTrained(DateTime.Today);
        editWord.calculateDueTime();
        data[index] = editWord.toString();
        File.WriteAllLines(path1 + filename + ".txt", data);
    }

    bool wordExists(string filename)
    {
        string[] entries = File.ReadAllLines(path + filename + ".txt");
        foreach (string str in entries)
        {
            if (str.ToLower().Contains(word.text.ToLower()) || str.ToLower().Contains(translated_word.text.ToLower()))
            {
                return true;
            }
        }
        return false;
    }
    public void addWord()
    {
        string filename = string.Concat(native_Language.text, foreign_language.text);
        Word newWord = new Word(word.text, translated_word.text, 0, DateTime.Today, DateTime.Today);

        if (fileExists(filename))
        {
            if (!wordExists(filename))
            {
                UIManager.player.increaseScore(2);
                UIManager.player.upgradeLevel();
                StreamWriter sw = File.AppendText(path + filename + ".txt");
                sw.WriteLine(newWord.toString());
                sw.Close();
            }
        }
        else
        {
            FileStream fs = File.Create(path + filename + ".txt");
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(newWord.toString());
            sw.Close();
            fs.Close();
        }
        newWordadded = true;
        PlayerPrefs.SetFloat("wordCount", PlayerPrefs.GetFloat("wordCount", 0) + 1);
        PlayerPrefs.SetFloat("dueWords", PlayerPrefs.GetFloat("dueWords", 0) + 1);
        PlayerPrefs.SetFloat("phase0", PlayerPrefs.GetFloat("phase0", 0) + 1);
        //Refresh only for editor
        //UnityEditor.AssetDatabase.Refresh();

    }
    public void submitPressed()
    {
        if (newWordadded)
        {
            submitMessage.text = "The word was added!";
        }
        else
        {
            submitMessage.text = "Word already exists!";
        }
    }
    bool fileExists(string filename)
    {
        return File.Exists(path + filename + ".txt");
    }
}
