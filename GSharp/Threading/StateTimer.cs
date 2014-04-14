using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using GSharp.Extensions.TimerEx;


//Author : KEG
//Datum  : 24.09.2013 13:48:51
//Datei  : StateTimer.cs


namespace GSharp.Theading {
    [DefaultProperty("Interval")]
    [DefaultEvent("StateChangedOn")]
    public class StateTimer : Component {

        #region Members
        private Timer timer;
        private bool stateOn;

        public object Tag;
        #endregion

        #region Events
        public delegate void StateChangedHandler(StateTimer sender);

        public event StateChangedHandler StateChangedOn;
        public event StateChangedHandler StateChangedOff;
        #endregion

        #region Initialization
        public StateTimer() {
            this.timer = new Timer();
            this.timer.Tick += new EventHandler(timer_Tick);
        }
        #endregion

        #region Eventhandler
        private void timer_Tick(object sender, EventArgs e) {
            this.Cancel();
        }
        #endregion

        #region Finalization
        ~StateTimer() {

        }
        #endregion

        #region Interface
        public void Push() {
            if (!this.stateOn && this.StateChangedOn != null)
                this.StateChangedOn(this);

            this.stateOn = true;
            this.timer.Enabled = true;
            this.timer.Reset();
        }

        public void Cancel() {
            this.timer.Enabled = false;
            this.stateOn = false;

            if (this.StateChangedOff != null)
                this.StateChangedOff(this);
        }
        #endregion

        #region Browsable Properties

        [DefaultValue(100)]
        public int Interval {
            get { return this.timer.Interval; }
            set { this.timer.Interval = value; }
        }

        public bool StateOn {
            get { return this.stateOn; }
            set {
                if (value == this.stateOn)
                    return;
                if (value) {
                    //einschalten
                    this.Push();
                } else {
                    //ausschelten
                    this.Cancel();
                }
            }
        }


        #endregion

    }
}