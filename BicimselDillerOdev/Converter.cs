using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicimselDillerOdev
{
    public class Converter
    {
        public static HashSet<int> EpsilonClosure(NFA nfa, HashSet<int> states)
        {
            Stack<int> stack = new Stack<int>(states);
            HashSet<int> closure = new HashSet<int>(states);

            while (stack.Count > 0)
            {
                int state = stack.Pop();
                foreach (int next in nfa.GetTransitions(state, null)) // null = epsilon
                {
                    if (!closure.Contains(next))
                    {
                        closure.Add(next);
                        stack.Push(next);
                    }
                }
            }

            return closure;
        }

        // NFA -> DFA dönüşümü
        public static DFA Convert(NFA nfa, HashSet<char> alphabet)
        {
            DFA dfa = new DFA();
            Dictionary<HashSet<int>, string> stateMapping = new Dictionary<HashSet<int>, string>(HashSet<int>.CreateSetComparer());
            Queue<HashSet<int>> queue = new Queue<HashSet<int>>();

            // Başlangıç durumu
            HashSet<int> startClosure = EpsilonClosure(nfa, new HashSet<int> { 1 });
            //string startState = string.Join(",", startClosure.OrderBy(x => x));
            string startState = $"{{{string.Join(",", startClosure.OrderBy(x => x))}}}";
            dfa.StartState = startState;
            dfa.States.Add(startState);
            stateMapping[startClosure] = startState;
            queue.Enqueue(startClosure);

            while (queue.Count > 0)
            {
                HashSet<int> currentSet = queue.Dequeue();
                string currentState = stateMapping[currentSet];

                // Final durum kontrolü
                if (currentSet.Any(s => nfa.FinalStates.Contains(s)))
                    dfa.FinalStates.Add(currentState);

                foreach (char symbol in alphabet)
                {
                    HashSet<int> moveSet = new HashSet<int>();
                    foreach (int state in currentSet)
                    {
                        moveSet.UnionWith(nfa.GetTransitions(state, symbol));
                    }

                    HashSet<int> closure = EpsilonClosure(nfa, moveSet);
                    if (closure.Count > 0)
                    {

                        //string newState = string.Join(",", closure.OrderBy(x => x));
                        string newState = $"{{{string.Join(",", closure.OrderBy(x => x))}}}";
                        if (!dfa.States.Contains(newState))
                        {
                            dfa.States.Add(newState);
                            stateMapping[closure] = newState;
                            queue.Enqueue(closure);
                        }
                        dfa.Transitions[(currentState, symbol)] = newState;
                    }
                    else
                    {
                        // Ölü duruma geçiş ekle
                        dfa.Transitions[(currentState, symbol)] = "DeadState";
                    }
                }
            }

            // Ölü durum ekleme
            if (dfa.States.All(s => s != "DeadState"))
            {
                dfa.States.Add("DeadState");
                foreach (char symbol in alphabet)
                {
                    dfa.Transitions[("DeadState", symbol)] = "DeadState";
                }
            }

            return dfa;
        }
    }
}
