// Copyright (c) 2010-2016, Rafael Leonel Pontani. All rights reserved.
// For licensing, see LICENSE.md or http://www.araframework.com.br/license
// This file is part of AraFramework project details visit http://www.arafrework.com.br
// AraFramework - Rafael Leonel Pontani, 2016-4-14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Ara2;
using Ara2.Dev;

namespace Ara2.Components
{
    [Serializable]
    [AraDevComponent(vCompatibleChildrenTypes: new Type[] { typeof(AraSplitContainer) }, vAddAlsoToStart: typeof(AraSplitContainer))]
    public class AraSplitContainers : AraComponentVisualAnchorConteiner,IAraDev
    {
        public AraSplitContainers(IAraContainerClient ConteinerFather)
            : base(AraObjectClienteServer.Create(ConteinerFather, "Div"), ConteinerFather, "AraSplitContainers")
        {
            Click = new AraComponentEvent<EventHandler>(this, "Click");
            IsVisible = new AraComponentEvent<EventHandler>(this, "IsVisible");
            onReziseSplitContainer = new AraComponentEvent<DonReziseSplitContainer>(this, "onReziseSplitContainer");

            this.AddChildBefore += this_AddChildBefore;
            this.AddChildAfter += this_AddChildAfter;
            this.RemuveChildBefore += this_RemuveChildBefore;
            this.EventInternal += AraTabs_EventInternal;


            this.MinWidth = 20;
            this.MinHeight = 10;
            this.Width = 200;
            this.Height = 100;
        }

        private void this_AddChildBefore(IAraObject Child)
        {
            if (!(Child is AraSplitContainer || Child is AraResizable || Child is AraDraggable))
                throw new Exception("Not allowed to add on a AraTabs an object other than AraSplitContainer.");
        }

        private void this_AddChildAfter(IAraObject Child)
        {
            if (Child is AraSplitContainer)
            {
                AraSplitContainer AraSplitContainer = (AraSplitContainer)Child;

                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.AddSplitContainer('" + AraSplitContainer.InstanceID + "');");
            }
        }

        private void this_RemuveChildBefore(IAraObject Child)
        {
            if (Child is AraSplitContainer)
            {
                AraSplitContainer SplitContainer = (AraSplitContainer)Child;
                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.delSplitContainer('" + SplitContainer.InstanceID + "');");
            }
        }

        public override void LoadJS()
        {
            Tick vTick = Tick.GetTick();
            vTick.Session.AddJs("Ara2/Components/AraSplitContainers/AraSplitContainers.js");
        }

        public void AraTabs_EventInternal(String vFunction)
        {
            switch (vFunction.ToUpper())
            {
                case "ISVISIBLE":
                    IsVisible.InvokeEvent(this, new EventArgs());
                    break;
                case "ONREZISESPLITCONTAINER":
                    {
                        Tick Tick=Tick.GetTick();
                        onReziseSplitContainer.InvokeEvent((AraSplitContainer)Tick.Session.GetObject(Tick.Page.Request["IdSplitContainer"].ToString()));
                    }
                    break;
            }
        }



        #region Eventos
        [AraDevEvent]
        public AraComponentEvent<EventHandler> Click;
        public delegate void DonReziseSplitContainer(AraSplitContainer AraSplitContainer);
        [AraDevEvent]
        public AraComponentEvent<EventHandler> IsVisible;
        [AraDevEvent]
        public AraComponentEvent<DonReziseSplitContainer> onReziseSplitContainer;
        #endregion


       

        public enum EOrientation
        {
            Vertical='V',
            Horizontal='H'
        }

        private EOrientation _Orientation = EOrientation.Vertical;
        [AraDevProperty(EOrientation.Vertical)]
        public EOrientation Orientation
        {
            get
            { return _Orientation; }
            set
            {
                _Orientation = value;

                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetOrientation('" + ((char)_Orientation) + "');");
            }
        }


        public void Refresh()
        {
            this.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.Refresh();");
        }

        #region Ara2Dev
        private string _Name = "";
        [AraDevProperty("")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private AraEvent<DStartEditPropertys> _StartEditPropertys = null;
        public AraEvent<DStartEditPropertys> StartEditPropertys
        {
            get
            {
                if (_StartEditPropertys == null)
                {
                    _StartEditPropertys = new AraEvent<DStartEditPropertys>();
                    this.Click += this_ClickEdit;
                }

                return _StartEditPropertys;
            }
            set
            {
                _StartEditPropertys = value;
            }
        }
        private void this_ClickEdit(object sender, EventArgs e)
        {
            if (_StartEditPropertys.InvokeEvent != null)
                _StartEditPropertys.InvokeEvent(this);
        }

        private AraEvent<DStartEditPropertys> _ChangeProperty = new AraEvent<DStartEditPropertys>();
        public AraEvent<DStartEditPropertys> ChangeProperty
        {
            get
            {
                return _ChangeProperty;
            }
            set
            {
                _ChangeProperty = value;
            }
        }
        #endregion
    }
}
