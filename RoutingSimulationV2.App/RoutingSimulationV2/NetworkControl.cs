using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3KT
{
    class NetworkControl
    {
        public static void SendMessage(List<Router>routers,int Source, int Target,string input)
        {         
            Dijkstra.ShortestPathMethod(routers, Source, Target);                                           
            int temp = 0;
            
            while (Dijkstra.shortestpath[0] != Target)
            {
                
                temp = Dijkstra.shortestpath[1];       // Dijkstra.shortestpath[1]; grąžiną artimiausio routerio numerį. Jei būtų [0] tai grąžintų source routerio numerį
                routers[temp].package = input;         // va čia vyksta perdavimas žinutės
                PrintRouterReceiveInfo(routers[temp]); // čia išprintina "maršrutizavimo lentelę"

                // šita atkomentavus turėtų išeit tipo runtime'e nutrūksta jungtis ir turi persiorentuot
                //if (temp == 4)
                //    NetworkControl.RemoveEdge(routers, routers[4].ipAddress, routers[1].ipaddress);
                
                // čia kiekvienas routeris gavęs žinutę persiskaičiuoja artimiausią kelią sekančiam perdavimui.
                Dijkstra.ShortestPathMethod(routers, temp, Target);             
                
            }
        }
        public static void RemoveEdge(List<Router> routers, string ipaddress1, string ipaddress2)
        {
            foreach (Router r in routers)
            {
                if(r.ipAddress == ipaddress1 || r.ipAddress == ipaddress2)
                foreach (Neighbour n in r.neighbours)
                {
                    if (n.router.ipAddress == ipaddress1|| n.router.ipAddress == ipaddress2)
                    {
                        r.neighbours.Remove(n);
                        break;
                    }
                }
            }

        }
        public static void RemoveRouter(List<Router> routers,string ipaddress) {
            foreach (Router r in routers)
            {

                foreach (Neighbour n in r.neighbours)
                {
                    if (n.router.ipAddress == ipaddress)
                    {
                        r.neighbours.Remove(n);
                        break;
                    }
                }
            }
            routers.Remove(Program.find(routers,ipaddress));
            
        }
        public static void AddRouter(List<Router> routers, string ipaddress)
        {
            Router temp = new Router(ipaddress);
            routers.Add(temp);
        }
        public static void PrintRouterReceiveInfo(Router r)
        {
            System.Console.Write("Router IP address: {0} \n", r.ipAddress);
            System.Console.Write("Router number: {0} \n", r.number);
            System.Console.WriteLine("Received message: {0}", r.package);
            System.Console.WriteLine("Neighbours: ");
            foreach (Neighbour n in r.neighbours)
            {           
                System.Console.Write("IP address - {0}",n.router.ipAddress);
                System.Console.Write(" Latency {0} \n", n.latency);
            }
               System.Console.WriteLine();
        }

        public static void PrintRouterInfo(Router r)
        {
            System.Console.Write("Router IP address: {0} \n", r.ipAddress);
            System.Console.Write("Router number: {0} \n", r.number);
            System.Console.WriteLine("Neighbours: ");
            foreach (Neighbour n in r.neighbours)
            {
                // System.Console.WriteLine();                   
                System.Console.Write("IP address - {0}", n.router.ipAddress);
                System.Console.Write(" Latency {0} \n", n.latency);
            }
            //System.Console.WriteLine();
            System.Console.WriteLine();
        }

        public static void PrintNetwork(List<Router> routers)
        {
            foreach(Router r in routers)
            {
                PrintRouterInfo(r);
            }
        }
        
    }
}
