using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace _3KT
{
    static public class Dijkstra
    {
        public static int[] dist;
        public static int[] prev;
        public static int[] shortestpath;
        private static Router RemoveObj(Queue<Router> Q, int place)
        {

            List<Router> b = new List<Router>();
            while (Q.Count != 0)
            {
                b.Add(Q.Dequeue());
            }
            //b.RemoveAt(place);
            foreach (Router r in b)
            {
                if (r.number != place)
                    Q.Enqueue(r);
                
            }
            foreach (Router r in b)
            {
                if (r.number == place)
                {

                    return r;
                }

            }
            return null;
        }
        private static int MinVertex(int[] dist, Queue<Router> Q)
        {
            List<Router> b = new List<Router>();
            while (Q.Count != 0)
            {
                b.Add(Q.Dequeue());
            }

            foreach (Router r in b)
            {

                Q.Enqueue(r);
            }

            int x = Int32.MaxValue;
            int y = -1;   // graph not connected, or no unvisited vertices           
            foreach (Router r in b)
            {
                // System.Console.WriteLine(b.Count);
                // System.Console.WriteLine(r.number);
                // System.Console.WriteLine(dist[r.number]);

                if (dist[r.number] < x)
                {
                    y = r.number;
                    x = dist[r.number];
                }
            }

            return y;
        }

        // šis metodas yra grynai grynai pagal wikipedijoj pateiktą pseudo kodą
        // https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm#Pseudocode
        static public void ShortestPathMethod(List<Router> a, int source, int Target)
        {
            dist = new int[a.Count];
            prev = new int[a.Count];

            int i;

            int alt;
            Queue<Router> Q = new Queue<Router>();

            dist[source] = 0;
            i = -1;
            foreach (Router r in a)
            {
                i++;
                r.number = i;
                if (r.ipAddress != a[source].ipAddress)
                {
                    dist[r.number] = Int32.MaxValue;
                }
                Q.Enqueue(r);
            }
            while (Q.Count != 0)
            {
                // System.Console.WriteLine(Q.Count);
                Router temp = RemoveObj(Q, MinVertex(dist, Q));

                foreach (Neighbour n in temp.neighbours)
                {
                    alt = dist[temp.number] + n.latency;

                    if (alt < dist[n.router.number])
                    {
                        dist[n.router.number] = alt;
                        prev[n.router.number] = temp.number;
                    }
                }

            }

            int d = -1;
            if (Target != source)
            {
                Stack<int> stack = new Stack<int>();
                stack.Push(Target);
                while (Dijkstra.prev[Target] != source)
                {
                    stack.Push(Dijkstra.prev[Target]);
                    Target = Dijkstra.prev[Target];
                }
                stack.Push(source);
                shortestpath = new int[a.Count];
                while (stack.Count != 0)
                {
                    d++;
                    shortestpath[d] = stack.Pop();
                    // System.Console.WriteLine(shortestpath[d]);

                }
            }
            else
            {
                shortestpath = new int[1];
                shortestpath[0] = Target;
                // System.Console.WriteLine(shortestpath[0]);
            }



        }

    }
}
