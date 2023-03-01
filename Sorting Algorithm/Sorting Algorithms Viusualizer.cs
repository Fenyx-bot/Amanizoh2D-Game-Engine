using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Sorting_Algorithm.Sorting_Types;
using System.IO;
using System.Threading;

namespace Sorting_Algorithm
{
    class Sorting_Algorithms_Viusualizer : Amanizoh2D
    {
        //Data Settings
        int Size;
        int MinValue;
        int MaxValue;
        int[] DataArray;

        //Extra Variables
        bool Sorting;
        bool Ascending;
        Random random = new Random();

        //Sorting Algorithms
        enum AlgoTypes
        {
            SELECTION,
            QUICK,
            WEIRD,
            BUBBLE
        }

        AlgoTypes CurrentType = AlgoTypes.SELECTION;

        public Sorting_Algorithms_Viusualizer() : base((uint)800, 600, "Visualizer", new Color(0, 0, 0)) { }

        //Inputs
        bool PressingSpace;
        bool SpacePressed = false;

        bool PressingA;
        bool APressed = false;

        bool PressingD;
        bool DPressed = false;

        bool PressingR;
        bool RPressed = false;

        bool PressingB;
        bool BPressed = false;

        bool PressingQ;
        bool QPressed = false;

        bool PressingS;
        bool SPressed = false;

        bool PressingW;
        bool WPressed = false;


        //Graphics Variables
        Font font;
        uint fontSize;
        Label2D InteractiveText;

        List<Shape2D> tempShapeList = new List<Shape2D>();

        public override void OnLoad()
        {
            Size = 20;
            MinValue = 0;
            MaxValue = 100;
            DataArray = GenerateData(Size, MinValue, MaxValue);
            Sorting = false;
            Ascending = true;

            //Graphics Settings
            float blockWidth = (float)Math.Round((double)((width - 100) / Size));
            float gapBetweenBlocks = blockWidth + 2;
            float startPos = 50;

            fontSize = 20;

            //Drawing Text
            if (File.Exists("Assets/Roboto-Regular.ttf"))
                font = new Font("Assets/Roboto-Regular.ttf");
            else
            {
                font = null;
                Log.Error("File 'Roboto-Regular.ttf' does not exist in the Aseets folder.");
                return;
            }

            InteractiveText = new Label2D("BubbleSort - Ascending", font, fontSize, new Vector2(width / 2, 35), Color.Blue,"UI Text", true);
            Label2D InfoLineOne = new Label2D("| R - Reset | S - SelectionSort | B - BubbleSort |", font, fontSize, new Vector2(width / 2, 70), Color.White, "UIText", true);
            Label2D InfoLineTwo = new Label2D("| A - Ascending | D - Descending |", font, fontSize, new Vector2(width / 2, 105), Color.White, "UIText", true);

            //Drawing Data Blocks
            for (int i = 0; i < Size; i++)
            {
                Shape2D dataGraphics = new Shape2D(new Vector2(startPos + (i * gapBetweenBlocks), 600), new Vector2(blockWidth, -DataArray[i] * 3), "DataSet", Color.Cyan, Color.White, DataArray[i]);
            }
        }

        public void Reset()
        {
            Log.Info("Page Reset!");
            AllLabels.Clear();
            AllShapes.Clear();
            app.Clear(windowColor);
            OnLoad();
        }

        public void ReloadGraphs()
        {
            Log.Info("Graphs Reloaded!");
            AllShapes.Clear();
            app.Clear(windowColor);

            //Drawing Data Blocks
            for (int i = 0; i < Size; i++)
            {
                Shape2D dataGraphics = new Shape2D(tempShapeList[i].Position, tempShapeList[i].Scale, tempShapeList[i].Tag, tempShapeList[i].Color, tempShapeList[i].OutLineColor, tempShapeList[i].value);
            }
            app.DispatchEvents();
            Renderer();
            app.Display();
        }

        public void Reload()
        {
            AllLabels.Clear();
            AllShapes.Clear();;
            app.Clear(windowColor);

            //Graphics Settings
            float blockWidth = (float)Math.Round((double)((width - 100) / Size));
            float gapBetweenBlocks = blockWidth + 2;
            float startPos = 50;

            //Drawing Text
            if (File.Exists("Assets/Roboto-Regular.ttf"))
                font = new Font("Assets/Roboto-Regular.ttf");
            else
            {
                font = null;
                Log.Error("File 'Roboto-Regular.ttf' does not exist in the Aseets folder.");
                return;
            }

            InteractiveText = new Label2D("BubbleSort - Ascending", font, fontSize, new Vector2(width / 2, 35), Color.Blue, "UIText", true);
            Label2D InfoLineOne = new Label2D("| R - Reset | S - SelectionSort | B - BubbleSort |", font, fontSize, new Vector2(width / 2, 70), Color.White, "UIText", true);
            Label2D InfoLineTwo = new Label2D("| A - Ascending | D - Descending |", font, fontSize, new Vector2(width / 2, 105), Color.White, "UIText", true);

            //Drawing Data Blocks
            for (int i = 0; i < Size; i++)
            {
                float blockHeight = -DataArray[i] * 3;
                Shape2D dataGraphics = new Shape2D(new Vector2(startPos + (i * gapBetweenBlocks), 600), new Vector2(blockWidth, blockHeight), "DataSet", Color.Cyan, Color.White, DataArray[i]);
            }
        }

        public override void OnUpdate()
        {
            Input();
            if (Ascending)
            {
                string currentTypeString = CurrentType.ToString().ToLowerInvariant();
                currentTypeString = currentTypeString[0].ToString().ToUpper() + currentTypeString.Substring(1);
                InteractiveText.text = currentTypeString + "Sort - " + "Ascending";
            }
            else
            {
                string currentTypeString = CurrentType.ToString().ToLowerInvariant();
                currentTypeString = currentTypeString[0].ToString().ToUpper() + currentTypeString.Substring(1);
                InteractiveText.text = currentTypeString + "Sort - " + "Descending";
            }
                
        }

        public void Input()
        {
            if (PressingSpace && !SpacePressed && !Sorting)
            {
                SpacePressed = true;
                Log.Info("Space pressed");
                if (Ascending && !Sorting)
                {
                    Sorting = true;
                    if (CurrentType == AlgoTypes.SELECTION)
                        SortBySelection(AllShapes, Size, Ascending);
                    else if (CurrentType == AlgoTypes.QUICK)
                        return;
                    else if (CurrentType == AlgoTypes.BUBBLE)
                        BubbleSort(AllShapes, Size, Ascending);
                    else if (CurrentType == AlgoTypes.WEIRD)
                        return;
                    Sorting = false;
                }
                else if (!Ascending && !Sorting)
                {
                    Sorting = true;
                    if (CurrentType == AlgoTypes.SELECTION)
                        SortBySelection(AllShapes, Size, Ascending);
                    else if (CurrentType == AlgoTypes.QUICK)
                        return;
                    else if (CurrentType == AlgoTypes.BUBBLE)
                        BubbleSort(AllShapes, Size, Ascending);
                    else if (CurrentType == AlgoTypes.WEIRD)
                        return;
                    Sorting = false;
                }
                    
            }

            if (PressingA && !APressed && !Ascending)
            {
                APressed = true;
                Ascending = true;
                Log.Info($"Ascending: {Ascending}");
            }

            if (PressingD && !DPressed && Ascending)
            {
                DPressed = true;
                Ascending = false;
                Log.Info($"Ascending: {Ascending}");
            }

            if (PressingR && !RPressed) 
            {
                RPressed = true;
                Reset();
            }

            if (PressingB && !BPressed)
            {
                BPressed = true;
                CurrentType = AlgoTypes.BUBBLE;
                Log.Info($"Sorting Algorithm: {CurrentType}");
            }

            if (PressingQ && !QPressed)
            {
                QPressed = true;
                CurrentType = AlgoTypes.QUICK;
                Log.Info($"Sorting Algorithm: {CurrentType}");
            }

            if (PressingS && !SPressed)
            {
                SPressed = true;
                CurrentType = AlgoTypes.SELECTION;
                Log.Info($"Sorting Algorithm: {CurrentType}");
            }

            if (PressingW && !WPressed)
            {
                WPressed = true;
                CurrentType = AlgoTypes.WEIRD;
                Log.Info($"Sorting Algorithm: {CurrentType}");
            }
        }

        public double GetRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

         int[] GenerateData(int size, int minvalue, int maxvalue)
        {
            int[] array = new int[size];

            for(int i = 0; i < size; i++)
            {
                array[i] = (int)GetRandomNumber(minvalue, maxvalue);
            }

            return array;
        }

        void SortBySelection(List<Shape2D> array, int size, bool ascending)
        {
            if (!ascending)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = i + 1; j < size; j++)
                    {
                        if (array[j].Color == Color.Red)
                            array[j].Color = Color.Cyan;
                    }
                    int max = i;
                    if (i > 0)
                        array[i - 1].Color = Color.Cyan;
                    for (int j = i + 1; j < size; j++)
                    {
                        array[i].Color = Color.Green;
                        array[max].Color = Color.Red;
                        if (array[j].value > array[max].value)
                        {
                            array[max].Color = Color.Cyan;
                            max = j;
                        }
                            
                    }
                    if (max != i)
                    {
                        int temp = array[max].value;
                        array[max].value = array[i].value;
                        array[i].value = temp;
                    }
                    tempShapeList.Clear();
                    for (int k = 0; k < size; k++)
                    {
                        tempShapeList.Add(array[k]);
                        Console.Write($"| {tempShapeList[k].value} |");
                    }
                        
                    ReloadGraphs();
                    Thread.Sleep(100);
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = i + 1; j < size; j++)
                    {
                        if (array[j].Color == Color.Red)
                        {
                            array[j].Color = Color.Cyan;
                        }
                    }
                    int min = i;
                    if (i > 0)
                    {
                        array[i - 1].Color = Color.Cyan;
                    }
                        
                    for (int j = i + 1; j < size; j++)
                    {
                        array[i].Color = Color.Green;
                        array[min].Color = Color.Red;
                        if (array[j].value < array[min].value)
                        {
                            array[min].Color = Color.Cyan;
                            min = j;
                        }
                    }
                    if (min != i)
                    {
                        int temp = array[min].value;
                        array[min].value = array[i].value;
                        array[i].value = temp;
                    }
                    tempShapeList.Clear();
                    for (int k = 0; k < size; k++)
                    {
                        tempShapeList.Add(array[k]);
                        Console.Write($"| {tempShapeList[k].value} |");
                    }
                    Thread.Sleep(100);
                    ReloadGraphs();
                }

            }
            
        }

        void BubbleSort(List<Shape2D> array, int size, bool ascending)
        {
            Log.Info("Started Sort");
            if (!ascending)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        array[j].Color = Color.Cyan;
                    }
                

                    for (int j = 0; j < size - i - 1; j++)
                    {
                        if (array[j].value < array[j + 1].value)
                        {
                            int temp = array[j].value;
                            array[j].value = array[j + 1].value;
                            array[j + 1].value = temp;
                            array[j].Color = Color.Red;
                            array[j + 1].Color = Color.Green;
                        }
                    }

                    //Drawing the array
                    tempShapeList.Clear();
                    for (int k = 0; k < size; k++)
                    {
                        tempShapeList.Add(array[k]);
                        Console.Write($"| {tempShapeList[k].value} |");
                    }

                    ReloadGraphs();
                    Thread.Sleep(100);

                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        array[j].Color = Color.Cyan;
                    }
                    for (int j = 0; j < size - i - 1; j++)
                    {
                        if (array[j].value > array[j + 1].value)
                        {
                            int temp = array[j].value;
                            array[j].value = array[j + 1].value;
                            array[j + 1].value = temp;
                            array[j].Color = Color.Red;
                            array[j + 1].Color = Color.Green;
                        }

                        //Drawing the array
                        tempShapeList.Clear();
                        for (int k = 0; k < size; k++)
                        {
                            tempShapeList.Add(array[k]);
                            Console.Write($"| {tempShapeList[k].value} |");
                        }

                        ReloadGraphs();
                        Thread.Sleep(100);
                    }
                }
            }
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Space)
            {
                PressingSpace = true;
            }

            if (e.Code == Keyboard.Key.A)
            {
                PressingA = true;
            }

            if (e.Code == Keyboard.Key.D)
            {
                PressingD = true;
            }

            if (e.Code == Keyboard.Key.R)
            {
                PressingR = true;
            }

            if (e.Code == Keyboard.Key.B)
            {
                PressingB = true;
            }

            if (e.Code == Keyboard.Key.Q)
            {
                PressingQ = true;
            }

            if (e.Code == Keyboard.Key.S)
            {
                PressingS = true;
            }

            if (e.Code == Keyboard.Key.W)
            {
                PressingW = true;
            }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Space)
            {
                PressingSpace = false;
                SpacePressed = false;
            }

            if (e.Code == Keyboard.Key.A)
            {
                PressingA = false;
                APressed = false;
            }

            if (e.Code == Keyboard.Key.D)
            {
                PressingD = false;
                DPressed = false;
            }

            if (e.Code == Keyboard.Key.R)
            {
                PressingR = false;
                RPressed = false;
            }

            if (e.Code == Keyboard.Key.B)
            {
                PressingB = false;
                BPressed = false;
            }

            if (e.Code == Keyboard.Key.Q)
            {
                PressingQ = false;
                QPressed = false;
            }

            if (e.Code == Keyboard.Key.S)
            {
                PressingS = false;
                SPressed = false;
            }

            if (e.Code == Keyboard.Key.W)
            {
                PressingW = false;
                WPressed = false;
            }
        }
    }
}
