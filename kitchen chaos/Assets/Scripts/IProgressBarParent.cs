using System;
using UnityEngine;

public interface IProgressBarParent 
{
    public event EventHandler<OnProgressBarValueChangedEventArgs> OnProgressBarValueChanged;
    public class OnProgressBarValueChangedEventArgs : EventArgs
    {
        public float progress;
    }
}
