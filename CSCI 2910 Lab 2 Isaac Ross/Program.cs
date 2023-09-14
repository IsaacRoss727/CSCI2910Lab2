using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;

namespace CSCI_2910_Lab_2_Isaac_Ross
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            string dataPath = projectFolder + Path.DirectorySeparatorChar + "AllPokemon.csv";

            List<Pokemon> PokemonList = new List<Pokemon>();
            List<Pokemon> SmashPokemon = new List<Pokemon>();
            List<Pokemon> PassPokemon = new List<Pokemon>();

            using (StreamReader reader = new StreamReader(dataPath))
            {
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    string[] lineElements = line.Split(',');


                    //Constructs the Pokemon Object
                    Pokemon p = new Pokemon()
                    {
                        PokedexNumber = lineElements[0],
                        Name = lineElements[1],
                        TypeOne = lineElements[2],
                        TypeTwo = lineElements[3],
                        TotalStats = int.Parse(lineElements[4]),
                        HitPoints = int.Parse(lineElements[5]),
                        Attack = int.Parse(lineElements[6]),
                        Defense = int.Parse(lineElements[7]),
                        SpecialDefense = int.Parse(lineElements[8]),
                        SpecialAttack = int.Parse(lineElements[9]),
                        Speed = int.Parse(lineElements[10]),
                        Generation = lineElements[11],
                    };

                    PokemonList.Add(p);

                }
                PokemonList.Sort();

                //Makes a list that the user can filter
                List<Pokemon> filteredPokemonList = PokemonList.OrderBy(p => int.Parse(p.PokedexNumber)).ToList();

                Console.WriteLine("Would you like to filter Pokemon? (Any other number defaults to No)");
                Console.WriteLine("1. No");
                Console.WriteLine("2. Filter By Type");
                Console.WriteLine("3. Filter By Generation");
                int userAnswer = int.Parse(Console.ReadLine());

                
                

                if (userAnswer == 2)
                {
                    //List<Pokemon> replacementList = new List<Pokemon>();
                    Console.WriteLine("What type of Pokemon would you like to display?");
                    string typeAnswer = Console.ReadLine();

                    typeAnswer.ToLower(); //ChatGPT assisted with the code to capitalize the first letter of the array.
                    char[] typeAnswerArray = typeAnswer.ToCharArray();
                    if (typeAnswerArray.Length > 0)
                    {
                        typeAnswerArray[0] = char.ToUpper(typeAnswerArray[0]);
                    }
                    string result = new string(typeAnswerArray);
                    filteredPokemonList = PokemonList.Where(p => p.TypeOne == result).ToList();
                    filteredPokemonList = filteredPokemonList.OrderBy(p => int.Parse(p.PokedexNumber)).ToList();

                }
                else if(userAnswer == 3) 
                {
                    Console.WriteLine("What Pokemon generation would you like to see?");
                    int generationAnswer = int.Parse(Console.ReadLine());
                    filteredPokemonList = PokemonList.Where(p => p.Generation == generationAnswer.ToString()).ToList();
                    filteredPokemonList = filteredPokemonList.OrderBy(p => int.Parse(p.PokedexNumber)).ToList();
                }
                else 
                {
                    filteredPokemonList = filteredPokemonList;

                }

                //Makes a queue for Pokemon
                Queue<Pokemon> pokemonQueue = new Queue<Pokemon>();

                foreach (Pokemon p in filteredPokemonList)
                {
                    pokemonQueue.Enqueue(p);
                }

                Stack<Pokemon> PokemonStack = new Stack<Pokemon>();
                Console.Clear();
                Console.WriteLine("Type 1 to view your most recently smashed Pokemon at any point (Must have smashed at least one Pokemon)");
                while (pokemonQueue.Count <= filteredPokemonList.Count) //ChatGPT assisted in making this not crash
                {
                    Pokemon mon = pokemonQueue.Peek();
                    Console.WriteLine($"Smash or Pass, \nPokedex Entry #{mon.PokedexNumber}: {mon.Name}");
                    string answer = Console.ReadLine();
                    if(answer == "1")
                    {
                        Pokemon lastSmashedPokemon = PokemonStack.Peek();
                        Console.WriteLine($"\nYour most recently smashed Pokemon is {PokemonList.Where(p => p.PokedexNumber == lastSmashedPokemon.PokedexNumber.ToString()).Select(p => p.Name).FirstOrDefault()}");
                    }
                    else if (answer.ToLower() == "smash")
                    {
                        Pokemon pokemon = pokemonQueue.Dequeue();
                        SmashPokemon.Add(mon);
                        PokemonStack.Push(mon);
                    }
                    else
                    {
                        Pokemon pokemon = pokemonQueue.Dequeue();
                        PassPokemon.Add(mon);
                    }
                    Console.WriteLine("\n");
                }

                Console.WriteLine($"You would smash {SmashPokemon.Count} Pokemon");
                Console.WriteLine($"You passed on {PassPokemon.Count} Pokemon");

                Console.WriteLine($"You would smash {(SmashPokemon.Count / PokemonList.Count) * 100}% of Pokemon");

                Console.WriteLine("Here are all the pokemon you would smash");
                foreach (Pokemon p in SmashPokemon)
                {
                    Console.WriteLine(p.Name);
                }
                /////////////////////////////////////////
                ///

                Dictionary<int, string> pokedexNumber = new Dictionary<int, string>();
                foreach (Pokemon p in PokemonList)
                {
                    pokedexNumber[int.Parse(p.PokedexNumber)] = p.Name;
                }
                bool loop = true;
                while (loop)
                {
                    int answer = int.Parse(Console.ReadLine());
                    if (answer == 0)
                        loop = false;
                    else if (answer >= 1 && answer <= 898)
                    {
                        string answerPokemon = pokedexNumber[answer];
                        Console.WriteLine($"Pokedex number {answer} is {pokedexNumber[answer]}");
                        Console.WriteLine($"{pokedexNumber[answer]} is from generation {PokemonList.Where(p => p.Name == pokedexNumber[answer]).Select(p => p.Generation).FirstOrDefault()}");
                        Console.WriteLine($"{pokedexNumber[answer]}'s Hit Point are {PokemonList.Where(p => p.Name == pokedexNumber[answer]).Select(p => p.HitPoints).FirstOrDefault()}, " +
                            $"Atack is {PokemonList.Where(p => p.Name == pokedexNumber[answer]).Select(p => p.Attack).FirstOrDefault()}, " +
                            $"Defense is {PokemonList.Where(p => p.Name == pokedexNumber[answer]).Select(p => p.Defense).FirstOrDefault()}, " +
                            $"Special Attack is {PokemonList.Where(p => p.Name == pokedexNumber[answer]).Select(p => p.SpecialAttack).FirstOrDefault()}, " +
                            $"Special Defense is {PokemonList.Where(p => p.Name == pokedexNumber[answer]).Select(p => p.SpecialDefense).FirstOrDefault()}, " +
                            $"Speed is {PokemonList.Where(p => p.Name == pokedexNumber[answer]).Select(p => p.Speed).FirstOrDefault()}, " +
                            $"With a total stat count of {PokemonList.Where(p => p.Name == pokedexNumber[answer]).Select(p => p.TotalStats).FirstOrDefault()}!");
                    }
                    else
                        loop = false;
                }

                ////////////////////////////////////////////////////
                ///


            }
        }
    }
}