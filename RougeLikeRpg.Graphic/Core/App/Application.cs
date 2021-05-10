using System;
using RougeLikeRpg.Graphic.Controls;

namespace RougeLikeRpg.Graphic.Core
{
    public abstract class Application
    {
        public ScreenPool ScreenPool { get; }
        
        public Color ClearColor { get; set; } 
        
        public Screen CurrentScreen { get; }

        public event EventHandler Start;
        public event EventHandler Clearing;
        public event EventHandler Updating;
        public event EventHandler Drawing;
        public event EventHandler End;
        
        
        public Application()
        {
            ScreenPool = new ScreenPool();
        }

        public void Run(IRunArguments args)
        {
           OnStart();
           Update();
           OnEnd();
        }

        public void Update()
        {
            OnUpdate();
            CurrentScreen.Update();
        }

        public void Draw()
        {
           OnDrawing();
           CurrentScreen.Draw();
        }

        public void Clear()
        {
            OnClearing();
            CurrentScreen.Clear(ClearColor);
        }

        protected virtual void OnStart()
        {
            Start?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUpdate()
        {
            Updating?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEnd()
        {
            End?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDrawing()
        {
            Drawing?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnClearing()
        {
            Clearing?.Invoke(this, EventArgs.Empty);
        }
    }
}