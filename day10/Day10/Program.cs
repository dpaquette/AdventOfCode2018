﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day10
{
    class LightInTheSky
    {
        public int X;
        public int Y;
        public int XVelocity;
        public int YVelocity;
    }

    class Program
    {
        static int Seconds = 0;

        static void Main(string[] args)
        {
            var inputs = @"position=<-31667, -52902> velocity=< 3,  5>
position=< 21400, -31662> velocity=<-2,  3>
position=<-10471, -31665> velocity=< 1,  3>
position=< 21416, -21042> velocity=<-2,  2>
position=< 21383,  53291> velocity=<-2, -5>
position=< 53242, -42275> velocity=<-5,  4>
position=< 32050,  21438> velocity=<-3, -2>
position=<-31699,  53293> velocity=< 3, -5>
position=<-21045,  42670> velocity=< 2, -4>
position=<-31671,  32055> velocity=< 3, -3>
position=< 10781,  53290> velocity=<-1, -5>
position=<-10432, -10423> velocity=< 1,  1>
position=< 53266, -10418> velocity=<-5,  1>
position=< 42647,  10820> velocity=<-4, -1>
position=< 32039,  10813> velocity=<-3, -1>
position=< 53257, -42278> velocity=<-5,  4>
position=<-10422,  10815> velocity=< 1, -1>
position=< 21394, -52903> velocity=<-2,  5>
position=< 42621, -21045> velocity=<-4,  2>
position=<-10425, -31665> velocity=< 1,  3>
position=< 10804, -31657> velocity=<-1,  3>
position=< 32010, -10423> velocity=<-3,  1>
position=<-21061,  53291> velocity=< 2, -5>
position=<-10474,  10817> velocity=< 1, -1>
position=<-52946, -42284> velocity=< 5,  4>
position=< 10801, -31658> velocity=<-1,  3>
position=< 53245,  32054> velocity=<-5, -3>
position=< 21436, -10427> velocity=<-2,  1>
position=<-21077, -31656> velocity=< 2,  3>
position=<-42299, -10425> velocity=< 4,  1>
position=<-52942, -21042> velocity=< 5,  2>
position=<-21092, -10418> velocity=< 2,  1>
position=<-31688,  42676> velocity=< 3, -4>
position=< 32039,  42669> velocity=<-3, -4>
position=<-42330, -52903> velocity=< 4,  5>
position=<-21073,  21433> velocity=< 2, -2>
position=<-31676, -52894> velocity=< 3,  5>
position=<-31684, -10420> velocity=< 3,  1>
position=<-21061, -31663> velocity=< 2,  3>
position=<-31707, -31662> velocity=< 3,  3>
position=<-21085, -21044> velocity=< 2,  2>
position=<-42328, -42275> velocity=< 4,  4>
position=<-31694,  32054> velocity=< 3, -3>
position=<-31672,  21438> velocity=< 3, -2>
position=<-10426,  32053> velocity=< 1, -3>
position=<-42323,  53293> velocity=< 4, -5>
position=< 21383, -31658> velocity=<-2,  3>
position=< 53251,  42673> velocity=<-5, -4>
position=<-31707, -52902> velocity=< 3,  5>
position=< 53275,  42672> velocity=<-5, -4>
position=< 10772,  53291> velocity=<-1, -5>
position=< 21375, -21039> velocity=<-2,  2>
position=< 32019,  10813> velocity=<-3, -1>
position=<-21064, -52903> velocity=< 2,  5>
position=< 10756,  32054> velocity=<-1, -3>
position=< 21396,  53288> velocity=<-2, -5>
position=< 42642, -10427> velocity=<-4,  1>
position=<-42283,  32057> velocity=< 4, -3>
position=< 21391,  53290> velocity=<-2, -5>
position=<-21059, -10427> velocity=< 2,  1>
position=<-31704, -21038> velocity=< 3,  2>
position=<-10446,  42670> velocity=< 1, -4>
position=<-31715, -52897> velocity=< 3,  5>
position=<-52958,  21436> velocity=< 5, -2>
position=< 32030, -10426> velocity=<-3,  1>
position=<-42283, -52896> velocity=< 4,  5>
position=<-10424, -21046> velocity=< 1,  2>
position=< 53281, -42282> velocity=<-5,  4>
position=<-31696, -21045> velocity=< 3,  2>
position=< 10777, -10418> velocity=<-1,  1>
position=< 42618,  53292> velocity=<-4, -5>
position=< 42656, -21037> velocity=<-4,  2>
position=< 10812, -52903> velocity=<-1,  5>
position=<-42299, -52897> velocity=< 4,  5>
position=< 53288, -10423> velocity=<-5,  1>
position=<-10426,  21435> velocity=< 1, -2>
position=<-10446, -52897> velocity=< 1,  5>
position=<-10477,  32055> velocity=< 1, -3>
position=<-31699, -42275> velocity=< 3,  4>
position=<-21041, -31661> velocity=< 2,  3>
position=<-31668,  42670> velocity=< 3, -4>
position=<-31703, -42275> velocity=< 3,  4>
position=<-31684, -21039> velocity=< 3,  2>
position=<-31704,  42675> velocity=< 3, -4>
position=<-42291,  10811> velocity=< 4, -1>
position=<-10433,  53290> velocity=< 1, -5>
position=< 32047, -31656> velocity=<-3,  3>
position=< 10785,  32050> velocity=<-1, -3>
position=<-52934,  21438> velocity=< 5, -2>
position=< 10796,  21433> velocity=<-1, -2>
position=<-42304,  53296> velocity=< 4, -5>
position=< 10813,  32053> velocity=<-1, -3>
position=<-21101, -42279> velocity=< 2,  4>
position=< 42626, -10419> velocity=<-4,  1>
position=<-52933,  53290> velocity=< 5, -5>
position=<-21101, -10425> velocity=< 2,  1>
position=< 42624,  32053> velocity=<-4, -3>
position=< 21378,  32049> velocity=<-2, -3>
position=<-21050, -10423> velocity=< 2,  1>
position=<-42299, -10422> velocity=< 4,  1>
position=<-10442, -21040> velocity=< 1,  2>
position=< 42616,  53292> velocity=<-4, -5>
position=<-10466,  42671> velocity=< 1, -4>
position=<-10474,  10811> velocity=< 1, -1>
position=<-52950, -52897> velocity=< 5,  5>
position=<-42320, -10422> velocity=< 4,  1>
position=< 32010,  42675> velocity=<-3, -4>
position=< 53272, -10424> velocity=<-5,  1>
position=< 10792, -31662> velocity=<-1,  3>
position=< 53288, -10424> velocity=<-5,  1>
position=< 42629, -31657> velocity=<-4,  3>
position=<-52937,  10816> velocity=< 5, -1>
position=< 42618,  53295> velocity=<-4, -5>
position=< 32047,  32049> velocity=<-3, -3>
position=<-21045,  53293> velocity=< 2, -5>
position=<-21056,  10817> velocity=< 2, -1>
position=< 21420, -21043> velocity=<-2,  2>
position=< 42621, -52899> velocity=<-4,  5>
position=< 32002, -10427> velocity=<-3,  1>
position=< 53265,  42677> velocity=<-5, -4>
position=< 42631,  32049> velocity=<-4, -3>
position=<-21058,  32058> velocity=< 2, -3>
position=< 53291,  32049> velocity=<-5, -3>
position=<-31703, -21046> velocity=< 3,  2>
position=<-52953, -10422> velocity=< 5,  1>
position=< 32037, -42284> velocity=<-3,  4>
position=< 53232, -42280> velocity=<-5,  4>
position=< 53268,  32052> velocity=<-5, -3>
position=<-31715, -10423> velocity=< 3,  1>
position=< 32018, -42283> velocity=<-3,  4>
position=<-52929,  53295> velocity=< 5, -5>
position=<-21077, -21046> velocity=< 2,  2>
position=<-21061, -21037> velocity=< 2,  2>
position=< 32026, -10420> velocity=<-3,  1>
position=< 21427,  21436> velocity=<-2, -2>
position=<-10477, -10424> velocity=< 1,  1>
position=<-10473,  10815> velocity=< 1, -1>
position=< 32047, -31657> velocity=<-3,  3>
position=<-10473,  42677> velocity=< 1, -4>
position=<-52922, -42276> velocity=< 5,  4>
position=< 32034,  21434> velocity=<-3, -2>
position=< 42640, -52899> velocity=<-4,  5>
position=< 42669,  32058> velocity=<-4, -3>
position=<-31664, -31660> velocity=< 3,  3>
position=<-21100, -10422> velocity=< 2,  1>
position=< 42637,  53287> velocity=<-4, -5>
position=< 21388, -52897> velocity=<-2,  5>
position=<-21082,  21430> velocity=< 2, -2>
position=< 42658, -52897> velocity=<-4,  5>
position=< 42621,  42671> velocity=<-4, -4>
position=<-42281, -42280> velocity=< 4,  4>
position=<-10453, -21037> velocity=< 1,  2>
position=<-52950,  10813> velocity=< 5, -1>
position=<-21090, -10418> velocity=< 2,  1>
position=< 10796, -52897> velocity=<-1,  5>
position=< 42626, -42276> velocity=<-4,  4>
position=< 32014,  42673> velocity=<-3, -4>
position=<-31680,  42672> velocity=< 3, -4>
position=< 53289,  32053> velocity=<-5, -3>
position=< 21400,  10818> velocity=<-2, -1>
position=<-52918,  10812> velocity=< 5, -1>
position=< 42666, -21038> velocity=<-4,  2>
position=< 21411,  10815> velocity=<-2, -1>
position=< 32002,  42676> velocity=<-3, -4>
position=< 21375, -52895> velocity=<-2,  5>
position=<-21041, -42280> velocity=< 2,  4>
position=< 53248,  32056> velocity=<-5, -3>
position=<-21073, -21044> velocity=< 2,  2>
position=<-21099,  53292> velocity=< 2, -5>
position=<-31695, -52901> velocity=< 3,  5>
position=<-21080, -42277> velocity=< 2,  4>
position=< 53232,  32055> velocity=<-5, -3>
position=< 21394,  42668> velocity=<-2, -4>
position=< 21383,  42677> velocity=<-2, -4>
position=<-21088, -21045> velocity=< 2,  2>
position=< 21388,  42676> velocity=<-2, -4>
position=<-21061,  10812> velocity=< 2, -1>
position=< 21415,  21438> velocity=<-2, -2>
position=<-10439,  42677> velocity=< 1, -4>
position=< 10805, -42278> velocity=<-1,  4>
position=<-31701,  10816> velocity=< 3, -1>
position=<-21057, -31661> velocity=< 2,  3>
position=< 32015, -31657> velocity=<-3,  3>
position=<-21061, -10422> velocity=< 2,  1>
position=< 21410,  53296> velocity=<-2, -5>
position=< 21431, -21040> velocity=<-2,  2>
position=< 21408,  53296> velocity=<-2, -5>
position=<-31719, -42283> velocity=< 3,  4>
position=< 42633,  21435> velocity=<-4, -2>
position=<-52930, -52897> velocity=< 5,  5>
position=< 21391,  53292> velocity=<-2, -5>
position=< 42618, -10425> velocity=<-4,  1>
position=<-42295, -21046> velocity=< 4,  2>
position=< 42617, -10426> velocity=<-4,  1>
position=< 21420, -10421> velocity=<-2,  1>
position=< 42637, -42275> velocity=<-4,  4>
position=<-52899, -10423> velocity=< 5,  1>
position=<-31718, -52898> velocity=< 3,  5>
position=< 42639,  32053> velocity=<-4, -3>
position=<-21093, -42276> velocity=< 2,  4>
position=<-42303,  21432> velocity=< 4, -2>
position=<-21085,  53289> velocity=< 2, -5>
position=< 21426, -52899> velocity=<-2,  5>
position=<-21049, -21039> velocity=< 2,  2>
position=<-31700,  53287> velocity=< 3, -5>
position=< 21434, -31661> velocity=<-2,  3>
position=<-10437, -21038> velocity=< 1,  2>
position=< 32050, -52896> velocity=<-3,  5>
position=< 21411, -21044> velocity=<-2,  2>
position=<-52940, -52894> velocity=< 5,  5>
position=< 32006,  21439> velocity=<-3, -2>
position=< 21386, -42280> velocity=<-2,  4>
position=< 53277, -21044> velocity=<-5,  2>
position=<-52940,  42668> velocity=< 5, -4>
position=<-10441,  42672> velocity=< 1, -4>
position=<-10482, -31659> velocity=< 1,  3>
position=<-52931, -31661> velocity=< 5,  3>
position=<-10424, -52903> velocity=< 1,  5>
position=<-21089, -10423> velocity=< 2,  1>
position=<-10471, -52894> velocity=< 1,  5>
position=<-42322, -52903> velocity=< 4,  5>
position=< 42672, -21046> velocity=<-4,  2>
position=< 10797, -52903> velocity=<-1,  5>
position=< 42613,  42670> velocity=<-4, -4>
position=<-42334,  10820> velocity=< 4, -1>
position=< 42663, -10423> velocity=<-4,  1>
position=< 10772,  21432> velocity=<-1, -2>
position=< 21415, -31660> velocity=<-2,  3>
position=<-52918,  32056> velocity=< 5, -3>
position=<-52929,  42677> velocity=< 5, -4>
position=<-52958, -42281> velocity=< 5,  4>
position=<-52958, -10419> velocity=< 5,  1>
position=< 32005,  42672> velocity=<-3, -4>
position=<-52949,  53291> velocity=< 5, -5>
position=< 42631, -52894> velocity=<-4,  5>
position=< 21391, -52895> velocity=<-2,  5>
position=< 42656, -42284> velocity=<-4,  4>
position=<-10462,  21435> velocity=< 1, -2>
position=<-10479, -31660> velocity=< 1,  3>
position=<-52940, -21037> velocity=< 5,  2>
position=<-42279, -52903> velocity=< 4,  5>
position=< 53259, -52898> velocity=<-5,  5>
position=< 10792,  53292> velocity=<-1, -5>
position=<-42291,  21439> velocity=< 4, -2>
position=<-52902, -10422> velocity=< 5,  1>
position=<-42319,  21430> velocity=< 4, -2>
position=<-10446, -21045> velocity=< 1,  2>
position=< 21388, -42277> velocity=<-2,  4>
position=< 32026, -42276> velocity=<-3,  4>
position=< 42618,  10814> velocity=<-4, -1>
position=<-31696,  10812> velocity=< 3, -1>
position=< 53245,  21433> velocity=<-5, -2>
position=<-42294, -10419> velocity=< 4,  1>
position=<-10470, -52903> velocity=< 1,  5>
position=< 53272,  42675> velocity=<-5, -4>
position=<-21093,  53288> velocity=< 2, -5>
position=< 10807, -42280> velocity=<-1,  4>
position=<-21088, -21040> velocity=< 2,  2>
position=< 42625, -31661> velocity=<-4,  3>
position=< 21431, -52894> velocity=<-2,  5>
position=< 42621, -10427> velocity=<-4,  1>
position=< 32006, -42280> velocity=<-3,  4>
position=< 42654,  53287> velocity=<-4, -5>
position=<-10453,  42669> velocity=< 1, -4>
position=<-10438, -31661> velocity=< 1,  3>
position=<-42326, -52896> velocity=< 4,  5>
position=<-31684, -52897> velocity=< 3,  5>
position=<-10442, -10418> velocity=< 1,  1>
position=<-31720, -21042> velocity=< 3,  2>
position=< 10792,  21438> velocity=<-1, -2>
position=<-42338, -31660> velocity=< 4,  3>
position=< 53272, -42284> velocity=<-5,  4>
position=< 53277, -52898> velocity=<-5,  5>
position=< 10792, -52897> velocity=<-1,  5>
position=<-42297, -31656> velocity=< 4,  3>
position=<-42330, -31665> velocity=< 4,  3>
position=<-10471, -31665> velocity=< 1,  3>
position=< 10812, -42283> velocity=<-1,  4>
position=<-52945, -31663> velocity=< 5,  3>
position=<-10433,  32051> velocity=< 1, -3>
position=< 21432, -10427> velocity=<-2,  1>
position=< 53284,  10814> velocity=<-5, -1>
position=< 10816, -42284> velocity=<-1,  4>
position=<-10466, -31660> velocity=< 1,  3>
position=<-52953,  10815> velocity=< 5, -1>
position=<-42329, -42280> velocity=< 4,  4>
position=<-10442,  53290> velocity=< 1, -5>
position=< 10809, -42283> velocity=<-1,  4>
position=<-52916,  10815> velocity=< 5, -1>
position=< 53282,  53292> velocity=<-5, -5>
position=<-52958, -42279> velocity=< 5,  4>
position=< 21420,  42671> velocity=<-2, -4>
position=<-31691, -31664> velocity=< 3,  3>
position=<-42306,  32058> velocity=< 4, -3>
position=< 42618,  32051> velocity=<-4, -3>
position=< 53275, -10423> velocity=<-5,  1>
position=< 42614,  32050> velocity=<-4, -3>
position=<-31664, -10424> velocity=< 3,  1>
position=< 32019, -21040> velocity=<-3,  2>
position=< 42621,  21438> velocity=<-4, -2>
position=< 32022, -31658> velocity=<-3,  3>
position=< 21420,  10818> velocity=<-2, -1>
position=<-52929,  53296> velocity=< 5, -5>
position=< 42669, -10421> velocity=<-4,  1>
position=<-31712, -10420> velocity=< 3,  1>
position=<-42334,  10816> velocity=< 4, -1>
position=<-52923,  21439> velocity=< 5, -2>
position=< 10782, -10423> velocity=<-1,  1>
position=<-10442, -42284> velocity=< 1,  4>
position=<-52899,  10811> velocity=< 5, -1>
position=< 42653,  53288> velocity=<-4, -5>
position=< 21394, -31656> velocity=<-2,  3>
position=< 21431, -21038> velocity=<-2,  2>
position=<-31704,  21433> velocity=< 3, -2>
position=< 32013, -10418> velocity=<-3,  1>
position=<-21091,  10811> velocity=< 2, -1>
position=<-52958, -42281> velocity=< 5,  4>
position=<-42312, -21041> velocity=< 4,  2>
position=< 42622, -52899> velocity=<-4,  5>
position=< 42650,  10811> velocity=<-4, -1>
position=< 53280, -21045> velocity=<-5,  2>
position=<-21065,  42672> velocity=< 2, -4>
position=< 42657,  21434> velocity=<-4, -2>
position=<-10440, -42275> velocity=< 1,  4>
position=< 31997, -52898> velocity=<-3,  5>
position=<-42304, -10427> velocity=< 4,  1>
position=<-10458,  42676> velocity=< 1, -4>
position=<-42311,  10818> velocity=< 4, -1>
position=< 21396,  10812> velocity=<-2, -1>
position=<-31718, -52903> velocity=< 3,  5>
position=<-21101, -31656> velocity=< 2,  3>
position=<-21052,  10818> velocity=< 2, -1>
position=< 53277, -52900> velocity=<-5,  5>
position=< 42634, -31664> velocity=<-4,  3>
position=<-21101, -31662> velocity=< 2,  3>
position=<-31669,  10816> velocity=< 3, -1>
position=< 42615,  32054> velocity=<-4, -3>
position=<-10442, -42275> velocity=< 1,  4>
position=<-21075,  10815> velocity=< 2, -1>
position=< 10776,  53295> velocity=<-1, -5>
position=<-52942, -42283> velocity=< 5,  4>
position=<-21053, -21046> velocity=< 2,  2>
position=< 32050, -10420> velocity=<-3,  1>
position=< 42669,  21433> velocity=<-4, -2>
position=<-10446, -31660> velocity=< 1,  3>
position=< 53240,  10816> velocity=<-5, -1>
position=<-21045, -31661> velocity=< 2,  3>
position=< 10756,  42676> velocity=<-1, -4>
position=< 21417,  10815> velocity=<-2, -1>
position=< 10780,  32057> velocity=<-1, -3>
position=<-52953, -52896> velocity=< 5,  5>
position=<-42339, -21039> velocity=< 4,  2>
position=< 10781, -42277> velocity=<-1,  4>
position=< 10801,  21431> velocity=<-1, -2>
position=< 31995,  32050> velocity=<-3, -3>
position=<-10446, -31664> velocity=< 1,  3>
position=<-10457,  21436> velocity=< 1, -2>
position=<-10434, -42283> velocity=< 1,  4>
position=<-31707, -42277> velocity=< 3,  4>
position=<-42298,  21439> velocity=< 4, -2>
position=<-21052,  53293> velocity=< 2, -5>
position=<-10447,  42668> velocity=< 1, -4>
position=< 42617,  42673> velocity=<-4, -4>
position=<-52922,  21430> velocity=< 5, -2>
position=<-31661, -52899> velocity=< 3,  5>".Split(Environment.NewLine);

            var lights = new List<LightInTheSky>();
            foreach (var input in inputs)
            {
                var regex = new Regex(@"(-?\d+)");
                var matches = regex.Matches(input);
                lights.Add(new LightInTheSky
                {
                    X = Int32.Parse(matches[0].Value),
                    Y = Int32.Parse(matches[1].Value),
                    XVelocity = Int32.Parse(matches[2].Value),
                    YVelocity = Int32.Parse(matches[3].Value)
                });
            }
            Console.WriteLine("Fast-Forwarding");
            DisplayLights(lights);
            while (lights.Max(l => l.Y) - lights.Min(l => l.Y) > 40)
            {
                MoveForward(lights);
            }
            DisplayLights(lights);
            while (true)
            {
                var consoleLine = Console.ReadKey();
                if (consoleLine.KeyChar == 'q')
                {
                    break;
                }
                else if (consoleLine.KeyChar == 'f')
                {
                    MoveForward(lights);
                }
                else if (consoleLine.KeyChar == 'b')
                {
                    MoveBackwards(lights);
                }
                DisplayLights(lights);
                

            }
            
            
            Console.ReadLine();
        }

        static void MoveForward(List<LightInTheSky> lights)
        {
            foreach (var light in lights)
            {

                light.X += light.XVelocity;
                light.Y += light.YVelocity;
            }

            Seconds += 1;
        }

        static void MoveBackwards(List<LightInTheSky> lights)
        {
            foreach (var light in lights)
            {

                light.X -= light.XVelocity;
                light.Y -= light.YVelocity;
            }

            Seconds -= 1;
        }

        static void DisplayLights(List<LightInTheSky> lights)
        {
            Console.Clear();
            var minX = -lights.Min(l => l.X);            
            var minY = -lights.Min(l => l.Y)+3;

            Console.SetCursorPosition(1, 1);
            Console.Write("Seconds: {0}", Seconds);
            foreach (var light in lights)
            {
                var x = light.X + minX;
                var y = light.Y + minY;
                if (x < Console.BufferWidth && y < Console.BufferHeight)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("#");
                }
                
            }

        }
    }


}
