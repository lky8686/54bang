/**
* @author zhangdaijun 20061227
*页面提示框
*随着窗体大小改变阴影
*/
if (!webPromptBox) {
    var webPromptBox = {};
}
webPromptBox.PageUI = function () {
    this.isScroll = false;
    this.msgClassName = "layer";
    this.bgDivId = "_divWebPromptBoxBg";
    this.msgDivId = "_divWebPromptBoxMsg";
};
webPromptBox.resizeCallbacks = [];
webPromptBox.scrollCallbacks = [];
webPromptBox.resize = function () {
    for (var i = 0; i < webPromptBox.resizeCallbacks.length; i++) {
        webPromptBox.resizeCallbacks[i].call();
    }
};
webPromptBox.scroll = function () {
    for (var i = 0; i < webPromptBox.scrollCallbacks.length; i++) {
        webPromptBox.scrollCallbacks[i].call();
    }
};
webPromptBox.isReady = false;
webPromptBox.readyList = [];
webPromptBox.PageUI.prototype = {
    init: function (w, h) {
        this.propmtBoxW = w;
        this.propmtBoxH = h;
        this.yesClick = false;
        this.noClick = false;
        //this.propmtBoxBorderColor = pbBorderColor;
    },
    render: function () {
        if (this.$(this.bgDivId)) return;
        var div = document.createElement("div");
        div.id = this.bgDivId;
        div.style.display = "none";
        document.body.appendChild(div);

        div = document.createElement("div");
        div.id = this.msgDivId;
        div.style.display = "none";
        document.body.appendChild(div);
        webPromptBox.isReady = true;
        if (webPromptBox.readyList.length > 0) {
            webPromptBox.readyList[0]();
        }
    },
    showDialog: function (strHTML) {

        var _self = this, arg = arguments;
        if (!webPromptBox.isReady) {
            webPromptBox.readyList[0] = function () {
                _self.showDialog.apply(_self, arg);
            }
            return;
        }
        webPromptBox.resizeCallbacks = []; //clear
        webPromptBox.scrollCallbacks = []; //clear

        var msgw, msgh, bordercolor, sWidth, sHeight, sWinObj = this.getWAndHOfWindow();
        msgw = parseInt(this.propmtBoxW) ? this.propmtBoxW : 400; /*box width*/
        msgh = parseInt(this.propmtBoxH) ? this.propmtBoxH : 145; /*box height*/
        this.propmtBoxW = msgw;
        this.propmtBoxH = msgh;
        bordercolor = this.propmtBoxBorderColor ? this.propmtBoxBorderColor : "#336699"; /*box border color*/
        this.hiddenSel("none");

        sWidth = sWinObj.w;
        sHeight = sWinObj.h
        var bgObj = this.$(this.bgDivId);
        bgObj.style.display = "";
        bgObj.className = "bg";
        var bgstyle = "position:absolute;filter:alpha(opacity=30);background:#000;opacity:0.3;left:0;top:0;width:100%;height:" + sHeight + "px;z-index:1000;display:'';";
        //if ($.browser.msie && $.browser.version !== '6.0') bgstyle += ";filter:alpha(opacity=100);background:url('http://img02.zhaopin.cn/2012/img/ui/alpha.png');";
        bgObj.setAttribute('style', bgstyle);
        bgObj.style.cssText = bgstyle;

        var msgObj = this.$(this.msgDivId);
        msgObj.style.display = "";
        msgObj.className = this.msgClassName;
        with (msgObj.style) {
            position = "absolute";
            top = 100 + this.getScrollPos().top + "px";
            left = sWinObj.w / 2 - msgw / 2 + "px";
            zIndex = "10001";
            display = "";
        }

        msgObj.innerHTML = this.initHtml(strHTML).join('');
        var _self = this;
        webPromptBox.resizeCallbacks[webPromptBox.resizeCallbacks.length] = function () { _self.resize(_self); };
        webPromptBox.scrollCallbacks[webPromptBox.scrollCallbacks.length] = function () { _self.scroll(_self); };
    },
    initHtml: function (opt) {
        var _html = [];
        var _width = opt.width ? opt.width + 'px' : 'auto';
        _html.push('<table style="width: ' + _width + '; background: white; border: #19477D 1px solid;" cellpadding="0" cellspacing="0">');
        _html.push('<tr><th style="cursor:move;background-color:#16457b;font-family:Microsoft Yahei;font-size:14px; color: white; height: 40px; line-height: 40px; padding: 0 10px; overflow:hidden;text-align:left;">');
        _html.push(opt.title || '消息');
        if (opt.btns && typeof (opt.btns.onNo) == 'function') {
            this.noClick = opt.btns.onNo;
            _html.push('<a href="javascript:;" title="关闭" style="width:18px;height:18px;color:#777;background:#fff;top:12px;right:15px; position:absolute; font-size: 12px; text-decoration: none;line-height:18px;text-align:center;" onclick="webPB.noClick.apply();return false;" onmouseover="this.style.color=\'#333\'" onmouseout="this.style.color=\'#777\'">X</a>');
        }
        else {
            _html.push('<a href="javascript:;" title="关闭" style="width:18px;height:18px;color:#777;background:#fff;top:12px;right:15px; position:absolute; font-size: 12px; text-decoration: none;line-height:18px;text-align:center;" onclick="webPB.close();return false;" onmouseover="this.style.color=\'#333\'"  onmouseout="this.style.color=\'#777\'">X</a>');
        }
        _html.push('</th></tr>');
        _html.push('<tr><td style="padding: 10px;">');
        if (typeof (opt) == 'string') {
            if (opt.indexOf('<') == -1) {
                _html.push('<div style="min-height:70px;_height:70px;min-width:300px;_width:300px">' + opt + '</div>');
            }
            else {
                _html.push(opt);
            }
        }
        else {
            if (opt.msg.indexOf('<') == -1) {
                _html.push('<div style="min-height:70px;_height:70px;min-width:300px;_width:300px">' + opt.msg + '</div>');
            } else {
                _html.push(opt.msg);
            }
        }
        _html.push('</td></tr>');
        if (opt.btns) {
            _html.push('<tr><td style="padding-bottom:10px; text-align: center;">');
            if (opt.btns.onYes) {
                var btnnoTitle = opt.btns.titleYes || '确定';
                if (typeof (opt.btns.onYes) == 'function') {
                    this.yesClick = opt.btns.onYes;
                    _html.push('<a style="display: inline-block;line-height: 26px;width: 86px;text-align: center;background-color: #fc9918;color: #fff;border-radius: 3px;border: 1px solid #f3ab28;font-weight: 600;letter-spacing: 2px;font-size: 14px;" href="javascript:;" onclick="webPB.yesClick.apply();return false;" onmouseover="this.style.textDecoration=\'none\';this.style.background=\'#F88F03\'" onmouseout="this.style.background=\'#fc9918\'" >' + btnnoTitle + '</a>');
                }
                else {
                    _html.push('<a style="display: inline-block;line-height: 26px;width: 86px;text-align: center;background-color: #fc9918;color: #fff;border-radius: 3px;border: 1px solid #f3ab28;font-weight: 600;letter-spacing: 2px;font-size: 14px;" href="javascript:;" onclick="webPB.close();return false;" onmouseover="this.style.textDecoration=\'none\';this.style.background=\'#F88F03\'" onmouseout="this.style.background=\'#fc9918\'" >' + btnnoTitle + '</a>');
                }
            }
            if (opt.btns.onNo) {
                var btn_no_title = opt.btns.titleNo || '关闭';
                if (typeof (opt.btns.onNo) == 'function') {
                    this.noClick = opt.btns.onNo;
                    _html.push('<a style="display: inline-block;line-height: 26px;width: 86px;text-align: center;background-color: #fc9918;border-radius: 3px;border: 1px solid #f3ab28;font-weight: 600;letter-spacing: 2px;font-size: 14px;color: #333;background-color: #fff;border-color: #ccc;margin-left: 22px;" href="javascript:;" onmouseover="this.style.textDecoration=\'none\';this.style.background=\'#f7f7f7\'"  onmouseout="this.style.background=\'#fff\'" onclick="webPB.noClick.apply();return false;">' + btn_no_title + '</a>');
                }
                else {
                    _html.push('<a style="display: inline-block;line-height: 26px;width: 86px;text-align: center;background-color: #fc9918;border-radius: 3px;border: 1px solid #f3ab28;font-weight: 600;letter-spacing: 2px;font-size: 14px;color: #333;background-color: #fff;border-color: #ccc;margin-left: 22px;" href="javascript:;" onmouseover="this.style.textDecoration=\'none\';this.style.background=\'#f7f7f7\'"  onmouseout="this.style.background=\'#fff\'" onclick="webPB.close();return false;">' + btn_no_title + '</a>');

                }
            }
            _html.push('</td></tr>');
        }
        _html.push('</table>');
        return _html;
    },
    hiddenSel: function (val) {
        if (navigator.appVersion.indexOf('MSIE') != -1) {
            var eles = document.getElementsByTagName("select");
            for (var i = 0; i < eles.length; i++) {
                eles[i].style.display = val;
            }
        }
    },
    close: function () {
        if (navigator.appVersion.indexOf('MSIE') != -1) {
            var eles = document.getElementsByTagName("select");
            for (var i = 0; i < eles.length; i++) {
                eles[i].style.display = "";
            }
        }
        if (this.$(this.msgDivId)) {
            this.$(this.msgDivId).innerHTML = "";
            this.$(this.msgDivId).style.display = "none";
        }
        if (this.$(this.bgDivId)) {
            this.$(this.bgDivId).style.display = "none";
        }
    },
    resize: function (_self) {
        var sWinObj = _self.getWAndHOfWindow();
        var w = sWinObj.w, h = sWinObj.h;
        var overlay = _self.$(_self.bgDivId);
        if (this.$("_bgDiv")) {
            if (overlay.width != '0px') {
                with (overlay.style) {
                    width = w + 'px';
                    height = h + 'px';
                    left = '0px';
                    top = '0px';
                }
                var msgObj = _self.$(_self.msgDivId);
                var msgw = _self.propmtBoxW;
                if (msgObj != null) {
                    with (msgObj.style) {
                        top = 200 + _self.getScrollPos().top + "px";
                        left = w / 2 - msgw / 2;
                    }
                }
            }
        }
    },
    scroll: function (_self) {
        if (!this.isScroll) {
            return;
        }
        var sWinObj = _self.getWAndHOfWindow(), w = sWinObj.w;
        var msgObj = _self.$(_self.msgDivId);
        if (msgObj != null) {
            with (msgObj.style) {
                top = 200 + _self.getScrollPos().top + "px";
                left = w / 2 - _self.propmtBoxW / 2 + "px";
            }
        }
    },
    getWAndHOfWindow: function () {
        var w =
            (window.innerWidth && window.scrollMaxX) ? window.innerWidth + window.scrollMaxX
                : (document.body.scrollWidth > document.body.offsetWidth) ? document.body.scrollWidth
                : document.body.offsetWidth;
        var h =
            (window.innerHeight && window.scrollMaxY) ? window.innerHeight + window.scrollMaxY
                : (document.body.scrollHeight > document.body.offsetHeight) ? document.body.scrollHeight
                : document.body.offsetHeight;

        return { "w": w, "h": h };
    },
    getScrollPos: function () {
        var scrollPos = { top: 0, left: 0 };
        if (typeof window.pageYOffset != 'undefined') {
            scrollPos.top = window.pageYOffset;
            scrollPos.left = window.pageXOffset;
        } else if (typeof document.compatMode != 'undefined' && document.compatMode != 'BackCompat') {
            scrollPos.top = document.documentElement.scrollTop;
            scrollPos.left = document.documentElement.scrollLeft;
        } else if (typeof document.body != 'undefined') {
            scrollPos.top = document.body.scrollTop;
            scrollPos.left = document.body.scrollLeft;
        }
        return scrollPos;
    },
    $: function (id) {
        if (typeof (id) == "string") {
            return document.getElementById(id);
        } else {
            return id;
        }
    }
};
$("window").bind(
    {
        resize: webPromptBox.resize
        , scroll: webPromptBox.scroll
    }
);
//$("window").bind("resize", webPromptBox.resize);
//$("window").bind("scroll", webPromptBox.scroll);
var webPB = new webPromptBox.PageUI();;
$(document).ready(
    function () {
        webPB.render();
        //webPB.close();
    }
    );
function showTimeOutPromptBox() {
    var msgHTML = new Array();
    msgHTML.push("<div class='alert_lay' style='margin-left:-150px'>");
    msgHTML.push('<!--背景圆角上-->');
    msgHTML.push("<div class='alert_t'></div>");
    msgHTML.push("<div class='box'>");
    msgHTML.push('<h1><span>查询超时！</span><a href="###" class="butn3" onclick="webPB.close();"></a></h1>');
    msgHTML.push("<div class='sech_layt' style='background:#fff'>");
    msgHTML.push("<div style='padding:30px 20px 10px 10px;text-align:center'>");
    msgHTML.push('如果您使用<b>关键词</b>查询，请尝试使用<a onclick="webPB.close();" id="openResumeSearch6" href="Search6.aspx?FromTimeout50=1" target="_blank">英才简历搜索5.1</a>。');
    msgHTML.push("<div style='margin:20px 0;text-align:center'>");
    msgHTML.push('<input type="button" class="btn" onclick="window.open(\'Search6.aspx?FromTimeout50=1\');webPB.close();" value="使用英才简历搜索5.1" />');
    msgHTML.push('</div>');
    msgHTML.push('</div>');
    msgHTML.push('</div>');
    msgHTML.push('</div>');
    msgHTML.push('<!--背景圆角下-->');
    msgHTML.push('<div class="alert_b"><img src="http://image.mychinahr.com/a/sjob6.0/style/image/laybj_br.gif" alt=""/></div>');
    msgHTML.push('</div>');
    webPB.showDialog(msgHTML.join(""));
}
//$(document).ready(function () { showTimeOutPromptBox(); })