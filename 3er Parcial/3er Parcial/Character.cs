﻿using System;
using System.Windows.Media;

using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3er_Parcial
{
    class Character
    {
        private string name;
        private string gender;
        private List<string> houses;
        private BitmapImage houseImage;
        private TextInfo Formatter = new CultureInfo("en-US", false).TextInfo;
        private List<string> siblings;
        private string kingOfHouse = null;

        public override string ToString()
        {
            return FormattedName;
        }

        public string Name {
            get {
                return name;
            }
        }
        public string FormattedName {
            get {
                string tempName = "";
                tempName = name.Replace('_', ' ');
                tempName = Formatter.ToTitleCase(tempName).Trim();
                return tempName;
            }
        }
        
        public string Gender {
            get {
                return Formatter.ToTitleCase(gender);
            }
        }
        public string Houses {
            get {
                if (houses == null){
                    string _mainHouse = MainHouse;
                }
                string _houses = "";
                foreach (string house in houses)
                    _houses+=(Formatter.ToTitleCase(house))+" / ";
                return _houses;
            } 
        }
        public string MainHouse{
            get {
                string returnValue = "";
                if (houses == null) {
                    houses = new List<string>();
                    PrologResult result = PrologHandler.Instance.Query("belongsHouses(House," + name + ").");
                    if (result.Status == Prolog.ExecutionResults.Success)
                    {
                        foreach (Dictionary<string, string> _house in result.Vars)
                        {
                            houses.Add(_house["House"]);
                        }
                    }
                    else {
                        houses.Add("none");
                    }
                }

                switch (gender)
                {
                    case "man":
                        returnValue = houses[0];
                        break;
                    case "woman":
                        if (houses.Count > 1) //Married woman.
                            returnValue = houses[1];
                        else
                            returnValue = houses[0];
                        break;
                    default:
                        return "none";
                }
                return returnValue;
            }
        }

        public BitmapImage HouseImage {
            get {
                if (houseImage == null)
                {
                    Uri uri = new Uri("pack://application:,,,/Images/"+MainHouse+".png");
                    houseImage = new BitmapImage(uri);
                }
                return houseImage;
            }

        }

        public SolidColorBrush GenderColor {
            get {
                switch(gender){
                    case "man":
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#2980b9")); //BELIZE HOLE (Blue)
                    case "woman":
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#8e44ad")); //WISTERIA (Purple)
                    default:
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#2c3e50")); //MIDNIGHT BLUE (Black)
                }
            }
        }

        public string KingOfHouse {
            get {
                if (kingOfHouse == null) {
                    string query = String.Format("king(Kingdom,{0}).", name);
                    PrologResult data = PrologHandler.Instance.Query(query);
                    if (data.Status == Prolog.ExecutionResults.Success)
                    {
                        kingOfHouse = data.Vars[0]["Kingdom"];
                    }
                    else
                        kingOfHouse = "None";
                }

                return kingOfHouse;
            }
        }

        public BitmapImage IsKing {
            get {
                if (KingOfHouse == "None")
                {
                    return null;
                }
                else
                {
                    Uri uri = new Uri("pack://application:,,,/Images/crown.png");
                    return new BitmapImage(uri);
                }
            }
        }

        public List<string> Siblings {
            get{
                if (siblings == null) {
                    string query = String.Format("siblings({0},Sib).", name);
                    PrologResult data = PrologHandler.Instance.Query(query);
                    siblings = new List<string>();
                    if (data.Status == Prolog.ExecutionResults.Success) {
                        //We're sure it HAS to return only 1 Variable result.
                        string unparsedData = data.Vars[0]["Sib"];
                        unparsedData = unparsedData.Replace("[", "");
                        unparsedData = unparsedData.Replace("]", "");
                        siblings = unparsedData.Split(',').ToList();
                    }

                }

                return siblings;
            }
        }

        public List<string> FormattedSiblings {
            get {
                List<string> fSiblings = new List<string>();
                List<string> tempSiblings = Siblings;
                for(int i = 0; i < tempSiblings.Count; i++)
                   fSiblings.Add(siblings[i] = Formatter.ToTitleCase(tempSiblings[i].Replace("_", " ")));

                return fSiblings;
            }
        }

        public Character(string name, string gender) {
            this.name = name;
            this.gender = gender;
        }
    }
}
