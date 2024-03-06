using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEditor;

public class Word
{
    public string word;
    public string translation;
    public int phase;
    public DateTime lastTrained;
    public DateTime dueTime;
    public Word(string word, string translation, int phase, DateTime lastTrained, DateTime dueTime)
    {
        this.word = word;
        this.translation = translation;
        this.phase = phase;
        this.lastTrained = lastTrained;
        this.dueTime = dueTime;
    }
    public string getWord()
    {
        return this.word;
    }
    public string getTranslation()
    {
        return this.translation;
    }
    public void setLastTrained(DateTime date)
    {
        this.lastTrained = date;
    }
    private void setDueTime(DateTime date)
    {
        this.dueTime = date;
    }
    public void calculateDueTime()
    {
        switch (this.phase)
        {
            case 0:
                setDueTime(this.lastTrained);
                break;
            case 1:
                setDueTime(this.lastTrained.AddDays(2));
                    break;
            case 2:
                setDueTime(this.lastTrained.AddDays(5));
                break;
            case 3:
                setDueTime(this.lastTrained.AddDays(7));
                break;
            case 4:
                setDueTime(this.lastTrained.AddDays(14));
                break;
            case 5:
                setDueTime(this.lastTrained.AddDays(21));
                break;
            case 6:
                setDueTime(this.lastTrained.AddDays(31));
                break;

        }
    }
    public DateTime getDueTime()
    {
        return this.dueTime;
    }
    public void setPhase(int phase)
    {
        this.phase = phase;
    }
    public int getPhase()
    {
        return this.phase;
    }
    public string toString()
    {
        return this.word+";"+this.translation + ";" + this.phase + ";"+this.lastTrained+";"+this.dueTime;
    }
    public static Word stringToWord(string s)
    {
        string[] temp = s.Split(";");
        return new Word(temp[0], temp[1], int.Parse(temp[2]),DateTime.Parse(temp[3]),DateTime.Parse(temp[4]));
    }
}
public class UploadWord : MonoBehaviour
{
    public TMP_Text native_Language;
    public TMP_Text foreign_language;
    public TMP_Text word;
    public TMP_Text translated_word;

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
        string[] data = File.ReadAllLines(path1+filename+".txt");
        //search for entry
        foreach(string word in data)
        {
            if (word.Contains(askedWord))
            {
                break;
            }
            index++;
        }
        Word editWord = Word.stringToWord(data[index]);
        editWord.setPhase(phase);
        editWord.calculateDueTime();
        data[index] = editWord.toString();
        File.WriteAllLines(path1 + filename + ".txt", data);
    }
    public void addWord()
    {
        Word newWord = new Word(word.text, translated_word.text, 0, DateTime.Today, DateTime.Today);
        string filename = string.Concat(native_Language.text, foreign_language.text);
        if (fileExists(filename))
        {
            StreamWriter sw = File.AppendText(path + filename + ".txt");
            sw.WriteLine(newWord.toString());
            sw.Close();
        }
        else
        {
            FileStream fs = File.Create(path + filename + ".txt");
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(newWord.toString());
            sw.Close();
            fs.Close();
        }
        //Refresh only for editor
        //UnityEditor.AssetDatabase.Refresh();

    }

    bool fileExists(string filename)
    {
        return File.Exists(path+filename+".txt");
    }
}
