using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicimselDillerOdev
{
    public class Printer
    {

        public static void PrintNFA(NFA nfa, HashSet<char> alphabet)
        {
            // Başlangıç durumu (Burada başlangıç durumu 1 olarak varsayılmıştır)
            string startState = "{1}";

            Console.WriteLine("NFA States: " + string.Join(", ", Enumerable.Range(1, nfa.States)));
            Console.WriteLine("Start State: " + startState);  // Başlangıç durumunu yazdır
            Console.WriteLine("Final States: " + string.Join(", ", nfa.FinalStates.Select(s => $"{{{s}}}")));
            Console.WriteLine("Transitions:");
            foreach (var transition in nfa.Transitions)
            {
                string symbol = transition.Key.Item2.HasValue ? transition.Key.Item2.ToString() : "/\\";
                string fromState = $"{{{transition.Key.Item1}}}";
                string toStates = string.Join(", ", transition.Value.Select(v => $"{{{v}}}"));
                Console.WriteLine($"  {fromState} --[{symbol}]--> {toStates}");
            }

            Console.WriteLine();
        }

        /*public static void PrintNFA(NFA nfa, HashSet<char> alphabet)
        {
            Console.WriteLine("NFA States: " + string.Join(", ", Enumerable.Range(1, nfa.States)));
            Console.WriteLine("Final States: " + string.Join(", ", nfa.FinalStates));
            Console.WriteLine("Transitions:");
            foreach (var transition in nfa.Transitions)
            {
                string symbol = transition.Key.Item2.HasValue ? transition.Key.Item2.ToString() : "E";
                Console.WriteLine($"  {transition.Key.Item1} --[{symbol}]--> {string.Join(", ", transition.Value)}");
            }
        }
        */
        public static void PrintDFA(DFA dfa)
        {
            Console.WriteLine("DFA States: " + string.Join(", ", dfa.States));
            Console.WriteLine("Start State: " + dfa.StartState);
            Console.WriteLine("Final States: " + string.Join(", ", dfa.FinalStates));
            Console.WriteLine("Transitions:");
            foreach (var transition in dfa.Transitions)
            {
                //Console.WriteLine($"  {transition.Key.Item1} --{transition.Key.Item2}--> {transition.Value}");
                Console.WriteLine($"  {transition.Key.Item1} ->[{transition.Key.Item2}]-> {transition.Value}");
            }
        }
    }
}
