using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace DFA
{
    [DataContract]
    public struct NFAState
    {
        [DataMember]
        public string name;
        [DataMember]
        public List<int> by0;
        [DataMember]
        public List<int> by1;
        [DataMember]
        public List<int> byE;
        [DataMember]
        public bool IsFinish;
        public NFAState(List<int> b0, List<int> b1, List<int> bye, bool isfin, string Name)
        {
            by0 = b0;
            by1 = b1;
            byE = bye;
            IsFinish = isfin;
            name = Name;
        }
    }
    [DataContract]
    public class NFA
    {
        [DataMember]
        public List<NFAState> NFAStateList;
        private string[,] SimpleStates;
        public DFA dfa = new DFA();
        List<int> FinalStates;
        List<List<string>> AllStates;
        public NFA()
        {
            NFAStateList = new List<NFAState>();
            AllStates = new List<List<string>>();
            FinalStates = new List<int>();
        }
        public void GetFirstState()
        {
            AllStates.Add(new List<string>());
            AllStates[0].Add("0");
            List<int> positions = new List<int>();
            positions.Add(0);
            for(int i = 0; positions.Count!=0; i++)
            {
                if(i == positions.Count)
                {
                    i = 0;
                }
                if(NFAStateList[positions[i]].byE.Count == 0)
                {
                    positions.RemoveAt(i);
                    i--;
                    continue;
                }
                if(NFAStateList[positions[i]].byE.Count == 1)
                {
                    AllStates[0][0] += "," + NFAStateList[positions[i]].byE[0];
                    positions[i] = NFAStateList[positions[i]].byE[0];
                }
                else
                {
                    AllStates[0][0] += "," + NFAStateList[positions[i]].byE[0];
                    for (int j = 1; j < NFAStateList[positions[i]].byE.Count; j++)
                    {
                        AllStates[0][0] += "," + NFAStateList[positions[i]].byE[j];
                        positions.Add(NFAStateList[positions[i]].byE[j]);
                    }
                    positions[i] = NFAStateList[positions[i]].byE[0];
                }
            }
            AllStates[0][0] = SortAndDeleteSetere(AllStates[0][0]);
        }

        private string SortAndDeleteSetere(string s)
        {
            string[] numbers = s.Split(',');
            List<string> list = numbers.ToList<string>();
            List<int> IndexToRemove = new List<int>();
            for(int i = 0; i < list.Count; i++)
            {
                for(int j =  i + 1; j < list.Count; j++)
                {
                    if(list[i] == list[j])
                    {
                        list.RemoveAt(j);
                        j--;
                    }
                }
            }
            for(int i = list.Count - 1; i >= 0; i--)
            {
                for(int j = i - 1; j >= 0; j--)
                {
                    if(Convert.ToInt32(list[i]) < Convert.ToInt32(list[j]))
                    {
                        string ptr = list[j];
                        list[j] = list[i];
                        list[i] = ptr;
                    }
                }
            }
            string str = "";
            for(int i = 0; i < list.Count - 1; i++)
            {
                str += list[i] + ",";
            }
            str += list[list.Count - 1];
            return str;
        }
        public void GetAllAnotherStates()
        {
            string str_for_0 = "";
            string str_for_1 = "";
            for(int i = 0; i < AllStates.Count; i++)
            {
                if(AllStates[i][0] == "")
                {
                    AllStates[i].Add("");
                    AllStates[i].Add("");
                    continue;
                }
                string[] numbers = AllStates[i][0].Split(',');
                for (int j = 0; j < numbers.Length; j++)
                {
                    str_for_0 += GetStateBy0(Convert.ToInt32(numbers[j]));
                    str_for_1 += GetStateBy1(Convert.ToInt32(numbers[j]));
                }
                str_for_0 = SortAndDeleteSetere(str_for_0.TrimEnd(','));
                str_for_1 = SortAndDeleteSetere(str_for_1.TrimEnd(','));
                if(!IsContain(str_for_0, AllStates))
                {
                    AllStates.Add(new List<string>());
                    AllStates[AllStates.Count - 1].Add(str_for_0);
                }
                AllStates[i].Add(str_for_0);
                if (!IsContain(str_for_1, AllStates))
                {
                    AllStates.Add(new List<string>());
                    AllStates[AllStates.Count - 1].Add(str_for_1);
                }
                AllStates[i].Add(str_for_1);
                str_for_0 = "";
                str_for_1 = "";
            }
            CreateTable();
            int l = 1;
        }
        private bool IsContain(string str_to_found, List<List<string>> StateList)
        {
            for(int i = 0;  i < StateList.Count; i++)
            {
                if(StateList[i].Contains(str_to_found))
                {
                    return true;
                }
            }
            return false;
        }
        private string GetStateBy1(int position)
        {
            List<int> positions = new List<int>();
            string str_for_1 = "";
            if(NFAStateList[position].by1.Count == 0)
            {
                return str_for_1;
            }
            for (int j = 0; j < NFAStateList[position].by1.Count; j++)
            {
                str_for_1 += NFAStateList[position].by1[j] + ",";
                positions.Add(NFAStateList[position].by1[j]);
            }
            for (int i = 0; positions.Count != 0; i++)
            {
                if (i == positions.Count)
                {
                    i = 0;
                }
                if (NFAStateList[positions[i]].byE.Count == 0)
                {
                    positions.RemoveAt(i);
                    i--;
                    continue;
                }
                for(int j = 0; j < NFAStateList[positions[i]].byE.Count; j++)
                {
                    str_for_1 += NFAStateList[positions[i]].byE[j] + ",";
                    positions.Add(NFAStateList[positions[i]].byE[j]);
                }
                positions.RemoveAt(i);
            }
            return str_for_1;
        }
        private string GetStateBy0(int position)
        {
            List<int> positions = new List<int>();
            string str_for_0 = "";
            if (NFAStateList[position].by0.Count == 0)
            {
                return str_for_0;
            }
            for (int j = 0; j < NFAStateList[position].by0.Count; j++)
            {
                str_for_0 += NFAStateList[position].by0[j] + ",";
                positions.Add(NFAStateList[position].by0[j]);
            }
            for (int i = 0; positions.Count != 0; i++)
            {
                if (i == positions.Count)
                {
                    i = 0;
                }
                if (NFAStateList[positions[i]].byE.Count == 0)
                {
                    positions.RemoveAt(i);
                    i--;
                    continue;
                }
                for (int j = 0; j < NFAStateList[positions[i]].byE.Count; j++)
                {
                    str_for_0 += NFAStateList[positions[i]].byE[j] + ",";
                    positions.Add(NFAStateList[positions[i]].byE[j]);
                }
                positions.RemoveAt(i);
            }
            return str_for_0;
        }
        private void CreateTable()
        {
            SimpleStates = new string[AllStates.Count, 3];
            for(int i = 0; i < AllStates.Count; i++)
            {
                SimpleStates[i,0] = Convert.ToString(i);
            }
            for(int i = 0; i < AllStates.Count; i++)
            {
                SimpleStates[i, 1] = Convert.ToString(CheckFirstItems(AllStates[i][1]));
                SimpleStates[i, 2] = Convert.ToString(CheckFirstItems(AllStates[i][2]));
            }
            for(int i = 0; i < NFAStateList.Count; i++)
            {
                if(NFAStateList[i].IsFinish == true)
                {
                    FinalStates.Add(i);
                }
            }
        }
        private int CheckFirstItems(string s)
        {
            for(int i = 0; i < AllStates.Count; i++)
            {
                if(AllStates[i][0] == s)
                {
                    return i;
                }
            }
            return -1;
        }
        public void CreateDFA()
        {
            for(int i = 0; i < AllStates.Count; i++)
            {
                State st = new State();
                st.Name = SimpleStates[i,0];
                for(int j = 1; j < 3; j++)
                {
                    st.By0 = Convert.ToInt32(SimpleStates[i, 1]);
                    st.By1 = Convert.ToInt32(SimpleStates[i, 2]);
                }
                for(int k = 0; k < FinalStates.Count; k++)
                {
                    string[] str = AllStates[i][0].Split(',');
                    for(int a = 0; a < str.Length; a++)
                    {
                        if(str[a] == Convert.ToString(FinalStates[k]))
                        {
                            st.IsFinish = true;
                            break;
                        }
                    }
                }
                dfa.Table.Add(st);
            }
        }
    }
}
