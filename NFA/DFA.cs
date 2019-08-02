using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace DFA
{
    [DataContract]
    public struct State
    {
        public State(int by0, int by1, bool isfinish, string str)
        {
            By0 = by0;
            By1 = by1;
            IsFinish = isfinish;
            Name = str;
        }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int By0 { get; set; }
        [DataMember]
        public int By1 { get; set; }
        [DataMember]
        public bool IsFinish { get; set; }
    }
    [DataContract]
    public class DFA
    {
        [DataMember]
        public List<State> Table;
        public string DFA_Path { get; set; }
        public DFA()
        {
            Table = new List<State>();
        }
        public bool CheckString(string str)
        {
            int final_state = 0;
            DFA_Path = "0";
            for (int i = 0; i < str.Length; i++)
            {
                if(str[i] == 'b')
                {
                    final_state = Table[final_state].By1;
                    DFA_Path += "->"+ Convert.ToString(final_state) + " (by b)";
                }
                else
                {
                    final_state = Table[final_state].By0;
                    DFA_Path += "->" + Convert.ToString(final_state) + " (by a)";
                }
            }
            return Table[final_state].IsFinish;
        }
    }
}
