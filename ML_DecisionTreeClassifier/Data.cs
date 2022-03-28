using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class Data    
    {
        public Data(string word, string attributeType, string answer)
        {
            //set word and attribute type
            this.word = word;
            this.attributeType = attributeType;

            //increase frequency type
            count++;

            //record the answer
            answers.Add(answer);

            //also record the number of answers
            if (!possibleAnswers.Contains(answer))
                possibleAnswers.Add(answer);

            //record the class value
            if(!possibleClassValues.Contains(word))
                possibleClassValues.Add(word);

        }

        public Data(double value, string attributeType, string answer)
        {
            //set value and attribute type
            this.continous = value;
            this.attributeType = attributeType;

            //increase frequency type
            count++;

            //record the answer
            answers.Add(answer);

            //also record the number of answers
            if(!possibleAnswers.Contains(answer))
                possibleAnswers.Add(answer);

            //record the class value
            if (!possibleClassValues.Contains(value.ToString()))
                possibleClassValues.Add(value.ToString());
        }

        public Data(int value, string attributeType, string answer)
        {
            //set value and attribute type
            this.integer = value;
            this.attributeType = attributeType;

            //increase frequency type
            count++;

            //record the answer
            answers.Add(answer);

            //also record the number of answers
            if (!possibleAnswers.Contains(answer))
                possibleAnswers.Add(answer);

        }

        //counter to be used to check frequency
        public int count = 0;

        //character to hold data type;
        public char dataType;

        //values to hold data
        public int integer;
        public double continous;
        public string word;
        public string attributeType;
       
        

        //List to hold values based on split
        public List<string> answers = new List<string>();
        public List<string> possibleAnswers = new List<string> ();

        //List to hold possible class values
        public List<string> possibleClassValues = new List<string>();
        
        public void insert(string answer)
        {
            //record the answer
            answers.Add(answer);

            //also record the number of answers
            if (!possibleAnswers.Contains(answer))
                possibleAnswers.Add(answer);
        }


        //test method to see if word exists in struct
        public bool doesContain(string word)
        {
            if (this.word == word)
                return true;
            else
            {
                return false;
            }
        }

        public bool doesContain(int value)
        {
            if (this.integer == value)
                return true;
            else
            {
                return false;
            }
        }
    }
}
