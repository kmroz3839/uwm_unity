using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Lockable<T>
{
    private T val;

    private bool lockState = false;

    public delegate void onValueChanged(T old, T nw);

    public onValueChanged eventValueChanged;

    public Lockable(T val){
        this.val = val;
    }

    public T Value { 
        get {
            return val;
        }
        set
        {
            if (!lockState)
            {
                if (eventValueChanged != null && value is IComparable && (!((IComparable)val).Equals((IComparable)value)))
                {
                    eventValueChanged(val, value);
                }
                val = value;
            }
        }
    }

    public void Lock()
    {
        lockState = true;
    }
    public void Unlock()
    {
        lockState = false;
    }
}
