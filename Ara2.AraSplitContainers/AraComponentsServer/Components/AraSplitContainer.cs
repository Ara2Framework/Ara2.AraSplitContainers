// Copyright (c) 2010-2016, Rafael Leonel Pontani. All rights reserved.
// For licensing, see LICENSE.md or http://www.araframework.com.br/license
// This file is part of AraFramework project details visit http://www.arafrework.com.br
// AraFramework - Rafael Leonel Pontani, 2016-4-14
using System;
using System.Collections.Generic;
using System.Linq;
using Ara2;
using Ara2.Dev;

namespace Ara2.Components
{
    [Serializable]
    [AraDevComponent(vResizable:false,vDraggable:false)]
    public class AraSplitContainer : AraDiv,IAraDev
    {
        private AraSplitContainers AraSplitContainers
        {
            get { return (AraSplitContainers)this.ConteinerFather; }
        }


        private int? _Percent =null;
        [AraDevProperty]
        public int? Percent
        {
            get
            {
                return _Percent;
            }
            set
            {
                if (value < 0 || value >100)
                    throw new Exception("invalid Percent");

                if (_Percent != value)
                {
                    _Percent = value;

                    AraSplitContainers.TickScriptCall();
                    Tick.GetTick().Script.Send(" vObj.SetPercent('" + this.InstanceID + "'," + (_Percent==null?"null":((int)_Percent).ToString()) + ");");
                }
                
            }
        }

        public void SetForceWidthHeight(decimal vValue)
        {
            AraSplitContainers.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.SetLaguraContainer('" + this.InstanceID + "'," + vValue.ToString().Replace(",", ".") + ");");
        }

        public AraSplitContainer(AraSplitContainers Container, int vPercent) :
            this(Container)
        {
            Percent = vPercent;
        }


        public AraSplitContainer(AraSplitContainers Container) :
            base(Container)
        {
            this.HeightChangeBefore += ChangeWH;
            this.WidthChangeBefore += ChangeWH;

            this.Left = 0;
            this.Top = 0;
            this.MinWidth = 10;
            this.MinHeight = 10;
            this.Width = 100;
            this.Height = 100;
        }

        private void ChangeWH(AraDistance ToDistance)
        {
            AraSplitContainers.Refresh();
        }


        #region Ara2Dev
        
        #endregion
    }
}