using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCI_2910_Lab_2_Isaac_Ross
{
    internal class Pokemon : IComparable<Pokemon>
    /*
     * 
     * Some code used from Willam Rochelle's Review Session
     * 
     * 
     */
    {
        public string PokedexNumber { get; set; }
        public string Name { get; set; }
        public string TypeOne { get; set; } = string.Empty;
        public string TypeTwo { get; set; } = string.Empty;
        public int TotalStats { get; set; }
        public int HitPoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialDefense { get; set; }
        public int SpecialAttack { get; set; }
        public int Speed { get; set; }
        public string Generation { get; set; }

        public Pokemon() { }

        public Pokemon(string pokedexNumber, string name, string typeOne, string typeTwo, string generation)
        {
            PokedexNumber = pokedexNumber;
            Name = name;
            TypeOne = typeOne;
            TypeTwo = typeTwo;
            Generation = generation;
        }

        public Pokemon(string pokedexNumber, string name, string typeOne, string typeTwo, int totalStats, int hitPoints, int attack, int defense, int specialDefense, int specialAttack, int speed, string generation)
        {
            PokedexNumber = pokedexNumber;
            Name = name;
            TypeOne = typeOne;
            TypeTwo = typeTwo;
            TotalStats = totalStats;
            HitPoints = hitPoints;
            Attack = attack;
            Defense = defense;
            SpecialDefense = specialDefense;
            SpecialAttack = specialAttack;
            Speed = speed;
            Generation = generation;
        }

        public int CompareTo(Pokemon? other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            string displayString = "";
            displayString += $"Pokedex Entry #{PokedexNumber}: {Name}\n";
            displayString += $"Type(s): {TypeOne}{(TypeTwo.Equals(" ") ? "" : ("/" + TypeTwo))}\n";
            displayString += $"Generation: {Generation}\n\n";

            return displayString;

        }
    }
}
