using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicimselDillerOdev
{
    public class DFA
    {
        public HashSet<string> States;
        public HashSet<string> FinalStates;
        public Dictionary<(string, char), string> Transitions;
        public string StartState;

        public DFA()
        {
            States = new HashSet<string>();
            FinalStates = new HashSet<string>();
            Transitions = new Dictionary<(string, char), string>();
        }
    }

}
