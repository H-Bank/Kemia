using System;
using System.IO;

namespace Kémia
{
    class Program
    {
        public struct adat
        {
            public string ev;
            public string nev;
            public string jel;
            public int rendsz;
            public string felf;
        }
        static void Main(string[] args)
        {
            string[] sorok = File.ReadAllLines("felfedezesek.csv");
            adat[] kemia = new adat[sorok.Length-1];
            for (int i = 0; i < sorok.Length-1; i++)
            {
                string[] sor = sorok[i+1].Split(';');
                kemia[i].ev = sor[0];
                kemia[i].nev = sor[1];
                kemia[i].jel = sor[2];
                kemia[i].rendsz = Convert.ToInt32(sor[3]);
                kemia[i].felf = sor[4];
            }

            Console.WriteLine("3. feladat: Elemek száma: " + (sorok.Length-1));

            Console.Write("4. feladat: ");
            int db = 0;
            for (int i=0;i<sorok.Length-1;i++)
            {
                //Console.WriteLine(kemia[i].ev);
                if (kemia[i].ev.Substring(1, 3) == "kor")
                {
                    db = db + 1;
                }
            }
            Console.WriteLine("Felfedezések száma az ókorban: " + db);

            bool van = true;
            char[] angolabc = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            string ben=null;
            while (van)
            {
                Console.Write("5. feladat: Kérek egy vegyjelet: ");
                string be = Console.ReadLine();
                if (be.Length == 2 || be.Length == 1 && be.Length != 0)
                {
                    string bel = be.ToLower();
                    ben = bel;
                    db = 0;
                    for (int i = 0; i < 26; i++)
                    {
                        if (bel[0] == angolabc[i])
                        {
                            db++;
                        }
                    }
                    if (db == 1)
                    {
                        van = false;
                    }
                    //Console.WriteLine(be.Length + db);
                    if (be.Length == 2 && db==1)
                    {
                        db = 0;
                        for (int i = 0; i < 26; i++)
                        {
                            if (bel[1] == angolabc[i])
                            {
                                db++;
                            }
                        }
                        if (db==1)
                        {
                            van = false;
                        }
                        else
                        {
                            van = true;
                        }
                    }
                }
            }

            Console.Write("6. feladat: ");
            int j = -1;
            bool ha = true;
            while (ha && j<sorok.Length-2)
            {
                j++;
                string jelb = kemia[j].jel.ToLower();
                if (ben==jelb)
                {
                    ha = false;
                }
            }
            if (!ha)
            {
                Console.WriteLine("Keresés\n\tAz elem vegyjele: "+kemia[j].jel+"\n\tAz elem neve: " + kemia[j].nev +"\n\tRendszáma: " + kemia[j].rendsz +"\n\tFelfedezés éve: " + kemia[j].ev +"\n\tFelfedező: " + kemia[j].felf);
            }
            else
            {
                Console.WriteLine("Keresés\n\tNincs ilyen elem az adatforrásban!");
            }

            Console.Write("7. feladat: ");
            int max = -1;
            for (int i=0; i<sorok.Length-2;i++)
            {
                if (!(kemia[i].ev.Substring(1, 3) == "kor"))
                {
                    int bm = Convert.ToInt32(kemia[i].ev);
                    int bn = Convert.ToInt32(kemia[i+1].ev);
                    if ((bn-bm)>max)
                    {
                        max = bn - bm;
                    }
                }
            }
            Console.WriteLine(max + " év volt a leghosszabb időszak két elem felfedezése között.");

            Console.WriteLine("8. feladat: Statisztika");
            int x = 0;
            int[,] evek = new int[sorok.Length-1, 2];
            van = true;
            int d = 0;
            for (int i = 0; i < sorok.Length-1; i++)
            {
                if (!(kemia[i].ev.Substring(1, 3) == "kor"))
                {
                    van = true;
                    d = 0;
                    int dik = Convert.ToInt32(kemia[i].ev);
                    while (d < x && van)
                    {
                        if (evek[d, 0] == dik)
                        {
                            van = false;
                        }
                        d++;
                    }
                    if (van)
                    {
                        evek[x, 0] = dik;
                        x++;
                    }
                }
            }

            for (int i = 0; i < x; i++)
            {
                db = 0;
                for (int a = 0; a < sorok.Length-1; a++)
                {
                    if (!(kemia[a].ev.Substring(1, 3) == "kor"))
                    {
                        int dok = Convert.ToInt32(kemia[a].ev);
                        if (evek[i, 0] == dok)
                        {
                            db++;
                        }
                    }
                }
                evek[i, 1] = db;
            }


            for (int i = 0; i < x; i++)
            {
                if (evek[i,1]>3)
                Console.WriteLine("\t" + evek[i, 0] + ": " + evek[i, 1] + " db");
            }

            Console.ReadLine();
        }
    }
}
