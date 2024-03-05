using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class UploadWord : MonoBehaviour
{
    public TMP_Text native_Language;
    public TMP_Text foreign_language;
    public TMP_Text word;
    public TMP_Text translated_word;

    //use Application.persistentDatapath for build
    private string path;
    // private string path="./Assets/Vocabs/";
    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/";
    }

    public void addWord()
    {
        string filename = string.Concat(native_Language.text, foreign_language.text);
        if (fileExists(filename))
        {
            StreamWriter sw = File.AppendText(path + filename + ".txt");
            //StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(word.text + ";" + translated_word.text);
            sw.Close();
            //fs.Close();
        }
        else
        {
            FileStream fs = File.Create(path + filename + ".txt");
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(word.text + ";" + translated_word.text);
            sw.Close();
            fs.Close();
        }
        //Refresh only for editor
        //UnityEditor.AssetDatabase.Refresh();

    }
    public void uploadFile()
    {

    }
    public void readWords()
    {
        string filename = string.Concat(native_Language.text, foreign_language.text);
        string[] words = File.ReadAllLines(filename);

        //suche das zu prüfende Wort
        // überprüfe ob das Wort stimmt
    }
    bool fileExists(string filename)
    {
        return File.Exists(path+filename+".txt");
    }
}
