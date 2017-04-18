using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace _3KT
{
    //
    // būk pasiruošęs 
    // 1. persiųst žinutę
    // 2. persiųst žinutę, tada pakeist edge'o svorį ir vėl persiųst (svorio pakeitimas - panaikini edge, tada pridedi edge su tam tikru svoriu)
    // 3. gali liept padaryt, kad runtime nutrūktų edge'as arba išsijungtų routeris, tada reiktų NetworkControl.cs SendMessage metode atkomentuot tekstą. (Patariu ten gerai įsigilinti)

    // kitas dalykas:
    // kai ištrini routerį numeriai vėl nuo 0 iki n persinumeruoja. Tad tada nebeatitinka ...152 su 2
    class Program
    {
        static public Router find(List<Router> routers, string address)
        {
            foreach (Router r in routers) {
                if (address == r.ipAddress)
                {
                    return r;
                }
             }
            return null;
        }
        static public void AddUndirectedEdge(List<Router> routers,string RouterIP,string NeighbourIP,int latency)
        {
            foreach (Router c in routers)
            {
                if (c.ipAddress == RouterIP)
                {
                    Neighbour temp;
                    if (find(routers, RouterIP) != null)
                    {
                        temp.router = find(routers, NeighbourIP);
                        temp.latency = latency;
                        c.neighbours.Add(temp);
                    }

                }
                if (c.ipAddress == NeighbourIP)
                {
                    Neighbour temp;
                    if (find(routers, RouterIP) != null)
                    { 
                        temp.router = find(routers, RouterIP);
                        temp.latency = latency;
                        c.neighbours.Add(temp);
                    }                   
                }
            }
        }
        static void Main(string[] args)
        {
            
            string[] lines = new string[998];
            List<Router> routers = new List<Router>();
            string path = @"D:\Dropbox\Programos\RoutingSimulationV2\RoutingSimulationV2.App\RoutingSimulationV2\Routers.txt"; 
            string path2 = @"D:\Dropbox\Programos\RoutingSimulationV2\RoutingSimulationV2.App\RoutingSimulationV2\Network.txt";
            StreamReader skaitymas = new StreamReader(path);
            
            using (skaitymas)
            {
                string eilute;
                int b = 0;
                while ((eilute = skaitymas.ReadLine()) != null)
                {
                    lines[b] = eilute;
                    Router laikinas = new Router(eilute);
                    routers.Add(laikinas);
                    b++;

                }
                
            }




            string[] lines2 = new string[998];
            StreamReader skaitymas2 = new StreamReader(path2);
            
            using (skaitymas2)
            {
                string eilute;
                int b = 0;
                while ((eilute = skaitymas2.ReadLine()) != null)
                {
                    lines2[b] = eilute;
                    b++;
                }
                
            }
            foreach (string line in lines2)
            {
                if (line == null)
                {
                    break;
                }
                string[] array = line.Split(' ');
                string RouterIP = array[0];
                string NeighbourIP = array[1];
                int latency;
                

                try
                {
                    latency = int.Parse(array[2]);
                    AddUndirectedEdge(routers, RouterIP, NeighbourIP, latency);
                }
                catch
                {
                    System.Console.WriteLine("Nėra įvesta visi atstumai tarp routerių duomenų faile");
                }
            }


            Console.WriteLine("Welcome to EseasSoft Routing Simulation");
            Console.WriteLine("[1] Add router");
            Console.WriteLine("[2] Remove router");
            Console.WriteLine("[3] Add link between routers");
            Console.WriteLine("[4] Remove link between routers");
            Console.WriteLine("[5] Send message");
            Console.WriteLine("[6] Print network");
            Console.WriteLine("[EXIT] Exit EseasSoft Routing Simulation");
            String S = Console.ReadLine();
            String input;
            int number;
            int from = 0, to = 0;
            while (S != "EXIT")
            {
                if (!(int.TryParse(S, out number)))
                {
                    Console.WriteLine("Incorrect format");
                }
                else
                {
                    switch (number)
                    {

                        case 1:
                            System.Console.WriteLine("Enter router IP address:");
                            input = Console.ReadLine();

                            NetworkControl.AddRouter(routers, input);                           
                            break;
                        case 2:
                            System.Console.WriteLine("Enter router NUMBER:");
                            input = Console.ReadLine();

                            try
                            {
                                from = int.Parse(input);

                            }
                            catch
                            {
                                System.Console.WriteLine("Incorrect router number format. (FORMAT: int)");

                            }
                            NetworkControl.RemoveRouter(routers, routers[from].ipAddress);
                            break;
                        case 3:
                            System.Console.WriteLine("Enter first router NUMBER:");
                            input = Console.ReadLine();

                            try
                            {
                                from = int.Parse(input);

                            }
                            catch
                            {
                                System.Console.WriteLine("Incorrect router number format. (FORMAT: int)");

                            }
                            System.Console.WriteLine("Enter second router NUMBER:");
                            input = Console.ReadLine();

                            try
                            {
                                to = int.Parse(input);
                            }
                            catch
                            {
                                System.Console.WriteLine("Incorrect router number format. (FORMAT: int)");
                            }
                            System.Console.WriteLine("Enter latency:");
                            input = Console.ReadLine();
                            int temp3 = 1;
                            try
                            {
                                 temp3 = int.Parse(input);
                            }
                            catch
                            {
                                System.Console.WriteLine("Incorrect latency format. (FORMAT: int)");
                            }
                            AddUndirectedEdge(routers, routers[from].ipAddress, routers[to].ipAddress, temp3);
                            break;
                        case 4:
                            System.Console.WriteLine("Enter first router NUMBER to delete link:");
                             input = Console.ReadLine();
                             
                            try
                            {
                                from = int.Parse(input);
                            }
                            catch
                            {
                                System.Console.WriteLine("Incorrect router number format. (FORMAT: int)");

                            }
                            System.Console.WriteLine("Enter second router NUMBER to delete link:");
                            input = Console.ReadLine();

                            try
                            {
                                to = int.Parse(input);
                            }
                            catch
                            {
                                System.Console.WriteLine("Incorrect router number format. (FORMAT: int)");
                            }
                            NetworkControl.RemoveEdge(routers, routers[from].ipAddress, routers[to].ipAddress);
                            break;


                        case 5:
                            System.Console.WriteLine("Enter SOURCE router NUMBER:");
                            input = Console.ReadLine();
                            
                            try
                            {
                                from = int.Parse(input);

                            }
                            catch
                            {
                                System.Console.WriteLine("Incorrect router number format. (FORMAT: int)");

                            }
                            System.Console.WriteLine("Enter TARGET router NUMBER");
                            input = Console.ReadLine();

                            try
                            {
                                to = int.Parse(input);

                            }
                            catch
                            {
                                System.Console.WriteLine("Incorrect router number format. (FORMAT: int)");

                            }
                            System.Console.WriteLine("Enter message");
                            input = Console.ReadLine();
                            if (from > routers.Count || to > routers.Count)
                            {
                                System.Console.WriteLine("Router with entered number does NOT exist.");
                            }
                            else
                            {                               
                                NetworkControl.SendMessage(routers, from, to, input);
                            }
                            break;
                        case 6:
                            NetworkControl.PrintNetwork(routers);
                            break;
                        
                    }
                }
                
                S = Console.ReadLine();
            }
           
            Console.ReadKey();
        }
    }
}
