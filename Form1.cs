using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DogFoodApp
{

    public partial class Form1 : Form
    {
        //this initialization is made to shortcut the textbox.text usage. 
        //when the values are errorchecked they will get values from the textboxes,
        //so that I don't need to refer to the textboxes all the time and it's simpler
        //I should have done the naming of textboxes as I was creating those, but didn't. this is 
        //work-around so that my code is simpler to read
        string name; 
        string breed;
        int age;
        string dogColor;
        float dogWeight;

        //Storing dogs into a list
        public List<Dog> dogList = new List<Dog>();

        public Form1()
        {
           
            InitializeComponent(); //GUI initialization
            CheckIfValuesArCorrect(); //Check if default values in textboxes are usable
       
        }
       
        private bool CheckIfValuesArCorrect()  //error handling
        {
            try
            {
                name = textBox1.Text;
                breed = textBox2.Text;
                age = Int32.Parse(textBox3.Text);
                dogColor = textBox5.Text;
                dogWeight = float.Parse(textBox4.Text);
            }
            catch
            {
                textBox6.Text = "Error in values entered";
                return false; 
            }
            return true;
            //returning true or false is not needed at the moment, but might be useful in the future
            //I thought when I wrote this that I would need it, but didn't eventually

        }

        private float[] CalculateAmountOfFood(float weight)
        {
            //Created an array for minimun and maximum values
            float[] amountOfFood = { 0.3f, 1.0f }; 
            //Value handling to return right amount of food
            if (5.6 > weight && weight <= 9.0)
            {
                amountOfFood[0] = 1.0f;
                amountOfFood[1] = 1.3f;
            }

            else if (9.1 > weight && weight <= 15.9)
            {
                amountOfFood[0] = 1.3f;
                amountOfFood[1] = 2.0f;
            }
            else if (16 > weight && weight <= 22.5)
            {
                amountOfFood[0] = 2.0f;
                amountOfFood[1] = 2.6f;
            }
            else if (22.6 > weight && weight <= 34.0)
            {
                amountOfFood[0] = 2.6f;
                amountOfFood[1] = 3.3f;
            }
            else if (34.1 > weight && weight <= 45.0)
            {
                amountOfFood[0] = 3.3f;
                amountOfFood[1] = 4.25f;
            }
            else if (weight > 45)
            {   //returns the minimun amount of food
                amountOfFood[0] = CalculateTheHeaviestDogs(weight); 
                //adds 1 cup to the minimum amount to have a maximum value
                //I have no scientific reason to add 1 cup, but I suppose it's not too much.
                //basis for this thinking is that 34,1 to 45 tolerance is already almost a full cup
                amountOfFood[1] = amountOfFood[0] + 1.0f; 
            }
            return amountOfFood; //return an array of minimun and maximum amount of food
        }

        private float CalculateTheHeaviestDogs(float weight) 
        //If dog is heavier than 45 kg, then calculate right amount fo food
        //and return the minimun amount of food
        {
            float amountOfFood = 4.25f;
            float weightToCount = weight - 45.0f;
            float quarterCupsToAdd = weightToCount / 4.5f;
            amountOfFood += quarterCupsToAdd * 0.25f;
            return amountOfFood;
        }

        //Button CALCULATE
        private void button1_Click(object sender, EventArgs e) 
        {
            CheckIfValuesArCorrect();
            float[] amountOfFood; //Create array for minimum and maximum amount of food
            Dog myDog = new Dog(name,breed,age,dogColor, dogWeight); //Instantiate a new dog object
            //MYDOG
            textBox6.Text = ("your dog " + myDog.GetName() + " which is breed " + myDog.GetBreed() + " is " + myDog.GetAge() + " old and it's "
                            + myDog.GetColor() + " and weighing " + myDog.GetWeight() + " kg.");
            //Calculate needed food
            amountOfFood = CalculateAmountOfFood(myDog.GetWeight()); 
            //RESULTS
            textBox7.Text = " Food needed is something between " + amountOfFood[0].ToString("0.00") + " and " + amountOfFood[1].ToString("0.00") + " kg.";
            listBox1.Items.Add(myDog.GetName());
            dogList.Add(myDog);
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBoxList = (ListBox)sender; //identifying object sender as listbox
            
            foreach (Dog dogName in dogList) //Going through list of dogs to identify which dog has the same name as in the listbox
            {
                string dog = dogName.GetName();                
                for (int i=1;i<listBoxList.Items.Count; i++)
                {                    
                    if (dog == listBoxList.SelectedItem.ToString())
                    {
                        textBox1.Text = dogName.GetName();
                        textBox2.Text = dogName.GetBreed();
                        textBox3.Text = dogName.GetAge().ToString();
                        textBox5.Text = dogName.GetColor();
                        textBox4.Text = dogName.GetWeight().ToString();
                    }
                }
            
               
            }
        }
    }

    public class Dog
    {
        private string name;
        private string breed;
        private int age;
        private string color;
        private float weight;
       

        public Dog(string name, string breed, int age, string color, float weight)
        {
            this.name = name;
            this.breed = breed;
            this.age = age;
            this.color = color;
            this.weight = weight;
        }

        public string GetName()
        {
            return name;
        }

        public string GetBreed()
        {
            return breed;
        }

        public int GetAge()
        {
            return age;
        }

        public string GetColor()
        {
            return color;
        }

        public float GetWeight()
        {
            return weight;
        }
              
    }
}
