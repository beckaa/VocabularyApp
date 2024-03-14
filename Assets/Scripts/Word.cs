using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
                    setDueTime(this.lastTrained.AddDays(30));
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
            return this.word + ";" + this.translation + ";" + this.phase + ";" + this.lastTrained + ";" + this.dueTime;
        }
        public static Word stringToWord(string s)
        {
            string[] temp = s.Split(";");
            return new Word(temp[0], temp[1], int.Parse(temp[2]), DateTime.Parse(temp[3]), DateTime.Parse(temp[4]));
        }
}
