using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Paint.components.Canvas
{

    // TODO сделать лимит сохранения

    internal class StateHistory<T>
    {
        private static readonly int DEFAULT_SAVE_COUNT = 20;

        private int saveCount;

        private T? currentState; 

        private Stack<T> redo;

        private Stack<T> undo;

        public StateHistory(): this(DEFAULT_SAVE_COUNT) { }

        public StateHistory(int saveCount) 
        {
            this.saveCount = saveCount;
            redo = new Stack<T>();
            undo = new Stack<T>();
        }

        public void SetCurrentState(T state)
        {
            if (state == null)
            {   
                throw new ArgumentNullException(nameof(state), "State cannot be null.");
            }
            currentState = (T)((ICloneable) state).Clone();
        }

        public T? GetCurrentState()
        {
            return currentState;
        }

        public void SaveState()
        {
            if (currentState != null)
            {
                redo.Push(currentState);
            }
        }

        public T? Redo(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "State cannot be null.");
            }
            if (redo.Count != 0)
            {
                T nextObj = redo.Pop();
                undo.Push((T)((ICloneable) obj).Clone());
                return nextObj;
            }
            return default(T);
        }
        public T? Undo(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "State cannot be null.");
            }
            if (undo.Count != 0)
            {
                T nextObj = undo.Pop();
                redo.Push((T)((ICloneable) obj).Clone());
                return nextObj;
            }
            return default(T);
        }
    }
}
