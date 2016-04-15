Ara.AraClass.Add('AraTabs', function (vAppId, vId, ConteinerFather) {
    

    // Eventos  ---------------------------------------
    this.Events = {};
    
    var TmpThis = this;
    this.Events.Click =
    {
        Enabled: false,
        ThreadType: 2, // Single_thread
        Function: function () {
            if (this.Enabled) {
                Ara.Tick.Send(this.ThreadType, TmpThis.AppId, TmpThis.id, "Click", null);
            }
        }
    };

    this.Events.IsVisible =
    {
        Enabled: false,
        ThreadType: 1,
        Function: function () {
            if (this.Enabled) {
                Ara.Tick.Send(this.ThreadType, TmpThis.AppId, TmpThis.id, "IsVisible", null);
            }
        }
    };

    this.ApplyStyleContainer = function () {
        $(this.Obj).addClass('ui-widget-content');
        $(this.Obj).addClass('ui-widget');
        $(this.Obj).addClass('ui-corner-all');
    }

    this.GetValue = function () {
        return this.Obj.innerHTML;
    }

    this.SetValue = function (vTmp) {
        this.Obj.innerHTML = vTmp;
    }

    this.SetVisible = function (vTmp) {
        if (vTmp)
            this.Obj.style.display = "block";
        else
            this.Obj.style.display = "none";
    }

    this.TextAdd = function (vTmp) {
        //this.Obj.innerHTML += vTmp;
        this.TmpTextAdd += vTmp;
    }

    this.TextAddEnd = function () {
        this.Obj.innerHTML += this.TmpTextAdd;
        this.TmpTextAdd = "";
    }

    this.Left = null;
    this.SetLeft = function (vTmp) {
        if (this.Left != vTmp) {
            this.Left = vTmp;
            this.Obj.style.left = vTmp ;
        }
    }

    this.Top = null;
    this.SetTop = function (vTmp) {
        if (this.Top != vTmp) {
            this.Top = vTmp;
            this.Obj.style.top = vTmp ;
        }
    }

    this.Width = null;
    this.SetWidth = function (vTmp) {
        if (this.Width != vTmp) {
            this.Width = vTmp;
            this.Obj.style.width = vTmp;
            if (this.Anchor != null)
                this.Anchor.RenderChildren();
        }
    }

    this.Height = null;
    this.SetHeight = function (vTmp) {
        if (this.Height != vTmp) {
            this.Height = vTmp;
            this.Obj.style.height = vTmp;
            if (this.Anchor!=null)
                this.Anchor.RenderChildren();
        }
    }

    
    this.SetOverFlow = function (vTmp) {
        this.Obj.style.overflow = vTmp;
    }

    this.SetAling = function (vTmp) {
        this.Obj.innerHTML = vTmp;
    }

    this.AddClass = function (vTmp) {
        this.ObjJQuery.addClass(vTmp);
    }

    this.DelClass = function (vTmp) {
        this.ObjJQuery.removeClass(vTmp);
    }

    this.destruct = function () {
        $(this.Obj).remove();
    }

    this.IsDestroyed = function () {
        if (!document.getElementById(this.id))
            return true;
        else
            return false;
    }

    this.SetTypePosition = function (vTypePosition) {
        if (vTypePosition != "static")
            $(this.Obj).css({ position: vTypePosition });
        else {
            $(this.Obj).css({ position: "", left: "", top: "" });
        }
    }

    this.AppId = vAppId;
    this.id = vId;
    this.ConteinerFather = ConteinerFather;

    this.Obj = document.getElementById(this.id);
    if (!this.Obj) {
        alert("Object '" + this.id + "' Not Found");
        return;
    }

    var TmpThis = this;
    $(this.Obj).css({ position: "absolute", top: "0px", left: "0px" });
    this.Left = 0;
    this.Top = 0;

    $(this.Obj).click(function () { TmpThis.Events.Click.Function(); });

    this.ControlVar = new ClassAraGenVarSend(this);
    this.ControlVar.AddPrototype("Top");
    this.ControlVar.AddPrototype("Left");
    this.ControlVar.AddPrototype("Width");
    this.ControlVar.AddPrototype("Height");

    this.Anchor = new ClassAraAnchor(this);
});