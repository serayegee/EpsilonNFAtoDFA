using BicimselDillerOdev;
using System.Security.Cryptography;
using static BicimselDillerOdev.Printer;

public class Program
{
    public static void Main(string[] args)
    {
        // NFA'yı oluşturma
        NFA nfa = new NFA(4);
        nfa.AddTransition(1, null, 3); // ε geçişi
        nfa.AddTransition(1, 'b', 2);
        nfa.AddTransition(2, 'b', 1);
        nfa.AddTransition(3, 'a', 3);
        nfa.AddTransition(3, 'b', 2);
        nfa.AddTransition(3, 'b', 4);
        nfa.FinalStates.Add(3); 

        // Alfabe tanımlama
        HashSet<char> alphabet = new HashSet<char> { 'a', 'b' };

        // Alfabe yazdırma
        Console.WriteLine("Alphabet: " + string.Join(", ", alphabet));

        // NFA yazdırma
        Printer.PrintNFA(nfa, alphabet);

        // NFA -> DFA dönüşümü
        DFA dfa = Converter.Convert(nfa, alphabet);

        // DFA yazdırma
        Printer.PrintDFA(dfa);
    }
}