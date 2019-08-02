using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace DFA
{
    [DataContract]
    public class nst
    {
        [DataMember]
        public List<int> e = new List<int>();
        [DataMember]
        public string[] vetki = new string[2] { "-", "-" };
        [DataMember]
        public string f = "false"; //конеч или не конеч
    };
}
