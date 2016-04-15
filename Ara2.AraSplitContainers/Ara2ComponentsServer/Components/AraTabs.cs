using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Ara2.Components
{
    public class AraTabs : AraComponentVisualAnchorConteiner
    {
        public AraTabs(IAraContainerClient ConteinerFather)
            : base(AraObjectClienteServer.Create(ConteinerFather,"Div"), ConteinerFather, "AraDiv")
        {
            Click = new AraComponentEvent<EventHandler>(this, "Click");
            IsVisible = new AraComponentEvent<EventHandler>(this, "IsVisible");
            this.EventInternal += AraTabs_EventInternal;
        }

        public static bool ArquivosHdCarregado = false;
        public override void LoadJS()
        {
            Tick vTick = Tick.GetTick();
            if (ArquivosHdCarregado == false)
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                AraTools.ManifestResourceStreamToPathFile(vTick.Page, asm, "Ara2/Components/AraTabs/AraTabs.js");

                ArquivosHdCarregado = true;
            }

            vTick.Session.AddJs("Ara2/AraSplitContainers/AraTabs.js");
        }

        public void AraTabs_EventInternal(String vFunction)
        {
            switch (vFunction.ToUpper())
            {
                case "CLICK":
                    Click.InvokeEvent(this, new EventArgs());
                    break;
                case "ISVISIBLE":
                    IsVisible.InvokeEvent(this, new EventArgs());
                    break;
            }
        }

        #region Eventos
        public AraComponentEvent<EventHandler> Click;
        public AraComponentEvent<EventHandler> IsVisible;
        
        #endregion


        public void RemoveInterface()
        {
            TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.RemoveInterface(); \n");
        }

        private string _Text = "";
        public string Text
        {
            set
            {
                _Text = value;
                Tick vTick = Tick.GetTick();
                this.TickScriptCall();
                vTick.Script.Send(" vObj.SetValue('" + AraTools.StringToStringJS(_Text) + "'); \n");
            }
            get { return _Text; }
        }

      

        
    }
}
