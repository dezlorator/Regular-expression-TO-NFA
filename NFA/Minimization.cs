using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFA
{
    class Minimization
    {
        List<int> by0 = new List<int>();
        List<int> by1 = new List<int>();
        List<string[]> strlist = new List<string[]>();
        List<List<State>> vstates = new List<List<State>>();
        List<int[]> numbers = new List<int[]>();
        List<State> list = new List<State>();
        char[,] matrix;
        int size;
        List<State> DFA = new List<State>();
        List<State> NewDFA = new List<State>();
        public Minimization(int size, List<State> dfa)
        {
            this.size = size;
            matrix = new char[size, size];
            for (int i = 0; i < dfa.Count; i++)
            {
                DFA.Add(new State(dfa[i].By0, dfa[i].By1, dfa[i].IsFinish, Convert.ToString(i)));
            }
        }
        public void StepOne()
        {
            List<int> true_list = new List<int>();
            List<int> false_list = new List<int>();
            for (int i = 0; i < size; i ++)
            {
                for(int j = 0; j < size; j ++)
                {
                    if(i > j)
                    {
                        matrix[i, j] = '*';
                    }
                    if(i==j)
                    {
                        matrix[i, j] = 'v';
                    }
                }
            }
            for(int i = 0; i < DFA.Count; i++)
            {
                if(DFA[i].IsFinish == true)
                {
                    true_list.Add(i);
                }
                else
                {
                    false_list.Add(i);
                }
            }
            for(int i = 0; i < true_list.Count; i++)
            {
                for(int j = 0; j < false_list.Count; j++)
                {
                    if(true_list[i] < false_list[j])
                    {
                        list.Add(new State(true_list[i], false_list[j], false, Convert.ToString(i)));
                    }
                }
            }
            for (int i = 0; i < false_list.Count; i++)
            {
                for (int j = 0; j < true_list.Count; j++)
                {
                    if (false_list[i] < true_list[j])
                    {
                        list.Add(new State(false_list[i], true_list[j], false, Convert.ToString(i)));
                    }
                }
            }
            for(int i = 0; i < list.Count; i++)
            {
                matrix[list[i].By0, list[i].By1] = 'x';
            }
        }
        public void StepTwo()
        {
            List<State> empty_place = new List<State>();
            List<State> by_0 = new List<State>();
            List<State> by_1 = new List<State>();
            int counter;
            for (int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if (matrix[i, j] =='\0')
                    {
                        empty_place.Add(new State(i, j, false, Convert.ToString(i)));
                    }
                }
            }
            for (int i = 0; i < empty_place.Count; i++)
            {
                if (DFA[empty_place[i].By0].By0 < DFA[empty_place[i].By1].By0)
                {
                    by_0.Add(new State(DFA[empty_place[i].By0].By0, DFA[empty_place[i].By1].By0, false, Convert.ToString(i)));
                }
                else
                {
                    by_0.Add(new State(DFA[empty_place[i].By1].By0, DFA[empty_place[i].By0].By0, false, Convert.ToString(i)));
                }
                if (DFA[empty_place[i].By0].By1 < DFA[empty_place[i].By1].By1)
                {
                    by_1.Add(new State(DFA[empty_place[i].By0].By1, DFA[empty_place[i].By1].By1, false, Convert.ToString(i)));
                }
                else
                {
                    by_1.Add(new State(DFA[empty_place[i].By1].By1, DFA[empty_place[i].By0].By1, false, Convert.ToString(i)));
                }
            }
            while (empty_place.Count != 0)
            {
                counter = 0;
                for (int i = 0; i < empty_place.Count; i++)
                {
                    if(empty_place[i].IsFinish == true)
                    {
                        continue;
                    }
                    if ((matrix[by_0[i].By0, by_0[i].By1] == 'x') || (matrix[by_1[i].By0, by_1[i].By1] == 'x'))
                    {
                        matrix[empty_place[i].By0, empty_place[i].By1] = 'x';
                        empty_place[i] = new State(empty_place[i].By0, empty_place[i].By1, true, Convert.ToString(i));
                        counter++;
                    }
                    else if ((matrix[by_0[i].By0, by_0[i].By1] == 'v') && (matrix[by_1[i].By0, by_1[i].By1] == 'v'))
                    {
                        matrix[empty_place[i].By0, empty_place[i].By1] = 'v';
                        empty_place[i] = new State(empty_place[i].By0, empty_place[i].By1, true, Convert.ToString(i));
                        counter++;
                    }
                }
                if(counter==0)
                {
                    for(int i = 0; i < empty_place.Count; i++)
                    {
                        if(empty_place[i].IsFinish == false)
                        {
                            matrix[empty_place[i].By0, empty_place[i].By1] = 'v';
                            empty_place[i] = new State(empty_place[i].By0, empty_place[i].By1, true, Convert.ToString(i));
                        }
                    }
                    break;
                }
            }
        }
        public List<State> Final_minimaze()
        {
            //вернуть листы
            for (int i = 0; i < size; i++)
            {
                vstates.Add(new List<State>());
                for (int j = 0; j < size; j++)
                {
                    if (i <= j && matrix[i, j] == 'v')
                    {
                        vstates[i].Add(new State(DFA[j].By0, DFA[j].By1, DFA[j].IsFinish, Convert.ToString(j)));
                    }
                }
            }
            for (int i = 0; i < vstates.Count; i++)
            {
                numbers.Add(new int[vstates[i].Count]);
                for (int j = 0; j < vstates[i].Count; j++)
                {
                    if (IsExist(numbers, Convert.ToInt32(vstates[i][j].Name)))
                    {
                        numbers[i][j] = Convert.ToInt32(vstates[i][j].Name);
                    }
                }
            }
            for (int i = 1; i < numbers.Count; i ++)
            {
                for(int j = 0; j < numbers[i].Length; j++)
                {
                    if(numbers[i][j] != 0)
                    {
                        break;
                    }
                    if(j == numbers[i].Length - 1)
                    {
                        numbers.RemoveAt(i);
                        i--;
                    }
                }
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                State st = new State();
                for(int j = 0;; j++)
                {
                    if(j == numbers[i].Length - 1)
                    {
                        st.Name += numbers[i][j];
                        break;
                    }
                    st.Name += numbers[i][j] + ",";
                }
                st.IsFinish = false;
                for(int j = 0; j < numbers[i].Length; j++)
                {
                    if(DFA[numbers[i][j]].IsFinish)
                    {
                        st.IsFinish = true;
                        break;
                    }
                }
                
                NewDFA.Add(st);
            }

            for(int i = 0; i < numbers.Count; i++)
            {
                by0.Add(DFA[numbers[i][0]].By0);
                by1.Add(DFA[numbers[i][0]].By1);
            }
            for(int i = 0; i < by0.Count; i ++)
            {
                int byy0 = 0, byy1 = 0;
                for (int j = 0; j < NewDFA.Count; j++)
                {
                    if(NewDFA[j].Name.Contains(Convert.ToString(by0[i])))
                    {
                        byy0 = (int)char.GetNumericValue(NewDFA[j].Name[0]);
                        break;
                    }
                }
                for(int j = 0; j < NewDFA.Count; j++)
                {
                    if (NewDFA[j].Name.Contains(Convert.ToString(by1[i])))
                    {
                        byy1 = (int)char.GetNumericValue(NewDFA[j].Name[0]);
                        break;
                    }
                }
                NewDFA[i] = new State(byy0, byy1, NewDFA[i].IsFinish, NewDFA[i].Name);
            }
            return NewDFA;
        }
        private bool IsExist(List<int[]> list, int value)
        {
            for(int i = 0; i < list.Count; i++)
            {
                for(int j = 0; j < list[i].Length; j++)
                {
                    if(list[i][j] == value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        int h = 9;
    }
}
