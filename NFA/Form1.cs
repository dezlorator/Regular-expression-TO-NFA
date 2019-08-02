using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace DFA
{
    public partial class Form1 : Form
    {
        DFA dfa = new DFA();
        public NFA nfa = new NFA();
        public REToNFA re_to_nfa = new REToNFA();
        string pathname;
        public Form1()
        {
            InitializeComponent();
        }


        public void Serialization(string path, REToNFA obj)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(REToNFA));
            REToNFA for_deser;
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                for_deser = (REToNFA)jsonFormatter.ReadObject(fs);
            }
            obj.REstring = for_deser.REstring;
        }
        public void Deserealization(string path, object obj)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(DFA));
            if(obj.GetType() == typeof(NFA))
            {
                jsonFormatter = new DataContractJsonSerializer(typeof(NFA));
            }
            else if (obj.GetType() == typeof(REToNFA))
            {
                jsonFormatter = new DataContractJsonSerializer(typeof(REToNFA));
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, obj);
            }
        }
        private void Checkbtn_Click(object sender, EventArgs e)
        {
            if (CheckString(StringBox.Text))
            {
                if (dfa.CheckString(StringBox.Text))
                {
                    MessageBox.Show(dfa.DFA_Path + ". This is correct state");
                }
                else
                {
                    MessageBox.Show(dfa.DFA_Path + ". This is wrong state");
                }
            }
            else
            {
                MessageBox.Show("Wrong string");
            }
        }
        public bool CheckString(string str)
        {
            for(int i = 0; i < str.Length; i++)
            {
                if(str[i]!='a'&&str[i]!='b')
                {
                    return false;
                }
            }
            return true;
        }

        private void Minimizationbtn_Click(object sender, EventArgs e)
        {
            Minimization min = new Minimization(dfa.Table.Count, dfa.Table);
            min.StepOne();
            min.StepTwo();
            dfa.Table = min.Final_minimaze();
            Minimizationbtn.Enabled = false;
        }

		private void Choose_file_btn_Click(object sender, EventArgs e)
		{
			OpenFileDialog Open_Dialog = new OpenFileDialog();
			Open_Dialog.Filter = "json files(*.json)|*.json";
			DialogResult dialog_result = Open_Dialog.ShowDialog();
			if (dialog_result == DialogResult.OK)
			{
				pathname = Open_Dialog.FileName;
				Serialization(pathname, re_to_nfa);
				Checkbtn.Enabled = true;
			}
			else if(dialog_result == DialogResult.None || dialog_result == DialogResult.Cancel)
			{
				return;
			}
			nfa.NFAStateList = re_to_nfa.CreateNFA();
			nfa.GetFirstState();
			nfa.GetAllAnotherStates();
			nfa.CreateDFA();
			dfa.Table = nfa.dfa.Table;
			Minimizationbtn.Enabled = true;
		}
	}
}
