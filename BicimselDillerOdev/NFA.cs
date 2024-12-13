using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicimselDillerOdev
{
    public class NFA
    {
        public int States;
        public HashSet<int> FinalStates;
        public Dictionary<(int, char?), List<int>> Transitions;

        public NFA(int states)
        {
            States = states;
            FinalStates = new HashSet<int>();
            Transitions = new Dictionary<(int, char?), List<int>>();
        }

        public void AddTransition(int from, char? symbol, int to)
        {
            if (!Transitions.ContainsKey((from, symbol)))
                Transitions[(from, symbol)] = new List<int>();
            Transitions[(from, symbol)].Add(to);
        }

        public List<int> GetTransitions(int state, char? symbol)
        {
            return Transitions.ContainsKey((state, symbol)) ? Transitions[(state, symbol)] : new List<int>();
        }
    }
}
