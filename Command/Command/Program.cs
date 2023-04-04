using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    interface iCommand
    {
        void Execute();
        void Undo();
    }
    class TVOnCommand : iCommand
    {
        private TV TV;

        public TVOnCommand(TV SetTV)
        {
            TV = SetTV;
        }
        public void Execute()
        {
            TV.On();
        }
        public void Undo()
        {
            TV.Off();
        }
    }
    class MicrowaveCommand : iCommand
    {
        private Microwave Microwave;
        private int Time;

        public MicrowaveCommand(Microwave Microwave, int Time)
        {
            this.Microwave = Microwave;
            this.Time = Time;
        }
        public void Execute()
        {
            Microwave.StartCooking(Time);
            Microwave.StopCooking();
        }
        public void Undo()
        {
            Microwave.StopCooking();
        }
    }

    class TV
    {
        public void On()
        {
            Console.WriteLine("TV is on");
        }
        public void Off()
        {
            Console.WriteLine("TV is off");
        }
    }

    class Microwave
    {
        public void StartCooking(int t)
        {
            Console.WriteLine("Start cook");
            System.Threading.Thread.Sleep(t);
        }
        public void StopCooking()
        {
            Console.WriteLine("The food is ready");
        }
    }
    class Controller
    {
        private iCommand Command;

        public Controller() { }
        public void SetCommand(iCommand Command)
        {
            this.Command = Command;
        }
        public void PressButton()
        {
            if (Command != null)
                Command.Execute();
        }
        public void PressUndo()
        {
            if (Command != null)
                Command.Undo();
        }
    }

    class Program
    {
        static void Invoker(iCommand Command, bool Undo)
        {
            Controller Controller = new Controller();
            Controller.SetCommand(Command);
            Controller.PressButton();
            if (Undo == true)
                Controller.PressUndo();
        }
        static void Main()
        {
            TV TV = new TV();
            iCommand Command = new TVOnCommand(TV);
            Invoker(Command, true);
            Microwave Microwave = new Microwave();
            Command = new MicrowaveCommand(Microwave, 5000);
            Invoker(Command, false);
        }
    }
}
