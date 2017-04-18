using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3KT
{
    public struct Neighbour
    {
        public Router router;
        public int latency;
    }
    public class Router
    {
        public int number;
        public string ipAddress;
        public List<Neighbour> neighbours = new List<Neighbour>();
        public Router (string a)
        {
            ipAddress = a;          
        }

        public Router(string a, int number)
        {
            ipAddress = a;
            this.number = number;
        }
        public Router() { }
        public string package;
                    
    }
}
