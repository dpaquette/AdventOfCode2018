﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Day13
{
    class MapState
    {
        public MapElement Element;
        public CartState Cart;
    }



    enum MapElement
    {
        Vertical,
        Horizontal,
        LeftCurve,
        RightCurve,
        Intersection,
        Empty
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    class CartState
    {
        public int X;
        public int Y;
        public Direction Direction;
        public int IntersectionCount = 0;
    }

    class Program
    {
        protected internal static MapState[,] MapGrid;
        protected internal static List<CartState> Carts;

        static void Main(string[] args)
        {
            var inputLines =
@"                   /----------\                                                                                                /--------------------\ 
             /-----+----------+-------------->\                                                                                |                    | 
             |     |          |               |                        /---------------------------------------------------\   |                    | 
/------------+-----+----------+----------\    |                        |                                                   |   |                    | 
|            |     |          | /--------+----+-------------------\    |               /--------------------\              |   |                    | 
|            |     |          | |        |    |                   |    |               |              /-----+--------------+--\|                    | 
|  /---------+-----+------\   | |        |    |                   |    |               |              |     |              |  ||                    | 
|  |      /--+-----+------+---+-+--------+----+-------------\     |    |               |              |     |              |  ||                    | 
|  |      |  |     |      |   | |    /---+----+-------------+-----+----+---------------+-------\      |     |              |  ||  /-------------\   | 
|  |      |  |     |     /+---+-+----+---+----+--\       /--+-----+----+---------------+-------+------+-----+--------------+--++--+-------------+-\ | 
|  |     /+--+-----+-----++---+-+----+---+----+--+-------+--+-----+----+---------------+--\    |      |     |              |  ||  |             | | | 
|  |     ||  |     |     ||   | |    |   v    |  |       |/-+-----+----+---------------+--+----+\     |     |              |  ||  |             | | | 
|  |     ||  |   /-+-----++---+-+----+---+----+--+-------++-+-----+----+---------------+--+--\ ||     |     |              |  ||  |             | | | 
|  |     ||  |   | |     ||   | |    |   |    |  |       || |     |    |               |  |  | ||     |     |              |  ||  |             | | | 
|  |     ||  |   | |     ||   | |    |   |    |  |   /---++-+-----+----+----------\    |  |  | ||/----+----\|              |  ||  |             | | | 
|  |     ||  |   | |     ||   | |    |   |    |  |   |   || |     |    |          | /--+--+--+-+++----+----++--------------+--++--+-------------+\| | 
|  |     ||  |   | |     ||   | \----+---+----+--+---+---++-+-----/    |          | |  |  |  | |||    |    ||              |  ||  |             ||| | 
|  |     ||  |   |/+-----++---+------+---+----+--+---+---++-+-\        |          | |  |  |  | |||    |    ||/-------------+--++--+---\         ||| | 
|  |     ||  |   ^||     ||   |      |/--+----+-\|   |   || | |        |          | | /+--+--+-+++----+----+++-------------+--++--+---+-------\ ||| | 
|  |     ||  \---+++-----++---+------++--+----/ ||   |   || | |   /----+----------+-+-++--+--+\|||    |    |||             |  |\--+---+-------+-+++-/ 
|  |     ||      |||     ||   |      ||  |      ||   |   || | |   |    |        /-+-+-++--+--+++++----+----+++-------------+--+-\ |   |       | |||   
|  |     ||      ||| /---++---+------++--+------++---+\  || | |   |    |/-------+-+-+-++--+--+++++-<--+----+++------\      |  | | |   |       | |||   
|  | /---++------+++-+---++---+------++--+\     ||  /++--++-+-+---+----++-------+-+-+-++--+--+++++----+----+++------+------+--+-+-+---+-----\ | |||   
|  | |   ||      |||/+---++---+------++--++-----++--+++--++-+-+\  |    ||       |/+-+-++--+--+++++----+----+++------+------+--+-+-+---+--\  | | |||   
|  | |   ||      |||||   \+---+------++--++-----+/  |||  || | ||  |    ||       ||| | || /+--+++++----+----+++------+------+--+-+-+--\|  |  | | |||   
|  | |   ||      |||||    |   |      ||  ||     |   |||  || |/++--+----++-------+++-+-++-++--+++++----+----+++------+------+--+\| |  ||  |  | | |||   
|  | |   ||      |||||    |   |      ||  ||  /--+---+++--++-++++--+----++-------+++-+-++-++--+++++--->+----+++------+------+--+++-+--++--+--+-+-+++--\
|  | |  /++------+++++----+---+------++--++--+--+---+++--++-++++--+---\||  /----+++-+-++-++--+++++----+----+++------+------+--+++-+--++--+--+-+-+++\ |
|  | |  |||      |||||    |   |      ||  ||  |  |   |||  || ||||  |   |||  |    ||| | || ||  |||||    |    |||      |      |  ||| |  ||  |  | | |||| |
|  | |  |||      |||||    |   |      ||  ||/-+--+---+++--++-++++--+\  |||  |    ||| | || ||  |||||    |    |||      |      |  ||| |  ||  |  | | |||| |
|  | |  ||| /----+++++----+---+------++--+++-+--+---+++--++-++++-\||  |||  |    ||| | || ||  |||||    |    |||      |      |  ||| |  ||  |  | | |||| |
|/-+-+--+++-+----+++++----+---+------++--+++\| /+---+++--++-++++-+++--+++--+----+++-+-++-++--+++++----+----+++------+----\ |  ||| |  ||  |  | | |||| |
|| | |  ||| |    |||||    | /-+------++--+++++-++---+++\ || |||| |||  |||/-+----+++-+-++\||  |||||  /-+----+++-\    |    | |  ||| |  ||  |  | | |||| |
|| | | /+++-+----+++++-\  | | |      ||/-+++++-++---++++-++-++++-+++--++++-+----+++-+-+++++--+++++->+-+----+++-+----+---\| |  ||| |  ||  |  | | |||| |
|| | | |||| |    ||||| |  | | |     /+++-+++++-++---++++-++-++++-+++-\|||| |    ||| | |||||  |||||  | |    ||| |    |   || |  ||| |  ||  |  | | |||| |
|| | | |||| |    ||||| |  | | |     |||| ||||| ||   |||| || |||| ||| ||||| |    ||| | |||\+--+++++--+-+----+++-+----+---++-+--+++-+--/|  |  | | |||| |
|| | | |||| |    ||||| |  | | |     |||| ||||| v|   |||| || |||| ||| ||||| |    ||| | ||| |  |||||  | \----+++-+----+---++-+--/|| \---+--+--+-+-/||| |
|| | | |||| |    ||||| |  | | |     |||| ||||| ||   \+++-++-++++-+++-+++++-+----+++-+-+++-+--+++++--+------+++-+----+---++-+---++-----+--+--/ |  ||| |
|| | | |||| |    ||||| |  | | |     |||| ||||| ||    ||| || |||| ||| ||||| |    ||| | |||/+--+++++--+----\ ||| |    |   || |   ||     |  |    |  ||| |
|| | | |||| |    ||||| |  | | |     |||| |||||/++----+++-++-++++-+++-+++++-+----+++-+-+++++--+++++--+----+-+++-+----+--\|| |   ||     |  |    |  ||| |
|| | | |||| |   /+++++-+--+-+-+-\   ||||/++++++++----+++-++-++++-+++-+++++-+----+++-+-+++++--+++++--+----+-+++\|    |  ||| |   ||     |  |    |  ||| |
|| | |/++++-+---++++++-+--+-+-+-+---+++++++++++++----+++-++-++++-+++\||||| |    ||| | |||||  |||||  |    | |||||    |  ||| |   ||     |  |    |  ||| |
|| | |||||| |   |||||| |  | | |/+---+++++++++++++----+++-++-++++-+++++++++-+----+++-+-+++++--+++++\ |    | |||||    |  |||/+---++---\ |  |    |  ||| |
|| | ||||\+-+---++++++-+--+-+-+++---+++++++++++++----+++-++-++++-+++++++++-+----+++-+-++++/  |||||| |    | |||||    |  |||||   ||   | |  |    |  ||| |
|| | \+++-+-+---++++++-+--+-+-+++---++++++/||||||    ||| || |||| ||||||||| |    ||| | |\++---++++++-+----+-+/|||    |/-+++++---++---+-+--+--\ |  ||| |
|| |  ||| | |   |||||| |  | | |||   |||||| ||||\+----+++-++-++++-+++++++++-+----+++-+-+-++---++++++-+----+-+-+++----++-++/||   ||   | |  |  | |  ||| |
|| |  ||| | |/--++++++-+--+-+-+++---++++++-++++-+---\||| || |||| |||||||^| |    ||| | | ||   |||||| |    | | |||    || || ||   ||   | |  |  | |  ||| |
||/+--+++-+-++--++++++-+--+-+-+++-\ |||||| |||| |   |||| || |||| ||||||||| |    ||| | | ||   |||||| |    | | |||    || || ||   ||   | |  |  | |  ||| |
|||^  ||| |/++--++++++-+--+-+-+++-+-++++++-++++-+---++++-++-++++-+++++++++-+----+++-+-+-++---++++++-+----+-+-+++-\  || || ||   ||   | |  |  | |  ||| |
||||  ||| ||||  |||\++-+--+-+-/||/+-++++++-++++-+---++++-++-++++-+++++++++-+-\  \++-+-+-++---++++++-+----+-+-+++-+--++-++-++---+/   | |  |  | |  ||| |
||||  ||| ||||  ||| || |  | |  |||| |||||| |||| |   |||| || |||| ||||||||| | |/--++-+-+-++---++++++-+----+-+-+++-+--++-++-++\  |    | |  |  | |  ||| |
||||  ||| ||||  ||| || |/-+-+--++++-++++++-++++-+---++++-++-++++-+++++++++-+-++--++-+-+-++---++++++-+----+-+-+++-+-\|| || |||  |    | |  |  | |  ||| |
||||  ||| ||||  ||| || || | |  |||| |||||| |||| |   |||| || |||| ||||||||| | ||  || | | ||   |||||| |    | | ||| | ||| || |||  |    | |  |  | |  ||| |
||||  ||| ||||  ||| || || | |  |||| |||||| |||| |   |||| || |||| ||||||||| | ||  || | | ||   |||||| |    | | ||| | ||| || |||  |    | |  |  | |  ||| |
||||  ||| ||||  ||| || || | |  |||| |||||| ||||/+---++++-++-++++-+++++++++-+-++--++-+-+-++---++++++-+----+-+-+++-+-+++-++-+++--+----+-+--+--+\|  ||| |
||||  ||| ||||  ||| || || | |  |||| |||||| ||||||   |||| || |||| ||||||||| | ||  || | | ||   |||||| |    | | ||| | ||| || |||  |    | |  |  |||  ||| |
\+++--+++-++++--+++-++-++-+-+--++++-+++++/ ||||||   |||| || |\++-+++++++++-+-++--++-+-+-++---++++++-+----+-+-+++-+-+++-++-+++--/    | |  |  |||  ||| |
 |||  ||| |\++--+++-++-++-+-+--++++-+++++--++++++---++++-++-+-++-+++++++++-+-++--++-+-+-++---++++++-+----+-+-+++-/ ||| || |||       | |  |  |||  ||| |
/+++--+++-+-++--+++-++-++-+-+--++++-+++++--++++++---++++-++-+\|| ||||||||| \-++--++-+-+-++---++++++-+----+-+-+++---+++-++-+++-------+-+--+--+++--++/ |
||||  ||| | ||  ||| \+-++-+-+--++++-+++++--++++++---++++-++-+++/ |||||||||   ||  || | | ||   |||||| |    | | \++---+++-++-+++-------+-/  |  |||  ||  |
||||  ||| | ||  |||  | || | |  |||| |||||  ||||||   |||| || |||  |||||||||   ||  || | | ||   |||||| |    | |  ||   ||| || |||       |    |  |||  ||  |
||||  ||| | ||  |||  | || | |  |||| |||\+--++++++---++++-++-+++--+++++++++---++--++-+-+-++---++++++-+----+-+--++---+++-+/ |||   /---+----+--+++--++-\|
||||  ||| | ||  |||  | || | |  |||| ||| |  ||||||   |||| || |||  |||||||||   ||  || | | ||   |||||| |    | |  ||/--+++-+--+++---+---+--\ |  |||  || ||
||||  ||| | ||  |||/-+-++-+-+--++++-+++-+--++++++---++++-++-+++--+++++++++--\||  || | \-++---++++++-+----+-+--+++--+++-+--+++---+---+--+-+--++/  || ||
||||  ||| | ||  ||v| | || | |  |||| ||| |  \+++++---++++-++-+++--++/||||||  |||  || |   |\---++++++-+----/ |  |||  ||| |  |||   |   |  | |  ||   || ||
||||/-+++-+-++--++++-+-++-+-+--++++-+++-+---+++++---++++-++-+++--++-++++++--+++--++-+---+----++++++\|      |  |||  ||| |  |||   |   |  | |  ||   || ||
||||| ||| | ||  |||| | || | |  |||| |\+-+---+++++---++++-++-+++--++-++++++--+++--++-+---+----++/|||||      |  |||  ||| |  |||   |   |  | |  ||   || ||
||||| ||| | ||  |||| | || | |  |||| | | |   |||||   |||| || ||| /++-++++++--+++--++-+---+----++-+++++------+--+++-\||| |  |||   |   |  | |  ||   || ||
||||| ||| | ||  |||| | || | |/-++++-+-+-+---+++++---++++-++-+++-+++-++++++--+++--++-+---+----++-+++++---\  | /+++-++++-+\ |||   |   |  | |  ||   || ||
||||| ||| | ||  |||| | || | || |||| | | |   |||||   |||| || ||| ||| ||||||  |||  || |   |    || ||||\---+--+-++/| |||| || |||   |   |  | |  ||   || ||
|||\+-+++-+-++--++++-+-++-//++-++++-+-+-+---+++++---++++-++-+++-+++-++++++--+++<-++-+---+----++-++++----+--+\|| | |||| || |||   |   |  | |  ||   || ||
||| | ||| | ||  |||| | ||/-+++-++++-+-+-+---+++++---++++-++-+++-+++-++++++--+++\ || |   |    || ||||    |  |||| | |||| || |||   |   |  | |  ||   || ||
||| | ||| | ||  |||| | ||| ||| |||| | | |   |||||   |||| || ||| ||| ||||||  |||| || |   |    ||/++++----+--++++-+-++++-++-+++---+\  |  | |  ||   || ||
||| | ||| | ||  |||| | |||/+++-++++-+-+-+---+++++---++++-++-+++-+++-++++++--++++-++-+---+----+++++++----+--++++-+-++++-++-+++--\||  |  | |  ||   || ||
||| | ||| | ||  |||| | ||||||| |||| | | |   |||||   |||| || ||| ||| ||||||  |||| || |   |    ||||||| /--+--++++\| |||| || |||  |||  |  | |  ||   || ||
||| | ||| | ||  |||| | ||||||| |||| | | | /-+++++---++++-++-+++-+++-++++++--++++-++-+---+----+++++++-+--+--++++++-++++-++-+++--+++--+-\| |  ||   || ||
||| | ||| | ||  |||| | ||||||| |||| | | | | |||||   |||| || ||| ||| ||||||  |||| \+-+---+----+++++++-+--+--++++++-++++-++-+++--+++--+-++-/  ||   || ||
||| | ||| | ||  |\++-+-+++++++-++++-+-+-+-+-+++++---++++-++-+++-+++-++++++--++++--+-+---+----/||||||/+--+--++++++-++++-++-+++--+++--+-++----++-\ || ||
||| | ||| | ||  | || | ||||||| |||| | |/+-+-+++++---++++-++-+++<+++-++++++--++++--+-+\  |     ||||||||  |  |||||| |||| || |||  |||  | ||    || | || ||
||| | ||| | ||  | || \-+++++++-++++-+-+++-+-+++++---++/| || ||| \++-++++++--++++--+-++--+-----++++++++--+--++++++-/||\-++-+++--+++--+-++----/| | || ||
||| | ||| | ||  | ||   ||||||| |||| | ||| | |||||   || |/++-+++--++-++++++--++++--+-++--+-----++++++++--+--++++++--++--++-+++--+++\ | ||     | | || ||
||| | ||| | ||  | ||   ||||||| |||| | ||| | |||||   |\-++++-+++--++-++++++--++++--/ ||  |     |\++++++--+--++++++--++--++-+++--++/| | ||     | | || ||
|\+-+-+++-+-++--+-++---+++++++-++++-+-+++-+-/||||   |  |||| |||  || ||||||  ||\+----++--+-----+-++++++--+--++++++--++--++-++/  || | | ||     | | v| ||
| | | ||| | ||  | ||   |||||||/++++-+-+++-+--++++---+\ |||| |||  ||/++++++--++-+----++--+-----+-++++++--+--++++++--++--++-++---++-+-+\||     | | || ||
| | | ||| | ||  | ||   |||||||||||| | ||| |  ||||   || |||| |||  |||||||||  || |    ||  |     | ||||||  |  ||||||  ||  || || /-++-+-++++-\   | | || ||
| | | ||| | ||  | ||   |||||||||||| | ||| |  ||||   ||/++++-+++--+++++++++--++-+----++--+-----+-++++++--+--++++++--++--++-++-+-++-+-++++-+\  | | || ||
| | | ||| \-++--+-++---++++++++++++-+-+++-+--++++---+++++++-/||  |||||||||  || |    ||  |   /-+-++++++--+--++++++--++--++-++-+-++-+-++++-++\ | | || ||
| | | |||   ||  | ||   |||||||||||| | ||| |  ||||   |||||||  ||  |||||||||  || |    ||  |   | | ||||||  |  ||||||  ||  || || | || | |||| ||| | | || ||
| | | |||   ||  | ||   ||||||||\+++-+-+++-+--++++---+++++++--++--+++++++++--++-+----++--+---+-+-++/|||  |  ||||||  ||  || || | || | |||| ||| | | || ||
| | | |||   ||  | ||   |||||||| ||| | ||| |  ||||  /+++++++--++--+++++++++--++-+----++--+-->+-+\|| |||  |  ||||||  ||  || \+-+-++-+-/||| ||| | | || ||
| \-+-+++---++--+-++---++++++++-++/ | ||| |  ||||  ||||||||  ||  |\+++++++--++-+----++--+---+-/||| |||  |  ||||||  ||  ||  | | |\-+--+++-+++-+-+-++-/|
|   | |||   ||  | ||   ||||||\+-++--+-+++-+--++++--++++++++--++--+-+++++++--++-+----++--+---+--+++-+++--/  ||||||  ||  ||  | | |  |  ||| ||| | | ||  |
|   | |||   ||  | ||   ||||||/+-++--+-+++-+--++++--++++++++--++--+-+++++++--++-+----++--+--\|  ||| |||     ||||||  ||  ||  | | |/-+--+++-+++-+\| ||  |
|   | |||   || /+-++---++++++++-++--+-+++-+--++++--++++++++--++--+-+++++++--++-+----++--+--++--+++-+++-----++++++--++--++-\| | || |  ||| ||| ||| ||  |
|   | |||   || || ||   ||\+++++-++--+-+++-+--++++--++++++++--++--+-+++++++--++-/    ||  |  ||  |||/+++-----++++++--++--++-++\| || |  ||| ||| ||| ||  |
|   | |||   || || ||   || ||||| ||  | ||| |  ||||  ||||||||  ||  | |||||||  ||      ||  |  ||  |||||||     ||||||  ||  || |||| || |  ||| ||| ||| ||  |
|   | |||   || || ||   || ||||| ||  | ||| |  ||||  |||\++++--++--+-+++++++--++------++--+--++--+++++++-----++++++--++--++-++++-++-+--+++-+/| ||| ||  |
|   | |||   || || ||   || \++++-++--+-+++-+--++++--+++-++++--++--+-+++++++--++------++--+--++--+++++++-----++++++--++--++-++++-/| |  ||| | | ||| ||  |
|   \-+++---++-++-++---++--++++-++--+-+++-+--++++--+++-++++--++--+-+++++++--++------++--+--++--++++/\+-----++++++--++--++-++++--+-+--+++-+-+-++/ ||  |
|     |||   || || ||   ||  |||| |\--+-+++-+--++++--+++-++++--++--+-+++++++--+/      ||  |  ||  ||||  |     ||||||  ||  || ||||  | |  ||| | | ||  ||  |
|     |||   || || ||   ||  |||| |   | ||| |  ||||  ||| ||\+--++--+-+++++++--+-------++--+--++--++++--+-----++++++--++--++-++++--+-+--+++-+-+-++--+/  |
|     |||   || || ||   ||  |||| |   | ||| |  ||||  ||| || |  ||  | |||||||  |       \+--+--++--++++--+-----++++++--++--++-++++--+-+--+++-+-+-++--/   |
|     |||   || || ||   ||  |||| |   | ||| |  ||||  \++-++-+--++--+-+++++++--+--------+--+--++--/||^  |     ||||||  ||  || ||||  | |  ||| | | ||      |
|     |||   || |\-++---++--++++-/   | |\+-+--++++---++-++-+--++--+-+++++++--+--------/  |  ||   ||\--+-----++++++--++--++-++/|  | |  ||| | | ||      |
|     |||   || |  |\---++--++++-----+-+-+-+--++++---++-++-+--++--+-+++++++--/           |  ||   ||   |     ||||||  ||  || || |  | |  ||| | | ||      |
|/----+++-\ || |  |    ||  \+++-----+-+-+-+--++++---++-++-+--++--+-+++++++--------------+--++---++---+-----+/||||  ||  || || |  | |  ||| | | ||      |
||    ||| | || |  |    ||   \++-----+-+-+-+--++++---++-/| |  ||  | |||||||              |  |\---++---+-----+-++++--++--++-++-+--+-+--+++-+-/ ||      |
||    ||| | || |  |    ||    ||     | | | |  ||||   ||  | |  ||  | |||||||              |  |    ||   |     | ||||  ||  || || |  | |  ||| |   ||      |
||    ||| | || |  |    ||    ||     | | | |  ||||   ||  | |  ||  | ||||\++--------------+--+----++---+-----+-++++--++--++-+/ |  | |  ||| |   ||      |
||    ||| | || |  |    ||    \+-----+-+-+-+--++++---++--+-+--++--+-++++-++--------------+--/    ||   |     | ||||  ||  || |  |  | |  ||| |   ||      |
||    ||| | || |  |    ||     |     | | \-+--++++---++--+-+--++--+-++++-++--------------+-------++---+-----+-+/||  ||  || |  |  | |  ||| |   ||      |
||    ||| | || |  |    ||     |     | |   |  ||||   ||  | |  ||  | |||| ||              |       ||   |     | | ||  ||  || |  |  | |  ||| |   ||      |
||    ||| | || |  |    ||     |     | |/--+--++++---++--+-+--++--+-++++-++------\       |       ||   |     | | ||  ||  || |  |  | |  ||| |   ||      |
||    ||| | \+-+--+----++-----+-----+-++--+--++++---++--+-+--++--/ |||| ||      |       |       ||   |     | | ||  ||  || |  |  | |  ||| |/--++-----\|
||    ||| |  | |  |    ||     |     | ||  |  ||||   ||  | \--++----++++-++------+-------+-------/|   |     | | ||  ||  || |  |  | |  ||| ||  ||     ||
|\----+++-/  | |  |    ||     |   /-+-++--+--++++---++--+----++----++++-++------+-------+--------+---+-----+-+-++--++--++-+--+--+-+--+++-++-\||     ||
|     |||    | |  \----++-----+---+-+-++--+--++++---++--+----+/    |||| ||      |       |        |   |     | | ||  ||  || |  |  \-+--+++-++-++/     ||
|     |||    | |       ||     |   | | ||  |  ||||   ||  |    |     |||| ||      |       |        |   |     | \-++--++--+/ |  |    |  ||| || ||      ||
|     |||    | |       ||     |   | \-++--+--++++---++--+----+-----++/| ||      |       |        |   |     |   ||  ||  |  |  |    |  ||| || ||      ||
|     |||    | |       |\-----+---+---++--+--++++---++--+----+-----++-+-++------+-------+--------+---+-----+---++--/|  |  |  |    |  ||| || ||      ||
|     |||    | |       |      |   |   ||  |  \+++---++--+----+-----++-+-++------+-------+--------+---+-----+---++---+--+--+--+----+--+++-++-++------+/
|     ||\----+-+-------+------+---+---++--+---+++---++--+----+-----++-/ ||      |       |        |/--+-----+---++---+--+--+--+----+\ ||| || ||      | 
\-----++-----+-+-------+------+---+---++--+---+++---++--+----/     ||   ||      |       |        \+--+-----/   ||   |  |  |  |    || ||| || ||      | 
      ||     | |       |/-----+---+---++--+---+++---++--+----------++---++------+-------+---------+-\|         |\---+--+--+--+----++-++/ || ||      | 
      ||     | |       ||     |   |   \+--+---++/   ||  |   /------++---++------+-------+---------+-++---------+----+--+--+--+-\  || ||  || ||      | 
      ||     | |       ||     |   |    |  |   ||    ||  |   |      ||   ||      |       |         | ||         ^    |  |  |  | |  || ||  || ||      | 
      ||     \-+-------++-----+---+----+--+---++----/|  |   |      ||   ||      |       |         | ||         |    |  |  |  | |  || ||  || ||      | 
      ||       |       ||     |   |    |  \---++-----+--+---+------++---++------+-------+---------+-++---------+----+--+--+--+-+--++-+/  || ||      | 
      ||       |       ||     |   |    |      ||     |  |   |      ||   \+------+-------+---------+-++---------+----/  |  |  | |  || |   || ||      | 
      ||       |       ||     \---+----+------++-----/  |   |      ||    |      |       |         | ||/--------+-------+--+--+-+--++-+---++-++-----\| 
      ||       |       ||         |    |      ||        |/--+------++----+------+-------+---------+-+++--------+-------+--+--+-+--++-+---++-++-\   || 
    /-++-------+-------++---------+----+------++--------++--+------++----+------+-------+---------+-+++------\ |       |  |  | |  || |   || || |   || 
    | ||    /--+-------++---------+----+------++--------++--+------++----+------+-------+--\      \-+++------+-+-------+--+--+-+--+/ |   || || |   || 
    | ||    |  |       ||         |    |      |\--------++--+------++----+------+-------+--+--------+++------+-+-------+--+--+-+--+--+---++-+/ |   || 
    | ||    |  |       |\---------+----+------+---------++--+------++----+------+-------+--+--------/||      | |       |  |  | |  |  |   || |  |   || 
    | ||    |  |       |          |    |      |         ||  |      ||    \------+-------/  |         \+------+-/       |  |  | |  |  |   || |  |   || 
    | ||    |  |       |          |    |      |  /------++--+------++-----------+----------+----------+------+---------+--+--+-+--+-\|   |\-+--+---+/ 
    | ||    |  |       |          |    |      \--+------++--+------++-----------+----------+----------+------+---------/  |  | |  | ||   |  |  |   |  
    | ||    |  |       |          |    |         |      \+--+------++-----------+----------+----------+------+------------+--+-+--/ ||   |  |  |   |  
    | |\----+--+-------/          |    |         |       \--+------++-----------+----------+----------+------+------------+--+-+----++---+--+--/   |  
    | |     |  |                  |    \---------+----------+------++-----------/          |          |      |            |  | |    ||   |  |      |  
    | |     \--+------------------+--------------+----------+------++----------------------/          |      |            |  | |    ||   |  |      |  
    | |        \------------------+--------------+----------+----->++---------------------------------+------+------------/  | |    ||   |  |      |  
    | |                           |              |          |      ||                                 |      |               \-+----++---/  |      |  
    \-+---------------------------+--------------+----------+------++---------------------------------+------/                 |    ||      |      |  
      |                           \--------------+----------+------++---------------------------------+------------------------+----++------/      |  
      \------------------------------------------+----------+------+/                                 \------------------------+----++-------------/  
                                                 |          \------+-----------------------------------------------------------/    ||                
                                                 |                 \----------------------------------------------------------------+/                
                                                 \----------------------------------------------------------------------------------/                 ".Split(Environment.NewLine);

            MapGrid = new MapState[inputLines[0].Length, inputLines.Length];

            Carts = new List<CartState>();

            for (var x = 0; x < inputLines[0].Length; x++)
            {

                for (int y = 0; y < inputLines.Length; y++)
                {
                    var inputLine = inputLines[y];
                    MapGrid[x, y] = new MapState();
                    char point = (char)inputLine[x];
                    switch (point)
                    {
                        case '/':
                            MapGrid[x, y].Element = MapElement.RightCurve;
                            break;
                        case '\\':
                            MapGrid[x, y].Element = MapElement.LeftCurve;
                            break;
                        case '-':
                            MapGrid[x, y].Element = MapElement.Horizontal;
                            break;
                        case '|':
                            MapGrid[x, y].Element = MapElement.Vertical;
                            break;
                        case '+':
                            MapGrid[x, y].Element = MapElement.Intersection;
                            break;
                        case '>':
                            MapGrid[x, y].Cart = new CartState { Direction = Direction.Right, X = x, Y = y }; ;
                            Carts.Add(MapGrid[x, y].Cart);
                            MapGrid[x, y].Element = MapElement.Horizontal;
                            break;
                        case '<':
                            MapGrid[x, y].Cart = new CartState { Direction = Direction.Left, X = x, Y = y }; ;
                            Carts.Add(MapGrid[x, y].Cart);
                            MapGrid[x, y].Element = MapElement.Horizontal;
                            break;
                        case 'v':
                            MapGrid[x, y].Cart = new CartState { Direction = Direction.Down, X = x, Y = y }; ;
                            Carts.Add(MapGrid[x, y].Cart);
                            MapGrid[x, y].Element = MapElement.Vertical;
                            break;
                        case '^':
                            MapGrid[x, y].Cart = new CartState { Direction = Direction.Up, X = x, Y = y }; ;
                            Carts.Add(MapGrid[x, y].Cart);
                            MapGrid[x, y].Element = MapElement.Vertical;
                            break;
                        default:
                            MapGrid[x, y].Element = MapElement.Empty;
                            break;
                    }
                }
            }

            PrintMapState();
            AdvanceCarts();
            var count = 1;
            while (Carts.Count > 1)
            {
                              
                AdvanceCarts();                
                count++;                
            }


            Console.WriteLine("Welcome to the tunnels");
            PrintMapState();
            Console.WriteLine("Location of last cart: {0},{1}", Carts[0].X, Carts[0].Y);
            Console.ReadLine();
        }

        static void AdvanceCarts()
        {
            var cartsToAdvance = Carts.OrderBy(c => c.X).ThenBy(c => c.Y).ToList();
            while (cartsToAdvance.Any())
            {
                var cart = cartsToAdvance.First();
                cartsToAdvance.RemoveAt(0);
                
                var currentLocation = MapGrid[cart.X, cart.Y];
                currentLocation.Cart = null;
                MapState newLocation;
                switch (cart.Direction)
                {
                    case Direction.Left:

                        cart.X -= 1;
                        newLocation = MapGrid[cart.X, cart.Y];
                        if (newLocation.Cart != null)
                        {
                            Carts.Remove(cart);
                            Carts.Remove(newLocation.Cart);
                            cartsToAdvance.Remove(newLocation.Cart);
                            newLocation.Cart = null;
                            Console.WriteLine("Collision detected at : {0}, {1}", cart.X, cart.Y);
                            Console.WriteLine("Carts Remaining: {0}", Carts.Count);
                            continue;
                        }

                        newLocation.Cart = cart;
                        if (newLocation.Element == MapElement.RightCurve)
                        {
                            cart.Direction = Direction.Down;
                        }
                        else if (newLocation.Element == MapElement.LeftCurve)
                        {
                            cart.Direction = Direction.Up;
                        }
                        else if (newLocation.Element == MapElement.Intersection)
                        {
                            switch (cart.IntersectionCount % 3)
                            {
                                case 0:
                                    cart.Direction = Direction.Down;
                                    break;
                                case 1:
                                    //stay straight;
                                    break;
                                case 2:
                                    cart.Direction = Direction.Up;
                                    break;
                            }
                            cart.IntersectionCount += 1;
                        }
                        

                        break;
                    case Direction.Right:

                        cart.X += 1;
                        newLocation = MapGrid[cart.X, cart.Y];
                        if (newLocation.Cart != null)
                        {
                            Carts.Remove(cart);
                            Carts.Remove(newLocation.Cart);
                            cartsToAdvance.Remove(newLocation.Cart);
                            newLocation.Cart = null;
                            Console.WriteLine("Collision detected at : {0}, {1}", cart.X, cart.Y);
                            Console.WriteLine("Carts Remaining: {0}", Carts);
                            continue;
                        }
                        newLocation.Cart = cart;
                        if (newLocation.Element == MapElement.LeftCurve)
                        {
                            cart.Direction = Direction.Down;
                        }
                        else if (newLocation.Element == MapElement.RightCurve)
                        {
                            cart.Direction = Direction.Up;
                        }
                        else if (newLocation.Element == MapElement.Intersection)
                        {
                            switch (cart.IntersectionCount % 3)
                            {
                                case 0:
                                    cart.Direction = Direction.Up;
                                    break;
                                case 1:
                                    //stay straight;
                                    break;
                                case 2:
                                    cart.Direction = Direction.Down;
                                    break;
                            }
                            cart.IntersectionCount += 1;
                        }
                        

                        break;
                    case Direction.Down:

                        cart.Y += 1;
                        newLocation = MapGrid[cart.X, cart.Y];
                        if (newLocation.Cart != null)
                        {                        
                            Carts.Remove(cart);
                            Carts.Remove(newLocation.Cart);
                            cartsToAdvance.Remove(newLocation.Cart);
                            newLocation.Cart = null;
                            Console.WriteLine("Collision detected at : {0}, {1}", cart.X, cart.Y);
                            Console.WriteLine("Carts Remaining: {0}", Carts.Count);
                            continue;
                        }
                        newLocation.Cart = cart;
                        if (newLocation.Element == MapElement.LeftCurve)
                        {
                            cart.Direction = Direction.Right;
                        }
                        else if (newLocation.Element == MapElement.RightCurve)
                        {
                            cart.Direction = Direction.Left;
                        }
                        else if (newLocation.Element == MapElement.Intersection)
                        {
                            switch (cart.IntersectionCount % 3)
                            {
                                case 0:
                                    cart.Direction = Direction.Right;
                                    break;
                                case 1:
                                    //stay straight;
                                    break;
                                case 2:
                                    cart.Direction = Direction.Left;
                                    break;
                            }
                            cart.IntersectionCount += 1;


                        }
                        

                        break;
                    case Direction.Up:
                        cart.Y -= 1;
                        newLocation = MapGrid[cart.X, cart.Y];
                        if (newLocation.Cart != null)
                        {
                            Carts.Remove(cart);
                            Carts.Remove(newLocation.Cart);
                            cartsToAdvance.Remove(newLocation.Cart);
                            newLocation.Cart = null;
                            Console.WriteLine("Collision detected at : {0}, {1}", cart.X, cart.Y);
                            Console.WriteLine("Carts Remaining: {0}", Carts.Count);
                            continue;
                        }
                        newLocation.Cart = cart;
                        if (newLocation.Element == MapElement.LeftCurve)
                        {
                            cart.Direction = Direction.Left;
                        }
                        else if (newLocation.Element == MapElement.RightCurve)
                        {
                            cart.Direction = Direction.Right;
                        }
                        else if (newLocation.Element == MapElement.Intersection)
                        {
                            switch (cart.IntersectionCount % 3)
                            {
                                case 0:
                                    cart.Direction = Direction.Left;
                                    break;
                                case 1:
                                    //stay straight;
                                    break;
                                case 2:
                                    cart.Direction = Direction.Right;
                                    break;
                            }
                            cart.IntersectionCount += 1;
                        }

                        break;
                }
            }
        }

        static void PrintMapState()
        {
            for (int y = 0; y < MapGrid.GetLength(1); y++)
            {
                for (int x = 0; x < MapGrid.GetLength(0); x++)
                {

                    var mapElement = MapGrid[x, y];
                    if (mapElement.Cart != null)
                    {
                        switch (mapElement.Cart.Direction)
                        {
                            case Direction.Down:
                                Console.Write('v');
                                break;
                            case Direction.Up:
                                Console.Write('^');
                                break;
                            case Direction.Left:
                                Console.Write('<');
                                break;
                            case Direction.Right:
                                Console.Write('>');
                                break;
                        }
                    }
                    else
                    {
                        switch (mapElement.Element)
                        {
                            case MapElement.Empty:
                                Console.Write(' ');
                                break;
                            case MapElement.Horizontal:
                                Console.Write('-');
                                break;
                            case MapElement.Vertical:
                                Console.Write('|');
                                break;
                            case MapElement.LeftCurve:
                                Console.Write('\\');
                                break;
                            case MapElement.RightCurve:
                                Console.Write('/');
                                break;
                            case MapElement.Intersection:
                                Console.Write('+');
                                break;
                        }
                    }
                    

                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}