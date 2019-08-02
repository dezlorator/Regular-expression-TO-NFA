using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace DFA
{
    public class REToNFA
    {
        public string REstring;
        int name_counter;
        List<NFAState> NFAStateList = new List<NFAState>();
        List<nst> nfa = new List<nst>(); //список из состояний
        Stack<int> state = new Stack<int>(); //СТЕК СОСТОЯНИЙ для обработки
        //текущ состояниее
        int nfa_size;
        public REToNFA()
        {
            name_counter = 0;
        }
        public List<NFAState> CreateNFA()
        {
            try
            {
                nfa.Clear();
                nfa_size = 0;
                postfix_to_nfa(rebuilt(REstring));
                int final_state = state.Peek(); state.Pop(); //финал 
                int start_state = state.Peek(); state.Pop(); //начал


                nfa[final_state].f = "true"; //отмечаем шо реально конечное
                nfa[start_state].f = "begin"; //отмечаем шо реально начальное
            }
            catch
            {
                MessageBox.Show("Регулярное выражение написано неверно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            for(int i = 0; i < nfa.Count; i++)
            {
                NFAState st = new NFAState();
                st.name = Convert.ToString(name_counter++);
                if(nfa[i].f == "true")
                {
                    st.IsFinish = true;
                }
                else
                {
                    st.IsFinish = false;
                }
                st.byE = nfa[i].e;
                List<int> list = new List<int>();
                if(nfa[i].vetki[0]!="-")
                {
                    list.Add(Convert.ToInt32(nfa[i].vetki[0]));
                }
                st.by0 = list;
                list = new List<int>();
                if (nfa[i].vetki[1] != "-")
                {
                    list.Add(Convert.ToInt32(nfa[i].vetki[1]));
                }
                st.by1 = list;
                NFAStateList.Add(st);
            }
            return NFAStateList;
        }
        //если в постфикс регулярке попался символ
        void symbol(int i)
        {
            nst init_nfa_state = new nst();
            nst init_nfa_state2 = new nst();
            nfa.Add(init_nfa_state);
            nfa.Add(init_nfa_state2); //добавили в список начальное состояние
            nfa[nfa_size].vetki[i] = Convert.ToString((nfa_size + 1));//уууу сук в ПЕРЕД след состоянием добавляем в ветку 0(если а) или 1(если б) значение КОЛИЧЕСТВО СОСТОЯНИЙ + 1
            state.Push(nfa_size);//добавили в стек количество состояний т е число
            nfa_size++;
            state.Push(nfa_size); //ЗАЧЕМ-ТО ДОБАВИЛИ ЕЩЕ РАЗ))0 добавили в стек количество состояний т е число
            nfa_size++;  //в итоге за раз делаем два шага тип )
        }
        //если +
        void plus()
        {
            nst init_nfa_state = new nst();
            nst init_nfa_state2 = new nst();
            nfa.Add(init_nfa_state); //добавили в список начальное состояние
            nfa.Add(init_nfa_state2);
            int d = state.Peek(); state.Pop();
            int c = state.Peek(); state.Pop();
            int b = state.Peek(); state.Pop();
            int a = state.Peek(); state.Pop();

            nfa[nfa_size].e.Add(a);//добавляем в ветку пустой строки имя состояния а
            nfa[nfa_size].e.Add(c);//добавляем в ветку пустой строки имя состояния с
            nfa[b].e.Add(nfa_size + 1); //добавляем в ветку пустой строки имя состояния а
            nfa[d].e.Add(nfa_size + 1); //добавляем в ветку пустой строки имя состояния а

            state.Push(nfa_size);
            nfa_size++;
            state.Push(nfa_size);
            nfa_size++;
        }
        //если это .
        void point()
        {
            int d = state.Peek(); state.Pop();
            int c = state.Peek(); state.Pop();
            int b = state.Peek(); state.Pop();
            int a = state.Peek(); state.Pop();
            nfa[b].e.Add(c);//записываем переход по пустой строке в состояние бе
            state.Push(a);
            state.Push(d);
        }
        //если в постфикс регулярке попалась *
        void star()
        {
            nst init_nfa_state = new nst();
            nst init_nfa_state2 = new nst();
            nfa.Add(init_nfa_state); //ааа ну да это мы как бы только и можем добавлять
            nfa.Add(init_nfa_state2);//добавили в список начальное состояние
            int b = state.Peek(); //послед состояние
            state.Pop(); //убралии
            int a = state.Peek(); //след состояние и потом убрали
            state.Pop();
            nfa[nfa_size].e.Add(a); //добавляем в первое сост в ветку ПУСТАЯ СТРОКА  доб какое-то(?) ИМЯ СОСТОЯНИЯ 
            nfa[nfa_size].e.Add(nfa_size + 1);  //добавляем в первое сост в ветку ПУСТАЯ СТРОКА  доб СЛЕД ИМЯ СОСТОЯНИЯ
            nfa[b].e.Add(a); ////добавляем в какое-то(?) сост в ветку ПУСТАЯ СТРОКА  доб ИМЯ СОСТОЯНИЯ
            nfa[b].e.Add(nfa_size + 1); //добавляем в какое-то(?) сост в ветку ПУСТАЯ СТРОКА  доб СЛЕД ИМЯ СОСТОЯНИЯ
            state.Push(nfa_size); //кидаем в стек номер нашего послед состояния
            nfa_size++;
            state.Push(nfa_size); //кидаем в стек номер нашего послед состояния
            nfa_size++;//в итоге за раз делаем два шага тип )
        }
        void postfix_to_nfa(string postfix)
        {
            for (int i = 0; i < postfix.Length; i++)//пока не конец постфикс записи
            {
                char h;
                switch (postfix[i])
                {
                    //если это какой-то из этих символов то мы идем в определен методы
                    case 'a':
                    case 'b':
                        if (postfix[i] == 'a') symbol(0);//это ветка а или б
                        else symbol(1);//это ветка а или б
                        break;
                    case '*': star(); break;
                    case '.': point(); break;
                    case '+': plus(); break;
                }
            }
        }
        string rebuilt(string re)
        {
            string res = "";
            char symb, symb2;
            for (int i = 0; i < re.Length; i++)
            { //пока не конец регулярки
                symb = re[i]; //символ регулярки
                if (i + 1 < re.Length)
                { //если дальше что-то есть
                    symb2 = re[i + 1]; //след символ регулярки
                    res += symb; //итог строка равно первому символу
                    if (symb != '(' && symb2 != ')' && symb != '+' && symb2 != '+' && symb2 != '*')
                    { //если первый символ не (+ и след символ не )+*
                      //т е первый символ мб буквой ) и * а второй ток ( и буквой
                        res += '.'; //к итог строке добавляем .
                    }
                }
            }

            res += re[re.Length - 1]; //допис послед символ
            string postfix = "";

            Stack<char> op = new Stack<char>(); //создали наш стек
            char c;
            for (int i = 0; i < res.Length; i++) //пока не конец регуляркм
            {
                switch (res[i]) //если символ регулярки это то
                {
                    case 'a':
                    case 'b':
                        postfix += res[i]; break; //если это символ то доб и в итон строку
                    case '(': //если скобка ( то 
                        op.Push(res[i]); break; //кидаем в стек
                    case ')': //если ) то
                        while (op.Peek() != '(')
                        { //пока не верхний эл не равен (
                          //тут мы когда )
                            postfix += op.Peek(); //запис в итог строку )
                            op.Pop(); //удаляем )
                        }
                        op.Pop(); //удаляем (
                        break;
                    default:
                        //если это + * .
                        while (op.Count != 0)
                        { //пока стек не пуст
                            c = op.Peek(); //символ = вершина стека
                            if (priority(c) >= priority(res[i]))
                            { //если приоритет вершини стека больше чем текущий символ
                                postfix += op.Peek(); //тогда записываем в итогг стр
                                op.Pop(); //и удаляем что записали
                            }
                            else break;
                        }
                        op.Push(res[i]);
                        break;
                }

            }
            while (op.Count != 0)
            { //потом докидываем все что есть из стека
                postfix += op.Peek();
                op.Pop();
            }
            return postfix;
        }
        //возвращает приоритетище
        int priority(char c)
        {
            switch (c)
            {
                case '*': return 3;
                case '.': return 2;
                case '+': return 1;
                default: return 0;
            }
        }
    }
}
