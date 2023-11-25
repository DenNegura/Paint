using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Canvas
{
    internal class StateHisory<T>
    {
        T currentState; 

        Stack<T> redo;

        Stack<T> undo;

        public StateHisory() 
        {
            redo = new Stack<T>();
            undo = new Stack<T>();
        }

        public void SetCurrentState(T state)
        {
            //currentState = (T) state.Clone();
        }

        public void SaveState()
        {
            if (currentState != null)
            {
                redo.Push(currentState);
            }
        }
    }
}
