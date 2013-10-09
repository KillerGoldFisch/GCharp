using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSharp.Extensions.EventEx {
    public static class EventExtensions {

        //public event EventHandler SomethingHappened = delegate {};
        static public void RaiseEvent(this EventHandler @eventX, object sender, EventArgs e) {
            if (@eventX != null)
                @eventX(sender, e);
        }

        static public void RaiseEvent<T>(this EventHandler<T> @eventX, object sender, T e)
            where T : EventArgs {
            if (@eventX != null)
                @eventX(sender, e);
        }
    }
}
