/*respond.min.js*/
/*! Respond.js v1.4.2: min/max-width media query polyfill
 * Copyright 2014 Scott Jehl
 * Licensed under MIT
 * http://j.mp/respondjs */

!function (a) { "use strict"; a.matchMedia = a.matchMedia || function (a) { var b, c = a.documentElement, d = c.firstElementChild || c.firstChild, e = a.createElement("body"), f = a.createElement("div"); return f.id = "mq-test-1", f.style.cssText = "position:absolute;top:-100em", e.style.background = "none", e.appendChild(f), function (a) { return f.innerHTML = '&shy;<style media="' + a + '"> #mq-test-1 { width: 42px; }</style>', c.insertBefore(e, d), b = 42 === f.offsetWidth, c.removeChild(e), { matches: b, media: a } } }(a.document) }(this), function (a) { "use strict"; function b() { v(!0) } var c = {}; a.respond = c, c.update = function () { }; var d = [], e = function () { var b = !1; try { b = new a.XMLHttpRequest } catch (c) { b = new a.ActiveXObject("Microsoft.XMLHTTP") } return function () { return b } }(), f = function (a, b) { var c = e(); c && (c.open("GET", a, !0), c.onreadystatechange = function () { 4 !== c.readyState || 200 !== c.status && 304 !== c.status || b(c.responseText) }, 4 !== c.readyState && c.send(null)) }, g = function (a) { return a.replace(c.regex.minmaxwh, "").match(c.regex.other) }; if (c.ajax = f, c.queue = d, c.unsupportedmq = g, c.regex = { media: /@media[^\{]+\{([^\{\}]*\{[^\}\{]*\})+/gi, keyframes: /@(?:\-(?:o|moz|webkit)\-)?keyframes[^\{]+\{(?:[^\{\}]*\{[^\}\{]*\})+[^\}]*\}/gi, comments: /\/\*[^*]*\*+([^/][^*]*\*+)*\//gi, urls: /(url\()['"]?([^\/\)'"][^:\)'"]+)['"]?(\))/g, findStyles: /@media *([^\{]+)\{([\S\s]+?)$/, only: /(only\s+)?([a-zA-Z]+)\s?/, minw: /\(\s*min\-width\s*:\s*(\s*[0-9\.]+)(px|em)\s*\)/, maxw: /\(\s*max\-width\s*:\s*(\s*[0-9\.]+)(px|em)\s*\)/, minmaxwh: /\(\s*m(in|ax)\-(height|width)\s*:\s*(\s*[0-9\.]+)(px|em)\s*\)/gi, other: /\([^\)]*\)/g }, c.mediaQueriesSupported = a.matchMedia && null !== a.matchMedia("only all") && a.matchMedia("only all").matches, !c.mediaQueriesSupported) { var h, i, j, k = a.document, l = k.documentElement, m = [], n = [], o = [], p = {}, q = 30, r = k.getElementsByTagName("head")[0] || l, s = k.getElementsByTagName("base")[0], t = r.getElementsByTagName("link"), u = function () { var a, b = k.createElement("div"), c = k.body, d = l.style.fontSize, e = c && c.style.fontSize, f = !1; return b.style.cssText = "position:absolute;font-size:1em;width:1em", c || (c = f = k.createElement("body"), c.style.background = "none"), l.style.fontSize = "100%", c.style.fontSize = "100%", c.appendChild(b), f && l.insertBefore(c, l.firstChild), a = b.offsetWidth, f ? l.removeChild(c) : c.removeChild(b), l.style.fontSize = d, e && (c.style.fontSize = e), a = j = parseFloat(a) }, v = function (b) { var c = "clientWidth", d = l[c], e = "CSS1Compat" === k.compatMode && d || k.body[c] || d, f = {}, g = t[t.length - 1], p = (new Date).getTime(); if (b && h && q > p - h) return a.clearTimeout(i), i = a.setTimeout(v, q), void 0; h = p; for (var s in m) if (m.hasOwnProperty(s)) { var w = m[s], x = w.minw, y = w.maxw, z = null === x, A = null === y, B = "em"; x && (x = parseFloat(x) * (x.indexOf(B) > -1 ? j || u() : 1)), y && (y = parseFloat(y) * (y.indexOf(B) > -1 ? j || u() : 1)), w.hasquery && (z && A || !(z || e >= x) || !(A || y >= e)) || (f[w.media] || (f[w.media] = []), f[w.media].push(n[w.rules])) } for (var C in o) o.hasOwnProperty(C) && o[C] && o[C].parentNode === r && r.removeChild(o[C]); o.length = 0; for (var D in f) if (f.hasOwnProperty(D)) { var E = k.createElement("style"), F = f[D].join("\n"); E.type = "text/css", E.media = D, r.insertBefore(E, g.nextSibling), E.styleSheet ? E.styleSheet.cssText = F : E.appendChild(k.createTextNode(F)), o.push(E) } }, w = function (a, b, d) { var e = a.replace(c.regex.comments, "").replace(c.regex.keyframes, "").match(c.regex.media), f = e && e.length || 0; b = b.substring(0, b.lastIndexOf("/")); var h = function (a) { return a.replace(c.regex.urls, "$1" + b + "$2$3") }, i = !f && d; b.length && (b += "/"), i && (f = 1); for (var j = 0; f > j; j++) { var k, l, o, p; i ? (k = d, n.push(h(a))) : (k = e[j].match(c.regex.findStyles) && RegExp.$1, n.push(RegExp.$2 && h(RegExp.$2))), o = k.split(","), p = o.length; for (var q = 0; p > q; q++) l = o[q], g(l) || m.push({ media: l.split("(")[0].match(c.regex.only) && RegExp.$2 || "all", rules: n.length - 1, hasquery: l.indexOf("(") > -1, minw: l.match(c.regex.minw) && parseFloat(RegExp.$1) + (RegExp.$2 || ""), maxw: l.match(c.regex.maxw) && parseFloat(RegExp.$1) + (RegExp.$2 || "") }) } v() }, x = function () { if (d.length) { var b = d.shift(); f(b.href, function (c) { w(c, b.href, b.media), p[b.href] = !0, a.setTimeout(function () { x() }, 0) }) } }, y = function () { for (var b = 0; b < t.length; b++) { var c = t[b], e = c.href, f = c.media, g = c.rel && "stylesheet" === c.rel.toLowerCase(); e && g && !p[e] && (c.styleSheet && c.styleSheet.rawCssText ? (w(c.styleSheet.rawCssText, e, f), p[e] = !0) : (!/^([a-zA-Z:]*\/\/)/.test(e) && !s || e.replace(RegExp.$1, "").split("/")[0] === a.location.host) && ("//" === e.substring(0, 2) && (e = a.location.protocol + e), d.push({ href: e, media: f }))) } x() }; y(), c.update = y, c.getEmValue = u, a.addEventListener ? a.addEventListener("resize", b, !1) : a.attachEvent && a.attachEvent("onresize", b) } }(this);

/* flexslider main rotator */
/*jquery.flexslider.min.js*/
/*
 * jQuery FlexSlider v2.2.0
 * Copyright 2012 WooThemes
 * Contributing Author: Tyler Smith
 */(function (e) { e.flexslider = function (t, n) { var r = e(t); r.vars = e.extend({}, e.flexslider.defaults, n); var i = r.vars.namespace, s = window.navigator && window.navigator.msPointerEnabled && window.MSGesture, o = ("ontouchstart" in window || s || window.DocumentTouch && document instanceof DocumentTouch) && r.vars.touch, u = "click touchend MSPointerUp", a = "", f, l = r.vars.direction === "vertical", c = r.vars.reverse, h = r.vars.itemWidth > 0, p = r.vars.animation === "fade", d = r.vars.asNavFor !== "", v = {}, m = !0; e.data(t, "flexslider", r); v = { init: function () { r.animating = !1; r.currentSlide = parseInt(r.vars.startAt ? r.vars.startAt : 0); isNaN(r.currentSlide) && (r.currentSlide = 0); r.animatingTo = r.currentSlide; r.atEnd = r.currentSlide === 0 || r.currentSlide === r.last; r.containerSelector = r.vars.selector.substr(0, r.vars.selector.search(" ")); r.slides = e(r.vars.selector, r); r.container = e(r.containerSelector, r); r.count = r.slides.length; r.syncExists = e(r.vars.sync).length > 0; r.vars.animation === "slide" && (r.vars.animation = "swing"); r.prop = l ? "top" : "marginLeft"; r.args = {}; r.manualPause = !1; r.stopped = !1; r.started = !1; r.startTimeout = null; r.transitions = !r.vars.video && !p && r.vars.useCSS && function () { var e = document.createElement("div"), t = ["perspectiveProperty", "WebkitPerspective", "MozPerspective", "OPerspective", "msPerspective"]; for (var n in t) if (e.style[t[n]] !== undefined) { r.pfx = t[n].replace("Perspective", "").toLowerCase(); r.prop = "-" + r.pfx + "-transform"; return !0 } return !1 }(); r.vars.controlsContainer !== "" && (r.controlsContainer = e(r.vars.controlsContainer).length > 0 && e(r.vars.controlsContainer)); r.vars.manualControls !== "" && (r.manualControls = e(r.vars.manualControls).length > 0 && e(r.vars.manualControls)); if (r.vars.randomize) { r.slides.sort(function () { return Math.round(Math.random()) - .5 }); r.container.empty().append(r.slides) } r.doMath(); r.setup("init"); r.vars.controlNav && v.controlNav.setup(); r.vars.directionNav && v.directionNav.setup(); r.vars.keyboard && (e(r.containerSelector).length === 1 || r.vars.multipleKeyboard) && e(document).bind("keyup", function (e) { var t = e.keyCode; if (!r.animating && (t === 39 || t === 37)) { var n = t === 39 ? r.getTarget("next") : t === 37 ? r.getTarget("prev") : !1; r.flexAnimate(n, r.vars.pauseOnAction) } }); r.vars.mousewheel && r.bind("mousewheel", function (e, t, n, i) { e.preventDefault(); var s = t < 0 ? r.getTarget("next") : r.getTarget("prev"); r.flexAnimate(s, r.vars.pauseOnAction) }); r.vars.pausePlay && v.pausePlay.setup(); r.vars.slideshow && r.vars.pauseInvisible && v.pauseInvisible.init(); if (r.vars.slideshow) { r.vars.pauseOnHover && r.hover(function () { !r.manualPlay && !r.manualPause && r.pause() }, function () { !r.manualPause && !r.manualPlay && !r.stopped && r.play() }); if (!r.vars.pauseInvisible || !v.pauseInvisible.isHidden()) r.vars.initDelay > 0 ? r.startTimeout = setTimeout(r.play, r.vars.initDelay) : r.play() } d && v.asNav.setup(); o && r.vars.touch && v.touch(); (!p || p && r.vars.smoothHeight) && e(window).bind("resize orientationchange focus", v.resize); r.find("img").attr("draggable", "false"); setTimeout(function () { r.vars.start(r) }, 200) }, asNav: { setup: function () { r.asNav = !0; r.animatingTo = Math.floor(r.currentSlide / r.move); r.currentItem = r.currentSlide; r.slides.removeClass(i + "active-slide").eq(r.currentItem).addClass(i + "active-slide"); if (!s) r.slides.click(function (t) { t.preventDefault(); var n = e(this), s = n.index(), o = n.offset().left - e(r).scrollLeft(); if (o <= 0 && n.hasClass(i + "active-slide")) r.flexAnimate(r.getTarget("prev"), !0); else if (!e(r.vars.asNavFor).data("flexslider").animating && !n.hasClass(i + "active-slide")) { r.direction = r.currentItem < s ? "next" : "prev"; r.flexAnimate(s, r.vars.pauseOnAction, !1, !0, !0) } }); else { t._slider = r; r.slides.each(function () { var t = this; t._gesture = new MSGesture; t._gesture.target = t; t.addEventListener("MSPointerDown", function (e) { e.preventDefault(); e.currentTarget._gesture && e.currentTarget._gesture.addPointer(e.pointerId) }, !1); t.addEventListener("MSGestureTap", function (t) { t.preventDefault(); var n = e(this), i = n.index(); if (!e(r.vars.asNavFor).data("flexslider").animating && !n.hasClass("active")) { r.direction = r.currentItem < i ? "next" : "prev"; r.flexAnimate(i, r.vars.pauseOnAction, !1, !0, !0) } }) }) } } }, controlNav: { setup: function () { r.manualControls ? v.controlNav.setupManual() : v.controlNav.setupPaging() }, setupPaging: function () { var t = r.vars.controlNav === "thumbnails" ? "control-thumbs" : "control-paging", n = 1, s, o; r.controlNavScaffold = e('<ol class="' + i + "control-nav " + i + t + '"></ol>'); if (r.pagingCount > 1) for (var f = 0; f < r.pagingCount; f++) { o = r.slides.eq(f); s = r.vars.controlNav === "thumbnails" ? '<img src="' + o.attr("data-thumb") + '"/>' : "<a>" + n + "</a>"; if ("thumbnails" === r.vars.controlNav && !0 === r.vars.thumbCaptions) { var l = o.attr("data-thumbcaption"); "" != l && undefined != l && (s += '<span class="' + i + 'caption">' + l + "</span>") } r.controlNavScaffold.append("<li>" + s + "</li>"); n++ } r.controlsContainer ? e(r.controlsContainer).append(r.controlNavScaffold) : r.append(r.controlNavScaffold); v.controlNav.set(); v.controlNav.active(); r.controlNavScaffold.delegate("a, img", u, function (t) { t.preventDefault(); if (a === "" || a === t.type) { var n = e(this), s = r.controlNav.index(n); if (!n.hasClass(i + "active")) { r.direction = s > r.currentSlide ? "next" : "prev"; r.flexAnimate(s, r.vars.pauseOnAction) } } a === "" && (a = t.type); v.setToClearWatchedEvent() }) }, setupManual: function () { r.controlNav = r.manualControls; v.controlNav.active(); r.controlNav.bind(u, function (t) { t.preventDefault(); if (a === "" || a === t.type) { var n = e(this), s = r.controlNav.index(n); if (!n.hasClass(i + "active")) { s > r.currentSlide ? r.direction = "next" : r.direction = "prev"; r.flexAnimate(s, r.vars.pauseOnAction) } } a === "" && (a = t.type); v.setToClearWatchedEvent() }) }, set: function () { var t = r.vars.controlNav === "thumbnails" ? "img" : "a"; r.controlNav = e("." + i + "control-nav li " + t, r.controlsContainer ? r.controlsContainer : r) }, active: function () { r.controlNav.removeClass(i + "active").eq(r.animatingTo).addClass(i + "active") }, update: function (t, n) { r.pagingCount > 1 && t === "add" ? r.controlNavScaffold.append(e("<li><a>" + r.count + "</a></li>")) : r.pagingCount === 1 ? r.controlNavScaffold.find("li").remove() : r.controlNav.eq(n).closest("li").remove(); v.controlNav.set(); r.pagingCount > 1 && r.pagingCount !== r.controlNav.length ? r.update(n, t) : v.controlNav.active() } }, directionNav: { setup: function () { var t = e('<ul class="' + i + 'direction-nav"><li><a class="' + i + 'prev" href="#">' + r.vars.prevText + '</a></li><li><a class="' + i + 'next" href="#">' + r.vars.nextText + "</a></li></ul>"); if (r.controlsContainer) { e(r.controlsContainer).append(t); r.directionNav = e("." + i + "direction-nav li a", r.controlsContainer) } else { r.append(t); r.directionNav = e("." + i + "direction-nav li a", r) } v.directionNav.update(); r.directionNav.bind(u, function (t) { t.preventDefault(); var n; if (a === "" || a === t.type) { n = e(this).hasClass(i + "next") ? r.getTarget("next") : r.getTarget("prev"); r.flexAnimate(n, r.vars.pauseOnAction) } a === "" && (a = t.type); v.setToClearWatchedEvent() }) }, update: function () { var e = i + "disabled"; r.pagingCount === 1 ? r.directionNav.addClass(e).attr("tabindex", "-1") : r.vars.animationLoop ? r.directionNav.removeClass(e).removeAttr("tabindex") : r.animatingTo === 0 ? r.directionNav.removeClass(e).filter("." + i + "prev").addClass(e).attr("tabindex", "-1") : r.animatingTo === r.last ? r.directionNav.removeClass(e).filter("." + i + "next").addClass(e).attr("tabindex", "-1") : r.directionNav.removeClass(e).removeAttr("tabindex") } }, pausePlay: { setup: function () { var t = e('<div class="' + i + 'pauseplay"><a></a></div>'); if (r.controlsContainer) { r.controlsContainer.append(t); r.pausePlay = e("." + i + "pauseplay a", r.controlsContainer) } else { r.append(t); r.pausePlay = e("." + i + "pauseplay a", r) } v.pausePlay.update(r.vars.slideshow ? i + "pause" : i + "play"); r.pausePlay.bind(u, function (t) { t.preventDefault(); if (a === "" || a === t.type) if (e(this).hasClass(i + "pause")) { r.manualPause = !0; r.manualPlay = !1; r.pause() } else { r.manualPause = !1; r.manualPlay = !0; r.play() } a === "" && (a = t.type); v.setToClearWatchedEvent() }) }, update: function (e) { e === "play" ? r.pausePlay.removeClass(i + "pause").addClass(i + "play").html(r.vars.playText) : r.pausePlay.removeClass(i + "play").addClass(i + "pause").html(r.vars.pauseText) } }, touch: function () { var e, n, i, o, u, a, f = !1, d = 0, v = 0, m = 0; if (!s) { t.addEventListener("touchstart", g, !1); function g(s) { if (r.animating) s.preventDefault(); else if (window.navigator.msPointerEnabled || s.touches.length === 1) { r.pause(); o = l ? r.h : r.w; a = Number(new Date); d = s.touches[0].pageX; v = s.touches[0].pageY; i = h && c && r.animatingTo === r.last ? 0 : h && c ? r.limit - (r.itemW + r.vars.itemMargin) * r.move * r.animatingTo : h && r.currentSlide === r.last ? r.limit : h ? (r.itemW + r.vars.itemMargin) * r.move * r.currentSlide : c ? (r.last - r.currentSlide + r.cloneOffset) * o : (r.currentSlide + r.cloneOffset) * o; e = l ? v : d; n = l ? d : v; t.addEventListener("touchmove", y, !1); t.addEventListener("touchend", b, !1) } } function y(t) { d = t.touches[0].pageX; v = t.touches[0].pageY; u = l ? e - v : e - d; f = l ? Math.abs(u) < Math.abs(d - n) : Math.abs(u) < Math.abs(v - n); var s = 500; if (!f || Number(new Date) - a > s) { t.preventDefault(); if (!p && r.transitions) { r.vars.animationLoop || (u /= r.currentSlide === 0 && u < 0 || r.currentSlide === r.last && u > 0 ? Math.abs(u) / o + 2 : 1); r.setProps(i + u, "setTouch") } } } function b(s) { t.removeEventListener("touchmove", y, !1); if (r.animatingTo === r.currentSlide && !f && u !== null) { var l = c ? -u : u, h = l > 0 ? r.getTarget("next") : r.getTarget("prev"); r.canAdvance(h) && (Number(new Date) - a < 550 && Math.abs(l) > 50 || Math.abs(l) > o / 2) ? r.flexAnimate(h, r.vars.pauseOnAction) : p || r.flexAnimate(r.currentSlide, r.vars.pauseOnAction, !0) } t.removeEventListener("touchend", b, !1); e = null; n = null; u = null; i = null } } else { t.style.msTouchAction = "none"; t._gesture = new MSGesture; t._gesture.target = t; t.addEventListener("MSPointerDown", w, !1); t._slider = r; t.addEventListener("MSGestureChange", E, !1); t.addEventListener("MSGestureEnd", S, !1); function w(e) { e.stopPropagation(); if (r.animating) e.preventDefault(); else { r.pause(); t._gesture.addPointer(e.pointerId); m = 0; o = l ? r.h : r.w; a = Number(new Date); i = h && c && r.animatingTo === r.last ? 0 : h && c ? r.limit - (r.itemW + r.vars.itemMargin) * r.move * r.animatingTo : h && r.currentSlide === r.last ? r.limit : h ? (r.itemW + r.vars.itemMargin) * r.move * r.currentSlide : c ? (r.last - r.currentSlide + r.cloneOffset) * o : (r.currentSlide + r.cloneOffset) * o } } function E(e) { e.stopPropagation(); var n = e.target._slider; if (!n) return; var r = -e.translationX, s = -e.translationY; m += l ? s : r; u = m; f = l ? Math.abs(m) < Math.abs(-r) : Math.abs(m) < Math.abs(-s); if (e.detail === e.MSGESTURE_FLAG_INERTIA) { setImmediate(function () { t._gesture.stop() }); return } if (!f || Number(new Date) - a > 500) { e.preventDefault(); if (!p && n.transitions) { n.vars.animationLoop || (u = m / (n.currentSlide === 0 && m < 0 || n.currentSlide === n.last && m > 0 ? Math.abs(m) / o + 2 : 1)); n.setProps(i + u, "setTouch") } } } function S(t) { t.stopPropagation(); var r = t.target._slider; if (!r) return; if (r.animatingTo === r.currentSlide && !f && u !== null) { var s = c ? -u : u, l = s > 0 ? r.getTarget("next") : r.getTarget("prev"); r.canAdvance(l) && (Number(new Date) - a < 550 && Math.abs(s) > 50 || Math.abs(s) > o / 2) ? r.flexAnimate(l, r.vars.pauseOnAction) : p || r.flexAnimate(r.currentSlide, r.vars.pauseOnAction, !0) } e = null; n = null; u = null; i = null; m = 0 } } }, resize: function () { if (!r.animating && r.is(":visible")) { h || r.doMath(); if (p) v.smoothHeight(); else if (h) { r.slides.width(r.computedW); r.update(r.pagingCount); r.setProps() } else if (l) { r.viewport.height(r.h); r.setProps(r.h, "setTotal") } else { r.vars.smoothHeight && v.smoothHeight(); r.newSlides.width(r.computedW); r.setProps(r.computedW, "setTotal") } } }, smoothHeight: function (e) { if (!l || p) { var t = p ? r : r.viewport; e ? t.animate({ height: r.slides.eq(r.animatingTo).height() }, e) : t.height(r.slides.eq(r.animatingTo).height()) } }, sync: function (t) { var n = e(r.vars.sync).data("flexslider"), i = r.animatingTo; switch (t) { case "animate": n.flexAnimate(i, r.vars.pauseOnAction, !1, !0); break; case "play": !n.playing && !n.asNav && n.play(); break; case "pause": n.pause() } }, pauseInvisible: { visProp: null, init: function () { var e = ["webkit", "moz", "ms", "o"]; if ("hidden" in document) return "hidden"; for (var t = 0; t < e.length; t++) e[t] + "Hidden" in document && (v.pauseInvisible.visProp = e[t] + "Hidden"); if (v.pauseInvisible.visProp) { var n = v.pauseInvisible.visProp.replace(/[H|h]idden/, "") + "visibilitychange"; document.addEventListener(n, function () { v.pauseInvisible.isHidden() ? r.startTimeout ? clearTimeout(r.startTimeout) : r.pause() : r.started ? r.play() : r.vars.initDelay > 0 ? setTimeout(r.play, r.vars.initDelay) : r.play() }) } }, isHidden: function () { return document[v.pauseInvisible.visProp] || !1 } }, setToClearWatchedEvent: function () { clearTimeout(f); f = setTimeout(function () { a = "" }, 3e3) } }; r.flexAnimate = function (t, n, s, u, a) { !r.vars.animationLoop && t !== r.currentSlide && (r.direction = t > r.currentSlide ? "next" : "prev"); d && r.pagingCount === 1 && (r.direction = r.currentItem < t ? "next" : "prev"); if (!r.animating && (r.canAdvance(t, a) || s) && r.is(":visible")) { if (d && u) { var f = e(r.vars.asNavFor).data("flexslider"); r.atEnd = t === 0 || t === r.count - 1; f.flexAnimate(t, !0, !1, !0, a); r.direction = r.currentItem < t ? "next" : "prev"; f.direction = r.direction; if (Math.ceil((t + 1) / r.visible) - 1 === r.currentSlide || t === 0) { r.currentItem = t; r.slides.removeClass(i + "active-slide").eq(t).addClass(i + "active-slide"); return !1 } r.currentItem = t; r.slides.removeClass(i + "active-slide").eq(t).addClass(i + "active-slide"); t = Math.floor(t / r.visible) } r.animating = !0; r.animatingTo = t; n && r.pause(); r.vars.before(r); r.syncExists && !a && v.sync("animate"); r.vars.controlNav && v.controlNav.active(); h || r.slides.removeClass(i + "active-slide").eq(t).addClass(i + "active-slide"); r.atEnd = t === 0 || t === r.last; r.vars.directionNav && v.directionNav.update(); if (t === r.last) { r.vars.end(r); r.vars.animationLoop || r.pause() } if (!p) { var m = l ? r.slides.filter(":first").height() : r.computedW, g, y, b; if (h) { g = r.vars.itemMargin; b = (r.itemW + g) * r.move * r.animatingTo; y = b > r.limit && r.visible !== 1 ? r.limit : b } else r.currentSlide === 0 && t === r.count - 1 && r.vars.animationLoop && r.direction !== "next" ? y = c ? (r.count + r.cloneOffset) * m : 0 : r.currentSlide === r.last && t === 0 && r.vars.animationLoop && r.direction !== "prev" ? y = c ? 0 : (r.count + 1) * m : y = c ? (r.count - 1 - t + r.cloneOffset) * m : (t + r.cloneOffset) * m; r.setProps(y, "", r.vars.animationSpeed); if (r.transitions) { if (!r.vars.animationLoop || !r.atEnd) { r.animating = !1; r.currentSlide = r.animatingTo } r.container.unbind("webkitTransitionEnd transitionend"); r.container.bind("webkitTransitionEnd transitionend", function () { r.wrapup(m) }) } else r.container.animate(r.args, r.vars.animationSpeed, r.vars.easing, function () { r.wrapup(m) }) } else if (!o) { r.slides.eq(r.currentSlide).css({ zIndex: 1 }).animate({ opacity: 0 }, r.vars.animationSpeed, r.vars.easing); r.slides.eq(t).css({ zIndex: 2 }).animate({ opacity: 1 }, r.vars.animationSpeed, r.vars.easing, r.wrapup) } else { r.slides.eq(r.currentSlide).css({ opacity: 0, zIndex: 1 }); r.slides.eq(t).css({ opacity: 1, zIndex: 2 }); r.wrapup(m) } r.vars.smoothHeight && v.smoothHeight(r.vars.animationSpeed) } }; r.wrapup = function (e) { !p && !h && (r.currentSlide === 0 && r.animatingTo === r.last && r.vars.animationLoop ? r.setProps(e, "jumpEnd") : r.currentSlide === r.last && r.animatingTo === 0 && r.vars.animationLoop && r.setProps(e, "jumpStart")); r.animating = !1; r.currentSlide = r.animatingTo; r.vars.after(r) }; r.animateSlides = function () { !r.animating && m && r.flexAnimate(r.getTarget("next")) }; r.pause = function () { clearInterval(r.animatedSlides); r.animatedSlides = null; r.playing = !1; r.vars.pausePlay && v.pausePlay.update("play"); r.syncExists && v.sync("pause") }; r.play = function () { r.playing && clearInterval(r.animatedSlides); r.animatedSlides = r.animatedSlides || setInterval(r.animateSlides, r.vars.slideshowSpeed); r.started = r.playing = !0; r.vars.pausePlay && v.pausePlay.update("pause"); r.syncExists && v.sync("play") }; r.stop = function () { r.pause(); r.stopped = !0 }; r.canAdvance = function (e, t) { var n = d ? r.pagingCount - 1 : r.last; return t ? !0 : d && r.currentItem === r.count - 1 && e === 0 && r.direction === "prev" ? !0 : d && r.currentItem === 0 && e === r.pagingCount - 1 && r.direction !== "next" ? !1 : e === r.currentSlide && !d ? !1 : r.vars.animationLoop ? !0 : r.atEnd && r.currentSlide === 0 && e === n && r.direction !== "next" ? !1 : r.atEnd && r.currentSlide === n && e === 0 && r.direction === "next" ? !1 : !0 }; r.getTarget = function (e) { r.direction = e; return e === "next" ? r.currentSlide === r.last ? 0 : r.currentSlide + 1 : r.currentSlide === 0 ? r.last : r.currentSlide - 1 }; r.setProps = function (e, t, n) { var i = function () { var n = e ? e : (r.itemW + r.vars.itemMargin) * r.move * r.animatingTo, i = function () { if (h) return t === "setTouch" ? e : c && r.animatingTo === r.last ? 0 : c ? r.limit - (r.itemW + r.vars.itemMargin) * r.move * r.animatingTo : r.animatingTo === r.last ? r.limit : n; switch (t) { case "setTotal": return c ? (r.count - 1 - r.currentSlide + r.cloneOffset) * e : (r.currentSlide + r.cloneOffset) * e; case "setTouch": return c ? e : e; case "jumpEnd": return c ? e : r.count * e; case "jumpStart": return c ? r.count * e : e; default: return e } }(); return i * -1 + "px" }(); if (r.transitions) { i = l ? "translate3d(0," + i + ",0)" : "translate3d(" + i + ",0,0)"; n = n !== undefined ? n / 1e3 + "s" : "0s"; r.container.css("-" + r.pfx + "-transition-duration", n) } r.args[r.prop] = i; (r.transitions || n === undefined) && r.container.css(r.args) }; r.setup = function (t) { if (!p) { var n, s; if (t === "init") { r.viewport = e('<div class="' + i + 'viewport"></div>').css({ overflow: "hidden", position: "relative" }).appendTo(r).append(r.container); r.cloneCount = 0; r.cloneOffset = 0; if (c) { s = e.makeArray(r.slides).reverse(); r.slides = e(s); r.container.empty().append(r.slides) } } if (r.vars.animationLoop && !h) { r.cloneCount = 2; r.cloneOffset = 1; t !== "init" && r.container.find(".clone").remove(); r.container.append(r.slides.first().clone().addClass("clone").attr("aria-hidden", "true")).prepend(r.slides.last().clone().addClass("clone").attr("aria-hidden", "true")) } r.newSlides = e(r.vars.selector, r); n = c ? r.count - 1 - r.currentSlide + r.cloneOffset : r.currentSlide + r.cloneOffset; if (l && !h) { r.container.height((r.count + r.cloneCount) * 200 + "%").css("position", "absolute").width("100%"); setTimeout(function () { r.newSlides.css({ display: "block" }); r.doMath(); r.viewport.height(r.h); r.setProps(n * r.h, "init") }, t === "init" ? 100 : 0) } else { r.container.width((r.count + r.cloneCount) * 200 + "%"); r.setProps(n * r.computedW, "init"); setTimeout(function () { r.doMath(); r.newSlides.css({ width: r.computedW, "float": "left", display: "block" }); r.vars.smoothHeight && v.smoothHeight() }, t === "init" ? 100 : 0) } } else { r.slides.css({ width: "100%", "float": "left", marginRight: "-100%", position: "relative" }); t === "init" && (o ? r.slides.css({ opacity: 0, display: "block", webkitTransition: "opacity " + r.vars.animationSpeed / 1e3 + "s ease", zIndex: 1 }).eq(r.currentSlide).css({ opacity: 1, zIndex: 2 }) : r.slides.css({ opacity: 0, display: "block", zIndex: 1 }).eq(r.currentSlide).css({ zIndex: 2 }).animate({ opacity: 1 }, r.vars.animationSpeed, r.vars.easing)); r.vars.smoothHeight && v.smoothHeight() } h || r.slides.removeClass(i + "active-slide").eq(r.currentSlide).addClass(i + "active-slide") }; r.doMath = function () { var e = r.slides.first(), t = r.vars.itemMargin, n = r.vars.minItems, i = r.vars.maxItems; r.w = r.viewport === undefined ? r.width() : r.viewport.width(); r.h = e.height(); r.boxPadding = e.outerWidth() - e.width(); if (h) { r.itemT = r.vars.itemWidth + t; r.minW = n ? n * r.itemT : r.w; r.maxW = i ? i * r.itemT - t : r.w; r.itemW = r.minW > r.w ? (r.w - t * (n - 1)) / n : r.maxW < r.w ? (r.w - t * (i - 1)) / i : r.vars.itemWidth > r.w ? r.w : r.vars.itemWidth; r.visible = Math.floor(r.w / r.itemW); r.move = r.vars.move > 0 && r.vars.move < r.visible ? r.vars.move : r.visible; r.pagingCount = Math.ceil((r.count - r.visible) / r.move + 1); r.last = r.pagingCount - 1; r.limit = r.pagingCount === 1 ? 0 : r.vars.itemWidth > r.w ? r.itemW * (r.count - 1) + t * (r.count - 1) : (r.itemW + t) * r.count - r.w - t } else { r.itemW = r.w; r.pagingCount = r.count; r.last = r.count - 1 } r.computedW = r.itemW - r.boxPadding }; r.update = function (e, t) { r.doMath(); if (!h) { e < r.currentSlide ? r.currentSlide += 1 : e <= r.currentSlide && e !== 0 && (r.currentSlide -= 1); r.animatingTo = r.currentSlide } if (r.vars.controlNav && !r.manualControls) if (t === "add" && !h || r.pagingCount > r.controlNav.length) v.controlNav.update("add"); else if (t === "remove" && !h || r.pagingCount < r.controlNav.length) { if (h && r.currentSlide > r.last) { r.currentSlide -= 1; r.animatingTo -= 1 } v.controlNav.update("remove", r.last) } r.vars.directionNav && v.directionNav.update() }; r.addSlide = function (t, n) { var i = e(t); r.count += 1; r.last = r.count - 1; l && c ? n !== undefined ? r.slides.eq(r.count - n).after(i) : r.container.prepend(i) : n !== undefined ? r.slides.eq(n).before(i) : r.container.append(i); r.update(n, "add"); r.slides = e(r.vars.selector + ":not(.clone)", r); r.setup(); r.vars.added(r) }; r.removeSlide = function (t) { var n = isNaN(t) ? r.slides.index(e(t)) : t; r.count -= 1; r.last = r.count - 1; isNaN(t) ? e(t, r.slides).remove() : l && c ? r.slides.eq(r.last).remove() : r.slides.eq(t).remove(); r.doMath(); r.update(n, "remove"); r.slides = e(r.vars.selector + ":not(.clone)", r); r.setup(); r.vars.removed(r) }; v.init() }; e(window).blur(function (e) { focused = !1 }).focus(function (e) { focused = !0 }); e.flexslider.defaults = { namespace: "flex-", selector: ".slides > li", animation: "fade", easing: "swing", direction: "horizontal", reverse: !1, animationLoop: !0, smoothHeight: !1, startAt: 0, slideshow: !0, slideshowSpeed: 7e3, animationSpeed: 600, initDelay: 0, randomize: !1, thumbCaptions: !1, pauseOnAction: !0, pauseOnHover: !1, pauseInvisible: !0, useCSS: !0, touch: !0, video: !1, controlNav: !0, directionNav: !0, prevText: "Previous", nextText: "Next", keyboard: !0, multipleKeyboard: !1, mousewheel: !1, pausePlay: !1, pauseText: "Pause", playText: "Play", controlsContainer: "", manualControls: "", sync: "", asNavFor: "", itemWidth: 0, itemMargin: 0, minItems: 1, maxItems: 0, move: 0, allowOneSlide: !0, start: function () { }, before: function () { }, after: function () { }, end: function () { }, added: function () { }, removed: function () { } }; e.fn.flexslider = function (t) { t === undefined && (t = {}); if (typeof t == "object") return this.each(function () { var n = e(this), r = t.selector ? t.selector : ".slides > li", i = n.find(r); if (i.length === 1 && t.allowOneSlide === !0 || i.length === 0) { i.fadeIn(400); t.start && t.start(n) } else n.data("flexslider") === undefined && new e.flexslider(this, t) }); var n = e(this).data("flexslider"); switch (t) { case "play": n.play(); break; case "pause": n.pause(); break; case "stop": n.stop(); break; case "next": n.flexAnimate(n.getTarget("next"), !0); break; case "prev": case "previous": n.flexAnimate(n.getTarget("prev"), !0); break; default: typeof t == "number" && n.flexAnimate(t, !0) } } })(jQuery);

/* bxSlider  */
/*jquery.bxslider.min.js*/
 /**
  * BxSlider v4.1.1 - Fully loaded, responsive content slider
  * http://bxslider.com
  *
  * Copyright 2013, Steven Wanderski - http://stevenwanderski.com - http://bxcreative.com
  * Written while drinking Belgian ales and listening to jazz
  *
  * Released under the MIT license - http://opensource.org/licenses/MIT
  */
 !function (t) { var e = {}, s = { mode: "horizontal", slideSelector: "", infiniteLoop: !0, hideControlOnEnd: !1, speed: 500, easing: null, slideMargin: 0, startSlide: 0, randomStart: !1, captions: !1, ticker: !1, tickerHover: !1, adaptiveHeight: !1, adaptiveHeightSpeed: 500, video: !1, useCSS: !0, preloadImages: "visible", responsive: !0, touchEnabled: !0, swipeThreshold: 50, oneToOneTouch: !0, preventDefaultSwipeX: !0, preventDefaultSwipeY: !1, pager: !0, pagerType: "full", pagerShortSeparator: " / ", pagerSelector: null, buildPager: null, pagerCustom: null, controls: !0, nextText: "Next", prevText: "Prev", nextSelector: null, prevSelector: null, autoControls: !1, startText: "Start", stopText: "Stop", autoControlsCombine: !1, autoControlsSelector: null, auto: !1, pause: 4e3, autoStart: !0, autoDirection: "next", autoHover: !1, autoDelay: 0, minSlides: 1, maxSlides: 1, moveSlides: 0, slideWidth: 0, onSliderLoad: function () { }, onSlideBefore: function () { }, onSlideAfter: function () { }, onSlideNext: function () { }, onSlidePrev: function () { } }; t.fn.bxSlider = function (n) { if (0 == this.length) return this; if (this.length > 1) return this.each(function () { t(this).bxSlider(n) }), this; var o = {}, r = this; e.el = this; var a = t(window).width(), l = t(window).height(), d = function () { o.settings = t.extend({}, s, n), o.settings.slideWidth = parseInt(o.settings.slideWidth), o.children = r.children(o.settings.slideSelector), o.children.length < o.settings.minSlides && (o.settings.minSlides = o.children.length), o.children.length < o.settings.maxSlides && (o.settings.maxSlides = o.children.length), o.settings.randomStart && (o.settings.startSlide = Math.floor(Math.random() * o.children.length)), o.active = { index: o.settings.startSlide }, o.carousel = o.settings.minSlides > 1 || o.settings.maxSlides > 1, o.carousel && (o.settings.preloadImages = "all"), o.minThreshold = o.settings.minSlides * o.settings.slideWidth + (o.settings.minSlides - 1) * o.settings.slideMargin, o.maxThreshold = o.settings.maxSlides * o.settings.slideWidth + (o.settings.maxSlides - 1) * o.settings.slideMargin, o.working = !1, o.controls = {}, o.interval = null, o.animProp = "vertical" == o.settings.mode ? "top" : "left", o.usingCSS = o.settings.useCSS && "fade" != o.settings.mode && function () { var t = document.createElement("div"), e = ["WebkitPerspective", "MozPerspective", "OPerspective", "msPerspective"]; for (var i in e) if (void 0 !== t.style[e[i]]) return o.cssPrefix = e[i].replace("Perspective", "").toLowerCase(), o.animProp = "-" + o.cssPrefix + "-transform", !0; return !1 }(), "vertical" == o.settings.mode && (o.settings.maxSlides = o.settings.minSlides), r.data("origStyle", r.attr("style")), r.children(o.settings.slideSelector).each(function () { t(this).data("origStyle", t(this).attr("style")) }), c() }, c = function () { r.wrap('<div class="bx-wrapper"><div class="bx-viewport"></div></div>'), o.viewport = r.parent(), o.loader = t('<div class="bx-loading" />'), o.viewport.prepend(o.loader), r.css({ width: "horizontal" == o.settings.mode ? 100 * o.children.length + 215 + "%" : "auto", position: "relative" }), o.usingCSS && o.settings.easing ? r.css("-" + o.cssPrefix + "-transition-timing-function", o.settings.easing) : o.settings.easing || (o.settings.easing = "swing"), f(), o.viewport.css({ width: "100%", overflow: "hidden", position: "relative" }), o.viewport.parent().css({ maxWidth: v() }), o.settings.pager || o.viewport.parent().css({ margin: "0 auto 0px" }), o.children.css({ "float": "horizontal" == o.settings.mode ? "left" : "none", listStyle: "none", position: "relative" }), o.children.css("width", u()), "horizontal" == o.settings.mode && o.settings.slideMargin > 0 && o.children.css("marginRight", o.settings.slideMargin), "vertical" == o.settings.mode && o.settings.slideMargin > 0 && o.children.css("marginBottom", o.settings.slideMargin), "fade" == o.settings.mode && (o.children.css({ position: "absolute", zIndex: 0, display: "none" }), o.children.eq(o.settings.startSlide).css({ zIndex: 50, display: "block" })), o.controls.el = t('<div class="bx-controls" />'), o.settings.captions && P(), o.active.last = o.settings.startSlide == x() - 1, o.settings.video && r.fitVids(); var e = o.children.eq(o.settings.startSlide); "all" == o.settings.preloadImages && (e = o.children), o.settings.ticker ? o.settings.pager = !1 : (o.settings.pager && T(), o.settings.controls && C(), o.settings.auto && o.settings.autoControls && E(), (o.settings.controls || o.settings.autoControls || o.settings.pager) && o.viewport.after(o.controls.el)), g(e, h) }, g = function (e, i) { var s = e.find("img, iframe").length; if (0 == s) return i(), void 0; var n = 0; e.find("img, iframe").each(function () { t(this).one("load", function () { ++n == s && i() }).each(function () { this.complete && t(this).load() }) }) }, h = function () { if (o.settings.infiniteLoop && "fade" != o.settings.mode && !o.settings.ticker) { var e = "vertical" == o.settings.mode ? o.settings.minSlides : o.settings.maxSlides, i = o.children.slice(0, e).clone().addClass("bx-clone"), s = o.children.slice(-e).clone().addClass("bx-clone"); r.append(i).prepend(s) } o.loader.remove(), S(), "vertical" == o.settings.mode && (o.settings.adaptiveHeight = !0), o.viewport.height(p()), r.redrawSlider(), o.settings.onSliderLoad(o.active.index), o.initialized = !0, o.settings.responsive && t(window).bind("resize", B), o.settings.auto && o.settings.autoStart && H(), o.settings.ticker && L(), o.settings.pager && I(o.settings.startSlide), o.settings.controls && W(), o.settings.touchEnabled && !o.settings.ticker && O() }, p = function () { var e = 0, s = t(); if ("vertical" == o.settings.mode || o.settings.adaptiveHeight) if (o.carousel) { var n = 1 == o.settings.moveSlides ? o.active.index : o.active.index * m(); for (s = o.children.eq(n), i = 1; i <= o.settings.maxSlides - 1; i++) s = n + i >= o.children.length ? s.add(o.children.eq(i - 1)) : s.add(o.children.eq(n + i)) } else s = o.children.eq(o.active.index); else s = o.children; return "vertical" == o.settings.mode ? (s.each(function () { e += t(this).outerHeight() }), o.settings.slideMargin > 0 && (e += o.settings.slideMargin * (o.settings.minSlides - 1))) : e = Math.max.apply(Math, s.map(function () { return t(this).outerHeight(!1) }).get()), e }, v = function () { var t = "100%"; return o.settings.slideWidth > 0 && (t = "horizontal" == o.settings.mode ? o.settings.maxSlides * o.settings.slideWidth + (o.settings.maxSlides - 1) * o.settings.slideMargin : o.settings.slideWidth), t }, u = function () { var t = o.settings.slideWidth, e = o.viewport.width(); return 0 == o.settings.slideWidth || o.settings.slideWidth > e && !o.carousel || "vertical" == o.settings.mode ? t = e : o.settings.maxSlides > 1 && "horizontal" == o.settings.mode && (e > o.maxThreshold || e < o.minThreshold && (t = (e - o.settings.slideMargin * (o.settings.minSlides - 1)) / o.settings.minSlides)), t }, f = function () { var t = 1; if ("horizontal" == o.settings.mode && o.settings.slideWidth > 0) if (o.viewport.width() < o.minThreshold) t = o.settings.minSlides; else if (o.viewport.width() > o.maxThreshold) t = o.settings.maxSlides; else { var e = o.children.first().width(); t = Math.floor(o.viewport.width() / e) } else "vertical" == o.settings.mode && (t = o.settings.minSlides); return t }, x = function () { var t = 0; if (o.settings.moveSlides > 0) if (o.settings.infiniteLoop) t = o.children.length / m(); else for (var e = 0, i = 0; e < o.children.length;)++t, e = i + f(), i += o.settings.moveSlides <= f() ? o.settings.moveSlides : f(); else t = Math.ceil(o.children.length / f()); return t }, m = function () { return o.settings.moveSlides > 0 && o.settings.moveSlides <= f() ? o.settings.moveSlides : f() }, S = function () { if (o.children.length > o.settings.maxSlides && o.active.last && !o.settings.infiniteLoop) { if ("horizontal" == o.settings.mode) { var t = o.children.last(), e = t.position(); b(-(e.left - (o.viewport.width() - t.width())), "reset", 0) } else if ("vertical" == o.settings.mode) { var i = o.children.length - o.settings.minSlides, e = o.children.eq(i).position(); b(-e.top, "reset", 0) } } else { var e = o.children.eq(o.active.index * m()).position(); o.active.index == x() - 1 && (o.active.last = !0), void 0 != e && ("horizontal" == o.settings.mode ? b(-e.left, "reset", 0) : "vertical" == o.settings.mode && b(-e.top, "reset", 0)) } }, b = function (t, e, i, s) { if (o.usingCSS) { var n = "vertical" == o.settings.mode ? "translate3d(0, " + t + "px, 0)" : "translate3d(" + t + "px, 0, 0)"; r.css("-" + o.cssPrefix + "-transition-duration", i / 1e3 + "s"), "slide" == e ? (r.css(o.animProp, n), r.bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function () { r.unbind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd"), D() })) : "reset" == e ? r.css(o.animProp, n) : "ticker" == e && (r.css("-" + o.cssPrefix + "-transition-timing-function", "linear"), r.css(o.animProp, n), r.bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function () { r.unbind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd"), b(s.resetValue, "reset", 0), N() })) } else { var a = {}; a[o.animProp] = t, "slide" == e ? r.animate(a, i, o.settings.easing, function () { D() }) : "reset" == e ? r.css(o.animProp, t) : "ticker" == e && r.animate(a, speed, "linear", function () { b(s.resetValue, "reset", 0), N() }) } }, w = function () { for (var e = "", i = x(), s = 0; i > s; s++) { var n = ""; o.settings.buildPager && t.isFunction(o.settings.buildPager) ? (n = o.settings.buildPager(s), o.pagerEl.addClass("bx-custom-pager")) : (n = s + 1, o.pagerEl.addClass("bx-default-pager")), e += '<div class="bx-pager-item"><a href="" data-slide-index="' + s + '" class="bx-pager-link">' + n + "</a></div>" } o.pagerEl.html(e) }, T = function () { o.settings.pagerCustom ? o.pagerEl = t(o.settings.pagerCustom) : (o.pagerEl = t('<div class="bx-pager" />'), o.settings.pagerSelector ? t(o.settings.pagerSelector).html(o.pagerEl) : o.controls.el.addClass("bx-has-pager").append(o.pagerEl), w()), o.pagerEl.delegate("a", "click", q) }, C = function () { o.controls.next = t('<a class="bx-next" href="">' + o.settings.nextText + "</a>"), o.controls.prev = t('<a class="bx-prev" href="">' + o.settings.prevText + "</a>"), o.controls.next.bind("click", y), o.controls.prev.bind("click", z), o.settings.nextSelector && t(o.settings.nextSelector).append(o.controls.next), o.settings.prevSelector && t(o.settings.prevSelector).append(o.controls.prev), o.settings.nextSelector || o.settings.prevSelector || (o.controls.directionEl = t('<div class="bx-controls-direction" />'), o.controls.directionEl.append(o.controls.prev).append(o.controls.next), o.controls.el.addClass("bx-has-controls-direction").append(o.controls.directionEl)) }, E = function () { o.controls.start = t('<div class="bx-controls-auto-item"><a class="bx-start" href="">' + o.settings.startText + "</a></div>"), o.controls.stop = t('<div class="bx-controls-auto-item"><a class="bx-stop" href="">' + o.settings.stopText + "</a></div>"), o.controls.autoEl = t('<div class="bx-controls-auto" />'), o.controls.autoEl.delegate(".bx-start", "click", k), o.controls.autoEl.delegate(".bx-stop", "click", M), o.settings.autoControlsCombine ? o.controls.autoEl.append(o.controls.start) : o.controls.autoEl.append(o.controls.start).append(o.controls.stop), o.settings.autoControlsSelector ? t(o.settings.autoControlsSelector).html(o.controls.autoEl) : o.controls.el.addClass("bx-has-controls-auto").append(o.controls.autoEl), A(o.settings.autoStart ? "stop" : "start") }, P = function () { o.children.each(function () { var e = t(this).find("img:first").attr("title"); void 0 != e && ("" + e).length && t(this).append('<div class="bx-caption"><span>' + e + "</span></div>") }) }, y = function (t) { o.settings.auto && r.stopAuto(), r.goToNextSlide(), t.preventDefault() }, z = function (t) { o.settings.auto && r.stopAuto(), r.goToPrevSlide(), t.preventDefault() }, k = function (t) { r.startAuto(), t.preventDefault() }, M = function (t) { r.stopAuto(), t.preventDefault() }, q = function (e) { o.settings.auto && r.stopAuto(); var i = t(e.currentTarget), s = parseInt(i.attr("data-slide-index")); s != o.active.index && r.goToSlide(s), e.preventDefault() }, I = function (e) { var i = o.children.length; return "short" == o.settings.pagerType ? (o.settings.maxSlides > 1 && (i = Math.ceil(o.children.length / o.settings.maxSlides)), o.pagerEl.html(e + 1 + o.settings.pagerShortSeparator + i), void 0) : (o.pagerEl.find("a").removeClass("active"), o.pagerEl.each(function (i, s) { t(s).find("a").eq(e).addClass("active") }), void 0) }, D = function () { if (o.settings.infiniteLoop) { var t = ""; 0 == o.active.index ? t = o.children.eq(0).position() : o.active.index == x() - 1 && o.carousel ? t = o.children.eq((x() - 1) * m()).position() : o.active.index == o.children.length - 1 && (t = o.children.eq(o.children.length - 1).position()), "horizontal" == o.settings.mode ? b(-t.left, "reset", 0) : "vertical" == o.settings.mode && b(-t.top, "reset", 0) } o.working = !1, o.settings.onSlideAfter(o.children.eq(o.active.index), o.oldIndex, o.active.index) }, A = function (t) { o.settings.autoControlsCombine ? o.controls.autoEl.html(o.controls[t]) : (o.controls.autoEl.find("a").removeClass("active"), o.controls.autoEl.find("a:not(.bx-" + t + ")").addClass("active")) }, W = function () { 1 == x() ? (o.controls.prev.addClass("disabled"), o.controls.next.addClass("disabled")) : !o.settings.infiniteLoop && o.settings.hideControlOnEnd && (0 == o.active.index ? (o.controls.prev.addClass("disabled"), o.controls.next.removeClass("disabled")) : o.active.index == x() - 1 ? (o.controls.next.addClass("disabled"), o.controls.prev.removeClass("disabled")) : (o.controls.prev.removeClass("disabled"), o.controls.next.removeClass("disabled"))) }, H = function () { o.settings.autoDelay > 0 ? setTimeout(r.startAuto, o.settings.autoDelay) : r.startAuto(), o.settings.autoHover && r.hover(function () { o.interval && (r.stopAuto(!0), o.autoPaused = !0) }, function () { o.autoPaused && (r.startAuto(!0), o.autoPaused = null) }) }, L = function () { var e = 0; if ("next" == o.settings.autoDirection) r.append(o.children.clone().addClass("bx-clone")); else { r.prepend(o.children.clone().addClass("bx-clone")); var i = o.children.first().position(); e = "horizontal" == o.settings.mode ? -i.left : -i.top } b(e, "reset", 0), o.settings.pager = !1, o.settings.controls = !1, o.settings.autoControls = !1, o.settings.tickerHover && !o.usingCSS && o.viewport.hover(function () { r.stop() }, function () { var e = 0; o.children.each(function () { e += "horizontal" == o.settings.mode ? t(this).outerWidth(!0) : t(this).outerHeight(!0) }); var i = o.settings.speed / e, s = "horizontal" == o.settings.mode ? "left" : "top", n = i * (e - Math.abs(parseInt(r.css(s)))); N(n) }), N() }, N = function (t) { speed = t ? t : o.settings.speed; var e = { left: 0, top: 0 }, i = { left: 0, top: 0 }; "next" == o.settings.autoDirection ? e = r.find(".bx-clone").first().position() : i = o.children.first().position(); var s = "horizontal" == o.settings.mode ? -e.left : -e.top, n = "horizontal" == o.settings.mode ? -i.left : -i.top, a = { resetValue: n }; b(s, "ticker", speed, a) }, O = function () { o.touch = { start: { x: 0, y: 0 }, end: { x: 0, y: 0 } }, o.viewport.bind("touchstart", X) }, X = function (t) { if (o.working) t.preventDefault(); else { o.touch.originalPos = r.position(); var e = t.originalEvent; o.touch.start.x = e.changedTouches[0].pageX, o.touch.start.y = e.changedTouches[0].pageY, o.viewport.bind("touchmove", Y), o.viewport.bind("touchend", V) } }, Y = function (t) { var e = t.originalEvent, i = Math.abs(e.changedTouches[0].pageX - o.touch.start.x), s = Math.abs(e.changedTouches[0].pageY - o.touch.start.y); if (3 * i > s && o.settings.preventDefaultSwipeX ? t.preventDefault() : 3 * s > i && o.settings.preventDefaultSwipeY && t.preventDefault(), "fade" != o.settings.mode && o.settings.oneToOneTouch) { var n = 0; if ("horizontal" == o.settings.mode) { var r = e.changedTouches[0].pageX - o.touch.start.x; n = o.touch.originalPos.left + r } else { var r = e.changedTouches[0].pageY - o.touch.start.y; n = o.touch.originalPos.top + r } b(n, "reset", 0) } }, V = function (t) { o.viewport.unbind("touchmove", Y); var e = t.originalEvent, i = 0; if (o.touch.end.x = e.changedTouches[0].pageX, o.touch.end.y = e.changedTouches[0].pageY, "fade" == o.settings.mode) { var s = Math.abs(o.touch.start.x - o.touch.end.x); s >= o.settings.swipeThreshold && (o.touch.start.x > o.touch.end.x ? r.goToNextSlide() : r.goToPrevSlide(), r.stopAuto()) } else { var s = 0; "horizontal" == o.settings.mode ? (s = o.touch.end.x - o.touch.start.x, i = o.touch.originalPos.left) : (s = o.touch.end.y - o.touch.start.y, i = o.touch.originalPos.top), !o.settings.infiniteLoop && (0 == o.active.index && s > 0 || o.active.last && 0 > s) ? b(i, "reset", 200) : Math.abs(s) >= o.settings.swipeThreshold ? (0 > s ? r.goToNextSlide() : r.goToPrevSlide(), r.stopAuto()) : b(i, "reset", 200) } o.viewport.unbind("touchend", V) }, B = function () { var e = t(window).width(), i = t(window).height(); (a != e || l != i) && (a = e, l = i, r.redrawSlider()) }; return r.goToSlide = function (e, i) { if (!o.working && o.active.index != e) if (o.working = !0, o.oldIndex = o.active.index, o.active.index = 0 > e ? x() - 1 : e >= x() ? 0 : e, o.settings.onSlideBefore(o.children.eq(o.active.index), o.oldIndex, o.active.index), "next" == i ? o.settings.onSlideNext(o.children.eq(o.active.index), o.oldIndex, o.active.index) : "prev" == i && o.settings.onSlidePrev(o.children.eq(o.active.index), o.oldIndex, o.active.index), o.active.last = o.active.index >= x() - 1, o.settings.pager && I(o.active.index), o.settings.controls && W(), "fade" == o.settings.mode) o.settings.adaptiveHeight && o.viewport.height() != p() && o.viewport.animate({ height: p() }, o.settings.adaptiveHeightSpeed), o.children.filter(":visible").fadeOut(o.settings.speed).css({ zIndex: 0 }), o.children.eq(o.active.index).css("zIndex", 51).fadeIn(o.settings.speed, function () { t(this).css("zIndex", 50), D() }); else { o.settings.adaptiveHeight && o.viewport.height() != p() && o.viewport.animate({ height: p() }, o.settings.adaptiveHeightSpeed); var s = 0, n = { left: 0, top: 0 }; if (!o.settings.infiniteLoop && o.carousel && o.active.last) if ("horizontal" == o.settings.mode) { var a = o.children.eq(o.children.length - 1); n = a.position(), s = o.viewport.width() - a.outerWidth() } else { var l = o.children.length - o.settings.minSlides; n = o.children.eq(l).position() } else if (o.carousel && o.active.last && "prev" == i) { var d = 1 == o.settings.moveSlides ? o.settings.maxSlides - m() : (x() - 1) * m() - (o.children.length - o.settings.maxSlides), a = r.children(".bx-clone").eq(d); n = a.position() } else if ("next" == i && 0 == o.active.index) n = r.find("> .bx-clone").eq(o.settings.maxSlides).position(), o.active.last = !1; else if (e >= 0) { var c = e * m(); n = o.children.eq(c).position() } if ("undefined" != typeof n) { var g = "horizontal" == o.settings.mode ? -(n.left - s) : -n.top; b(g, "slide", o.settings.speed) } } }, r.goToNextSlide = function () { if (o.settings.infiniteLoop || !o.active.last) { var t = parseInt(o.active.index) + 1; r.goToSlide(t, "next") } }, r.goToPrevSlide = function () { if (o.settings.infiniteLoop || 0 != o.active.index) { var t = parseInt(o.active.index) - 1; r.goToSlide(t, "prev") } }, r.startAuto = function (t) { o.interval || (o.interval = setInterval(function () { "next" == o.settings.autoDirection ? r.goToNextSlide() : r.goToPrevSlide() }, o.settings.pause), o.settings.autoControls && 1 != t && A("stop")) }, r.stopAuto = function (t) { o.interval && (clearInterval(o.interval), o.interval = null, o.settings.autoControls && 1 != t && A("start")) }, r.getCurrentSlide = function () { return o.active.index }, r.getSlideCount = function () { return o.children.length }, r.redrawSlider = function () { o.children.add(r.find(".bx-clone")).outerWidth(u()), o.viewport.css("height", p()), o.settings.ticker || S(), o.active.last && (o.active.index = x() - 1), o.active.index >= x() && (o.active.last = !0), o.settings.pager && !o.settings.pagerCustom && (w(), I(o.active.index)) }, r.destroySlider = function () { o.initialized && (o.initialized = !1, t(".bx-clone", this).remove(), o.children.each(function () { void 0 != t(this).data("origStyle") ? t(this).attr("style", t(this).data("origStyle")) : t(this).removeAttr("style") }), void 0 != t(this).data("origStyle") ? this.attr("style", t(this).data("origStyle")) : t(this).removeAttr("style"), t(this).unwrap().unwrap(), o.controls.el && o.controls.el.remove(), o.controls.next && o.controls.next.remove(), o.controls.prev && o.controls.prev.remove(), o.pagerEl && o.pagerEl.remove(), t(".bx-caption", this).remove(), o.controls.autoEl && o.controls.autoEl.remove(), clearInterval(o.interval), o.settings.responsive && t(window).unbind("resize", B)) }, r.reloadSlider = function (t) { void 0 != t && (n = t), r.destroySlider(), d() }, d(), this } }(jQuery);

/* Easy Responsive Tabs (Job Profiles)  */
/*easyResponsiveTabs.js*/
 // Easy Responsive Tabs Plugin
 // Author: Samson.Onna <Email : samson3d@gmail.com>
 (function ($) {
     $.fn.extend({
         easyResponsiveTabs: function (options) {
             //Set the default values, use comma to separate the settings, example:
             var defaults = {
                 type: 'default', //default, vertical, accordion;
                 width: 'auto',
                 fit: true,
                 closed: false,
                 activate: function () { }
             }
             //Variables
             var options = $.extend(defaults, options);
             var opt = options, jtype = opt.type, jfit = opt.fit, jwidth = opt.width, vtabs = 'vertical', accord = 'accordion';
             var hash = window.location.hash;
             var historyApi = !!(window.history && history.replaceState);

             //Events
             $(this).bind('tabactivate', function (e, currentTab) {
                 if (typeof options.activate === 'function') {
                     options.activate.call(currentTab, e)
                 }
             });

             //Main function
             this.each(function () {
                 var $respTabs = $(this);
                 var $respTabsList = $respTabs.find('ul.resp-tabs-list');
                 var respTabsId = $respTabs.attr('id');
                 $respTabs.find('ul.resp-tabs-list li').addClass('resp-tab-item');
                 $respTabs.css({
                     'display': 'block',
                     'width': jwidth
                 });

                 $respTabs.find('.resp-tabs-container > div').addClass('resp-tab-content');
                 jtab_options();
                 //Properties Function
                 function jtab_options() {
                     if (jtype == vtabs) {
                         $respTabs.addClass('resp-vtabs');
                     }
                     if (jfit == true) {
                         $respTabs.css({ width: '100%', margin: '0px' });
                     }
                     if (jtype == accord) {
                         $respTabs.addClass('resp-easy-accordion');
                         $respTabs.find('.resp-tabs-list').css('display', 'none');
                     }
                 }

                 //Assigning the h2 markup to accordion title
                 var $tabItemh2;
                 $respTabs.find('.resp-tab-content').before("<h2 class='resp-accordion' role='tab'><span class='resp-arrow'></span></h2>");

                 var itemCount = 0;
                 $respTabs.find('.resp-accordion').each(function () {
                     $tabItemh2 = $(this);
                     var $tabItem = $respTabs.find('.resp-tab-item:eq(' + itemCount + ')');
                     var $accItem = $respTabs.find('.resp-accordion:eq(' + itemCount + ')');
                     $accItem.append($tabItem.html());
                     $accItem.data($tabItem.data());
                     $tabItemh2.attr('aria-controls', 'tab_item-' + (itemCount));
                     itemCount++;
                 });

                 //Assigning the 'aria-controls' to Tab items
                 var count = 0,
                     $tabContent;
                 $respTabs.find('.resp-tab-item').each(function () {
                     $tabItem = $(this);
                     $tabItem.attr('aria-controls', 'tab_item-' + (count));
                     $tabItem.attr('role', 'tab');

                     //Assigning the 'aria-labelledby' attr to tab-content
                     var tabcount = 0;
                     $respTabs.find('.resp-tab-content').each(function () {
                         $tabContent = $(this);
                         $tabContent.attr('aria-labelledby', 'tab_item-' + (tabcount));
                         tabcount++;
                     });
                     count++;
                 });

                 // Show correct content area
                 var tabNum = 0;
                 if (hash != '') {
                     var matches = hash.match(new RegExp(respTabsId + "([0-9]+)"));
                     if (matches !== null && matches.length === 2) {
                         tabNum = parseInt(matches[1], 10) - 1;
                         if (tabNum > count) {
                             tabNum = 0;
                         }
                     }
                 }

                 //Active correct tab
                 $($respTabs.find('.resp-tab-item')[tabNum]).addClass('resp-tab-active');

                 //keep closed if option = 'closed' or option is 'accordion' and the element is in accordion mode
                 if (options.closed !== true && !(options.closed === 'accordion' && !$respTabsList.is(':visible')) && !(options.closed === 'tabs' && $respTabsList.is(':visible'))) {
                     $($respTabs.find('.resp-accordion')[tabNum]).addClass('resp-tab-active');
                     $($respTabs.find('.resp-tab-content')[tabNum]).addClass('resp-tab-content-active').attr('style', 'display:block');
                 }
                     //assign proper classes for when tabs mode is activated before making a selection in accordion mode
                 else {
                     $($respTabs.find('.resp-tab-content')[tabNum]).addClass('resp-tab-content-active resp-accordion-closed')
                 }

                 //Tab Click action function
                 $respTabs.find("[role=tab]").each(function () {

                     var $currentTab = $(this);
                     $currentTab.click(function () {

                         var $currentTab = $(this);
                         var $tabAria = $currentTab.attr('aria-controls');

                         if ($currentTab.hasClass('resp-accordion') && $currentTab.hasClass('resp-tab-active')) {
                             $respTabs.find('.resp-tab-content-active').slideUp('', function () { $(this).addClass('resp-accordion-closed'); });
                             $currentTab.removeClass('resp-tab-active');
                             return false;
                         }
                         if (!$currentTab.hasClass('resp-tab-active') && $currentTab.hasClass('resp-accordion')) {
                             $respTabs.find('.resp-tab-active').removeClass('resp-tab-active');
                             $respTabs.find('.resp-tab-content-active').slideUp().removeClass('resp-tab-content-active resp-accordion-closed');
                             $respTabs.find("[aria-controls=" + $tabAria + "]").addClass('resp-tab-active');

                             $respTabs.find('.resp-tab-content[aria-labelledby = ' + $tabAria + ']').slideDown().addClass('resp-tab-content-active');
                         } else {
                             $respTabs.find('.resp-tab-active').removeClass('resp-tab-active');
                             $respTabs.find('.resp-tab-content-active').removeAttr('style').removeClass('resp-tab-content-active').removeClass('resp-accordion-closed');
                             $respTabs.find("[aria-controls=" + $tabAria + "]").addClass('resp-tab-active');
                             $respTabs.find('.resp-tab-content[aria-labelledby = ' + $tabAria + ']').addClass('resp-tab-content-active').attr('style', 'display:block');
                         }
                         //Trigger tab activation event
                         $currentTab.trigger('tabactivate', $currentTab);

                         //Update Browser History
                         if (historyApi) {
                             var currentHash = window.location.hash;
                             var newHash = respTabsId + (parseInt($tabAria.substring(9), 10) + 1).toString();
                             if (currentHash != "") {
                                 var re = new RegExp(respTabsId + "[0-9]+");
                                 if (currentHash.match(re) != null) {
                                     newHash = currentHash.replace(re, newHash);
                                 }
                                 else {
                                     newHash = currentHash + "|" + newHash;
                                 }
                             }
                             else {
                                 newHash = '#' + newHash;
                             }

                             history.replaceState(null, null, newHash);
                         }
                     });

                 });

                 //Window resize function                   
                 $(window).resize(function () {
                     $respTabs.find('.resp-accordion-closed').removeAttr('style');
                 });
             });
         }
     });
 })(jQuery);


/* fancybox lightbox */
/*jquery.fancybox.js*/
 /*!
  * fancyBox - jQuery Plugin
  * version: 2.1.5 (Fri, 14 Jun 2013)
  * @requires jQuery v1.6 or later
  *
  * Examples at http://fancyapps.com/fancybox/
  * License: www.fancyapps.com/fancybox/#license
  *
  * Copyright 2012 Janis Skarnelis - janis@fancyapps.com
  *
  */

 (function (window, document, $, undefined) {
     "use strict";

     var H = $("html"),
         W = $(window),
         D = $(document),
         F = $.fancybox = function () {
             F.open.apply(this, arguments);
         },
         IE = navigator.userAgent.match(/msie/i),
         didUpdate = null,
         isTouch = document.createTouch !== undefined,

         isQuery = function (obj) {
             return obj && obj.hasOwnProperty && obj instanceof $;
         },
         isString = function (str) {
             return str && $.type(str) === "string";
         },
         isPercentage = function (str) {
             return isString(str) && str.indexOf('%') > 0;
         },
         isScrollable = function (el) {
             return (el && !(el.style.overflow && el.style.overflow === 'hidden') && ((el.clientWidth && el.scrollWidth > el.clientWidth) || (el.clientHeight && el.scrollHeight > el.clientHeight)));
         },
         getScalar = function (orig, dim) {
             var value = parseInt(orig, 10) || 0;

             if (dim && isPercentage(orig)) {
                 value = F.getViewport()[dim] / 100 * value;
             }

             return Math.ceil(value);
         },
         getValue = function (value, dim) {
             return getScalar(value, dim) + 'px';
         };

     $.extend(F, {
         // The current version of fancyBox
         version: '2.1.5',

         defaults: {
             padding: 15,
             margin: 20,

             width: 800,
             height: 600,
             minWidth: 100,
             minHeight: 100,
             maxWidth: 9999,
             maxHeight: 9999,
             pixelRatio: 1, // Set to 2 for retina display support

             autoSize: true,
             autoHeight: false,
             autoWidth: false,

             autoResize: true,
             autoCenter: !isTouch,
             fitToView: true,
             aspectRatio: false,
             topRatio: 0.5,
             leftRatio: 0.5,

             scrolling: 'auto', // 'auto', 'yes' or 'no'
             wrapCSS: '',

             arrows: true,
             closeBtn: true,
             closeClick: false,
             nextClick: false,
             mouseWheel: true,
             autoPlay: false,
             playSpeed: 3000,
             preload: 3,
             modal: false,
             loop: true,

             ajax: {
                 dataType: 'html',
                 headers: { 'X-fancyBox': true }
             },
             iframe: {
                 scrolling: 'auto',
                 preload: true
             },
             swf: {
                 wmode: 'transparent',
                 allowfullscreen: 'true',
                 allowscriptaccess: 'always'
             },

             keys: {
                 next: {
                     13: 'left', // enter
                     34: 'up',   // page down
                     39: 'left', // right arrow
                     40: 'up'    // down arrow
                 },
                 prev: {
                     8: 'right',  // backspace
                     33: 'down',   // page up
                     37: 'right',  // left arrow
                     38: 'down'    // up arrow
                 },
                 close: [27], // escape key
                 play: [32], // space - start/stop slideshow
                 toggle: [70]  // letter "f" - toggle fullscreen
             },

             direction: {
                 next: 'left',
                 prev: 'right'
             },

             scrollOutside: true,

             // Override some properties
             index: 0,
             type: null,
             href: null,
             content: null,
             title: null,

             // HTML templates
             tpl: {
                 wrap: '<div class="fancybox-wrap" tabIndex="-1"><div class="fancybox-skin"><div class="fancybox-outer"><div class="fancybox-inner"></div></div></div></div>',
                 image: '<img class="fancybox-image" src="{href}" alt="" />',
                 iframe: '<iframe id="fancybox-frame{rnd}" name="fancybox-frame{rnd}" class="fancybox-iframe" frameborder="0" vspace="0" hspace="0" webkitAllowFullScreen mozallowfullscreen allowFullScreen' + (IE ? ' allowtransparency="true"' : '') + '></iframe>',
                 error: '<p class="fancybox-error">The requested content cannot be loaded.<br/>Please try again later.</p>',
                 closeBtn: '<a title="Close" class="fancybox-item fancybox-close" href="javascript:;"></a>',
                 next: '<a title="Next" class="fancybox-nav fancybox-next" href="javascript:;"><span></span></a>',
                 prev: '<a title="Previous" class="fancybox-nav fancybox-prev" href="javascript:;"><span></span></a>'
             },

             // Properties for each animation type
             // Opening fancyBox
             openEffect: 'fade', // 'elastic', 'fade' or 'none'
             openSpeed: 250,
             openEasing: 'swing',
             openOpacity: true,
             openMethod: 'zoomIn',

             // Closing fancyBox
             closeEffect: 'fade', // 'elastic', 'fade' or 'none'
             closeSpeed: 250,
             closeEasing: 'swing',
             closeOpacity: true,
             closeMethod: 'zoomOut',

             // Changing next gallery item
             nextEffect: 'elastic', // 'elastic', 'fade' or 'none'
             nextSpeed: 250,
             nextEasing: 'swing',
             nextMethod: 'changeIn',

             // Changing previous gallery item
             prevEffect: 'elastic', // 'elastic', 'fade' or 'none'
             prevSpeed: 250,
             prevEasing: 'swing',
             prevMethod: 'changeOut',

             // Enable default helpers
             helpers: {
                 overlay: true,
                 title: true
             },

             // Callbacks
             onCancel: $.noop, // If canceling
             beforeLoad: $.noop, // Before loading
             afterLoad: $.noop, // After loading
             beforeShow: $.noop, // Before changing in current item
             afterShow: $.noop, // After opening
             beforeChange: $.noop, // Before changing gallery item
             beforeClose: $.noop, // Before closing
             afterClose: $.noop  // After closing
         },

         //Current state
         group: {}, // Selected group
         opts: {}, // Group options
         previous: null,  // Previous element
         coming: null,  // Element being loaded
         current: null,  // Currently loaded element
         isActive: false, // Is activated
         isOpen: false, // Is currently open
         isOpened: false, // Have been fully opened at least once

         wrap: null,
         skin: null,
         outer: null,
         inner: null,

         player: {
             timer: null,
             isActive: false
         },

         // Loaders
         ajaxLoad: null,
         imgPreload: null,

         // Some collections
         transitions: {},
         helpers: {},

         /*
          *	Static methods
          */

         open: function (group, opts) {
             if (!group) {
                 return;
             }

             if (!$.isPlainObject(opts)) {
                 opts = {};
             }

             // Close if already active
             if (false === F.close(true)) {
                 return;
             }

             // Normalize group
             if (!$.isArray(group)) {
                 group = isQuery(group) ? $(group).get() : [group];
             }

             // Recheck if the type of each element is `object` and set content type (image, ajax, etc)
             $.each(group, function (i, element) {
                 var obj = {},
                     href,
                     title,
                     content,
                     type,
                     rez,
                     hrefParts,
                     selector;

                 if ($.type(element) === "object") {
                     // Check if is DOM element
                     if (element.nodeType) {
                         element = $(element);
                     }

                     if (isQuery(element)) {
                         obj = {
                             href: element.data('fancybox-href') || element.attr('href'),
                             title: element.data('fancybox-title') || element.attr('title'),
                             isDom: true,
                             element: element
                         };

                         if ($.metadata) {
                             $.extend(true, obj, element.metadata());
                         }

                     } else {
                         obj = element;
                     }
                 }

                 href = opts.href || obj.href || (isString(element) ? element : null);
                 title = opts.title !== undefined ? opts.title : obj.title || '';

                 content = opts.content || obj.content;
                 type = content ? 'html' : (opts.type || obj.type);

                 if (!type && obj.isDom) {
                     type = element.data('fancybox-type');

                     if (!type) {
                         rez = element.prop('class').match(/fancybox\.(\w+)/);
                         type = rez ? rez[1] : null;
                     }
                 }

                 if (isString(href)) {
                     // Try to guess the content type
                     if (!type) {
                         if (F.isImage(href)) {
                             type = 'image';

                         } else if (F.isSWF(href)) {
                             type = 'swf';

                         } else if (href.charAt(0) === '#') {
                             type = 'inline';

                         } else if (isString(element)) {
                             type = 'html';
                             content = element;
                         }
                     }

                     // Split url into two pieces with source url and content selector, e.g,
                     // "/mypage.html #my_id" will load "/mypage.html" and display element having id "my_id"
                     if (type === 'ajax') {
                         hrefParts = href.split(/\s+/, 2);
                         href = hrefParts.shift();
                         selector = hrefParts.shift();
                     }
                 }

                 if (!content) {
                     if (type === 'inline') {
                         if (href) {
                             content = $(isString(href) ? href.replace(/.*(?=#[^\s]+$)/, '') : href); //strip for ie7

                         } else if (obj.isDom) {
                             content = element;
                         }

                     } else if (type === 'html') {
                         content = href;

                     } else if (!type && !href && obj.isDom) {
                         type = 'inline';
                         content = element;
                     }
                 }

                 $.extend(obj, {
                     href: href,
                     type: type,
                     content: content,
                     title: title,
                     selector: selector
                 });

                 group[i] = obj;
             });

             // Extend the defaults
             F.opts = $.extend(true, {}, F.defaults, opts);

             // All options are merged recursive except keys
             if (opts.keys !== undefined) {
                 F.opts.keys = opts.keys ? $.extend({}, F.defaults.keys, opts.keys) : false;
             }

             F.group = group;

             return F._start(F.opts.index);
         },

         // Cancel image loading or abort ajax request
         cancel: function () {
             var coming = F.coming;

             if (!coming || false === F.trigger('onCancel')) {
                 return;
             }

             F.hideLoading();

             if (F.ajaxLoad) {
                 F.ajaxLoad.abort();
             }

             F.ajaxLoad = null;

             if (F.imgPreload) {
                 F.imgPreload.onload = F.imgPreload.onerror = null;
             }

             if (coming.wrap) {
                 coming.wrap.stop(true, true).trigger('onReset').remove();
             }

             F.coming = null;

             // If the first item has been canceled, then clear everything
             if (!F.current) {
                 F._afterZoomOut(coming);
             }
         },

         // Start closing animation if is open; remove immediately if opening/closing
         close: function (event) {
             F.cancel();

             if (false === F.trigger('beforeClose')) {
                 return;
             }

             F.unbindEvents();

             if (!F.isActive) {
                 return;
             }

             if (!F.isOpen || event === true) {
                 $('.fancybox-wrap').stop(true).trigger('onReset').remove();

                 F._afterZoomOut();

             } else {
                 F.isOpen = F.isOpened = false;
                 F.isClosing = true;

                 $('.fancybox-item, .fancybox-nav').remove();

                 F.wrap.stop(true, true).removeClass('fancybox-opened');

                 F.transitions[F.current.closeMethod]();
             }
         },

         // Manage slideshow:
         //   $.fancybox.play(); - toggle slideshow
         //   $.fancybox.play( true ); - start
         //   $.fancybox.play( false ); - stop
         play: function (action) {
             var clear = function () {
                 clearTimeout(F.player.timer);
             },
                 set = function () {
                     clear();

                     if (F.current && F.player.isActive) {
                         F.player.timer = setTimeout(F.next, F.current.playSpeed);
                     }
                 },
                 stop = function () {
                     clear();

                     D.unbind('.player');

                     F.player.isActive = false;

                     F.trigger('onPlayEnd');
                 },
                 start = function () {
                     if (F.current && (F.current.loop || F.current.index < F.group.length - 1)) {
                         F.player.isActive = true;

                         D.bind({
                             'onCancel.player beforeClose.player': stop,
                             'onUpdate.player': set,
                             'beforeLoad.player': clear
                         });

                         set();

                         F.trigger('onPlayStart');
                     }
                 };

             if (action === true || (!F.player.isActive && action !== false)) {
                 start();
             } else {
                 stop();
             }
         },

         // Navigate to next gallery item
         next: function (direction) {
             var current = F.current;

             if (current) {
                 if (!isString(direction)) {
                     direction = current.direction.next;
                 }

                 F.jumpto(current.index + 1, direction, 'next');
             }
         },

         // Navigate to previous gallery item
         prev: function (direction) {
             var current = F.current;

             if (current) {
                 if (!isString(direction)) {
                     direction = current.direction.prev;
                 }

                 F.jumpto(current.index - 1, direction, 'prev');
             }
         },

         // Navigate to gallery item by index
         jumpto: function (index, direction, router) {
             var current = F.current;

             if (!current) {
                 return;
             }

             index = getScalar(index);

             F.direction = direction || current.direction[(index >= current.index ? 'next' : 'prev')];
             F.router = router || 'jumpto';

             if (current.loop) {
                 if (index < 0) {
                     index = current.group.length + (index % current.group.length);
                 }

                 index = index % current.group.length;
             }

             if (current.group[index] !== undefined) {
                 F.cancel();

                 F._start(index);
             }
         },

         // Center inside viewport and toggle position type to fixed or absolute if needed
         reposition: function (e, onlyAbsolute) {
             var current = F.current,
                 wrap = current ? current.wrap : null,
                 pos;

             if (wrap) {
                 pos = F._getPosition(onlyAbsolute);

                 if (e && e.type === 'scroll') {
                     delete pos.position;

                     wrap.stop(true, true).animate(pos, 200);

                 } else {
                     wrap.css(pos);

                     current.pos = $.extend({}, current.dim, pos);
                 }
             }
         },

         update: function (e) {
             var type = (e && e.type),
                 anyway = !type || type === 'orientationchange';

             if (anyway) {
                 clearTimeout(didUpdate);

                 didUpdate = null;
             }

             if (!F.isOpen || didUpdate) {
                 return;
             }

             didUpdate = setTimeout(function () {
                 var current = F.current;

                 if (!current || F.isClosing) {
                     return;
                 }

                 F.wrap.removeClass('fancybox-tmp');

                 if (anyway || type === 'load' || (type === 'resize' && current.autoResize)) {
                     F._setDimension();
                 }

                 if (!(type === 'scroll' && current.canShrink)) {
                     F.reposition(e);
                 }

                 F.trigger('onUpdate');

                 didUpdate = null;

             }, (anyway && !isTouch ? 0 : 300));
         },

         // Shrink content to fit inside viewport or restore if resized
         toggle: function (action) {
             if (F.isOpen) {
                 F.current.fitToView = $.type(action) === "boolean" ? action : !F.current.fitToView;

                 // Help browser to restore document dimensions
                 if (isTouch) {
                     F.wrap.removeAttr('style').addClass('fancybox-tmp');

                     F.trigger('onUpdate');
                 }

                 F.update();
             }
         },

         hideLoading: function () {
             D.unbind('.loading');

             $('#fancybox-loading').remove();
         },

         showLoading: function () {
             var el, viewport;

             F.hideLoading();

             el = $('<div id="fancybox-loading"><div></div></div>').click(F.cancel).appendTo('body');

             // If user will press the escape-button, the request will be canceled
             D.bind('keydown.loading', function (e) {
                 if ((e.which || e.keyCode) === 27) {
                     e.preventDefault();

                     F.cancel();
                 }
             });

             if (!F.defaults.fixed) {
                 viewport = F.getViewport();

                 el.css({
                     position: 'absolute',
                     top: (viewport.h * 0.5) + viewport.y,
                     left: (viewport.w * 0.5) + viewport.x
                 });
             }
         },

         getViewport: function () {
             var locked = (F.current && F.current.locked) || false,
                 rez = {
                     x: W.scrollLeft(),
                     y: W.scrollTop()
                 };

             if (locked) {
                 rez.w = locked[0].clientWidth;
                 rez.h = locked[0].clientHeight;

             } else {
                 // See http://bugs.jquery.com/ticket/6724
                 rez.w = isTouch && window.innerWidth ? window.innerWidth : W.width();
                 rez.h = isTouch && window.innerHeight ? window.innerHeight : W.height();
             }

             return rez;
         },

         // Unbind the keyboard / clicking actions
         unbindEvents: function () {
             if (F.wrap && isQuery(F.wrap)) {
                 F.wrap.unbind('.fb');
             }

             D.unbind('.fb');
             W.unbind('.fb');
         },

         bindEvents: function () {
             var current = F.current,
                 keys;

             if (!current) {
                 return;
             }

             // Changing document height on iOS devices triggers a 'resize' event,
             // that can change document height... repeating infinitely
             W.bind('orientationchange.fb' + (isTouch ? '' : ' resize.fb') + (current.autoCenter && !current.locked ? ' scroll.fb' : ''), F.update);

             keys = current.keys;

             if (keys) {
                 D.bind('keydown.fb', function (e) {
                     var code = e.which || e.keyCode,
                         target = e.target || e.srcElement;

                     // Skip esc key if loading, because showLoading will cancel preloading
                     if (code === 27 && F.coming) {
                         return false;
                     }

                     // Ignore key combinations and key events within form elements
                     if (!e.ctrlKey && !e.altKey && !e.shiftKey && !e.metaKey && !(target && (target.type || $(target).is('[contenteditable]')))) {
                         $.each(keys, function (i, val) {
                             if (current.group.length > 1 && val[code] !== undefined) {
                                 F[i](val[code]);

                                 e.preventDefault();
                                 return false;
                             }

                             if ($.inArray(code, val) > -1) {
                                 F[i]();

                                 e.preventDefault();
                                 return false;
                             }
                         });
                     }
                 });
             }

             if ($.fn.mousewheel && current.mouseWheel) {
                 F.wrap.bind('mousewheel.fb', function (e, delta, deltaX, deltaY) {
                     var target = e.target || null,
                         parent = $(target),
                         canScroll = false;

                     while (parent.length) {
                         if (canScroll || parent.is('.fancybox-skin') || parent.is('.fancybox-wrap')) {
                             break;
                         }

                         canScroll = isScrollable(parent[0]);
                         parent = $(parent).parent();
                     }

                     if (delta !== 0 && !canScroll) {
                         if (F.group.length > 1 && !current.canShrink) {
                             if (deltaY > 0 || deltaX > 0) {
                                 F.prev(deltaY > 0 ? 'down' : 'left');

                             } else if (deltaY < 0 || deltaX < 0) {
                                 F.next(deltaY < 0 ? 'up' : 'right');
                             }

                             e.preventDefault();
                         }
                     }
                 });
             }
         },

         trigger: function (event, o) {
             var ret, obj = o || F.coming || F.current;

             if (!obj) {
                 return;
             }

             if ($.isFunction(obj[event])) {
                 ret = obj[event].apply(obj, Array.prototype.slice.call(arguments, 1));
             }

             if (ret === false) {
                 return false;
             }

             if (obj.helpers) {
                 $.each(obj.helpers, function (helper, opts) {
                     if (opts && F.helpers[helper] && $.isFunction(F.helpers[helper][event])) {
                         F.helpers[helper][event]($.extend(true, {}, F.helpers[helper].defaults, opts), obj);
                     }
                 });
             }

             D.trigger(event);
         },

         isImage: function (str) {
             return isString(str) && str.match(/(^data:image\/.*,)|(\.(jp(e|g|eg)|gif|png|bmp|webp|svg)((\?|#).*)?$)/i);
         },

         isSWF: function (str) {
             return isString(str) && str.match(/\.(swf)((\?|#).*)?$/i);
         },

         _start: function (index) {
             var coming = {},
                 obj,
                 href,
                 type,
                 margin,
                 padding;

             index = getScalar(index);
             obj = F.group[index] || null;

             if (!obj) {
                 return false;
             }

             coming = $.extend(true, {}, F.opts, obj);

             // Convert margin and padding properties to array - top, right, bottom, left
             margin = coming.margin;
             padding = coming.padding;

             if ($.type(margin) === 'number') {
                 coming.margin = [margin, margin, margin, margin];
             }

             if ($.type(padding) === 'number') {
                 coming.padding = [padding, padding, padding, padding];
             }

             // 'modal' propery is just a shortcut
             if (coming.modal) {
                 $.extend(true, coming, {
                     closeBtn: false,
                     closeClick: false,
                     nextClick: false,
                     arrows: false,
                     mouseWheel: false,
                     keys: null,
                     helpers: {
                         overlay: {
                             closeClick: false
                         }
                     }
                 });
             }

             // 'autoSize' property is a shortcut, too
             if (coming.autoSize) {
                 coming.autoWidth = coming.autoHeight = true;
             }

             if (coming.width === 'auto') {
                 coming.autoWidth = true;
             }

             if (coming.height === 'auto') {
                 coming.autoHeight = true;
             }

             /*
              * Add reference to the group, so it`s possible to access from callbacks, example:
              * afterLoad : function() {
              *     this.title = 'Image ' + (this.index + 1) + ' of ' + this.group.length + (this.title ? ' - ' + this.title : '');
              * }
              */

             coming.group = F.group;
             coming.index = index;

             // Give a chance for callback or helpers to update coming item (type, title, etc)
             F.coming = coming;

             if (false === F.trigger('beforeLoad')) {
                 F.coming = null;

                 return;
             }

             type = coming.type;
             href = coming.href;

             if (!type) {
                 F.coming = null;

                 //If we can not determine content type then drop silently or display next/prev item if looping through gallery
                 if (F.current && F.router && F.router !== 'jumpto') {
                     F.current.index = index;

                     return F[F.router](F.direction);
                 }

                 return false;
             }

             F.isActive = true;

             if (type === 'image' || type === 'swf') {
                 coming.autoHeight = coming.autoWidth = false;
                 coming.scrolling = 'visible';
             }

             if (type === 'image') {
                 coming.aspectRatio = true;
             }

             if (type === 'iframe' && isTouch) {
                 coming.scrolling = 'scroll';
             }

             // Build the neccessary markup
             coming.wrap = $(coming.tpl.wrap).addClass('fancybox-' + (isTouch ? 'mobile' : 'desktop') + ' fancybox-type-' + type + ' fancybox-tmp ' + coming.wrapCSS).appendTo(coming.parent || 'body');

             $.extend(coming, {
                 skin: $('.fancybox-skin', coming.wrap),
                 outer: $('.fancybox-outer', coming.wrap),
                 inner: $('.fancybox-inner', coming.wrap)
             });

             $.each(["Top", "Right", "Bottom", "Left"], function (i, v) {
                 coming.skin.css('padding' + v, getValue(coming.padding[i]));
             });

             F.trigger('onReady');

             // Check before try to load; 'inline' and 'html' types need content, others - href
             if (type === 'inline' || type === 'html') {
                 if (!coming.content || !coming.content.length) {
                     return F._error('content');
                 }

             } else if (!href) {
                 return F._error('href');
             }

             if (type === 'image') {
                 F._loadImage();

             } else if (type === 'ajax') {
                 F._loadAjax();

             } else if (type === 'iframe') {
                 F._loadIframe();

             } else {
                 F._afterLoad();
             }
         },

         _error: function (type) {
             $.extend(F.coming, {
                 type: 'html',
                 autoWidth: true,
                 autoHeight: true,
                 minWidth: 0,
                 minHeight: 0,
                 scrolling: 'no',
                 hasError: type,
                 content: F.coming.tpl.error
             });

             F._afterLoad();
         },

         _loadImage: function () {
             // Reset preload image so it is later possible to check "complete" property
             var img = F.imgPreload = new Image();

             img.onload = function () {
                 this.onload = this.onerror = null;

                 F.coming.width = this.width / F.opts.pixelRatio;
                 F.coming.height = this.height / F.opts.pixelRatio;

                 F._afterLoad();
             };

             img.onerror = function () {
                 this.onload = this.onerror = null;

                 F._error('image');
             };

             img.src = F.coming.href;

             if (img.complete !== true) {
                 F.showLoading();
             }
         },

         _loadAjax: function () {
             var coming = F.coming;

             F.showLoading();

             F.ajaxLoad = $.ajax($.extend({}, coming.ajax, {
                 url: coming.href,
                 error: function (jqXHR, textStatus) {
                     if (F.coming && textStatus !== 'abort') {
                         F._error('ajax', jqXHR);

                     } else {
                         F.hideLoading();
                     }
                 },
                 success: function (data, textStatus) {
                     if (textStatus === 'success') {
                         coming.content = data;

                         F._afterLoad();
                     }
                 }
             }));
         },

         _loadIframe: function () {
             var coming = F.coming,
                 iframe = $(coming.tpl.iframe.replace(/\{rnd\}/g, new Date().getTime()))
                     .attr('scrolling', isTouch ? 'auto' : coming.iframe.scrolling)
                     .attr('src', coming.href);

             // This helps IE
             $(coming.wrap).bind('onReset', function () {
                 try {
                     $(this).find('iframe').hide().attr('src', '//about:blank').end().empty();
                 } catch (e) { }
             });

             if (coming.iframe.preload) {
                 F.showLoading();

                 iframe.one('load', function () {
                     $(this).data('ready', 1);

                     // iOS will lose scrolling if we resize
                     if (!isTouch) {
                         $(this).bind('load.fb', F.update);
                     }

                     // Without this trick:
                     //   - iframe won't scroll on iOS devices
                     //   - IE7 sometimes displays empty iframe
                     $(this).parents('.fancybox-wrap').width('100%').removeClass('fancybox-tmp').show();

                     F._afterLoad();
                 });
             }

             coming.content = iframe.appendTo(coming.inner);

             if (!coming.iframe.preload) {
                 F._afterLoad();
             }
         },

         _preloadImages: function () {
             var group = F.group,
                 current = F.current,
                 len = group.length,
                 cnt = current.preload ? Math.min(current.preload, len - 1) : 0,
                 item,
                 i;

             for (i = 1; i <= cnt; i += 1) {
                 item = group[(current.index + i) % len];

                 if (item.type === 'image' && item.href) {
                     new Image().src = item.href;
                 }
             }
         },

         _afterLoad: function () {
             var coming = F.coming,
                 previous = F.current,
                 placeholder = 'fancybox-placeholder',
                 current,
                 content,
                 type,
                 scrolling,
                 href,
                 embed;

             F.hideLoading();

             if (!coming || F.isActive === false) {
                 return;
             }

             if (false === F.trigger('afterLoad', coming, previous)) {
                 coming.wrap.stop(true).trigger('onReset').remove();

                 F.coming = null;

                 return;
             }

             if (previous) {
                 F.trigger('beforeChange', previous);

                 previous.wrap.stop(true).removeClass('fancybox-opened')
                     .find('.fancybox-item, .fancybox-nav')
                     .remove();
             }

             F.unbindEvents();

             current = coming;
             content = coming.content;
             type = coming.type;
             scrolling = coming.scrolling;

             $.extend(F, {
                 wrap: current.wrap,
                 skin: current.skin,
                 outer: current.outer,
                 inner: current.inner,
                 current: current,
                 previous: previous
             });

             href = current.href;

             switch (type) {
                 case 'inline':
                 case 'ajax':
                 case 'html':
                     if (current.selector) {
                         content = $('<div>').html(content).find(current.selector);

                     } else if (isQuery(content)) {
                         if (!content.data(placeholder)) {
                             content.data(placeholder, $('<div class="' + placeholder + '"></div>').insertAfter(content).hide());
                         }

                         content = content.show().detach();

                         current.wrap.bind('onReset', function () {
                             if ($(this).find(content).length) {
                                 content.hide().replaceAll(content.data(placeholder)).data(placeholder, false);
                             }
                         });
                     }
                     break;

                 case 'image':
                     content = current.tpl.image.replace('{href}', href);
                     break;

                 case 'swf':
                     content = '<object id="fancybox-swf" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="100%" height="100%"><param name="movie" value="' + href + '"></param>';
                     embed = '';

                     $.each(current.swf, function (name, val) {
                         content += '<param name="' + name + '" value="' + val + '"></param>';
                         embed += ' ' + name + '="' + val + '"';
                     });

                     content += '<embed src="' + href + '" type="application/x-shockwave-flash" width="100%" height="100%"' + embed + '></embed></object>';
                     break;
             }

             if (!(isQuery(content) && content.parent().is(current.inner))) {
                 current.inner.append(content);
             }

             // Give a chance for helpers or callbacks to update elements
             F.trigger('beforeShow');

             // Set scrolling before calculating dimensions
             current.inner.css('overflow', scrolling === 'yes' ? 'scroll' : (scrolling === 'no' ? 'hidden' : scrolling));

             // Set initial dimensions and start position
             F._setDimension();

             F.reposition();

             F.isOpen = false;
             F.coming = null;

             F.bindEvents();

             if (!F.isOpened) {
                 $('.fancybox-wrap').not(current.wrap).stop(true).trigger('onReset').remove();

             } else if (previous.prevMethod) {
                 F.transitions[previous.prevMethod]();
             }

             F.transitions[F.isOpened ? current.nextMethod : current.openMethod]();

             F._preloadImages();
         },

         _setDimension: function () {
             var viewport = F.getViewport(),
                 steps = 0,
                 canShrink = false,
                 canExpand = false,
                 wrap = F.wrap,
                 skin = F.skin,
                 inner = F.inner,
                 current = F.current,
                 width = current.width,
                 height = current.height,
                 minWidth = current.minWidth,
                 minHeight = current.minHeight,
                 maxWidth = current.maxWidth,
                 maxHeight = current.maxHeight,
                 scrolling = current.scrolling,
                 scrollOut = current.scrollOutside ? current.scrollbarWidth : 0,
                 margin = current.margin,
                 wMargin = getScalar(margin[1] + margin[3]),
                 hMargin = getScalar(margin[0] + margin[2]),
                 wPadding,
                 hPadding,
                 wSpace,
                 hSpace,
                 origWidth,
                 origHeight,
                 origMaxWidth,
                 origMaxHeight,
                 ratio,
                 width_,
                 height_,
                 maxWidth_,
                 maxHeight_,
                 iframe,
                 body;

             // Reset dimensions so we could re-check actual size
             wrap.add(skin).add(inner).width('auto').height('auto').removeClass('fancybox-tmp');

             wPadding = getScalar(skin.outerWidth(true) - skin.width());
             hPadding = getScalar(skin.outerHeight(true) - skin.height());

             // Any space between content and viewport (margin, padding, border, title)
             wSpace = wMargin + wPadding;
             hSpace = hMargin + hPadding;

             origWidth = isPercentage(width) ? (viewport.w - wSpace) * getScalar(width) / 100 : width;
             origHeight = isPercentage(height) ? (viewport.h - hSpace) * getScalar(height) / 100 : height;

             if (current.type === 'iframe') {
                 iframe = current.content;

                 if (current.autoHeight && iframe.data('ready') === 1) {
                     try {
                         if (iframe[0].contentWindow.document.location) {
                             inner.width(origWidth).height(9999);

                             body = iframe.contents().find('body');

                             if (scrollOut) {
                                 body.css('overflow-x', 'hidden');
                             }

                             origHeight = body.outerHeight(true);
                         }

                     } catch (e) { }
                 }

             } else if (current.autoWidth || current.autoHeight) {
                 inner.addClass('fancybox-tmp');

                 // Set width or height in case we need to calculate only one dimension
                 if (!current.autoWidth) {
                     inner.width(origWidth);
                 }

                 if (!current.autoHeight) {
                     inner.height(origHeight);
                 }

                 if (current.autoWidth) {
                     origWidth = inner.width();
                 }

                 if (current.autoHeight) {
                     origHeight = inner.height();
                 }

                 inner.removeClass('fancybox-tmp');
             }

             width = getScalar(origWidth);
             height = getScalar(origHeight);

             ratio = origWidth / origHeight;

             // Calculations for the content
             minWidth = getScalar(isPercentage(minWidth) ? getScalar(minWidth, 'w') - wSpace : minWidth);
             maxWidth = getScalar(isPercentage(maxWidth) ? getScalar(maxWidth, 'w') - wSpace : maxWidth);

             minHeight = getScalar(isPercentage(minHeight) ? getScalar(minHeight, 'h') - hSpace : minHeight);
             maxHeight = getScalar(isPercentage(maxHeight) ? getScalar(maxHeight, 'h') - hSpace : maxHeight);

             // These will be used to determine if wrap can fit in the viewport
             origMaxWidth = maxWidth;
             origMaxHeight = maxHeight;

             if (current.fitToView) {
                 maxWidth = Math.min(viewport.w - wSpace, maxWidth);
                 maxHeight = Math.min(viewport.h - hSpace, maxHeight);
             }

             maxWidth_ = viewport.w - wMargin;
             maxHeight_ = viewport.h - hMargin;

             if (current.aspectRatio) {
                 if (width > maxWidth) {
                     width = maxWidth;
                     height = getScalar(width / ratio);
                 }

                 if (height > maxHeight) {
                     height = maxHeight;
                     width = getScalar(height * ratio);
                 }

                 if (width < minWidth) {
                     width = minWidth;
                     height = getScalar(width / ratio);
                 }

                 if (height < minHeight) {
                     height = minHeight;
                     width = getScalar(height * ratio);
                 }

             } else {
                 width = Math.max(minWidth, Math.min(width, maxWidth));

                 if (current.autoHeight && current.type !== 'iframe') {
                     inner.width(width);

                     height = inner.height();
                 }

                 height = Math.max(minHeight, Math.min(height, maxHeight));
             }

             // Try to fit inside viewport (including the title)
             if (current.fitToView) {
                 inner.width(width).height(height);

                 wrap.width(width + wPadding);

                 // Real wrap dimensions
                 width_ = wrap.width();
                 height_ = wrap.height();

                 if (current.aspectRatio) {
                     while ((width_ > maxWidth_ || height_ > maxHeight_) && width > minWidth && height > minHeight) {
                         if (steps++ > 19) {
                             break;
                         }

                         height = Math.max(minHeight, Math.min(maxHeight, height - 10));
                         width = getScalar(height * ratio);

                         if (width < minWidth) {
                             width = minWidth;
                             height = getScalar(width / ratio);
                         }

                         if (width > maxWidth) {
                             width = maxWidth;
                             height = getScalar(width / ratio);
                         }

                         inner.width(width).height(height);

                         wrap.width(width + wPadding);

                         width_ = wrap.width();
                         height_ = wrap.height();
                     }

                 } else {
                     width = Math.max(minWidth, Math.min(width, width - (width_ - maxWidth_)));
                     height = Math.max(minHeight, Math.min(height, height - (height_ - maxHeight_)));
                 }
             }

             if (scrollOut && scrolling === 'auto' && height < origHeight && (width + wPadding + scrollOut) < maxWidth_) {
                 width += scrollOut;
             }

             inner.width(width).height(height);

             wrap.width(width + wPadding);

             width_ = wrap.width();
             height_ = wrap.height();

             canShrink = (width_ > maxWidth_ || height_ > maxHeight_) && width > minWidth && height > minHeight;
             canExpand = current.aspectRatio ? (width < origMaxWidth && height < origMaxHeight && width < origWidth && height < origHeight) : ((width < origMaxWidth || height < origMaxHeight) && (width < origWidth || height < origHeight));

             $.extend(current, {
                 dim: {
                     width: getValue(width_),
                     height: getValue(height_)
                 },
                 origWidth: origWidth,
                 origHeight: origHeight,
                 canShrink: canShrink,
                 canExpand: canExpand,
                 wPadding: wPadding,
                 hPadding: hPadding,
                 wrapSpace: height_ - skin.outerHeight(true),
                 skinSpace: skin.height() - height
             });

             if (!iframe && current.autoHeight && height > minHeight && height < maxHeight && !canExpand) {
                 inner.height('auto');
             }
         },

         _getPosition: function (onlyAbsolute) {
             var current = F.current,
                 viewport = F.getViewport(),
                 margin = current.margin,
                 width = F.wrap.width() + margin[1] + margin[3],
                 height = F.wrap.height() + margin[0] + margin[2],
                 rez = {
                     position: 'absolute',
                     top: margin[0],
                     left: margin[3]
                 };

             if (current.autoCenter && current.fixed && !onlyAbsolute && height <= viewport.h && width <= viewport.w) {
                 rez.position = 'fixed';

             } else if (!current.locked) {
                 rez.top += viewport.y;
                 rez.left += viewport.x;
             }

             rez.top = getValue(Math.max(rez.top, rez.top + ((viewport.h - height) * current.topRatio)));
             rez.left = getValue(Math.max(rez.left, rez.left + ((viewport.w - width) * current.leftRatio)));

             return rez;
         },

         _afterZoomIn: function () {
             var current = F.current;

             if (!current) {
                 return;
             }

             F.isOpen = F.isOpened = true;

             F.wrap.css('overflow', 'visible').addClass('fancybox-opened');

             F.update();

             // Assign a click event
             if (current.closeClick || (current.nextClick && F.group.length > 1)) {
                 F.inner.css('cursor', 'pointer').bind('click.fb', function (e) {
                     if (!$(e.target).is('a') && !$(e.target).parent().is('a')) {
                         e.preventDefault();

                         F[current.closeClick ? 'close' : 'next']();
                     }
                 });
             }

             // Create a close button
             if (current.closeBtn) {
                 $(current.tpl.closeBtn).appendTo(F.skin).bind('click.fb', function (e) {
                     e.preventDefault();

                     F.close();
                 });
             }

             // Create navigation arrows
             if (current.arrows && F.group.length > 1) {
                 if (current.loop || current.index > 0) {
                     $(current.tpl.prev).appendTo(F.outer).bind('click.fb', F.prev);
                 }

                 if (current.loop || current.index < F.group.length - 1) {
                     $(current.tpl.next).appendTo(F.outer).bind('click.fb', F.next);
                 }
             }

             F.trigger('afterShow');

             // Stop the slideshow if this is the last item
             if (!current.loop && current.index === current.group.length - 1) {
                 F.play(false);

             } else if (F.opts.autoPlay && !F.player.isActive) {
                 F.opts.autoPlay = false;

                 F.play();
             }
         },

         _afterZoomOut: function (obj) {
             obj = obj || F.current;

             $('.fancybox-wrap').trigger('onReset').remove();

             $.extend(F, {
                 group: {},
                 opts: {},
                 router: false,
                 current: null,
                 isActive: false,
                 isOpened: false,
                 isOpen: false,
                 isClosing: false,
                 wrap: null,
                 skin: null,
                 outer: null,
                 inner: null
             });

             F.trigger('afterClose', obj);
         }
     });

     /*
      *	Default transitions
      */

     F.transitions = {
         getOrigPosition: function () {
             var current = F.current,
                 element = current.element,
                 orig = current.orig,
                 pos = {},
                 width = 50,
                 height = 50,
                 hPadding = current.hPadding,
                 wPadding = current.wPadding,
                 viewport = F.getViewport();

             if (!orig && current.isDom && element.is(':visible')) {
                 orig = element.find('img:first');

                 if (!orig.length) {
                     orig = element;
                 }
             }

             if (isQuery(orig)) {
                 pos = orig.offset();

                 if (orig.is('img')) {
                     width = orig.outerWidth();
                     height = orig.outerHeight();
                 }

             } else {
                 pos.top = viewport.y + (viewport.h - height) * current.topRatio;
                 pos.left = viewport.x + (viewport.w - width) * current.leftRatio;
             }

             if (F.wrap.css('position') === 'fixed' || current.locked) {
                 pos.top -= viewport.y;
                 pos.left -= viewport.x;
             }

             pos = {
                 top: getValue(pos.top - hPadding * current.topRatio),
                 left: getValue(pos.left - wPadding * current.leftRatio),
                 width: getValue(width + wPadding),
                 height: getValue(height + hPadding)
             };

             return pos;
         },

         step: function (now, fx) {
             var ratio,
                 padding,
                 value,
                 prop = fx.prop,
                 current = F.current,
                 wrapSpace = current.wrapSpace,
                 skinSpace = current.skinSpace;

             if (prop === 'width' || prop === 'height') {
                 ratio = fx.end === fx.start ? 1 : (now - fx.start) / (fx.end - fx.start);

                 if (F.isClosing) {
                     ratio = 1 - ratio;
                 }

                 padding = prop === 'width' ? current.wPadding : current.hPadding;
                 value = now - padding;

                 F.skin[prop](getScalar(prop === 'width' ? value : value - (wrapSpace * ratio)));
                 F.inner[prop](getScalar(prop === 'width' ? value : value - (wrapSpace * ratio) - (skinSpace * ratio)));
             }
         },

         zoomIn: function () {
             var current = F.current,
                 startPos = current.pos,
                 effect = current.openEffect,
                 elastic = effect === 'elastic',
                 endPos = $.extend({ opacity: 1 }, startPos);

             // Remove "position" property that breaks older IE
             delete endPos.position;

             if (elastic) {
                 startPos = this.getOrigPosition();

                 if (current.openOpacity) {
                     startPos.opacity = 0.1;
                 }

             } else if (effect === 'fade') {
                 startPos.opacity = 0.1;
             }

             F.wrap.css(startPos).animate(endPos, {
                 duration: effect === 'none' ? 0 : current.openSpeed,
                 easing: current.openEasing,
                 step: elastic ? this.step : null,
                 complete: F._afterZoomIn
             });
         },

         zoomOut: function () {
             var current = F.current,
                 effect = current.closeEffect,
                 elastic = effect === 'elastic',
                 endPos = { opacity: 0.1 };

             if (elastic) {
                 endPos = this.getOrigPosition();

                 if (current.closeOpacity) {
                     endPos.opacity = 0.1;
                 }
             }

             F.wrap.animate(endPos, {
                 duration: effect === 'none' ? 0 : current.closeSpeed,
                 easing: current.closeEasing,
                 step: elastic ? this.step : null,
                 complete: F._afterZoomOut
             });
         },

         changeIn: function () {
             var current = F.current,
                 effect = current.nextEffect,
                 startPos = current.pos,
                 endPos = { opacity: 1 },
                 direction = F.direction,
                 distance = 200,
                 field;

             startPos.opacity = 0.1;

             if (effect === 'elastic') {
                 field = direction === 'down' || direction === 'up' ? 'top' : 'left';

                 if (direction === 'down' || direction === 'right') {
                     startPos[field] = getValue(getScalar(startPos[field]) - distance);
                     endPos[field] = '+=' + distance + 'px';

                 } else {
                     startPos[field] = getValue(getScalar(startPos[field]) + distance);
                     endPos[field] = '-=' + distance + 'px';
                 }
             }

             // Workaround for http://bugs.jquery.com/ticket/12273
             if (effect === 'none') {
                 F._afterZoomIn();

             } else {
                 F.wrap.css(startPos).animate(endPos, {
                     duration: current.nextSpeed,
                     easing: current.nextEasing,
                     complete: F._afterZoomIn
                 });
             }
         },

         changeOut: function () {
             var previous = F.previous,
                 effect = previous.prevEffect,
                 endPos = { opacity: 0.1 },
                 direction = F.direction,
                 distance = 200;

             if (effect === 'elastic') {
                 endPos[direction === 'down' || direction === 'up' ? 'top' : 'left'] = (direction === 'up' || direction === 'left' ? '-' : '+') + '=' + distance + 'px';
             }

             previous.wrap.animate(endPos, {
                 duration: effect === 'none' ? 0 : previous.prevSpeed,
                 easing: previous.prevEasing,
                 complete: function () {
                     $(this).trigger('onReset').remove();
                 }
             });
         }
     };

     /*
      *	Overlay helper
      */

     F.helpers.overlay = {
         defaults: {
             closeClick: true,      // if true, fancyBox will be closed when user clicks on the overlay
             speedOut: 200,       // duration of fadeOut animation
             showEarly: true,      // indicates if should be opened immediately or wait until the content is ready
             css: {},        // custom CSS properties
             locked: !isTouch,  // if true, the content will be locked into overlay
             fixed: true       // if false, the overlay CSS position property will not be set to "fixed"
         },

         overlay: null,      // current handle
         fixed: false,     // indicates if the overlay has position "fixed"
         el: $('html'), // element that contains "the lock"

         // Public methods
         create: function (opts) {
             opts = $.extend({}, this.defaults, opts);

             if (this.overlay) {
                 this.close();
             }

             this.overlay = $('<div class="fancybox-overlay"></div>').appendTo(F.coming ? F.coming.parent : opts.parent);
             this.fixed = false;

             if (opts.fixed && F.defaults.fixed) {
                 this.overlay.addClass('fancybox-overlay-fixed');

                 this.fixed = true;
             }
         },

         open: function (opts) {
             var that = this;

             opts = $.extend({}, this.defaults, opts);

             if (this.overlay) {
                 this.overlay.unbind('.overlay').width('auto').height('auto');

             } else {
                 this.create(opts);
             }

             if (!this.fixed) {
                 W.bind('resize.overlay', $.proxy(this.update, this));

                 this.update();
             }

             if (opts.closeClick) {
                 this.overlay.bind('click.overlay', function (e) {
                     if ($(e.target).hasClass('fancybox-overlay')) {
                         if (F.isActive) {
                             F.close();
                         } else {
                             that.close();
                         }

                         return false;
                     }
                 });
             }

             this.overlay.css(opts.css).show();
         },

         close: function () {
             var scrollV, scrollH;

             W.unbind('resize.overlay');

             if (this.el.hasClass('fancybox-lock')) {
                 $('.fancybox-margin').removeClass('fancybox-margin');

                 scrollV = W.scrollTop();
                 scrollH = W.scrollLeft();

                 this.el.removeClass('fancybox-lock');

                 W.scrollTop(scrollV).scrollLeft(scrollH);
             }

             $('.fancybox-overlay').remove().hide();

             $.extend(this, {
                 overlay: null,
                 fixed: false
             });
         },

         // Private, callbacks

         update: function () {
             var width = '100%', offsetWidth;

             // Reset width/height so it will not mess
             this.overlay.width(width).height('100%');

             // jQuery does not return reliable result for IE
             if (IE) {
                 offsetWidth = Math.max(document.documentElement.offsetWidth, document.body.offsetWidth);

                 if (D.width() > offsetWidth) {
                     width = D.width();
                 }

             } else if (D.width() > W.width()) {
                 width = D.width();
             }

             this.overlay.width(width).height(D.height());
         },

         // This is where we can manipulate DOM, because later it would cause iframes to reload
         onReady: function (opts, obj) {
             var overlay = this.overlay;

             $('.fancybox-overlay').stop(true, true);

             if (!overlay) {
                 this.create(opts);
             }

             if (opts.locked && this.fixed && obj.fixed) {
                 if (!overlay) {
                     this.margin = D.height() > W.height() ? $('html').css('margin-right').replace("px", "") : false;
                 }

                 obj.locked = this.overlay.append(obj.wrap);
                 obj.fixed = false;
             }

             if (opts.showEarly === true) {
                 this.beforeShow.apply(this, arguments);
             }
         },

         beforeShow: function (opts, obj) {
             var scrollV, scrollH;

             if (obj.locked) {
                 if (this.margin !== false) {
                     $('*').filter(function () {
                         return ($(this).css('position') === 'fixed' && !$(this).hasClass("fancybox-overlay") && !$(this).hasClass("fancybox-wrap"));
                     }).addClass('fancybox-margin');

                     this.el.addClass('fancybox-margin');
                 }

                 scrollV = W.scrollTop();
                 scrollH = W.scrollLeft();

                 this.el.addClass('fancybox-lock');

                 W.scrollTop(scrollV).scrollLeft(scrollH);
             }

             this.open(opts);
         },

         onUpdate: function () {
             if (!this.fixed) {
                 this.update();
             }
         },

         afterClose: function (opts) {
             // Remove overlay if exists and fancyBox is not opening
             // (e.g., it is not being open using afterClose callback)
             //if (this.overlay && !F.isActive) {
             if (this.overlay && !F.coming) {
                 this.overlay.fadeOut(opts.speedOut, $.proxy(this.close, this));
             }
         }
     };

     /*
      *	Title helper
      */

     F.helpers.title = {
         defaults: {
             type: 'float', // 'float', 'inside', 'outside' or 'over',
             position: 'bottom' // 'top' or 'bottom'
         },

         beforeShow: function (opts) {
             var current = F.current,
                 text = current.title,
                 type = opts.type,
                 title,
                 target;

             if ($.isFunction(text)) {
                 text = text.call(current.element, current);
             }

             if (!isString(text) || $.trim(text) === '') {
                 return;
             }

             title = $('<div class="fancybox-title fancybox-title-' + type + '-wrap">' + text + '</div>');

             switch (type) {
                 case 'inside':
                     target = F.skin;
                     break;

                 case 'outside':
                     target = F.wrap;
                     break;

                 case 'over':
                     target = F.inner;
                     break;

                 default: // 'float'
                     target = F.skin;

                     title.appendTo('body');

                     if (IE) {
                         title.width(title.width());
                     }

                     title.wrapInner('<span class="child"></span>');

                     //Increase bottom margin so this title will also fit into viewport
                     F.current.margin[2] += Math.abs(getScalar(title.css('margin-bottom')));
                     break;
             }

             title[(opts.position === 'top' ? 'prependTo' : 'appendTo')](target);
         }
     };

     // jQuery plugin initialization
     $.fn.fancybox = function (options) {
         var index,
             that = $(this),
             selector = this.selector || '',
             run = function (e) {
                 var what = $(this).blur(), idx = index, relType, relVal;

                 if (!(e.ctrlKey || e.altKey || e.shiftKey || e.metaKey) && !what.is('.fancybox-wrap')) {
                     relType = options.groupAttr || 'data-fancybox-group';
                     relVal = what.attr(relType);

                     if (!relVal) {
                         relType = 'rel';
                         relVal = what.get(0)[relType];
                     }

                     if (relVal && relVal !== '' && relVal !== 'nofollow') {
                         what = selector.length ? $(selector) : that;
                         what = what.filter('[' + relType + '="' + relVal + '"]');
                         idx = what.index(this);
                     }

                     options.index = idx;

                     // Stop an event from bubbling if everything is fine
                     if (F.open(what, options) !== false) {
                         e.preventDefault();
                     }
                 }
             };

         options = options || {};
         index = options.index || 0;

         if (!selector || options.live === false) {
             that.unbind('click.fb-start').bind('click.fb-start', run);

         } else {
             D.undelegate(selector, 'click.fb-start').delegate(selector + ":not('.fancybox-item, .fancybox-nav')", 'click.fb-start', run);
         }

         this.filter('[data-fancybox-start=1]').trigger('click');

         return this;
     };

     // Tests that need a body at doc ready
     D.ready(function () {
         var w1, w2;

         if ($.scrollbarWidth === undefined) {
             // http://benalman.com/projects/jquery-misc-plugins/#scrollbarwidth
             $.scrollbarWidth = function () {
                 var parent = $('<div style="width:50px;height:50px;overflow:auto"><div/></div>').appendTo('body'),
                     child = parent.children(),
                     width = child.innerWidth() - child.height(99).innerWidth();

                 parent.remove();

                 return width;
             };
         }

         if ($.support.fixedPosition === undefined) {
             $.support.fixedPosition = (function () {
                 var elem = $('<div style="position:fixed;top:20px;"></div>').appendTo('body'),
                     fixed = (elem[0].offsetTop === 20 || elem[0].offsetTop === 15);

                 elem.remove();

                 return fixed;
             }());
         }

         $.extend(F.defaults, {
             scrollbarWidth: $.scrollbarWidth(),
             fixed: $.support.fixedPosition,
             parent: $('body')
         });

         //Get real width of page scroll-bar
         w1 = $(window).width();

         H.addClass('fancybox-lock-test');

         w2 = $(window).width();

         H.removeClass('fancybox-lock-test');

         $("<style type='text/css'>.fancybox-margin{margin-right:" + (w2 - w1) + "px;}</style>").appendTo("head");
     });

 }(window, document, jQuery));

/* fancyforms - customize form elements */
/*jquery.fancyform.js*/
 /*!
 * Fancyform - jQuery Plugin
 * Simple and fancy form styling alternative
 *
 * Examples and documentation at: https://github.com/Lutrasoft/Fancyform
 * 
 * Copyright (c) 2010-2013 - Lutrasoft
 * 
 * Version: 1.4.2
 * Requires: jQuery v1.6.1+ 
 *
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 */
 (function ($) {
     $.simpleEllipsis = function (t, c) {
         return t.length < c ? t : t.substring(0, c) + "...";
     }

     var _touch = !!('ontouchstart' in window),
         _removeClasses = function () {
             var _this = $(this),
                 options = _this.data("options") || _this.data("settings"),
                 k;

             for (k in options) {
                 _this.parent().removeClass(k);
             }
         };

     $.fn.extend({
         /*
         Get the caret on an textarea
         */
         caret: function (start, end) {
             var elem = this[0], val = this.val(), r, re, rc;

             if (elem) {
                 // get caret range
                 if (typeof start == "undefined") {
                     if (elem.selectionStart) {
                         start = elem.selectionStart;
                         end = elem.selectionEnd;
                     }
                         // <= IE 8
                     else if (document.selection) {
                         this.focus();

                         r = document.selection.createRange();
                         if (r == null) {
                             return { start: 0, end: e.value.length, length: 0 }
                         }

                         re = elem.createTextRange();
                         rc = re.duplicate();

                         re.moveToBookmark(r.getBookmark());
                         rc.setEndPoint('EndToStart', re);

                         // IE counts a line (not \n or \r) as 1 extra character
                         return { start: rc.text.length - (rc.text.split("\n").length + 1) + 2, end: rc.text.length + r.text.length - (rc.text.split("\n").length + 1) + 2, length: r.text.length, text: r.text };
                     }
                 }
                     // set caret range
                 else {
                     if (typeof end != "number") end = -1;
                     if (typeof start != "number" || start < 0) start = 0;
                     if (end > val.length) end = val.length;

                     end = Math.max(start, end);
                     start = Math.min(start, end);

                     elem.focus();

                     if (elem.selectionStart) {
                         elem.selectionStart = start;
                         elem.selectionEnd = end;
                     }
                     else if (document.selection) {
                         r = elem.createTextRange();
                         r.collapse(true);
                         r.moveStart("character", start);
                         r.moveEnd("character", end - start);
                         r.select();
                     }
                 }

                 return { start: start, end: end };
             }
         },
         /*
         Replace checkboxes with images
         */
         transformCheckbox: function (settings) {

             var defaults = {
                 base: "image", // Can be image/class
                 checked: "",
                 unchecked: "",
                 disabledChecked: "",
                 disabledUnchecked: "",
                 tristateHalfChecked: "",
                 changeHandler: function (is_checked) { },
                 trigger: "self", // Can be self, parent
                 tristate: 0
             },
             options = $.extend(defaults, settings),
             method = {
                 // Handling the image
                 setImage: function () {
                     var cb = $(this),
                         settings = cb.data('settings'),
                         src;

                     if (cb.is(":disabled")) {
                         src = cb.is(":checked") ? "disabledChecked" : "disabledUnchecked";
                     }
                     else if (cb.hasClass("half-checked")) // Tri-state image
                     {
                         src = "tristateHalfChecked";
                     }
                     else if (cb.is(":checked")) {
                         src = "checked";
                     }
                     else {
                         src = "unchecked";
                     }

                     if (settings.base == "image") {
                         cb.next().attr("src", settings[src]);
                     }
                     else {
                         _removeClasses.call(this);
                         cb.parent().addClass(src);
                     }
                 },
                 setProp: function (el, name, bool) {
                     $(el).prop(name, bool).change();
                     method.setImage.call(el);

                     // Checked and radio, change others
                     if (name == "checked" && !$(el).data('settings').type) {
                         $("[name='" + $(el).attr("name") + "']").each(function () {
                             method.setImage.call(this);
                         });
                     }
                 },
                 // Handling the check/uncheck/disable/enable functions
                 uncheck: function () {
                     method.setProp(this, "checked", 0);
                 },
                 check: function () {
                     method.setProp(this, "checked", 1);
                 },
                 disable: function () {
                     method.setProp(this, "disabled", 1);
                 },
                 enable: function () {
                     method.setProp(this, "disabled", 0);
                 },
                 // Clicking the image
                 imageClick: function () {
                     var _this = $(this),
                         settings = _this.data('settings');

                     if (!_this.is(":disabled")) {
                         if (_this.is(":checked") && settings.type) {
                             method.uncheck.call(_this);
                             options.changeHandler.call(_this, 1);
                         }
                         else {
                             method.check.call(_this);
                             options.changeHandler.call(_this, 0);
                         }
                         method.handleTriState.call(_this);
                     }
                 },
                 // Tristate
                 handleTriState: function () {
                     var cb = $(this),
                         settings = cb.data('settings'),
                         li = cb.parent(),
                         ul = li.find("ul"),
                         pli = li.closest("li");

                     if (settings.tristate) {
                         // Fix children
                         if (cb.hasClass("half-checked") || cb.is(":checked")) {
                             cb.removeClass("half-checked");
                             method.check.call(cb);
                             ul.find("input:checkbox").removeClass("half-checked").each(method.check);
                         }
                         else if (cb.not(":checked")) {
                             cb.removeClass("half-checked");
                             ul.find("input:checkbox").each(method.uncheck);
                         }
                         ul.find("input:checkbox").each(method.setImage);

                         // Fix parents
                         if (cb.parent().parent().parent().is("li")) {
                             method.handleTriStateLevel.call(cb.parent().parent().parent());
                         }

                         cb.trigger("transformCheckbox.tristate");
                     }
                 },
                 // Handle all including parent levels
                 handleTriStateLevel: function (upwards) {
                     var _this = $(this),
                         firstCheckbox = _this.find("input:checkbox").first(),
                         ul = _this.find("ul"),
                         inputs = ul.find("input:checkbox"),
                         checked = inputs.filter(":checked");

                     if (upwards !== false || inputs.length) {
                         firstCheckbox.removeClass("half-checked");

                         if (inputs.length == checked.length) {
                             method.check.call(firstCheckbox);
                         }
                         else if (checked.length) {
                             firstCheckbox.addClass("half-checked");
                         }
                         else {
                             method.uncheck.call(firstCheckbox);
                         }
                         method.setImage.call(firstCheckbox);

                         if (upwards !== false && _this.parent().parent().is("li")) {
                             method.handleTriStateLevel.call(_this.parent().parent());
                         }
                     }
                 }
             }

             return this.each(function () {
                 if (typeof settings == "string") {
                     method[settings].call(this);
                 }
                 else {
                     var _this = $(this);

                     // Is already initialized
                     if (!_this.data("tf.init")) {
                         // set initialized
                         _this.data("tf.init", 1)
                                .data("settings", options);

                         options.type = _this.is("[type=checkbox]");

                         // Radio hide
                         _this.hide();

                         // Afbeelding
                         if (options.base == "image") {
                             _this.after("<img />");
                         }
                         else {
                             _this.wrap("<span class='trans-element-" + (options.type ? "checkbox" : "radio") + "' />");
                         }
                         method.setImage.call(this);
                         if (settings.tristate) {
                             method.handleTriStateLevel.call(_this.parent(), false);
                         }

                         if (options.base == "image") {
                             switch (options.trigger) {
                                 case "parent":
                                     _this.parent().click($.proxy(method.imageClick, this));
                                     break;
                                 case "self":
                                     _this.next("img").click($.proxy(method.imageClick, this));
                                     break;
                             }
                         }
                         else {
                             switch (options.trigger) {
                                 case "parent":
                                     _this.parent().parent().click($.proxy(method.imageClick, this));
                                     break;
                                 case "self":
                                     _this.parent().click($.proxy(method.imageClick, this));
                                     break;
                             }
                         }
                     }
                 }
             });
         },
         /*
         Replace select with list
         =========================
         HTML will look like
         <ul>
         <li><span>Selected value</span>
         <ul>
         <li data-settings='{"alwaysvisible" : true}'><span>Option</span></li>
         <li><span>Option</span></li>
         </ul>
         </li>
         </ul>
         */
         transformSelect: function (opts) {
             var defaults = {
                 dropDownClass: "transformSelect",
                 showFirstItemInDrop: 1,

                 acceptManualInput: 0,
                 useManualInputAsFilter: 0,

                 subTemplate: function (option) {
                     if ($(this)[0].type == "select-multiple") {

                         return "<span><input type='checkbox' value='" + $(option).val() + "' " + ($(option).is(":selected") ? "checked='checked'" : "") + " name='" + $(this).attr("name").replace("_backup", "") + "' />" + $(option).text() + "</span>";
                     }
                     else {
                         return "<span>" + $(option).text() + "</span>";
                     }
                 },
                 initValue: function () { return $(this).text(); },
                 valueTemplate: function () { return $(this).text(); },

                 ellipsisLength: null,
                 addDropdownToBody: 0
             };

             var options = $(this).data("settings"),
             method = {
                 init: function () {
                     // Generate HTML
                     var _this = this,
                         t = $(_this),
                         selectedIndex = 0,
                         selectedOption = t.find("option:first");

                     // Hide mezelf
                     t.hide();

                     if (t.find("option:selected").length && _this.type != "select-multiple") {
                         selectedOption = t.find("option:selected");
                         selectedIndex = t.find("option").index(selectedOption);
                     }

                     // Maak een ul aan
                     var ul = "<ul class='" + options.dropDownClass + " trans-element'><li>";

                     if (options.acceptManualInput && !_touch) {
                         var value = t.data("value") || options.initValue.call(selectedOption);
                         ul += "<ins></ins><input type='text' name='" + t.attr("name").replace("_backup", "") + "' value='" + value + "' />";

                         // Save old select
                         if (t.attr("name").indexOf("_backup") < 0) {
                             t.attr("name", t.attr("name") + "_backup");
                         }
                     }
                     else {
                         if (options.ellipsisLength) {
                             ul += "<span title=\"" + selectedOption.text() + "\">" + $.simpleEllipsis(options.initValue.call(selectedOption), options.ellipsisLength) + "</span>";
                         }
                         else {
                             ul += "<span>" + options.initValue.call(selectedOption) + "</span>";
                         }
                     }

                     ul += "<ul style='display: none;'>";

                     t.children().each(function (i) {
                         if (!i && !options.showFirstItemInDrop) {
                             // Don't do anything when you don't wanna show the first element
                         }
                         else {
                             ul += method[
                                 this.tagName == "OPTION" ? "getLIOptionChild" : "getLIOptgroupChildren"
                             ].call(_this, this);
                         }
                     });

                     ul += "</ul></li></ul>";

                     var $ul = $(ul),
                         $lis = $ul.find("ul li:not(.group)"),
                         $inp = $ul.find("input");
                     t.after($ul);

                     // Bind handlers
                     if (t.is(":disabled")) {
                         method.disabled.call(_this, 1);
                     }

                     if (_this.type == "select-multiple" && !_touch) {
                         if (t.attr("name") && t.attr("name").indexOf("_backup") == -1) {
                             t.attr("name", t.attr("name") + "_backup");
                         }
                         $lis.click(method.selectCheckbox);
                     }
                     else {
                         $lis.click(method.selectNewValue);

                         $inp.click(method.openDrop)
                                     .keydown(function (e) {
                                         // Tab or enter
                                         if ($.inArray(e.which, [9, 13]) >= 0)
                                             method.closeAllDropdowns();
                                     })
                                     .prev("ins")
                                     .click(method.openDrop);
                     }

                     if (options.useManualInputAsFilter) {
                         $inp.keyup(method.filterByInput);
                     }

                     $ul.find("span:first").click(method.openDrop);

                     // Set data if we use addDropdownToBody option
                     $ul.find("ul:first").data("trans-element", $ul).addClass("transformSelectDropdown");
                     $ul.data("trans-element-drop", $ul.find("ul:first"));

                     if (options.addDropdownToBody) {
                         $ul.find("ul:first").appendTo("body");
                     }

                     // Check if there is already an event
                     $("html").unbind("click.transformSelect").bind("click.transformSelect", method.closeDropDowns)                    // Bind hotkeys

                     if ($.hotkeys && !$("body").data("trans-element-select")) {
                         $("body").data("trans-element-select", 1);

                         $(document)
                             .bind("keydown", "up", function (e) {
                                 var ul = $(".trans-focused"), select, selectedIndex;
                                 // Only enable on trans-element without input
                                 if (!ul.length || ul.find("input").length) return 0;
                                 select = ul.prevAll("select").first();

                                 selectedIndex = select[0].selectedIndex - 1
                                 if (selectedIndex < 0) {
                                     selectedIndex = select.find("option").length - 1;
                                 }

                                 method.selectIndex.call(select, selectedIndex);

                                 return 0;
                             })
                             .bind("keydown", "down", function (e) {
                                 var ul = $(".trans-focused"), select, selectedIndex;
                                 // Only enable on trans-element without input
                                 if (!ul.length || ul.find("input").length) return 0;
                                 select = ul.prevAll("select").first();

                                 selectedIndex = select[0].selectedIndex + 1
                                 if (selectedIndex > select.find("option").length - 1) {
                                     selectedIndex = 0;
                                 }

                                 method.selectIndex.call(select, selectedIndex);
                                 return 0;
                             });
                     }

                     // Gebruik native selects
                     if (_touch) {
                         if (!options.showFirstItemInDrop) {
                             t.find("option:first").remove();
                         }
                         t.appendTo($ul.find("li:first"))
                             .show()
                             .css({
                                 opacity: 0,
                                 position: "absolute",
                                 width: "100%",
                                 height: "100%",
                                 left: 0,
                                 top: 0
                             });
                         $ul.find("li:first").css({
                             position: "relative"
                         });
                         t.change(method.mobileChange);
                     }
                 },
                 getUL: function () {
                     return _touch ? $(this).closest("ul") : $(this).next(".trans-element:first");
                 },
                 getSelect: function ($ul) {
                     return _touch ? $ul.find("select") : $ul.prevAll("select:first");
                 },
                 disabled: function (disabled) {
                     method.getUL.call(this)[disabled ? "addClass" : "removeClass"]("disabled");
                 },
                 repaint: function () {
                     var ul = method.getUL.call(this);
                     ul.data("trans-element-drop").remove();
                     ul.remove();

                     method.init.call(this);
                 },
                 filterByInput: function () {
                     var _this = $(this),
                         val = _this.val().toLowerCase(),
                         ul = _this.closest("ul"),
                         drop = ul.data("trans-element-drop"),
                         li = drop.find("li");

                     // val == ""
                     if (!val) {
                         li.show();
                     }
                     else {
                         li.each(function () {
                             var _li = $(this);
                             if (!!_li.data("settings").alwaysvisible) {
                                 _li.show();
                             }
                             else {
                                 _li[_li.text().toLowerCase().indexOf(val) < 0 ? "hide" : "show"]();
                             }
                         });
                     }
                 },
                 selectIndex: function (index) {
                     var select = $(this),
                         ul = method.getUL.call(this),
                         drop = ul.data("trans-element-drop");

                     try {
                         drop.find("li").filter(function () {
                         }).first().trigger("click");
                         return $(this).text() == select.find("option").eq(index).text();
                     }
                     catch (e) { }
                 },
                 selectValue: function (value) {
                     var select = $(this),
                         ul = method.getUL.call(this),
                         drop = ul.data("trans-element-drop");

                     method.selectIndex.call(this, select.find(value ? "option[value='" + value + "']" : "option:not([value])").index());
                 },
                 /*
                 *	GET option child
                 */
                 getLIOptionChild: function (option) {
                     var settings = $(option).attr("data-settings") || '',
                         cls = ($(option).attr('class') || '') +
                                     ($(option).is(":selected") ? ' selected' : '');

                     return "<li data-settings='" + settings + "' class='" + cls + "'>" + options.subTemplate.call(this, $(option)) + "</li>";
                 },
                 /*
                 *	GET optgroup children
                 */
                 getLIOptgroupChildren: function (group) {
                     var _this = this,
                         li = "<li class='group'><span>" + $(group).attr("label") + "</span><ul>";

                     $(group).find("option").each(function () {
                         li += method.getLIOptionChild.call(_this, this);
                     });

                     li += "</ul></li>";

                     return li;
                 },
                 getLIIndex: function (el) {
                     var index = 0, group = el.closest(".group"), sel;
                     if (group.length) {
                         index = el.closest(".transformSelectDropdown").find("li").index(el) - group.prevAll(".group").length - 1;
                     }
                     else {
                         index = el.parent().find("li").index(el) - el.prevAll(".group").length;
                     }
                     if (!options.showFirstItemInDrop) {
                         index += 1;
                     }
                     return index;
                 },
                 /*
                 *	Select a new value
                 */
                 selectNewValue: function () {
                     var _this = $(this),
                         $drop = _this.closest(".transformSelectDropdown"),
                         $ul = $drop.data("trans-element"),
                         select = method.getSelect($ul),
                         index = method.getLIIndex(_this);

                     select[0].selectedIndex = index;

                     // If it has an input, there is no span used for value holding
                     if ($ul.find("input").length) {
                         $ul.find("input").val(options.valueTemplate.call(_this));
                     }
                     else {
                         sel = select.find("option:selected");
                         $ul
                             .find("span:first")
                             .html(
                                 options.ellipsisLength
                                 ? $.simpleEllipsis(options.valueTemplate.call(sel), options.ellipsisLength)
                                 : options.valueTemplate.call(sel)
                             );
                     }

                     // Set selected
                     $drop.find(".selected").removeClass("selected");
                     _this.addClass("selected");

                     method.closeAllDropdowns();

                     // Trigger onchange
                     select.trigger("change");

                     $(".trans-element").removeClass("trans-focused");
                     $ul.addClass("trans-focused");

                     // Update validator
                     if ($.fn.validate && select.closest("form").length) {
                         select.valid();
                     }
                 },
                 mobileChange: function () {
                     var select = $(this),
                         $ul = method.getUL.call(this),
                         sel = select.find("option:selected");

                     if (this.type != "select-multiple") {
                         $ul
                         .find("span:first")
                         .html(
                             options.ellipsisLength
                             ? $.simpleEllipsis(options.valueTemplate.call(sel), options.ellipsisLength)
                             : options.valueTemplate.call(sel)
                         );
                     }
                 },
                 selectCheckbox: function (e) {
                     var _this = $(this),
                         $drop = _this.closest(".transformSelectDropdown"),
                         $ul = $drop.data("trans-element"),
                         select = method.getSelect($ul),
                         t = _this.closest("li"),
                         checkbox = t.find(":checkbox"),
                         index, group;

                     if ($(e.target).is("li")) {
                         t = _this;
                     }

                     index = method.getLIIndex(t);

                     if (!$(e.target).is(":checkbox")) {
                         checkbox.prop("checked", !checkbox.is(":checked"));
                     }

                     select.find("option").eq(index).prop("selected", checkbox.is(":checked"));

                     if (checkbox.data("tfc.init")) {
                         checkbox.transformCheckbox("setImage");
                     }

                     if (!$(e.target).is(":checkbox")) {
                         checkbox.change();
                     }
                     select.change();
                 },
                 /*
                 *	Open clicked dropdown
                 *		and Close all others
                 */
                 openDrop: function () {
                     var UL = $(this).closest(".trans-element"),
                         childUL = UL.data("trans-element-drop"),
                         childLI = $(this).parent();

                     if (UL.hasClass("disabled")) {
                         return 0;
                     }

                     // Close on second click
                     if (childLI.hasClass("open") && !$(this).is("input")) {
                         method.closeAllDropdowns();
                     }
                         // Open on first click
                     else {
                         childLI
                             .css({ 'z-index': 1200 })
                             .addClass("open");

                         childUL.css({ 'z-index': 1200 }).show();

                         method.hideAllOtherDropdowns.call(this);
                     }

                     if (options.addDropdownToBody) {
                         childUL.css({
                             position: "absolute",
                             top: childLI.offset().top + childLI.outerHeight(),
                             left: childLI.offset().left
                         });
                     }
                 },
                 /*
                 *	Hide all elements except this element
                 */
                 hideAllOtherDropdowns: function () {
                     // Hide elements with the same class
                     var allElements = $("body").find("*"),
                         elIndex = allElements.index($(this).parent());

                     $("body").find("ul.trans-element").each(function () {
                         var childUL = $(this).data("trans-element-drop");

                         if (elIndex - 1 != allElements.index($(this))) {
                             childUL
                                    .hide()
                                    .css('z-index', 0)
                                         .parent()
                                         .css('z-index', 0)
                                         .removeClass("open");
                         }
                     });
                 },
                 /*
                 *	Close all dropdowns
                 */
                 closeDropDowns: function (e) {
                     if (!$(e.target).closest(".trans-element").length) {
                         method.closeAllDropdowns();
                     }
                 },
                 closeAllDropdowns: function () {
                     $("ul.trans-element").each(function () {
                         $(this).data("trans-element-drop").hide();
                         $(this).find("li:first").removeClass("open")
                     }).removeClass("trans-focused");
                 }
             }

             if (typeof opts == "string") {
                 method[opts].apply(this, Array.prototype.slice.call(arguments, 1))
                 return this;
             }
             return this.each(function () {
                 var _this = $(this);

                 // Is already initialized
                 if (!_this.data("tfs.init")) {
                     options = $.extend(defaults, opts);
                     _this.data("settings", options);

                     // set initialized
                     _this.data("tfs.init", 1);

                     // Call init functions
                     method.init.call(this);
                 }
             });
         },
         /*
         Transform a input:file to your own layout
         ============================================
         Basic CSS:
         <style>
         .customInput {
         display: inline-block;
         font-size: 12px;
         }
         
         .customInput .inputPath {
         width: 150px;
         padding: 4px;
         display: inline-block;
         border: 1px solid #ABADB3;
         background-color: #FFF;
         overflow: hidden;
         vertical-align: bottom;
         white-space: nowrap;
         -o-text-overflow: ellipsis;
         text-overflow:    ellipsis;
         }
         
         .customInput .inputButton {
         display: inline-block;
         padding: 4px;
         border: 1px solid #ABADB3;
         background-color: #CCC;
         vertical-align: bottom;
         }        </style>
         */
         transformFile: function (options) {
             var method = {
                 file: function (fn, cssClass) {
                     return this.each(function () {
                         var el = $(this),
                             holder = $('<div></div>').appendTo(el).css({
                                 position: 'absolute',
                                 overflow: 'hidden',
                                 '-moz-opacity': '0',
                                 filter: 'alpha(opacity: 0)',
                                 opacity: '0',
                                 zoom: '1',
                                 width: el.outerWidth() + 'px',
                                 height: el.outerHeight() + 'px',
                                 'z-index': 1
                             }),
                             wid = 0,
                             inp,
                             addInput = function () {
                                 var current = inp = holder.html('<input ' + (window.FormData ? 'multiple ' : '') + 'type="file" style="border:none; position:absolute">').find('input');

                                 wid = wid || current.width();

                                 current.change(function () {
                                     current.unbind('change');

                                     addInput();
                                     fn(current[0]);
                                 });
                             },
                             position = function (e) {
                                 holder.offset(el.offset());
                                 if (e) {
                                     inp.offset({ left: e.pageX - wid + 25, top: e.pageY - 10 });
                                     addMouseOver();
                                 }
                             },
                             addMouseOver = function () {
                                 el.addClass(cssClass + 'MouseOver');
                             },
                             removeMouseOver = function () {
                                 el.removeClass(cssClass + 'MouseOver');
                             };

                         addInput();

                         el.mouseover(position);
                         el.mousemove(position);
                         el.mouseout(removeMouseOver);
                         position();
                     });
                 }
             };

             return this.each(function (i) {
                 // Is already initialized
                 if (!$(this).data("tff.init")) {
                     // set initialized
                     $(this).data("tff.init", 1);

                     // 
                     var el = $(this).hide(),
                         id = null,
                         name = el.attr('name'),
                         cssClass = (!options ? 'customInput' : (options.cssClass ? options.cssClass : 'customInput')),
                         label = (!options ? 'Browse...' : (options.label ? options.label : 'Browse...'));

                     if (!el.attr('id')) { el.attr('id', 'custom_input_file_' + (new Date().getTime()) + Math.floor(Math.random() * 100000)); }
                     id = el.attr('id');

                     el.after('<span id="' + id + '_custom_input" class="' + cssClass + '"><span class="inputPath" id="' + id + '_custom_input_path">&nbsp;</span><span class="inputButton">' + label + '</span></span>');

                     method.file.call($('#' + id + '_custom_input'), function (inp) {
                         inp.id = id;
                         inp.name = name;
                         $('#' + id).replaceWith(inp)
                                    .removeAttr('style').hide();
                         $('#' + id + '_custom_input_path').html($('#' + id).val().replace(/\\/g, '/').replace(/.*\//, ''));
                     }, cssClass);
                 }
             });

         },
         /*
         Replace a textarea
         */
         transformTextarea: function (options, arg1) {
             var defaults = {
                 hiddenTextareaClass: "hiddenTextarea"
             },
                 settings = $.extend(defaults, options),

                 method = {
                     // Init the module
                     init: function () {
                         var _this = $(this);

                         // This only happens in IE
                         if (_this.css("line-height") == "normal") {
                             _this.css("line-height", "12px");
                         }

                         // Set the CSS
                         var CSS = {
                             'line-height': _this.css("line-height"),
                             'font-family': _this.css("font-family"),
                             'font-size': _this.css("font-size"),
                             "border": "1px solid black",
                             "width": _this.width(),
                             "letter-spacing": _this.css("letter-spacing"),
                             "text-indent": _this.css("text-indent"),
                             "padding": _this.css("padding"),
                             "overflow": "hidden",
                             "white-space": _this.css("white-space")
                         };

                         _this
                         // Add a new textarea
                                 .css(CSS)
                                 .keyup(method.keyup)
                                 .keydown(method.keyup)
                                 .bind("mousewheel", method.mousewheel)
                         // Append a div
                             .after($("<div />"))
                                 .next()
                                 .addClass(settings.hiddenTextareaClass)
                                 .css(CSS)
                                 .css("width", _this.width() - 5)	// Minus 5 because there is some none typeable padding?
                                 .hide()

                     },

                     // Mousewheel
                     mousewheel: function (e, delta) {
                         e.preventDefault();
                         var lineHeight = $(this).css("line-height"),
                             scrollTo = $(this)[0].scrollTop + (parseFloat(lineHeight) * (delta * -1));
                         method.scrollToPx.call(this, scrollTo);
                     },
                     // Used to scroll 
                     keyup: function (e) {
                         // Check if it has to scroll
                         // Arrow keys down have to scroll down / up (only if to far)
                         /*
                         Keys:
                         37, 38, 39, 40  = Arrow keys (L,U,R,D)
                         13				= Enter
                         */
                         if ($.inArray(e.which, [37, 38, 39, 40]) >= 0) {
                             method.checkCaretScroll.call(this);
                         }
                         else {
                             method.checkScroll.call(this, e.which);
                         }

                         method.scrollCallBack.call(this);
                     },
                     /*
                     Check cursor position to scroll
                     */
                     checkCaretScroll: function () {
                         var src = $(this),
                             caretStart = src.caret().start,
                             val = src.val(),
                             sTop = src.scrollTop(),
                             lHeight = parseInt(src.css("line-height")),
                             textBefore = val.substr(0, caretStart),
                             textAfter = val.substr(caretStart),
                             tar = src.next("." + settings.hiddenTextareaClass),
                             vScroll;

                         // First or last element (don't do anything)
                         if (caretStart) {
                             // Also pick the first char of a row
                             if (val.substr(caretStart - 1, 1) == '\n') {
                                 textBefore = val.substr(0, caretStart + 1);
                             }

                             method.toDiv.call(this, 0, textBefore, textAfter);

                             // If you go through the bottom
                             if (tar.height() > (src.height() + sTop)) {
                                 vScroll = sTop + lHeight;
                             }
                                 // if you go through the top
                             else if (tar.height() <= sTop) {
                                 vScroll = sTop - lHeight;
                             }

                             // Scroll the px
                             if (vScroll) {
                                 method.scrollToPx.call(this, vScroll);
                             }
                         }
                     },

                     // Check the old and new height if it needs to scroll
                     checkScroll: function (key) {
                         // Scroll if needed
                         var src = $(this),
                             tar = src.next("." + settings.hiddenTextareaClass),

                         // Put into the div to check new height
                             caretStart = src.caret().start,
                             v = src.val(),
                             textBefore = v.substr(0, caretStart),
                             textAfter = v.substr(caretStart);

                         method.toDiv.call(this, 1, textBefore, textAfter);

                         // If your halfway the scroll, then dont scroll
                         if (
                             (src.scrollTop() + src.height()) > tar.height()
                         ) {
                             return;
                         }

                         // Scroll if needed
                         if (tar.data("old-height") != tar.data("new-height")) {
                             var scrollDiff = tar.data("new-height") - tar.data("old-height");
                             method.scrollToPx.call(this, src.scrollTop() + scrollDiff);
                         }

                     },

                     // Place the value of the textarea into the DIV
                     toDiv: function (setHeight, html, textAfter) {
                         var src = $(this),
                             tar = src.next("." + settings.hiddenTextareaClass),
                             regEnter = /\n/g,
                             regSpace = /\s\s/g,
                             regSingleSpace = /\s/g,
                             res = src.val(),
                             appendEnter = 0,
                             appendEnterSpace = 0,
                             brXHTML = "<br />";
                         if (html)
                             res = html;

                         // If last key is enter
                         // 		or last key is space, and key before that was enter, then add enter
                         if (regEnter.test(res.substring(res.length - 1))) {
                             appendEnter = 1;
                         }

                         if (
                                 regEnter.test(res.substring(res.length - 2, res.length - 1)) &&
                                 regSingleSpace.test(res.substring(res.length - 1))
                             ) {
                             appendEnterSpace = 1;
                         }

                         // Set old and new height + set the content
                         if (setHeight)
                             tar.data("old-height", tar.height());

                         res = res.replace(regEnter, "<br>") // No space or it will be replaced by the function below
                                     .replace(regSpace, "&nbsp; ")
                                     .replace(regSpace, "&nbsp; ") // 2x because 1x can result in: &nbsp;(space)(space) and that is not seen within the div
                                     .replace(/<br>/ig, brXHTML);
                         tar.html(res);

                         if ((appendEnter || appendEnterSpace) && $.trim(textAfter)) {
                             if (appendEnterSpace && $.browser.msie)
                                 tar.append(brXHTML);
                             tar.append(brXHTML);
                         }

                         if (setHeight) {
                             tar.data("new-height", tar.height());
                         }
                     },

                     // Scroll to a given percentage
                     scrollToPercentage: function (perc) {
                         // Between 0 and 100
                         if (perc >= 0 && perc <= 100) {
                             var src = $(this),
                                 tar = src.next("." + settings.hiddenTextareaClass),
                                 maxScroll = parseFloat(src[0].scrollHeight) - src.height(),
                                 scrollT = maxScroll * perc / 100;

                             // Round on a row
                             method.scrollToPx.call(this, scrollT);
                         }
                     },

                     // Scroll to given PX
                     scrollToPx: function (px) {
                         var _this = this;
                         // Round on a row
                         $(_this).scrollTop(method.roundToLineHeight.call(_this, px));
                         method.scrollCallBack.call(_this);
                     },

                     // Round to line height
                     roundToLineHeight: function (num) {
                         var lh = parseInt($(this).css("line-height"));
                         return Math.ceil(num / lh) * lh;
                     },

                     // Reset to default
                     remove: function () {
                         $(this)
                             .unbind("keyup")
                             .css({
                                 overflow: "auto",
                                 border: ""
                             })
                         .next("div")
                             .remove();
                     },
                     scrollCallBack: function () {
                         var _this = this,
                             _$this = $(_this),
                             _this0 = _$this[0],
                             maxScroll = parseFloat(_this0.scrollHeight) - _$this.height(),
                             percentage = parseFloat(_this0.scrollTop) / maxScroll * 100;
                         percentage = percentage > 100 ? 100 : percentage;
                         percentage = percentage < 0 ? 0 : percentage;
                         percentage = isNaN(percentage) ? 100 : percentage;
                         _$this.trigger("scrollToPx", [_this0.scrollTop, percentage]);
                     }
                 };

             if (typeof options == "string") {
                 method[options].call(this, arg1);
                 return this;
             }
             return this.each(function () {
                 if (!$(this).next().hasClass(settings.hiddenTextareaClass)) {
                     method.init.call(this);
                     method.toDiv.call(this, 1);
                 }
             });
         }
     });

     // Radio and checkbox now use the same function
     $.fn.transformRadio = $.fn.transformCheckbox;
 })(jQuery);

/* Columnizer plugin */
/*jquery.columnizer.js*/        
 // version 1.6.0
 // http://welcome.totheinter.net/columnizer-jquery-plugin/
 // created by: Adam Wulf @adamwulf, adam.wulf@gmail.com

 (function ($) {

     $.fn.columnize = function (options) {


         var defaults = {
             // default width of columns
             width: 400,
             // optional # of columns instead of width
             columns: false,
             // true to build columns once regardless of window resize
             // false to rebuild when content box changes bounds
             buildOnce: false,
             // an object with options if the text should overflow
             // it's container if it can't fit within a specified height
             overflow: false,
             // this function is called after content is columnized
             doneFunc: function () { },
             // if the content should be columnized into a 
             // container node other than it's own node
             target: false,
             // re-columnizing when images reload might make things
             // run slow. so flip this to true if it's causing delays
             ignoreImageLoading: true,
             // should columns float left or right
             columnFloat: "left",
             // ensure the last column is never the tallest column
             lastNeverTallest: false,
             // (int) the minimum number of characters to jump when splitting
             // text nodes. smaller numbers will result in higher accuracy
             // column widths, but will take slightly longer
             accuracy: false,
             // don't automatically layout columns, only use manual columnbreak
             manualBreaks: false,
             // previx for all the CSS classes used by this plugin
             // default to empty string for backwards compatibility
             cssClassPrefix: ""
         };
         options = $.extend(defaults, options);

         if (typeof (options.width) == "string") {
             options.width = parseInt(options.width, 10);
             if (isNaN(options.width)) {
                 options.width = defaults.width;
             }
         }

         /**
          * appending a text node to a <table> will
          * cause a jquery crash.
          * so wrap all append() calls and revert to
          * a simple appendChild() in case it fails
          */
         function appendSafe($target, $elem) {
             try {
                 $target.append($elem);
             } catch (e) {
                 $target[0].appendChild($elem[0]);
             }
         }

         return this.each(function () {
             var $inBox = options.target ? $(options.target) : $(this);
             var maxHeight = $(this).height();
             var $cache = $('<div></div>'); // this is where we'll put the real content
             var lastWidth = 0;
             var columnizing = false;
             var manualBreaks = options.manualBreaks;
             var cssClassPrefix = defaults.cssClassPrefix;
             if (typeof (options.cssClassPrefix) == "string") {
                 cssClassPrefix = options.cssClassPrefix;
             }


             var adjustment = 0;

             appendSafe($cache, $(this).contents().clone(true));

             // images loading after dom load
             // can screw up the column heights,
             // so recolumnize after images load
             if (!options.ignoreImageLoading && !options.target) {
                 if (!$inBox.data("imageLoaded")) {
                     $inBox.data("imageLoaded", true);
                     if ($(this).find("img").length > 0) {
                         // only bother if there are
                         // actually images...
                         var func = function ($inBox, $cache) {
                             return function () {
                                 if (!$inBox.data("firstImageLoaded")) {
                                     $inBox.data("firstImageLoaded", "true");
                                     appendSafe($inBox.empty(), $cache.children().clone(true));
                                     $inBox.columnize(options);
                                 }
                             };
                         }($(this), $cache);
                         $(this).find("img").one("load", func);
                         $(this).find("img").one("abort", func);
                         return;
                     }
                 }
             }

             $inBox.empty();

             columnizeIt();

             if (!options.buildOnce) {
                 $(window).resize(function () {
                     if (!options.buildOnce) {
                         if ($inBox.data("timeout")) {
                             clearTimeout($inBox.data("timeout"));
                         }
                         $inBox.data("timeout", setTimeout(columnizeIt, 200));
                     }
                 });
             }

             function prefixTheClassName(className, withDot) {
                 var dot = withDot ? "." : "";
                 if (cssClassPrefix.length) {
                     return dot + cssClassPrefix + "-" + className;
                 }
                 return dot + className;
             }

             /**
              * this fuction builds as much of a column as it can without
              * splitting nodes in half. If the last node in the new column
              * is a text node, then it will try to split that text node. otherwise
              * it will leave the node in $pullOutHere and return with a height
              * smaller than targetHeight.
              * 
              * Returns a boolean on whether we did some splitting successfully at a text point
              * (so we know we don't need to split a real element). return false if the caller should
              * split a node if possible to end this column.
              *
              * @param putInHere, the jquery node to put elements into for the current column
              * @param $pullOutHere, the jquery node to pull elements out of (uncolumnized html)
              * @param $parentColumn, the jquery node for the currently column that's being added to
              * @param targetHeight, the ideal height for the column, get as close as we can to this height
              */
             function columnize($putInHere, $pullOutHere, $parentColumn, targetHeight) {
                 //
                 // add as many nodes to the column as we can,
                 // but stop once our height is too tall
                 while ((manualBreaks || $parentColumn.height() < targetHeight) &&
                     $pullOutHere[0].childNodes.length) {
                     var node = $pullOutHere[0].childNodes[0];
                     //
                     // Because we're not cloning, jquery will actually move the element"
                     // http://welcome.totheinter.net/2009/03/19/the-undocumented-life-of-jquerys-append/
                     if ($(node).find(prefixTheClassName("columnbreak", true)).length) {
                         //
                         // our column is on a column break, so just end here
                         return;
                     }
                     if ($(node).hasClass(prefixTheClassName("columnbreak"))) {
                         //
                         // our column is on a column break, so just end here
                         return;
                     }
                     appendSafe($putInHere, $(node));
                 }
                 if ($putInHere[0].childNodes.length === 0) return;

                 // now we're too tall, so undo the last one
                 var kids = $putInHere[0].childNodes;
                 var lastKid = kids[kids.length - 1];
                 $putInHere[0].removeChild(lastKid);
                 var $item = $(lastKid);

                 // now lets try to split that last node
                 // to fit as much of it as we can into this column
                 if ($item[0].nodeType == 3) {
                     // it's a text node, split it up
                     var oText = $item[0].nodeValue;
                     var counter2 = options.width / 18;
                     if (options.accuracy)
                         counter2 = options.accuracy;
                     var columnText;
                     var latestTextNode = null;
                     while ($parentColumn.height() < targetHeight && oText.length) {
                         //
                         // it's been brought up that this won't work for chinese
                         // or other languages that don't have the same use of whitespace
                         // as english. This will need to be updated in the future
                         // to better handle non-english languages.
                         //
                         // https://github.com/adamwulf/Columnizer-jQuery-Plugin/issues/124
                         var indexOfSpace = oText.indexOf(' ', counter2);
                         if (indexOfSpace != -1) {
                             columnText = oText.substring(0, indexOfSpace);
                         } else {
                             columnText = oText;
                         }
                         latestTextNode = document.createTextNode(columnText);
                         appendSafe($putInHere, $(latestTextNode));

                         if (oText.length > counter2 && indexOfSpace != -1) {
                             oText = oText.substring(indexOfSpace);
                         } else {
                             oText = "";
                         }
                     }
                     if ($parentColumn.height() >= targetHeight && latestTextNode !== null) {
                         // too tall :(
                         $putInHere[0].removeChild(latestTextNode);
                         oText = latestTextNode.nodeValue + oText;
                     }
                     if (oText.length) {
                         $item[0].nodeValue = oText;
                     } else {
                         return false; // we ate the whole text node, move on to the next node
                     }
                 }

                 if ($pullOutHere.contents().length) {
                     $pullOutHere.prepend($item);
                 } else {
                     appendSafe($pullOutHere, $item);
                 }

                 return $item[0].nodeType == 3;
             }

             /**
              * Split up an element, which is more complex than splitting text. We need to create 
              * two copies of the element with it's contents divided between each
              */
             function split($putInHere, $pullOutHere, $parentColumn, targetHeight) {
                 if ($putInHere.contents(":last").find(prefixTheClassName("columnbreak", true)).length) {
                     //
                     // our column is on a column break, so just end here
                     return;
                 }
                 if ($putInHere.contents(":last").hasClass(prefixTheClassName("columnbreak"))) {
                     //
                     // our column is on a column break, so just end here
                     return;
                 }
                 if ($pullOutHere.contents().length) {
                     var $cloneMe = $pullOutHere.contents(":first");
                     //
                     // make sure we're splitting an element
                     if (typeof $cloneMe.get(0) == 'undefined' || $cloneMe.get(0).nodeType != 1) return;

                     //
                     // clone the node with all data and events
                     var $clone = $cloneMe.clone(true);
                     //
                     // need to support both .prop and .attr if .prop doesn't exist.
                     // this is for backwards compatibility with older versions of jquery.
                     if ($cloneMe.hasClass(prefixTheClassName("columnbreak"))) {
                         //
                         // ok, we have a columnbreak, so add it into
                         // the column and exit
                         appendSafe($putInHere, $clone);
                         $cloneMe.remove();
                     } else if (manualBreaks) {
                         // keep adding until we hit a manual break
                         appendSafe($putInHere, $clone);
                         $cloneMe.remove();
                     } else if ($clone.get(0).nodeType == 1 && !$clone.hasClass(prefixTheClassName("dontend"))) {
                         appendSafe($putInHere, $clone);
                         if ($clone.is("img") && $parentColumn.height() < targetHeight + 20) {
                             //
                             // we can't split an img in half, so just add it
                             // to the column and remove it from the pullOutHere section
                             $cloneMe.remove();
                         } else if ($cloneMe.hasClass(prefixTheClassName("dontsplit")) && $parentColumn.height() < targetHeight + 20) {
                             //
                             // pretty close fit, and we're not allowed to split it, so just
                             // add it to the column, remove from pullOutHere, and be done
                             $cloneMe.remove();
                         } else if ($clone.is("img") || $cloneMe.hasClass(prefixTheClassName("dontsplit"))) {
                             //
                             // it's either an image that's too tall, or an unsplittable node
                             // that's too tall. leave it in the pullOutHere and we'll add it to the 
                             // next column
                             $clone.remove();
                         } else {
                             //
                             // ok, we're allowed to split the node in half, so empty out
                             // the node in the column we're building, and start splitting
                             // it in half, leaving some of it in pullOutHere
                             $clone.empty();
                             if (!columnize($clone, $cloneMe, $parentColumn, targetHeight)) {
                                 // this node may still have non-text nodes to split
                                 // add the split class and then recur
                                 $cloneMe.addClass(prefixTheClassName("split"));

                                 //if this node was ol element, the child should continue the number ordering
                                 if ($cloneMe.get(0).tagName == 'OL') {
                                     var startWith = $clone.get(0).childElementCount + $clone.get(0).start;
                                     $cloneMe.attr('start', startWith + 1);
                                 }

                                 if ($cloneMe.children().length) {
                                     split($clone, $cloneMe, $parentColumn, targetHeight);
                                 }
                             } else {
                                 // this node only has text node children left, add the
                                 // split class and move on.
                                 $cloneMe.addClass(prefixTheClassName("split"));
                             }
                             if ($clone.get(0).childNodes.length === 0) {
                                 // it was split, but nothing is in it :(
                                 $clone.remove();
                                 $cloneMe.removeClass(prefixTheClassName("split"));
                             }
                         }
                     }
                 }
             }


             function singleColumnizeIt() {
                 if ($inBox.data("columnized") && $inBox.children().length == 1) {
                     return;
                 }
                 $inBox.data("columnized", true);
                 $inBox.data("columnizing", true);

                 $inBox.empty();
                 $inBox.append($("<div class='"
                     + prefixTheClassName("first") + " "
                     + prefixTheClassName("last") + " "
                     + prefixTheClassName("column") + " "
                     + "' style='width:100%; float: " + options.columnFloat + ";'></div>")); //"
                 $col = $inBox.children().eq($inBox.children().length - 1);
                 $destroyable = $cache.clone(true);
                 if (options.overflow) {
                     targetHeight = options.overflow.height;
                     columnize($col, $destroyable, $col, targetHeight);
                     // make sure that the last item in the column isn't a "dontend"
                     if (!$destroyable.contents().find(":first-child").hasClass(prefixTheClassName("dontend"))) {
                         split($col, $destroyable, $col, targetHeight);
                     }

                     while ($col.contents(":last").length && checkDontEndColumn($col.contents(":last").get(0))) {
                         var $lastKid = $col.contents(":last");
                         $lastKid.remove();
                         $destroyable.prepend($lastKid);
                     }

                     var html = "";
                     var div = document.createElement('DIV');
                     while ($destroyable[0].childNodes.length > 0) {
                         var kid = $destroyable[0].childNodes[0];
                         if (kid.attributes) {
                             for (var i = 0; i < kid.attributes.length; i++) {
                                 if (kid.attributes[i].nodeName.indexOf("jQuery") === 0) {
                                     kid.removeAttribute(kid.attributes[i].nodeName);
                                 }
                             }
                         }
                         div.innerHTML = "";
                         div.appendChild($destroyable[0].childNodes[0]);
                         html += div.innerHTML;
                     }
                     var overflow = $(options.overflow.id)[0];
                     overflow.innerHTML = html;

                 } else {
                     appendSafe($col, $destroyable.contents());
                 }
                 $inBox.data("columnizing", false);

                 if (options.overflow && options.overflow.doneFunc) {
                     options.overflow.doneFunc();
                 }

             }

             /**
              * returns true if the input dom node
              * should not end a column.
              * returns false otherwise
              */
             function checkDontEndColumn(dom) {
                 if (dom.nodeType == 3) {
                     // text node. ensure that the text
                     // is not 100% whitespace
                     if (/^\s+$/.test(dom.nodeValue)) {
                         //
                         // ok, it's 100% whitespace,
                         // so we should return checkDontEndColumn
                         // of the inputs previousSibling
                         if (!dom.previousSibling) return false;
                         return checkDontEndColumn(dom.previousSibling);
                     }
                     return false;
                 }
                 if (dom.nodeType != 1) return false;
                 if ($(dom).hasClass(prefixTheClassName("dontend"))) return true;
                 if (dom.childNodes.length === 0) return false;
                 return checkDontEndColumn(dom.childNodes[dom.childNodes.length - 1]);
             }

             function columnizeIt() {
                 //reset adjustment var
                 adjustment = 0;
                 if (lastWidth == $inBox.width()) return;
                 lastWidth = $inBox.width();

                 var numCols = Math.round($inBox.width() / options.width);
                 var optionWidth = options.width;
                 var optionHeight = options.height;
                 if (options.columns) numCols = options.columns;
                 if (manualBreaks) {
                     numCols = $cache.find(prefixTheClassName("columnbreak", true)).length + 1;
                     optionWidth = false;
                 }

                 //			if ($inBox.data("columnized") && numCols == $inBox.children().length) {
                 //				return;
                 //			}
                 if (numCols <= 1) {
                     return singleColumnizeIt();
                 }
                 if ($inBox.data("columnizing")) return;
                 $inBox.data("columnized", true);
                 $inBox.data("columnizing", true);

                 $inBox.empty();
                 $inBox.append($("<div style='width:" + (Math.floor(100 / numCols)) + "%; float: " + options.columnFloat + ";'></div>")); //"
                 $col = $inBox.children(":last");
                 appendSafe($col, $cache.clone());
                 maxHeight = $col.height();
                 $inBox.empty();

                 var targetHeight = maxHeight / numCols;
                 var firstTime = true;
                 var maxLoops = 3;
                 var scrollHorizontally = false;
                 if (options.overflow) {
                     maxLoops = 1;
                     targetHeight = options.overflow.height;
                 } else if (optionHeight && optionWidth) {
                     maxLoops = 1;
                     targetHeight = optionHeight;
                     scrollHorizontally = true;
                 }

                 //
                 // We loop as we try and workout a good height to use. We know it initially as an average 
                 // but if the last column is higher than the first ones (which can happen, depending on split
                 // points) we need to raise 'adjustment'. We try this over a few iterations until we're 'solid'.
                 //
                 // also, lets hard code the max loops to 20. that's /a lot/ of loops for columnizer,
                 // and should keep run aways in check. if somehow someone has content combined with
                 // options that would cause an infinite loop, then this'll definitely stop it.
                 for (var loopCount = 0; loopCount < maxLoops && loopCount < 20; loopCount++) {
                     $inBox.empty();
                     var $destroyable, className, $col, $lastKid;
                     try {
                         $destroyable = $cache.clone(true);
                     } catch (e) {
                         // jquery in ie6 can't clone with true
                         $destroyable = $cache.clone();
                     }
                     $destroyable.css("visibility", "hidden");
                     // create the columns
                     for (var i = 0; i < numCols; i++) {
                         /* create column */
                         className = (i === 0) ? prefixTheClassName("first") : "";
                         className += " " + prefixTheClassName("column");
                         className = (i == numCols - 1) ? (prefixTheClassName("last") + " " + className) : className;
                         $inBox.append($("<div class='" + className + "' style='width:" + (Math.floor(100 / numCols)) + "%; float: " + options.columnFloat + ";'></div>")); //"
                     }

                     // fill all but the last column (unless overflowing)
                     i = 0;
                     while (i < numCols - (options.overflow ? 0 : 1) || scrollHorizontally && $destroyable.contents().length) {
                         if ($inBox.children().length <= i) {
                             // we ran out of columns, make another
                             $inBox.append($("<div class='" + className + "' style='width:" + (Math.floor(100 / numCols)) + "%; float: " + options.columnFloat + ";'></div>")); //"
                         }
                         $col = $inBox.children().eq(i);
                         if (scrollHorizontally) {
                             $col.width(optionWidth + "px");
                         }
                         columnize($col, $destroyable, $col, targetHeight);
                         // make sure that the last item in the column isn't a "dontend"
                         split($col, $destroyable, $col, targetHeight);

                         while ($col.contents(":last").length && checkDontEndColumn($col.contents(":last").get(0))) {
                             $lastKid = $col.contents(":last");
                             $lastKid.remove();
                             $destroyable.prepend($lastKid);
                         }
                         i++;

                         //
                         // https://github.com/adamwulf/Columnizer-jQuery-Plugin/issues/47
                         //
                         // check for infinite loop.
                         //
                         // this could happen when a dontsplit or dontend item is taller than the column
                         // we're trying to build, and its never actually added to a column.
                         //
                         // this results in empty columns being added with the dontsplit item
                         // perpetually waiting to get put into a column. lets force the issue here
                         if ($col.contents().length === 0 && $destroyable.contents().length) {
                             //
                             // ok, we're building zero content columns. this'll happen forever
                             // since nothing can ever get taken out of destroyable.
                             //
                             // to fix, lets put 1 item from destroyable into the empty column
                             // before we iterate
                             $col.append($destroyable.contents(":first"));
                         } else if (i == numCols - (options.overflow ? 0 : 1) && !options.overflow) {
                             //
                             // ok, we're about to exit the while loop because we're done with all
                             // columns except the last column.
                             //
                             // if $destroyable still has columnbreak nodes in it, then we need to keep
                             // looping and creating more columns.
                             if ($destroyable.find(prefixTheClassName("columnbreak", true)).length) {
                                 numCols++;
                             }
                         }
                     }
                     if (options.overflow && !scrollHorizontally) {
                         var IE6 = false /*@cc_on || @_jscript_version < 5.7 @*/;
                         var IE7 = (document.all) && (navigator.appVersion.indexOf("MSIE 7.") != -1);
                         if (IE6 || IE7) {
                             var html = "";
                             var div = document.createElement('DIV');
                             while ($destroyable[0].childNodes.length > 0) {
                                 var kid = $destroyable[0].childNodes[0];
                                 for (i = 0; i < kid.attributes.length; i++) {
                                     if (kid.attributes[i].nodeName.indexOf("jQuery") === 0) {
                                         kid.removeAttribute(kid.attributes[i].nodeName);
                                     }
                                 }
                                 div.innerHTML = "";
                                 div.appendChild($destroyable[0].childNodes[0]);
                                 html += div.innerHTML;
                             }
                             var overflow = $(options.overflow.id)[0];
                             overflow.innerHTML = html;
                         } else {
                             $(options.overflow.id).empty().append($destroyable.contents().clone(true));
                         }
                     } else if (!scrollHorizontally) {
                         // the last column in the series
                         $col = $inBox.children().eq($inBox.children().length - 1);
                         $destroyable.contents().each(function () {
                             $col.append($(this));
                         });
                         var afterH = $col.height();
                         var diff = afterH - targetHeight;
                         var totalH = 0;
                         var min = 10000000;
                         var max = 0;
                         var lastIsMax = false;
                         var numberOfColumnsThatDontEndInAColumnBreak = 0;
                         $inBox.children().each(function ($inBox) {
                             return function ($item) {
                                 var $col = $inBox.children().eq($item);
                                 var endsInBreak = $col.children(":last").find(prefixTheClassName("columnbreak", true)).length;
                                 if (!endsInBreak) {
                                     var h = $col.height();
                                     lastIsMax = false;
                                     totalH += h;
                                     if (h > max) {
                                         max = h;
                                         lastIsMax = true;
                                     }
                                     if (h < min) min = h;
                                     numberOfColumnsThatDontEndInAColumnBreak++;
                                 }
                             };
                         }($inBox));

                         var avgH = totalH / numberOfColumnsThatDontEndInAColumnBreak;
                         if (totalH === 0) {
                             //
                             // all columns end in a column break,
                             // so we're done here
                             loopCount = maxLoops;
                         } else if (options.lastNeverTallest && lastIsMax) {
                             // the last column is the tallest
                             // so allow columns to be taller
                             // and retry
                             //
                             // hopefully this'll mean more content fits into
                             // earlier columns, so that the last column
                             // can be shorter than the rest
                             adjustment += 5;

                             targetHeight = targetHeight + 30;
                             if (loopCount == maxLoops - 1) maxLoops++;
                         } else if (max - min > 30) {
                             // too much variation, try again
                             targetHeight = avgH + 30;
                         } else if (Math.abs(avgH - targetHeight) > 20) {
                             // too much variation, try again
                             targetHeight = avgH;
                         } else {
                             // solid, we're done
                             loopCount = maxLoops;
                         }
                     } else {
                         // it's scrolling horizontally, fix the width/classes of the columns
                         $inBox.children().each(function (i) {
                             $col = $inBox.children().eq(i);
                             $col.width(optionWidth + "px");
                             if (i === 0) {
                                 $col.addClass(prefixTheClassName("first"));
                             } else if (i == $inBox.children().length - 1) {
                                 $col.addClass(prefixTheClassName("last"));
                             } else {
                                 $col.removeClass(prefixTheClassName("first"));
                                 $col.removeClass(prefixTheClassName("last"));
                             }
                         });
                         $inBox.width($inBox.children().length * optionWidth + "px");
                     }
                     $inBox.append($("<br style='clear:both;'>"));
                 }
                 $inBox.find(prefixTheClassName("column", true)).find(":first" + prefixTheClassName("removeiffirst", true)).remove();
                 $inBox.find(prefixTheClassName("column", true)).find(':last' + prefixTheClassName("removeiflast", true)).remove();
                 $inBox.find(prefixTheClassName("split", true)).find(":first" + prefixTheClassName("removeiffirst", true)).remove();
                 $inBox.find(prefixTheClassName("split", true)).find(':last' + prefixTheClassName("removeiflast", true)).remove();
                 $inBox.data("columnizing", false);

                 if (options.overflow) {
                     options.overflow.doneFunc();
                 }
                 options.doneFunc();
             }
         });
     };
 })(jQuery);

/* common js to init plugins */
/*scripts_header_footer.js*/
 //*Fix for iPhone orientation bug
 //Source: http://www.blog.highub.com/mobile-2/a-fix-for-iphone-viewport-scale-bug/
 //*/
 var metas = document.getElementsByTagName('meta');
 var i;

 if (navigator.userAgent.match(/iPhone/i)) {
     for (i = 0; i < metas.length; i++) {
         if (metas[i].name == "viewport") {
             metas[i].content = "width=device-width, minimum-scale=1.0, maximum-scale=1.0";
         }
     }
     document.addEventListener("gesturestart", gestureStart, false);
 }
 function gestureStart() {
     for (i = 0; i < metas.length; i++) {
         if (metas[i].name == "viewport") {
             metas[i].content = "width=device-width, minimum-scale=0.25, maximum-scale=1.6";
         }
     }
 }

 $(document).ready(function () {

     //Get Inner Width
     var w = $(window).innerWidth();

     //Get Inner Width after screen resize
     $(window).resize(function () {
         w = $(window).innerWidth();
     });

     function winWidth() {
         //Get Inner Width
         var ww = $(window).innerWidth();

         //Get Inner Width after screen resize
         $(window).resize(function () {
             ww = $(window).innerWidth();
         });

         return ww;
     }


     //************************/
     //* UTILITY MENU LINKS   */
     //***********************/
     var u_link, u_content = $('.utility_content');

     $('a.utility_link, a.mm_utility_link').click(function (e) {

         //$(this).parent().addClass('active').siblings().removeClass('active');
         $('.utility_nav li').removeClass('active');
         $('.utility_content_wrapper').css('background-color', '#EFEFEF');

         $(this).parent().addClass('active');

         e.preventDefault();

         u_link = $(this).attr('href');

         $('.utility_nav_wrapper').addClass('open');

         $('.close_utility_btn').show();

         if (u_content.is(':visible')) {

             u_content.hide(); //hide other open utility content
             $(u_link).show();
         }
         else {
             $(u_link).slideDown('slow');
         }

         //social media - background blue
         if (u_link == '#social_media') {

             $('.utility_content_wrapper').css('background-color', '#1A9EFF');
         }

         var link_class = $(this).attr('class');

         //if the link clicked was from the minimenu, close it then go to the top of the page
         if (link_class.indexOf('mm_utility_link') != -1) {
             $("#menu_button").removeClass("expanded");
             $("#navigation_list").removeClass("expanded");
             $("#minimenu").removeClass("child-expanded"); //apply padding when menu is expanded
             scrollTo(0, 0);
         }
         $('#request_quote_national.utility_content').show();

     });
	 
	

     $('.close_utility_btn a').click(function (e) {

         e.preventDefault();

         $('.utility_content').addClass('no-padding');

         u_content.hide().slideUp(1200, function () {

             $('.close_utility_btn').hide();
             $('.utility_nav_wrapper').removeClass('open');
             $('.utility_content_wrapper').css('background-color', '#EFEFEF');
             $('.utility_nav li').removeClass('active');
         });

         $('#request_quote_national.utility_content').show();
     });
	 
	  



     //********************/
     //* MEGA MENU		*/	
     //* SECTIONS: GLOBAL	*/			
     //********************/

     //* show mini menu */
     $("#menu_button").click(function () {
         $("#menu_button").toggleClass("expanded");
         $("#navigation_list").toggleClass("expanded");
         $("#minimenu").toggleClass("child-expanded"); //apply padding when menu is expanded
     });

     //* show sub navigation mini menu */
     $(".mobile-nav-wrapper a").click(function () {
         var id = $(this).attr('id');

         $(".sub_menu_header").toggleClass("expanded");
         $("ul#" + id + "-menu").toggleClass("expanded");
         $("#sub-minimenu").toggleClass("child-expanded"); //apply padding when menu is expanded
     });


   
	 
	 /* mini menu expand children */
	  $(".minimenu-wrap .arrow-plus-minus").click(function(e){
	  $(this).toggleClass("expand-minimenu");
	  $(this).parent().next(".lvl-3-list").slideToggle();
	  e.preventDefault();
	  
	  if($(this).hasClass("expand-minimenu")){ $(this).html("-"); }else{ $(this).html("+"); }
	  });
	  
	  $(".minimenu-wrap #minimenu .lvl-2-title").click(function(e){
	  $(this).next(".arrow-plus-minus").toggleClass("expand-minimenu");
	  $(this).parent().next(".lvl-3-list").slideToggle();
	  e.preventDefault();
	  
	  if($(this).next(".arrow-plus-minus").hasClass("expand-minimenu")){ $(this).next(".arrow-plus-minus").html("-"); }else{ $(this).next(".arrow-plus-minus").html("+"); }
	  });

     //* mega menu show and hide */

     $(".megamenu-outer-wrap").hide();

     /* $(document.body).on('click', '.desktop-nav-link.faux-hover > a' ,function() {
          // cache selectors to improve efficency a tiny tiny bit
          $parentOfThis = $(this).parent();
          $megamenuOuterWrap = $(".megamenu-outer-wrap");
          // button 
          $parentOfThis.removeClass("faux-hover").addClass("no-hover");
          $parentOfThis.siblings().removeClass("faux-hover").addClass("no-hover");
          // content 
          $megamenuOuterWrap.css('z-index','1000').stop().slideUp(900);
          
          // move main content down
          if (!$(".desktop-nav-link.faux-hover").length) {
              $(".main_nav_wrapper").stop().animate({ marginBottom: '0px'}, 900);
          }
      }); 
      
       $(document.body).on('click', '.desktop-nav-link.no-hover > a' ,function() {
          // cache selectors to improve efficency a tiny tiny bit
          $parentOfThis = $(this).parent();
          $megamenuOuterWrap = $(".megamenu-outer-wrap");
          // button 
          $parentOfThis.removeClass("no-hover").addClass("faux-hover");
          //$('.desktop-nav-link.faux-hover').removeClass("faux-hover").addClass("no-hover");
          $parentOfThis.siblings().removeClass("faux-hover").addClass("no-hover");
          
          // content 
          $parentOfThis.siblings().find($megamenuOuterWrap).stop().css('z-index','1000').slideUp(900);
          $parentOfThis.find($megamenuOuterWrap).stop().css('z-index','1001').slideDown(900);
          
          // move main content down
          $(".main_nav_wrapper").stop().animate({ marginBottom: '590px'}, 600);
      });*/


     $(document.body).on('click', '.desktop-nav-link.faux-hover > a', function () {
         // cache selectors to improve efficency a tiny tiny bit
         $parentOfThis = $(this).parent();
         $megamenuOuterWrap = $(".megamenu-outer-wrap");

         // button 
         $parentOfThis.removeClass("faux-hover").addClass("no-hover");

         $(".desktop-nav-link").not($parentOfThis).removeClass("faux-hover").addClass("no-hover");

         $(".desktop-nav-link").not($parentOfThis).removeClass("faux-hover").addClass("no-hover");

         // content 
         $megamenuOuterWrap.css('z-index', '1000').stop().slideUp(900);

         // move main content down
         if (!$(".desktop-nav-link.faux-hover").length) {
             $(".main_nav_wrapper").stop().animate({ marginBottom: '0px' }, 900);
         }
     });

     $(document.body).on('click', '.desktop-nav-link.no-hover > a', function () {
         // cache selectors to improve efficency a tiny tiny bit
         $parentOfThis = $(this).parent();
         $megamenuOuterWrap = $(".megamenu-outer-wrap");

         // button 
         $parentOfThis.removeClass("no-hover").addClass("faux-hover");

         $(".desktop-nav-link").not($parentOfThis).removeClass("faux-hover").addClass("no-hover");

         // content 
         $(".desktop-nav-link").not($parentOfThis).find($megamenuOuterWrap).stop().css('z-index', '1000').slideUp(900);
         $parentOfThis.find($megamenuOuterWrap).stop().css('z-index', '1001').slideDown(900);

         // move main content down
         $(".main_nav_wrapper").stop().animate({ marginBottom: '590px' }, 600);
     });

     if ($(document.body).on('click', '.find-location-wrap .desktop-nav-link.no-hover > a', function () {
         // alert('help');
     })
         )

         //********************************/
         //* FANCYFORM INIT  */	
         //* SECTION: MULTIPLE			*/	
         //*******************************/

         var customSelect = $(".custom-select").length;

     if (customSelect !== 0) {
         $(".custom-select").transformSelect();
     }


     //*************************************************************************
     //* Equal Heights: http://codepen.io/micahgodbolt/pen/FgqLc								 
     //*************************************************************************
     //alert($(window).width())//

     //if ($(window).width() < 760) {
     // alert('Less than 960');

     //$('.col-height-equal').css("height","100% !important");
     //}
     //else {



     equalheight = function (container) {
         var currentTallest = 0,
           currentRowStart = 0,
           rowDivs = new Array(),
           $el,
           topPosition = 0;
         $(container).each(function () {

             $el = $(this);
             $($el).height('auto');
             topPosition = $el.position().top;

             if (currentRowStart != topPosition) {
                 for (currentDiv = 0 ; currentDiv < rowDivs.length ; currentDiv++) {
                     rowDivs[currentDiv].height(currentTallest);
                 }
                 rowDivs.length = 0; // empty the array
                 currentRowStart = topPosition;
                 currentTallest = $el.height();
                 rowDivs.push($el);
             } else {
                 rowDivs.push($el);
                 currentTallest = (currentTallest < $el.height()) ? ($el.height()) : (currentTallest);
             }

             for (currentDiv = 0 ; currentDiv < rowDivs.length ; currentDiv++) {
                 rowDivs[currentDiv].height(currentTallest);
             }
         });
     };

     $(window).load(function () {
         equalheight('.col-height-equal');
     });

     $(window).resize(function () {
         equalheight('.col-height-equal');
     });

     //Register page - resizes height of column divs


     var oldHeight = $('.guest_register_saf').height();



     $(document).on('change', '.guest_register_saf', function () {
         // $('.guest_register_saf').on('resize change', function() {
         // console.log( $('.guest_register_saf').outerHeight() );

         $('.send_file_content .send_file_main').css('background', '#1084DB');

         var newHeight = $('.guest_register_saf').height();

         var adjustedHeight = newHeight - oldHeight;

         $('.send_file_content .col-height-equal').height($('.send_file_content .col-height-equal').height() + adjustedHeight);

         $('.guest_register_saf').css('height', 'auto');

         oldHeight = $('.guest_register_saf').height();


     });




     //end document
 });

/*scripts.js*/

 // JavaScript Document


 //*Fix for iPhone orientation bug
 //Source: http://www.blog.highub.com/mobile-2/a-fix-for-iphone-viewport-scale-bug/
 //*/
 var metas = document.getElementsByTagName('meta');
 var i;

 if (navigator.userAgent.match(/iPhone/i)) {
     for (i = 0; i < metas.length; i++) {
         if (metas[i].name == "viewport") {
             metas[i].content = "width=device-width, minimum-scale=1.0, maximum-scale=1.0";
         }
     }
     document.addEventListener("gesturestart", gestureStart, false);
 }
 function gestureStart() {
     for (i = 0; i < metas.length; i++) {
         if (metas[i].name == "viewport") {
             metas[i].content = "width=device-width, minimum-scale=0.25, maximum-scale=1.6";
         }
     }
 }

 $(document).ready(function () {

     //Get Inner Width
     var w = $(window).innerWidth();

     //Get Inner Width after screen resize
     $(window).resize(function () {
         w = $(window).innerWidth();
     });

     function winWidth() {
         //Get Inner Width
         var ww = $(window).innerWidth();

         //Get Inner Width after screen resize
         $(window).resize(function () {
             ww = $(window).innerWidth();
         });

         return ww;
     }


     //*******************************************************************************/
     //* SUBPAGE SUBNAVIGATION - Add active class to subnav items (TEMPORARY)*/
     //* SECTION: SUB NAVIGATION												*/
     //*******************************************************************************/

     //var page_id = $(".site_container").attr("data-id");

     var page_id = $(".site_container").attr('class').split(' ')[1];


     $("#sub_navigation .menu-items-block ul li").each(function (i) {

         var list_id = $(this).attr("class").split("-link");

         //compare id in the .site_container to the li class
         if (page_id == list_id[0]) {

             $("li." + list_id[0] + "-link").addClass('active');
         }

     });


     ////get the page title from the nav link
     var page_title = $("#sub_navigation .menu-items-block ul li.active a").html();

     $("#mobile-nav-header #page-title").html(page_title);

     $("#mobile-nav-header").click(function (e) {
         e.preventDefault();
         $('.sub_navigation_wrapper').toggleClass('sub_visible');

     });

     //products & services
     if (page_id == "products-services") {

         $("#mobile-nav-header #page-title").html("All Products &amp; Services");

     }






     //********************************************************/
     //* FLEXSLIDER INIT - PAGE HEADER SLIDERS */
     //* SECTIONS: HOME (NATIONAL & LOCAL) & JOIN OUR TEAM	*/			
     //********************************************************/

     //* Flexslider Init for main home page slider */
     $(".main_rotator .flexslider").flexslider({
         animation: "slide",
         slideshow: true
     });


     //********************************/
     //* BXSLIDER INIT  */
     //* SECTION: MULTIPLE			*/	
     //*******************************/

     //*Set the maximum number of slides shown based on the screen width*/
     var maxSlides;

     function setMaxSlides() {
         //var w = $(window).innerWidth();

         if (w < 465) {
             maxSlides = 1;
         } else {
             maxSlides = 5;
         }
     }

     setMaxSlides();

     $(window).resize(setMaxSlides);



     //Products & Services slider	
     $(".products_services ul").bxSlider({
         pager: false,
         slideWidth: 380,
         minSlides: 1,
         maxSlides: 3,
         moveSlides: 1
     });


     //Case study slider
     $(".case_studies_section ul").bxSlider({
         pager: false,
         slideWidth: 380,
         minSlides: 1,
         maxSlides: 3,
         moveSlides: 1
     });

     //Our Portolio slider
     $(".our_portfolio ul").bxSlider({
         pager: false,
         slideWidth: 380,
         minSlides: 1,
         maxSlides: 3,
         moveSlides: 1
     });

     //Awards Slider - About Us Landing Page
     $(".awards_slider ul").bxSlider({
         pager: false,
         slideWidth: 200,
         slideMargin: 15,
         minSlides: 1,
         maxSlides: 5,
         moveSlides: 1

     });

     //Job Profiles Slider - Join Our Team Landing Page
     $("#job_profile_slider ul").bxSlider({
         pager: false,
         slideWidth: 390,
         slideMargin: (w < 610) ? 0 : 10,
         //minSlides: 1,
         //maxSlides: 3,
         maxSlides: (w < 610) ? 1 : 3,
         moveSlides: 1,
         responsive: true
     });

     //Products & Services Landing page Top Slider
     $("#products-services .products_services_top_page_slider ul").bxSlider({
         pager: false,
         slideWidth: 150,
         slideMargin: 21,
         minSlides: 1,
         maxSlides: 7,
         moveSlides: 1
     });

     // Products & Services Category and Details Top Slider	
     $(".products_category_top_page_slider ul").bxSlider({
         pager: false,
         slideWidth: 82,
         slideMargin: 3,
         minSlides: 1,
         maxSlides: 14,
         moveSlides: 1
     });

     // Products & Services Category Inner (Content) Slider
     $(".products_category_inner_slider .flexslider").flexslider({
         animation: "slide",
         slideshow: false,
         directionNav: false,
         itemWidth: 578,
         maxItems: 1,
         itemMargin: 0
     });




     //************************************************************************/
     //* PARTNERS (NATIONAL SITE) - DISPLAY DETAILS							*/	
     //* SECTION: ABOUT US													*/	
     //* PAGE: partners.html											*/
     //************************************************************************/

     //*Display partner details */
     $('.partners .partner_logo a').click(function (e) {

         e.preventDefault();

         $(this).parent().siblings().removeClass('active'); // remove active class (down arrow) from logo div in same row only

         var p_id = $(this).attr('id'); //get the id from the clicked logo
         var row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .parter_row div
         row = row.split('_'); // split the row id string at the underscore
         var row_id = row[1]; // extract the number from the id string

         $(this).parent().addClass('active'); // display white pointer arrow at the bottom of the logo

         //if device width is greater than ~420px display the details box below the row of logos

         $(window).resize(function () {
             w = $(window).innerWidth();
         });

         if (w > 640) {

             //open the details box located beneath the partner logo's row
             $('#detail_box_' + row_id).slideDown(function () {

                 var content_container = $(this).find('.detail_content');

                 content_container.html(''); //clear previous content

                 $('#' + p_id + '_detail').clone().appendTo(content_container); //copy the hidden partner_detail div to the details box

                 $(this).find('#' + p_id + '_detail').show(); //display the hidden partner_detail div
             });
         }
         else {

             $('.partner_detail').hide(); //hide other partner's detail

             $('#' + p_id + '_detail').show();

         }

         //Close partner details box
         $('#partners_container .partners_detail_wrapper a.close_button').click(function () {

             var d_row = $(this).parent().parent().attr('id'); //get the id from the parent .partners_detail_row div
             d_row = d_row.split('detail_box_'); // get the # from the id string

             $(this).parent().parent().slideUp();
             $('.partner_row#row_' + d_row[1] + ' .partner_logo').removeClass('active'); // remove active class (down arrow) from logo divs on the row //where the box is closed only
         });

         //Close partner details box
         $('#partners_container .partner_logo a.close_button').click(function () {

             $('partner_logo').removeClass('active');
             //var d_row = $(this).parent().parent().attr('id'); //get the id from the parent .partners_detail_row div
             //d_row = d_row.split('detail_box_'); // get the # from the id string

             //$(this).parent().parent().slideUp();
             //$('.partner_row#row_'+d_row[1]+' .partner_logo').removeClass('active'); // remove active class (down arrow) from logo divs on the row //where the box is closed only
         });

     });

     //************************************************************************/

     //*Display partner details */
     $('.mgmt-team .cs_container a').click(function (e) {

         e.preventDefault();

         $(this).parent().siblings().removeClass('active'); // remove active class (down arrow) from logo div in same row only

         var p_id = $(this).attr('id'); //get the id from the clicked logo
         var row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .parter_row div
         row = row.split('_'); // split the row id string at the underscore
         var row_id = row[1]; // extract the number from the id string

         $(this).parent().addClass('active'); // display white pointer arrow at the bottom of the logo



         $(window).resize(function () {
             w = $(window).innerWidth();
         });



         if (w > 465) {

             //open the details box located beneath the partner logo's row
             $('#profile_detail_box_' + row_id).slideDown(function () {

                 var content_container = $(this).find('.profile_detail_content');

                 content_container.html(''); //clear previous content

                 $('#' + p_id + '_detail').clone().appendTo(content_container); //copy the hidden partner_detail div to the details box

                 $(this).find('#' + p_id + '_detail').show(); //display the hidden partner_detail div
             });
         }
         else {

             $('.profile_detail').hide(); //hide other partner's detail

             $('#' + p_id + '_detail').show();



         }



         //Close partner details box
         $('a.close_button').click(function () {

             var d_row = $(this).parent().parent().attr('id'); //get the id from the parent .partners_detail_row div
             d_row = d_row.split('profile_detail_box_'); // get the # from the id string

             $(this).parent().parent().slideUp();
             $('.mgmt_profile_row#row_' + d_row[1] + ' .cs_container').removeClass('active'); // remove active class (down arrow) from logo divs on the row //where the box is closed only
         });
     });

     if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
         // some code..
         $('#mgmt_team_main .cs_image_content_wrapper').css('display', 'block');

     }

     //*****************************************************************************************************/
     //* PRODUCTS & SERVICES - Inititalize content and thumbnail sliders in each product description area  */	
     //* SECTION: PRODUCTS & SERVICES																		 */	
     //* PAGE: products_services.html																		 */
     //*****************************************************************************************************/
     //* Loop through each instance of the sliders and apply the flexslider settings then sync the top and bottom sliders*/
     //$(".product-detail-wrapper").each(function (i) {

     //    var sectionID = $(this).attr("id");
     //    var topSlider = "#" + sectionID + "-large-slider";
     //    var bottomSlider = "#" + sectionID + "-thumb-slider";



     //    // The slider being synced (thumb slider) must be initialized first
     //    $(bottomSlider).flexslider({
     //        animation: "slide",
     //        controlNav: false,
     //        animationLoop: false,
     //        slideshow: false,
     //        itemWidth: 107,
     //        itemMargin: 10,
     //        maxItems: 5,
     //        move: 1,
     //        asNavFor: topSlider
     //    });

     //    $(topSlider).flexslider({
     //        animation: "slide",
     //        itemWidth: 578,
     //        maxItems: 1,
     //        move: 0,
     //        controlNav: false,
     //        animationLoop: false,
     //        slideshow: false,
     //        sync: bottomSlider
     //    });

     //    //Hide navigation if there are 5 or less thumbs
     //    var tCount = $(bottomSlider + " ul.slides li").length;
     //    if (tCount <= 5) {
     //        $(bottomSlider + " .flex-direction-nav").hide();
     //    }

     //});

     // "Hide" captions on the Product & Services page sliders

     $('.product-detail-slider .large-slider .slides li').append('<a class="hide-caption">Hide</a>');

     $('a.hide-caption').click(function () {

         var caption = $(this).parent().find('.caption');

         caption.toggle();

         if (caption.is(':hidden')) {
             $(this).html('Show Caption').addClass('display-hidden');
         }
         else {
             $(this).html('Hide').removeClass('display-hidden');
         }
     });


     //*******************************************************************************************/
     //* DISPLAY OVERLAY & DETAILS ON CLICK
     //* SECTIONS/PAGES: 
     //	INSIGHTS > CASE STUDIES (NATIONAL): case_studies.html
     //	PORTFOLIO (LOCAL): portfolio.html
     //	ABOUT US > WHY DIFFERENT (LOCAL): why_we_are_different.html (Our Team section)
     //*******************************************************************************************/

     //Case Studies & Portfolio Pages - Hide/Show Content
     $("#case_studies_main .cs_image").hover(function (e) {
         e.preventDefault();
         $(".cs_image_content_wrapper").hide();
         $(this).parent().find(".cs_image_content_wrapper").show();
     });

     //Portfolio Pages - Hide/Show Content
     $(".portfolio_content .cs_image").hover(function (e) {
         e.preventDefault();
         $(".cs_image_content_wrapper").hide();
         $(this).parent().find(".cs_image_content_wrapper").show();
     });

     // About (Local) - Our Team Section - Hide/Show Team member info
     //$(".our_team .cs_image").click(function(e) {
     // e.preventDefault();
     // $(".our_team .cs_image_content_wrapper:not(.our_team .no_photo .cs_image_content_wrapper)").hide();
     // $(this).parent().find(".cs_image_content_wrapper").show();
     //	});


     //**********************************************************/
     //* FANCYBOX (LIGHTBOX) INIT - Display lightbox on click   */	
     //* SECTION: PORTFOLIO & SUBSCRIBE MENU ITEM (LOCAL)	*/	
     //* PAGE: portfolio.html								*/
     //**********************************************************/

     var fancybox = $(".fancybox");

     // Call fancybox on pages with the fancybox class only
     if (fancybox.length > 0) {

         //View Map link
         $('.view-map.fancybox').fancybox({
             'type': 'iframe',
             'afterLoad': function () {
                 $('.fancybox-wrap').attr('id', 'view-map-fancybox'); /*Add ID to .fancybox-skin for custom styling*/
                 this.title = '<h2>' + this.title + '</h2>'; /*Use <a> title attribute as the header */
             },
             'helpers': {
                 'title': {
                     'type': 'inside', //move title above iframe
                     'position': 'top'
                 }
             }
         });

         //Subscribe (Local)
         $("#subscribe_lb, #minimenu #subscribe_lb").fancybox({
             'type': 'inline',
             'autoScale': true,
             'autoDimensions': true,
             'showNavArrows': false,
             'afterLoad': function () {
                 $('.fancybox-wrap').attr('id', 'subscribe-fancybox'); /*Add ID to .fancybox-skin for custom styling*/
             }

         });

         //Portfolio
         $(".portfolio_content .cs_image_content a").fancybox({

             prevEffect: 'none',
             nextEffect: 'none',

             //display content inside cs_image_content divs as text below image
             beforeLoad: function () {
                 var el, id = $(this.element).data('content-id');

                 if (id) {
                     el = $('#' + id);

                     if (el.length) {
                         this.title = el.html();
                     }
                 }
             }
         });
     }

     //********************************************************************************/
     //* EASY RESPONSIVE TABS   */
     //* SECTION: JOIN OUR TEAM > JOB PROFILES	*/	
     //* PAGE: job_profiles.html														*/
     //* PLUGIN INFO: https://github.com/samsono/Easy-Responsive-Tabs-to-Accordion	*/
     //********************************************************************************/

     var tabs_ul = $('.resp-tabs-list').length; //*check for existence of tabs */

     if (tabs_ul !== 0) {
         $(".responsive_tabs").easyResponsiveTabs({
             type: 'default', //Types: default, vertical, accordion           
             width: 'auto', //auto or any custom width
             fit: true   // 100% fits in a container
         });
     }

     var active_tab = $(".resp-tab-active");

     //display quote
     $(".statement-text p#quote_" + active_tab.attr('data-tabname')).show();

     //change button text
     $(".profiles_red_bg .cta-button-text span").html(active_tab.html());

     //update quote and button text on click
     $(".resp-tab-item").click(function () {

         //clear current quote and button text
         $(".statement-text p").hide();
         $(".profiles_red_bg .cta-button-text span").html(" ");

         active_quote = $(this).attr('data-tabname');
         active_button = $(this).html();

         $(".statement-text p#quote_" + active_quote).show();
         $(".profiles_red_bg .cta-button-text span").html(active_button);
     });


     //***********************************************************/
     //* BACK TO PREVIOUS PAGE */
     //* SECTION/PAGES: 
     //	ABOUT US > MANAGEMENT TEAM - management_team.html
     //	JOIN OUR TEAM > JOB DESCRIPTION - job_description.html */
     //***********************************************************/

     $('.cta-button-wrap.back-button.prev-page').click(function (e) {
         e.preventDefault();
         history.go(-1);
     });

     //************************************************************************/
     //* NEWS (ARTICLE) - DISPLAY NEXT ARTICLE TITLE							*/	
     //* SECTION: ABOUT US													*/	
     //* PAGE: news_detail.html												*/
     //************************************************************************/	

     //*detect mobile browsers. Use click event to open on mobile and hover for desktop*/
     if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
         $('#faux-slider a.bx-next').click(function (e) {
             e.preventDefault();
             $('#faux-slider .next_article').toggle();
         });
     }
     else {
         $('#faux-slider a.bx-next, #faux-slider .next_article ').hover(function () {
             $('.next_article').toggle();
         });
     }

     /****************************************/
     /*NEWS ARTICLE SLIDER */
     /*SECTION: ABOUT > NEWS > NEWS DETAILS */
     /****************************************/

     function setText(n, p) {
         var prevText = $('.article_title').eq(p).html(), /*get the topic name from the h3.article_title  in the slide content*/
             nextText = $('.article_title').eq(n).html();

         //Display article title inside of the bx controls
         $('.bx-prev').html(prevText);
         $('.bx-next').html(nextText);
     }

     var totalSlides = $('.news_article_wrapper').length,
         prevNum,
         nextNum,
         hash = (window.location.hash),
         startSlide;

     //get hash value from url to set starting slide
     if (hash) {

         hash = hash.split('#'); //remove '#'

         hash = parseInt(hash[1]); //convert string into integer then subtract below to get starting slide number

         startSlide = (hash <= totalSlides && hash > 0) ? (hash - 1) : 0; //only set startSlide number if the hash value is valid -- less than or equal to total slides AND greater than 0
     }
     else {
         startSlide = 0;
     }


     $('.news_article_slider').bxSlider({
         slideSelector: '.news_article_wrapper',
         nextSelector: '.news-next',
         prevSelector: '.news-prev',
         prevText: ' ',
         nextText: ' ',
         pager: false,
         startSlide: startSlide,

         //set topic numbers on slider load
         onSliderLoad: function ($currentIndex) {
             if (hash) {

                 setText(($currentIndex + 2), ($currentIndex));
             }
             else {
                 setText(($currentIndex + 2), totalSlides);
             }

         },

         //update slide number after slide transistion
         onSlideAfter: function ($slideElement, oldIndex, newIndex) {

             if (oldIndex == 0)
                 prevNum = (totalSlides - 1);
             else
                 prevNum = (oldIndex - 1);

             //if the next slide number = the total number of slides, set the next number to 1				
             if ((newIndex + 1) == totalSlides) {
                 nextNum = 1;
             }
             else {
                 nextNum = newIndex + 2; //add 2 to display next slide number
             }

             setText(nextNum, prevNum);

         }
     });


     //*************************************************************************/
     //* COLUMNIZER: http://welcome.totheinter.net/columnizer-jquery-plugin/ */	
     //* SECTION: FIND LOCATION - DETAIL PAGE									 */	
     //*************************************************************************/

     var cols = $('.columnizer').length;

     if (cols) {

         $('#locations_columns .location_list').addClass('dontsplit');

         $('#locations_columns h4').addClass('dontend');

         $('#locations_columns').columnize({
             buildOnce: false,
             width: 400
         });
     }

     //*************************************************************************/
     //* Products and Services page */	
     //* Adds class to nth child								 */	
     //*************************************************************************/

     $('.product-detail-wrapper:nth-child(2n)').addClass('even');


     //end document  
 });


/*infobox.js*/
 /**
  * @name InfoBox
  * @version 1.1.9 [October 2, 2011]
  * @author Gary Little (inspired by proof-of-concept code from Pamela Fox of Google)
  * @copyright Copyright 2010 Gary Little [gary at luxcentral.com]
  * @fileoverview InfoBox extends the Google Maps JavaScript API V3 <tt>OverlayView</tt> class.
  *  <p>
  *  An InfoBox behaves like a <tt>google.maps.InfoWindow</tt>, but it supports several
  *  additional properties for advanced styling. An InfoBox can also be used as a map label.
  *  <p>
  *  An InfoBox also fires the same events as a <tt>google.maps.InfoWindow</tt>.
  *  <p>
  *  Browsers tested:
  *  <p>
  *  Mac -- Safari (4.0.4), Firefox (3.6), Opera (10.10), Chrome (4.0.249.43), OmniWeb (5.10.1)
  *  <br>
  *  Win -- Safari, Firefox, Opera, Chrome (3.0.195.38), Internet Explorer (8.0.6001.18702)
  *  <br>
  *  iPod Touch/iPhone -- Safari (3.1.2)
  */

 /*!
  *
  * Licensed under the Apache License, Version 2.0 (the "License");
  * you may not use this file except in compliance with the License.
  * You may obtain a copy of the License at
  *
  *       http://www.apache.org/licenses/LICENSE-2.0
  *
  * Unless required by applicable law or agreed to in writing, software
  * distributed under the License is distributed on an "AS IS" BASIS,
  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  * See the License for the specific language governing permissions and
  * limitations under the License.
  */

 /*jslint browser:true */
 /*global google */

 /**
  * @name InfoBoxOptions
  * @class This class represents the optional parameter passed to the {@link InfoBox} constructor.
  * @property {string|Node} content The content of the InfoBox (plain text or an HTML DOM node).
  * @property {boolean} disableAutoPan Disable auto-pan on <tt>open</tt> (default is <tt>false</tt>).
  * @property {number} maxWidth The maximum width (in pixels) of the InfoBox. Set to 0 if no maximum.
  * @property {Size} pixelOffset The offset (in pixels) from the top left corner of the InfoBox
  *  (or the bottom left corner if the <code>alignBottom</code> property is <code>true</code>)
  *  to the map pixel corresponding to <tt>position</tt>.
  * @property {LatLng} position The geographic location at which to display the InfoBox.
  * @property {number} zIndex The CSS z-index style value for the InfoBox.
  *  Note: This value overrides a zIndex setting specified in the <tt>boxStyle</tt> property.
  * @property {string} boxClass The name of the CSS class defining the styles for the InfoBox container.
  *  The default name is <code>infoBox</code>.
  * @property {Object} [boxStyle] An object literal whose properties define specific CSS
  *  style values to be applied to the InfoBox. Style values defined here override those that may
  *  be defined in the <code>boxClass</code> style sheet. If this property is changed after the
  *  InfoBox has been created, all previously set styles (except those defined in the style sheet)
  *  are removed from the InfoBox before the new style values are applied.
  * @property {string} closeBoxMargin The CSS margin style value for the close box.
  *  The default is "2px" (a 2-pixel margin on all sides).
  * @property {string} closeBoxURL The URL of the image representing the close box.
  *  Note: The default is the URL for Google's standard close box.
  *  Set this property to "" if no close box is required.
  * @property {Size} infoBoxClearance Minimum offset (in pixels) from the InfoBox to the
  *  map edge after an auto-pan.
  * @property {boolean} isHidden Hide the InfoBox on <tt>open</tt> (default is <tt>false</tt>).
  * @property {boolean} alignBottom Align the bottom left corner of the InfoBox to the <code>position</code>
  *  location (default is <tt>false</tt> which means that the top left corner of the InfoBox is aligned).
  * @property {string} pane The pane where the InfoBox is to appear (default is "floatPane").
  *  Set the pane to "mapPane" if the InfoBox is being used as a map label.
  *  Valid pane names are the property names for the <tt>google.maps.MapPanes</tt> object.
  * @property {boolean} enableEventPropagation Propagate mousedown, click, dblclick,
  *  and contextmenu events in the InfoBox (default is <tt>false</tt> to mimic the behavior
  *  of a <tt>google.maps.InfoWindow</tt>). Set this property to <tt>true</tt> if the InfoBox
  *  is being used as a map label. iPhone note: This property setting has no effect; events are
  *  always propagated.
  */

 /**
  * Creates an InfoBox with the options specified in {@link InfoBoxOptions}.
  *  Call <tt>InfoBox.open</tt> to add the box to the map.
  * @constructor
  * @param {InfoBoxOptions} [opt_opts]
  */
 function InfoBox(opt_opts) {

     opt_opts = opt_opts || {};

     google.maps.OverlayView.apply(this, arguments);

     // Standard options (in common with google.maps.InfoWindow):
     //
     this.content_ = opt_opts.content || "";
     this.disableAutoPan_ = opt_opts.disableAutoPan || false;
     this.maxWidth_ = opt_opts.maxWidth || 0;
     this.pixelOffset_ = opt_opts.pixelOffset || new google.maps.Size(0, 0);
     this.position_ = opt_opts.position || new google.maps.LatLng(0, 0);
     this.zIndex_ = opt_opts.zIndex || null;

     // Additional options (unique to InfoBox):
     //
     this.boxClass_ = opt_opts.boxClass || "infoBox";
     this.boxStyle_ = opt_opts.boxStyle || {};
     this.closeBoxMargin_ = opt_opts.closeBoxMargin || "2px";
     this.closeBoxURL_ = opt_opts.closeBoxURL || "http://www.google.com/intl/en_us/mapfiles/close.gif";
     if (opt_opts.closeBoxURL === "") {
         this.closeBoxURL_ = "";
     }
     this.infoBoxClearance_ = opt_opts.infoBoxClearance || new google.maps.Size(1, 1);
     this.isHidden_ = opt_opts.isHidden || false;
     this.alignBottom_ = opt_opts.alignBottom || false;
     this.pane_ = opt_opts.pane || "floatPane";
     this.enableEventPropagation_ = opt_opts.enableEventPropagation || false;

     this.div_ = null;
     this.closeListener_ = null;
     this.eventListener1_ = null;
     this.eventListener2_ = null;
     this.eventListener3_ = null;
     this.moveListener_ = null;
     this.contextListener_ = null;
     this.fixedWidthSet_ = null;
 }

 /* InfoBox extends OverlayView in the Google Maps API v3.
  */
 InfoBox.prototype = new google.maps.OverlayView();

 /**
  * Creates the DIV representing the InfoBox.
  * @private
  */
 InfoBox.prototype.createInfoBoxDiv_ = function () {

     var bw;
     var me = this;

     // This handler prevents an event in the InfoBox from being passed on to the map.
     //
     var cancelHandler = function (e) {
         e.cancelBubble = true;

         if (e.stopPropagation) {

             e.stopPropagation();
         }
     };

     // This handler ignores the current event in the InfoBox and conditionally prevents
     // the event from being passed on to the map. It is used for the contextmenu event.
     //
     var ignoreHandler = function (e) {

         e.returnValue = false;

         if (e.preventDefault) {

             e.preventDefault();
         }

         if (!me.enableEventPropagation_) {

             cancelHandler(e);
         }
     };

     if (!this.div_) {

         this.div_ = document.createElement("div");

         this.setBoxStyle_();

         if (typeof this.content_.nodeType === "undefined") {
             this.div_.innerHTML = this.getCloseBoxImg_() + this.content_;
         } else {
             this.div_.innerHTML = this.getCloseBoxImg_();
             this.div_.appendChild(this.content_);
         }

         // Add the InfoBox DIV to the DOM
         this.getPanes()[this.pane_].appendChild(this.div_);

         this.addClickHandler_();

         if (this.div_.style.width) {

             this.fixedWidthSet_ = true;

         } else {

             if (this.maxWidth_ !== 0 && this.div_.offsetWidth > this.maxWidth_) {

                 this.div_.style.width = this.maxWidth_;
                 this.div_.style.overflow = "auto";
                 this.fixedWidthSet_ = true;

             } else { // The following code is needed to overcome problems with MSIE

                 bw = this.getBoxWidths_();

                 this.div_.style.width = (this.div_.offsetWidth - bw.left - bw.right) + "px";
                 this.fixedWidthSet_ = false;
             }
         }

         this.panBox_(this.disableAutoPan_);

         if (!this.enableEventPropagation_) {

             // Cancel event propagation.
             //
             this.eventListener1_ = google.maps.event.addDomListener(this.div_, "mousedown", cancelHandler);
             this.eventListener2_ = google.maps.event.addDomListener(this.div_, "click", cancelHandler);
             this.eventListener3_ = google.maps.event.addDomListener(this.div_, "dblclick", cancelHandler);
             this.eventListener4_ = google.maps.event.addDomListener(this.div_, "mouseover", function (e) {
                 this.style.cursor = "default";
             });
         }

         this.contextListener_ = google.maps.event.addDomListener(this.div_, "contextmenu", ignoreHandler);

         /**
          * This event is fired when the DIV containing the InfoBox's content is attached to the DOM.
          * @name InfoBox#domready
          * @event
          */
         google.maps.event.trigger(this, "domready");
     }
 };

 /**
  * Returns the HTML <IMG> tag for the close box.
  * @private
  */
 InfoBox.prototype.getCloseBoxImg_ = function () {

     var img = "";

     if (this.closeBoxURL_ !== "") {

         img = "<img";
         img += " src='" + this.closeBoxURL_ + "'";
         img += " align=right"; // Do this because Opera chokes on style='float: right;'
         img += " style='";
         img += " position: relative;"; // Required by MSIE
         img += " cursor: pointer;";
         img += " margin: " + this.closeBoxMargin_ + ";";
         img += "'>";
     }

     return img;
 };

 /**
  * Adds the click handler to the InfoBox close box.
  * @private
  */
 InfoBox.prototype.addClickHandler_ = function () {

     var closeBox;

     if (this.closeBoxURL_ !== "") {

         closeBox = this.div_.firstChild;
         this.closeListener_ = google.maps.event.addDomListener(closeBox, 'click', this.getCloseClickHandler_());

     } else {

         this.closeListener_ = null;
     }
 };

 /**
  * Returns the function to call when the user clicks the close box of an InfoBox.
  * @private
  */
 InfoBox.prototype.getCloseClickHandler_ = function () {

     var me = this;

     return function (e) {

         // 1.0.3 fix: Always prevent propagation of a close box click to the map:
         e.cancelBubble = true;

         if (e.stopPropagation) {

             e.stopPropagation();
         }

         me.close();

         /**
          * This event is fired when the InfoBox's close box is clicked.
          * @name InfoBox#closeclick
          * @event
          */
         google.maps.event.trigger(me, "closeclick");
     };
 };

 /**
  * Pans the map so that the InfoBox appears entirely within the map's visible area.
  * @private
  */
 InfoBox.prototype.panBox_ = function (disablePan) {

     var map;
     var bounds;
     var xOffset = 0, yOffset = 0;

     if (!disablePan) {

         map = this.getMap();

         if (map instanceof google.maps.Map) { // Only pan if attached to map, not panorama

             if (!map.getBounds().contains(this.position_)) {
                 // Marker not in visible area of map, so set center
                 // of map to the marker position first.
                 map.setCenter(this.position_);
             }

             bounds = map.getBounds();

             var mapDiv = map.getDiv();
             var mapWidth = mapDiv.offsetWidth;
             var mapHeight = mapDiv.offsetHeight;
             var iwOffsetX = this.pixelOffset_.width;
             var iwOffsetY = this.pixelOffset_.height;
             var iwWidth = this.div_.offsetWidth;
             var iwHeight = this.div_.offsetHeight;
             var padX = this.infoBoxClearance_.width;
             var padY = this.infoBoxClearance_.height;
             var pixPosition = this.getProjection().fromLatLngToContainerPixel(this.position_);

             if (pixPosition.x < (-iwOffsetX + padX)) {
                 xOffset = pixPosition.x + iwOffsetX - padX;
             } else if ((pixPosition.x + iwWidth + iwOffsetX + padX) > mapWidth) {
                 xOffset = pixPosition.x + iwWidth + iwOffsetX + padX - mapWidth;
             }
             if (this.alignBottom_) {
                 if (pixPosition.y < (-iwOffsetY + padY + iwHeight)) {
                     yOffset = pixPosition.y + iwOffsetY - padY - iwHeight;
                 } else if ((pixPosition.y + iwOffsetY + padY) > mapHeight) {
                     yOffset = pixPosition.y + iwOffsetY + padY - mapHeight;
                 }
             } else {
                 if (pixPosition.y < (-iwOffsetY + padY)) {
                     yOffset = pixPosition.y + iwOffsetY - padY;
                 } else if ((pixPosition.y + iwHeight + iwOffsetY + padY) > mapHeight) {
                     yOffset = pixPosition.y + iwHeight + iwOffsetY + padY - mapHeight;
                 }
             }

             if (!(xOffset === 0 && yOffset === 0)) {

                 // Move the map to the shifted center.
                 //
                 var c = map.getCenter();
                 map.panBy(xOffset, yOffset);
             }
         }
     }
 };

 /**
  * Sets the style of the InfoBox by setting the style sheet and applying
  * other specific styles requested.
  * @private
  */
 InfoBox.prototype.setBoxStyle_ = function () {

     var i, boxStyle;

     if (this.div_) {

         // Apply style values from the style sheet defined in the boxClass parameter:
         this.div_.className = this.boxClass_;

         // Clear existing inline style values:
         this.div_.style.cssText = "";

         // Apply style values defined in the boxStyle parameter:
         boxStyle = this.boxStyle_;
         for (i in boxStyle) {

             if (boxStyle.hasOwnProperty(i)) {

                 this.div_.style[i] = boxStyle[i];
             }
         }

         // Fix up opacity style for benefit of MSIE:
         //
         if (typeof this.div_.style.opacity !== "undefined" && this.div_.style.opacity !== "") {

             this.div_.style.filter = "alpha(opacity=" + (this.div_.style.opacity * 100) + ")";
         }

         // Apply required styles:
         //
         this.div_.style.position = "absolute";
         this.div_.style.visibility = 'hidden';
         if (this.zIndex_ !== null) {

             this.div_.style.zIndex = this.zIndex_;
         }
     }
 };

 /**
  * Get the widths of the borders of the InfoBox.
  * @private
  * @return {Object} widths object (top, bottom left, right)
  */
 InfoBox.prototype.getBoxWidths_ = function () {

     var computedStyle;
     var bw = { top: 0, bottom: 0, left: 0, right: 0 };
     var box = this.div_;

     if (document.defaultView && document.defaultView.getComputedStyle) {

         computedStyle = box.ownerDocument.defaultView.getComputedStyle(box, "");

         if (computedStyle) {

             // The computed styles are always in pixel units (good!)
             bw.top = parseInt(computedStyle.borderTopWidth, 10) || 0;
             bw.bottom = parseInt(computedStyle.borderBottomWidth, 10) || 0;
             bw.left = parseInt(computedStyle.borderLeftWidth, 10) || 0;
             bw.right = parseInt(computedStyle.borderRightWidth, 10) || 0;
         }

     } else if (document.documentElement.currentStyle) { // MSIE

         if (box.currentStyle) {

             // The current styles may not be in pixel units, but assume they are (bad!)
             bw.top = parseInt(box.currentStyle.borderTopWidth, 10) || 0;
             bw.bottom = parseInt(box.currentStyle.borderBottomWidth, 10) || 0;
             bw.left = parseInt(box.currentStyle.borderLeftWidth, 10) || 0;
             bw.right = parseInt(box.currentStyle.borderRightWidth, 10) || 0;
         }
     }

     return bw;
 };

 /**
  * Invoked when <tt>close</tt> is called. Do not call it directly.
  */
 InfoBox.prototype.onRemove = function () {

     if (this.div_) {

         this.div_.parentNode.removeChild(this.div_);
         this.div_ = null;
     }
 };

 /**
  * Draws the InfoBox based on the current map projection and zoom level.
  */
 InfoBox.prototype.draw = function () {

     this.createInfoBoxDiv_();

     var pixPosition = this.getProjection().fromLatLngToDivPixel(this.position_);

     this.div_.style.left = (pixPosition.x + this.pixelOffset_.width) + "px";

     if (this.alignBottom_) {
         this.div_.style.bottom = -(pixPosition.y + this.pixelOffset_.height) + "px";
     } else {
         this.div_.style.top = (pixPosition.y + this.pixelOffset_.height) + "px";
     }

     if (this.isHidden_) {

         this.div_.style.visibility = 'hidden';

     } else {

         this.div_.style.visibility = "visible";
     }
 };

 /**
  * Sets the options for the InfoBox. Note that changes to the <tt>maxWidth</tt>,
  *  <tt>closeBoxMargin</tt>, <tt>closeBoxURL</tt>, and <tt>enableEventPropagation</tt>
  *  properties have no affect until the current InfoBox is <tt>close</tt>d and a new one
  *  is <tt>open</tt>ed.
  * @param {InfoBoxOptions} opt_opts
  */
 InfoBox.prototype.setOptions = function (opt_opts) {
     if (typeof opt_opts.boxClass !== "undefined") { // Must be first

         this.boxClass_ = opt_opts.boxClass;
         this.setBoxStyle_();
     }
     if (typeof opt_opts.boxStyle !== "undefined") { // Must be second

         this.boxStyle_ = opt_opts.boxStyle;
         this.setBoxStyle_();
     }
     if (typeof opt_opts.content !== "undefined") {

         this.setContent(opt_opts.content);
     }
     if (typeof opt_opts.disableAutoPan !== "undefined") {

         this.disableAutoPan_ = opt_opts.disableAutoPan;
     }
     if (typeof opt_opts.maxWidth !== "undefined") {

         this.maxWidth_ = opt_opts.maxWidth;
     }
     if (typeof opt_opts.pixelOffset !== "undefined") {

         this.pixelOffset_ = opt_opts.pixelOffset;
     }
     if (typeof opt_opts.alignBottom !== "undefined") {

         this.alignBottom_ = opt_opts.alignBottom;
     }
     if (typeof opt_opts.position !== "undefined") {

         this.setPosition(opt_opts.position);
     }
     if (typeof opt_opts.zIndex !== "undefined") {

         this.setZIndex(opt_opts.zIndex);
     }
     if (typeof opt_opts.closeBoxMargin !== "undefined") {

         this.closeBoxMargin_ = opt_opts.closeBoxMargin;
     }
     if (typeof opt_opts.closeBoxURL !== "undefined") {

         this.closeBoxURL_ = opt_opts.closeBoxURL;
     }
     if (typeof opt_opts.infoBoxClearance !== "undefined") {

         this.infoBoxClearance_ = opt_opts.infoBoxClearance;
     }
     if (typeof opt_opts.isHidden !== "undefined") {

         this.isHidden_ = opt_opts.isHidden;
     }
     if (typeof opt_opts.enableEventPropagation !== "undefined") {

         this.enableEventPropagation_ = opt_opts.enableEventPropagation;
     }

     if (this.div_) {

         this.draw();
     }
 };

 /**
  * Sets the content of the InfoBox.
  *  The content can be plain text or an HTML DOM node.
  * @param {string|Node} content
  */
 InfoBox.prototype.setContent = function (content) {
     this.content_ = content;

     if (this.div_) {

         if (this.closeListener_) {

             google.maps.event.removeListener(this.closeListener_);
             this.closeListener_ = null;
         }

         // Odd code required to make things work with MSIE.
         //
         if (!this.fixedWidthSet_) {

             this.div_.style.width = "";
         }

         if (typeof content.nodeType === "undefined") {
             this.div_.innerHTML = this.getCloseBoxImg_() + content;
         } else {
             this.div_.innerHTML = this.getCloseBoxImg_();
             this.div_.appendChild(content);
         }

         // Perverse code required to make things work with MSIE.
         // (Ensures the close box does, in fact, float to the right.)
         //
         if (!this.fixedWidthSet_) {
             this.div_.style.width = this.div_.offsetWidth + "px";
             if (typeof content.nodeType === "undefined") {
                 this.div_.innerHTML = this.getCloseBoxImg_() + content;
             } else {
                 this.div_.innerHTML = this.getCloseBoxImg_();
                 this.div_.appendChild(content);
             }
         }

         this.addClickHandler_();
     }

     /**
      * This event is fired when the content of the InfoBox changes.
      * @name InfoBox#content_changed
      * @event
      */
     google.maps.event.trigger(this, "content_changed");
 };

 /**
  * Sets the geographic location of the InfoBox.
  * @param {LatLng} latlng
  */
 InfoBox.prototype.setPosition = function (latlng) {

     this.position_ = latlng;

     if (this.div_) {

         this.draw();
     }

     /**
      * This event is fired when the position of the InfoBox changes.
      * @name InfoBox#position_changed
      * @event
      */
     google.maps.event.trigger(this, "position_changed");
 };

 /**
  * Sets the zIndex style for the InfoBox.
  * @param {number} index
  */
 InfoBox.prototype.setZIndex = function (index) {

     this.zIndex_ = index;

     if (this.div_) {

         this.div_.style.zIndex = index;
     }

     /**
      * This event is fired when the zIndex of the InfoBox changes.
      * @name InfoBox#zindex_changed
      * @event
      */
     google.maps.event.trigger(this, "zindex_changed");
 };

 /**
  * Returns the content of the InfoBox.
  * @returns {string}
  */
 InfoBox.prototype.getContent = function () {

     return this.content_;
 };

 /**
  * Returns the geographic location of the InfoBox.
  * @returns {LatLng}
  */
 InfoBox.prototype.getPosition = function () {

     return this.position_;
 };

 /**
  * Returns the zIndex for the InfoBox.
  * @returns {number}
  */
 InfoBox.prototype.getZIndex = function () {

     return this.zIndex_;
 };

 /**
  * Shows the InfoBox.
  */
 InfoBox.prototype.show = function () {

     this.isHidden_ = false;
     if (this.div_) {
         this.div_.style.visibility = "visible";
     }
 };

 /**
  * Hides the InfoBox.
  */
 InfoBox.prototype.hide = function () {

     this.isHidden_ = true;
     if (this.div_) {
         this.div_.style.visibility = "hidden";
     }
 };

 /**
  * Adds the InfoBox to the specified map or Street View panorama. If <tt>anchor</tt>
  *  (usually a <tt>google.maps.Marker</tt>) is specified, the position
  *  of the InfoBox is set to the position of the <tt>anchor</tt>. If the
  *  anchor is dragged to a new location, the InfoBox moves as well.
  * @param {Map|StreetViewPanorama} map
  * @param {MVCObject} [anchor]
  */
 InfoBox.prototype.open = function (map, anchor) {

     var me = this;

     if (anchor) {

         this.position_ = anchor.getPosition();
         this.moveListener_ = google.maps.event.addListener(anchor, "position_changed", function () {
             me.setPosition(this.getPosition());
         });
     }

     this.setMap(map);

     if (this.div_) {

         this.panBox_();
     }
 };

 /**
  * Removes the InfoBox from the map.
  */
 InfoBox.prototype.close = function () {

     if (this.closeListener_) {

         google.maps.event.removeListener(this.closeListener_);
         this.closeListener_ = null;
     }

     if (this.eventListener1_) {

         google.maps.event.removeListener(this.eventListener1_);
         google.maps.event.removeListener(this.eventListener2_);
         google.maps.event.removeListener(this.eventListener3_);
         google.maps.event.removeListener(this.eventListener4_);
         this.eventListener1_ = null;
         this.eventListener2_ = null;
         this.eventListener3_ = null;
         this.eventListener4_ = null;
     }

     if (this.moveListener_) {

         google.maps.event.removeListener(this.moveListener_);
         this.moveListener_ = null;
     }

     if (this.contextListener_) {

         google.maps.event.removeListener(this.contextListener_);
         this.contextListener_ = null;
     }

     this.setMap(null);
 };

/* commented out bez it has lot of issues */
/*my-location.js*/

 //this map is shown on the local home page, aboutUs, why_we're_different
 $(document).ready(function () {
     var hasMap = $('#location_map').length;
     if (hasMap != 0) {
         var w = $(window).innerWidth();
         function initialize() {
             var myLatLng;
             var loc = $('#location_map').attr('class');
             var lat = $('.hiddenCenterLat').val();
             var long = $('.hiddenCenterLong').val();

             // Map coordinates for the National and local maps. 
             if (loc == 'corporate') {
                 //myLatLng = new google.maps.LatLng(33.565330, -117.664140);
                 myLatLng = new google.maps.LatLng(33.565330, -117.664140);
             }
             else {
                 //myLatLng = new google.maps.LatLng(33.565330, -117.664140);
                 myLatLng = new google.maps.LatLng(lat, long);
             }

             //disable map dragging on mobile devices
             var isDraggable;
             if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
                 isDraggable = false;
             }
             else {
                 isDraggable = true;
             }
             var mapOptions = {
                 center: myLatLng,
                 zoom: 16,
                 mapTypeId: 'Styled',
                 disableDefaultUI: true,
                 scrollwheel: false,
                 draggable: isDraggable
             };
             var styles = [
                 {
                     stylers: [{
                         saturation: -100
                     },
                     {
                         featureType: "road",
                         elementType: "geometry",
                         stylers: [
                           { hue: -45 },
                           { saturation: 100 }
                         ]
                     }
                     ]
                 }];

             var map = new google.maps.Map(document.getElementById("location_map"), mapOptions);
             /*map.panBy(-300, -50);*/ //move center point of the map to the right    		
             var styledMapType = new google.maps.StyledMapType(styles, { name: 'Styled' });
             map.mapTypes.set('Styled', styledMapType);
             /****** Set custom purple marker *****/
             var markerImg = '/images/location-map-marker.png';
             var marker = new google.maps.Marker({
                 position: myLatLng,
                 map: map,
                 icon: markerImg
             });
             marker.setMap(map);

         }
         //google.maps.event.addDomListener(window, 'resize', initialize); //allow map to resize with browser window. Note: Infobox will disappear. Refresh at new browser size to see infoBox.
         google.maps.event.addDomListener(window, 'load', initialize);
     } // end if

 });

/*find-location.js*/

 //this map is shown on National ulitlity nav "Find my location" click

 var locationsArray = [],
 infoBoxContentArray = [],
 markerArray = [],
 markerImg,
 markerImgActive;

 $(document).ready(function () {

     function initialize() {
         var mapProp = {
             center: new google.maps.LatLng(21.699825, 80.664063),
             zoom: 5,
             mapTypeId: google.maps.MapTypeId.ROADMAP
         };
         var map = new google.maps.Map(document.getElementById("find_location_map"), mapProp);
     }

     function findLocationInit(loc) {

         var hasLocationMap = $('#find_location_map').length;
         if (hasLocationMap != 0) {

             $('#find_location_map_ajaxImg').show();
             var myLatLng, mapZoom = 4;
             var panMapX, panMapY = -50;
             if (loc == 'AF') {

                 myLatLng = new google.maps.LatLng(3.575607, 20.414192); // map center
                 mapZoom = 3;
                 panMapX = -200;
                 panMapY = 20;
                 locationsArray = GetLocationsData(loc);
             }


             if (loc == 'AS') {

                 myLatLng = new google.maps.LatLng(21.699825, 80.664063); // map center
                 mapZoom = 5;
                 panMapX = -300;
                 locationsArray = GetLocationsData(loc);
             }

             if (loc == 'EU') {

                 myLatLng = new google.maps.LatLng(48.110850, 11.409180); // map center
                 mapZoom = 5;
                 panMapX = -300;
                 locationsArray = GetLocationsData(loc);
             }

             if (loc == 'SA') {

                 myLatLng = new google.maps.LatLng(-20.957432, -54.083632); // map center
                 panMapX = -300;
                 locationsArray = GetLocationsData(loc);
             }

             if (loc == 'NA') {
                 myLatLng = new google.maps.LatLng(39.625702, -98.125937); // map center 
                 panMapX = -150;
                 locationsArray = GetLocationsData(loc);
             }

             //disable map dragging on mobile devices
             var isDraggable;

             if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
                 isDraggable = false;
             }
             else {
                 isDraggable = true;
             }

             var mapOptions = {
                 center: myLatLng,
                 zoom: mapZoom,
                 mapTypeId: 'Styled',
                 disableDefaultUI: true,
                 scrollwheel: false,
                 draggable: isDraggable,
                 zoomControl: true,
                 zoomControlOptions: {
                     style: google.maps.ZoomControlStyle.DEFAULT,
                     position: google.maps.ControlPosition.RIGHT_BOTTOM
                 },
             };

             var styles = [
                 //{
                 //    "featureType": "water",
                 //    "elementType": "all",
                 //    "stylers": [
                 //        {
                 //            "color": "#651b64"
                 //        },
                 //        {
                 //            "visibility": "on"
                 //        }
                 //    ]
                 //},
                 //{
                 //    "featureType": "road",
                 //    "stylers": [
                 //        {
                 //            "visibility": "off"
                 //        }
                 //    ]
                 //},
                 //{
                 //    "featureType": "transit",
                 //    "stylers": [
                 //        {
                 //            "visibility": "off"
                 //        }
                 //    ]
                 //},
                 //{
                 //    "featureType": "administrative",
                 //    "stylers": [
                 //        {
                 //            "visibility": "off"
                 //        }
                 //    ]
                 //},
                 //{
                 //    "featureType": "landscape",
                 //    "elementType": "all",
                 //    "stylers": [
                 //        {
                 //            "color": "#420a48"
                 //        }
                 //    ]
                 //},
                 //{
                 //    "featureType": "poi",
                 //    "stylers": [
                 //        {
                 //            "color": "#420a48"
                 //        }
                 //    ]
                 //},
                 //{
                 //    "elementType": "labels",
                 //    "stylers": [
                 //        {
                 //            "visibility": "off"
                 //        }
                 //    ]
                 //}
             ];


             var map = new google.maps.Map(document.getElementById("find_location_map"), mapOptions);
             map.panBy(panMapX, panMapY);
             var styledMapType = new google.maps.StyledMapType(styles, { name: 'Styled' });

             map.mapTypes.set('Styled', styledMapType);
             var marker, infoBoxContent;

             /****** Set custom orange marker *****/
             markerImg = '/images/location-map-marker-orange.png';
             markerImgActive = '/location-map-marker-orange.png';

             /***** INFOBOX 
                 The js/infobox.js file is required for the custom infoBox to work.
             *****/
             var infoBoxOptions = {
                 boxClass: 'find_location_infobox',
                 pixelOffset: new google.maps.Size(20, -75), // float infoBox left
                 //closeBoxURL: 'images/close_x_white_purple_bg.png'
             };

             var infoBox = new InfoBox(infoBoxOptions);

             //Loop through test locations array to place markers and build infoBox content
             if (locationsArray) {
                 for (var i = 0; i < locationsArray.length; i++) {
                     var data = locationsArray[i];
                     var pos = new google.maps.LatLng(data.Latitude, data.Longitude);
                     marker = new google.maps.Marker({
                         position: pos,
                         map: map,
                         icon: markerImg
                     });

                     markerArray.push(marker);
                     marker.setMap(map);
                     var fullUrl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');

                     infoBoxContent = '<div class="infobox_content">';
                     infoBoxContent += '<h3>' + data.CenterName + '</h3>';
                     infoBoxContent += '<p>' + data.Address1 + '<br>';
                     infoBoxContent += data.City + ', ' + data.State + ' ' + data.Zipcode + '<br />' + data.PhoneNumber + '</p>';
                     infoBoxContent += '<p><a href="' + fullUrl + '/' + data.FransId + '/"><img src="/images/locations/btn_choose_location.png" alt="Choose Location"/></a>';
                     infoBoxContent += '</div>';

                     infoBoxContentArray.push(infoBoxContent); //add infobox content to array to be called later in the marker click function
                     google.maps.event.addListener(marker, 'click', (function (marker, i) {
                         return function () {
                             resetMarkers();
                             infoBox.setContent(infoBoxContentArray[i]);
                             infoBox.open(map, marker);
                             marker.setIcon(markerImgActive);
                         }
                     })(marker, i));

                 }
             }// end loop

             //reset custom markers when infoBox is closed
             google.maps.event.addListener(infoBox, 'closeclick', resetMarkers);
             $('#find_location_map_ajaxImg').hide();
         }

     } // end findLocationInit function

     //allow map to resize with browser window. 
     google.maps.event.addDomListener(window, 'resize', findLocationInit);

     //var country_id = 'NA'; //default country
     var continentCode = 'NA'; //default continent

     //Change markers  from active to default
     function resetMarkers() {
         for (var m = 0; m < markerArray.length; m++) {
             markerArray[m].setIcon(markerImg);
         }
     }

     function loadMap() {
         setTimeout(function () {
             if ($('#find_location_map').length > 0) {
                 findLocationInit(continentCode);
             }
         }, 1500);

         $('#find_location_map').fadeIn();
     }

     //Display default map when Find Location button is clicked
     $('#find_location_link').click(loadMap);

     //Change map location when map menu links are clicked
     //    $('a.fl-menu-link').click(function (e) {
     //        e.preventDefault();
     //        //must clear infobox content when selecting another country
     //        infoBoxContentArray = [];
     //        $(this).parent().addClass('active').siblings().removeClass('active');
     //        var country_id = $(this).attr('id');
     //        if (country_id) {
     //            continentCode = getContinentCode(country_id);
     //            loadMap(continentCode);
     //        }
     //    });

     //home page world maps click action
     $('#worldMap a').click(function () {
         var region_id = $(this).attr('id');
         $('html,body').animate({ scrollTop: 0 }, 'slow', function () {
             if (region_id) {
                 continentCode = getContinentCode(region_id);
                 $('.utility_nav_wrapper').addClass('open');
                 $('.close_utility_btn').show();
                 $('.utility_nav_left li').removeClass('active');
                 $('#search_social_close_wrapper').delay(1500).addClass('find_location');
                 $('#find_location').show();
                 loadMap(continentCode);
                 MakeContinentTabActive(continentCode);
             }
         });
     });


     //footer find location click action
     $('#footerFindLocation').click(function () {
         $('html,body').animate({ scrollTop: 0 }, 'slow', function () {
             var continentCode = 'NA'; //default
             $('.utility_nav_wrapper').addClass('open');
             $('.close_utility_btn').show();
             $('.utility_nav_left li').removeClass('active');
             $('#search_social_close_wrapper').delay(1500).addClass('find_location');
             $('#find_location').show();
             loadMap(continentCode);
             MakeContinentTabActive(continentCode);
         });
     });


     function GetLocationsData(cCode) {
         var data = '';
         $.ajax({
             type: "POST",
             url: "/Handlers/GetCenterLocations.ashx",
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: "{'continentCode':'" + cCode + "'}",
             async: false,
             cache: false,
             success: function (response) {
                 //console.log(response);
                 data = response;
             },
             error: function (response, status, error) {
                 //console.log(error);
             }
         });
         return data;
     }

     function getContinentCode(country_id) {
         var code;
         if (country_id == 'fl-north-america' || country_id == 'north-america') {
             code = 'NA';
         }
         else if (country_id == 'fl-south-america' || country_id == 'south-america') {
             code = 'SA';
         }
         else if (country_id == 'fl-europe' || country_id == 'europe') {
             code = 'EU';
         }
         else if (country_id == 'fl-africa' || country_id == 'africa') {
             code = 'AF';
         }
         else if (country_id == 'fl-asia' || country_id == 'asia') {
             code = 'AS';
         }
         else {
             code = 'NA';
         }
         return code;
     }


     function MakeContinentTabActive(code) {
         if (code == 'NA') {
             $('#fl-north-america').parent().addClass('active').siblings().removeClass('active');
         }
         else if (code == 'SA') {
             $('#fl-south-america').parent().addClass('active').siblings().removeClass('active');
         }
         else if (code == 'EU') {
             $('#fl-europe').parent().addClass('active').siblings().removeClass('active');
         }
         else if (code == 'AF') {
             $('#fl-africa').parent().addClass('active').siblings().removeClass('active');
         }
         else if (code == 'AS') {
             $('#fl-asia').parent().addClass('active').siblings().removeClass('active');
         }
         else {
             $('#fl-north-america').parent().addClass('active').siblings().removeClass('active');
         }
     }


 });

/*SirSpeedy.js*/

 $(document).ready(function () {

     //global copyright date
     var currentTime = new Date();
     var year = currentTime.getFullYear();
     if ($('#copyRightYear')) {
         $('#copyRightYear').html(year);
     }

     $('a#footerFindLocation').click(function () {
         $('.find-location-wrap').find('.desktop-nav-link').removeClass('no-hover').addClass('faux-hover');
         $('.find_location_link').click();
         $('.megamenu-outer-wrap').show();
     });

 });

/*
     json2.js
     2015-02-25
 
     Public Domain.
 
     NO WARRANTY EXPRESSED OR IMPLIED. USE AT YOUR OWN RISK.
 
     See http://www.JSON.org/js.html
 
 
     This code should be minified before deployment.
     See http://javascript.crockford.com/jsmin.html
 
     USE YOUR OWN COPY. IT IS EXTREMELY UNWISE TO LOAD CODE FROM SERVERS YOU DO
     NOT CONTROL.
 
 
     This file creates a global JSON object containing two methods: stringify
     and parse.
 
         JSON.stringify(value, replacer, space)
             value       any JavaScript value, usually an object or array.
 
             replacer    an optional parameter that determines how object
                         values are stringified for objects. It can be a
                         function or an array of strings.
 
             space       an optional parameter that specifies the indentation
                         of nested structures. If it is omitted, the text will
                         be packed without extra whitespace. If it is a number,
                         it will specify the number of spaces to indent at each
                         level. If it is a string (such as '\t' or '&nbsp;'),
                         it contains the characters used to indent at each level.
 
             This method produces a JSON text from a JavaScript value.
 
             When an object value is found, if the object contains a toJSON
             method, its toJSON method will be called and the result will be
             stringified. A toJSON method does not serialize: it returns the
             value represented by the name/value pair that should be serialized,
             or undefined if nothing should be serialized. The toJSON method
             will be passed the key associated with the value, and this will be
             bound to the value
 
             For example, this would serialize Dates as ISO strings.
 
                 Date.prototype.toJSON = function (key) {
                     function f(n) {
                         // Format integers to have at least two digits.
                         return n < 10 
                         ? '0' + n 
                         : n;
                     }
 
                     return this.getUTCFullYear()   + '-' +
                          f(this.getUTCMonth() + 1) + '-' +
                          f(this.getUTCDate())      + 'T' +
                          f(this.getUTCHours())     + ':' +
                          f(this.getUTCMinutes())   + ':' +
                          f(this.getUTCSeconds())   + 'Z';
                 };
 
             You can provide an optional replacer method. It will be passed the
             key and value of each member, with this bound to the containing
             object. The value that is returned from your method will be
             serialized. If your method returns undefined, then the member will
             be excluded from the serialization.
 
             If the replacer parameter is an array of strings, then it will be
             used to select the members to be serialized. It filters the results
             such that only members with keys listed in the replacer array are
             stringified.
 
             Values that do not have JSON representations, such as undefined or
             functions, will not be serialized. Such values in objects will be
             dropped; in arrays they will be replaced with null. You can use
             a replacer function to replace those with JSON values.
             JSON.stringify(undefined) returns undefined.
 
             The optional space parameter produces a stringification of the
             value that is filled with line breaks and indentation to make it
             easier to read.
 
             If the space parameter is a non-empty string, then that string will
             be used for indentation. If the space parameter is a number, then
             the indentation will be that many spaces.
 
             Example:
 
             text = JSON.stringify(['e', {pluribus: 'unum'}]);
             // text is '["e",{"pluribus":"unum"}]'
 
 
             text = JSON.stringify(['e', {pluribus: 'unum'}], null, '\t');
             // text is '[\n\t"e",\n\t{\n\t\t"pluribus": "unum"\n\t}\n]'
 
             text = JSON.stringify([new Date()], function (key, value) {
                 return this[key] instanceof Date ?
                     'Date(' + this[key] + ')' : value;
             });
             // text is '["Date(---current time---)"]'
 
 
         JSON.parse(text, reviver)
             This method parses a JSON text to produce an object or array.
             It can throw a SyntaxError exception.
 
             The optional reviver parameter is a function that can filter and
             transform the results. It receives each of the keys and values,
             and its return value is used instead of the original value.
             If it returns what it received, then the structure is not modified.
             If it returns undefined then the member is deleted.
 
             Example:
 
             // Parse the text. Values that look like ISO date strings will
             // be converted to Date objects.
 
             myData = JSON.parse(text, function (key, value) {
                 var a;
                 if (typeof value === 'string') {
                     a =
 /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)Z$/.exec(value);
                     if (a) {
                         return new Date(Date.UTC(+a[1], +a[2] - 1, +a[3], +a[4],
                             +a[5], +a[6]));
                     }
                 }
                 return value;
             });
 
             myData = JSON.parse('["Date(09/09/2001)"]', function (key, value) {
                 var d;
                 if (typeof value === 'string' &&
                         value.slice(0, 5) === 'Date(' &&
                         value.slice(-1) === ')') {
                     d = new Date(value.slice(5, -1));
                     if (d) {
                         return d;
                     }
                 }
                 return value;
             });
 
 
     This is a reference implementation. You are free to copy, modify, or
     redistribute.
 */

 /*jslint 
     eval, for, this 
 */

 /*property
     JSON, apply, call, charCodeAt, getUTCDate, getUTCFullYear, getUTCHours,
     getUTCMinutes, getUTCMonth, getUTCSeconds, hasOwnProperty, join,
     lastIndex, length, parse, prototype, push, replace, slice, stringify,
     test, toJSON, toString, valueOf
 */


 // Create a JSON object only if one does not already exist. We create the
 // methods in a closure to avoid creating global variables.

 if (typeof JSON !== 'object') {
     JSON = {};
 }

 (function () {
     'use strict';

     function f(n) {
         // Format integers to have at least two digits.
         return n < 10
         ? '0' + n
         : n;
     }

     function this_value() {
         return this.valueOf();
     }

     if (typeof Date.prototype.toJSON !== 'function') {

         Date.prototype.toJSON = function () {

             return isFinite(this.valueOf())
             ? this.getUTCFullYear() + '-' +
                     f(this.getUTCMonth() + 1) + '-' +
                     f(this.getUTCDate()) + 'T' +
                     f(this.getUTCHours()) + ':' +
                     f(this.getUTCMinutes()) + ':' +
                     f(this.getUTCSeconds()) + 'Z'
             : null;
         };

         Boolean.prototype.toJSON = this_value;
         Number.prototype.toJSON = this_value;
         String.prototype.toJSON = this_value;
     }

     var cx,
         escapable,
         gap,
         indent,
         meta,
         rep;


     function quote(string) {

         // If the string contains no control characters, no quote characters, and no
         // backslash characters, then we can safely slap some quotes around it.
         // Otherwise we must also replace the offending characters with safe escape
         // sequences.

         escapable.lastIndex = 0;
         return escapable.test(string)
         ? '"' + string.replace(escapable, function (a) {
             var c = meta[a];
             return typeof c === 'string'
             ? c
             : '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
         }) + '"'
         : '"' + string + '"';
     }


     function str(key, holder) {

         // Produce a string from holder[key].

         var i,          // The loop counter.
             k,          // The member key.
             v,          // The member value.
             length,
             mind = gap,
             partial,
             value = holder[key];

         // If the value has a toJSON method, call it to obtain a replacement value.

         if (value && typeof value === 'object' &&
                 typeof value.toJSON === 'function') {
             value = value.toJSON(key);
         }

         // If we were called with a replacer function, then call the replacer to
         // obtain a replacement value.

         if (typeof rep === 'function') {
             value = rep.call(holder, key, value);
         }

         // What happens next depends on the value's type.

         switch (typeof value) {
             case 'string':
                 return quote(value);

             case 'number':

                 // JSON numbers must be finite. Encode non-finite numbers as null.

                 return isFinite(value)
                 ? String(value)
                 : 'null';

             case 'boolean':
             case 'null':

                 // If the value is a boolean or null, convert it to a string. Note:
                 // typeof null does not produce 'null'. The case is included here in
                 // the remote chance that this gets fixed someday.

                 return String(value);

                 // If the type is 'object', we might be dealing with an object or an array or
                 // null.

             case 'object':

                 // Due to a specification blunder in ECMAScript, typeof null is 'object',
                 // so watch out for that case.

                 if (!value) {
                     return 'null';
                 }

                 // Make an array to hold the partial results of stringifying this object value.

                 gap += indent;
                 partial = [];

                 // Is the value an array?

                 if (Object.prototype.toString.apply(value) === '[object Array]') {

                     // The value is an array. Stringify every element. Use null as a placeholder
                     // for non-JSON values.

                     length = value.length;
                     for (i = 0; i < length; i += 1) {
                         partial[i] = str(i, value) || 'null';
                     }

                     // Join all of the elements together, separated with commas, and wrap them in
                     // brackets.

                     v = partial.length === 0
                     ? '[]'
                     : gap
                     ? '[\n' + gap + partial.join(',\n' + gap) + '\n' + mind + ']'
                     : '[' + partial.join(',') + ']';
                     gap = mind;
                     return v;
                 }

                 // If the replacer is an array, use it to select the members to be stringified.

                 if (rep && typeof rep === 'object') {
                     length = rep.length;
                     for (i = 0; i < length; i += 1) {
                         if (typeof rep[i] === 'string') {
                             k = rep[i];
                             v = str(k, value);
                             if (v) {
                                 partial.push(quote(k) + (
                                     gap
                                     ? ': '
                                     : ':'
                                 ) + v);
                             }
                         }
                     }
                 } else {

                     // Otherwise, iterate through all of the keys in the object.

                     for (k in value) {
                         if (Object.prototype.hasOwnProperty.call(value, k)) {
                             v = str(k, value);
                             if (v) {
                                 partial.push(quote(k) + (
                                     gap
                                     ? ': '
                                     : ':'
                                 ) + v);
                             }
                         }
                     }
                 }

                 // Join all of the member texts together, separated with commas,
                 // and wrap them in braces.

                 v = partial.length === 0
                 ? '{}'
                 : gap
                 ? '{\n' + gap + partial.join(',\n' + gap) + '\n' + mind + '}'
                 : '{' + partial.join(',') + '}';
                 gap = mind;
                 return v;
         }
     }

     // If the JSON object does not yet have a stringify method, give it one.

     if (typeof JSON.stringify !== 'function') {
         escapable = /[\\\"\u0000-\u001f\u007f-\u009f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g;
         meta = {    // table of character substitutions
             '\b': '\\b',
             '\t': '\\t',
             '\n': '\\n',
             '\f': '\\f',
             '\r': '\\r',
             '"': '\\"',
             '\\': '\\\\'
         };
         JSON.stringify = function (value, replacer, space) {

             // The stringify method takes a value and an optional replacer, and an optional
             // space parameter, and returns a JSON text. The replacer can be a function
             // that can replace values, or an array of strings that will select the keys.
             // A default replacer method can be provided. Use of the space parameter can
             // produce text that is more easily readable.

             var i;
             gap = '';
             indent = '';

             // If the space parameter is a number, make an indent string containing that
             // many spaces.

             if (typeof space === 'number') {
                 for (i = 0; i < space; i += 1) {
                     indent += ' ';
                 }

                 // If the space parameter is a string, it will be used as the indent string.

             } else if (typeof space === 'string') {
                 indent = space;
             }

             // If there is a replacer, it must be a function or an array.
             // Otherwise, throw an error.

             rep = replacer;
             if (replacer && typeof replacer !== 'function' &&
                     (typeof replacer !== 'object' ||
                     typeof replacer.length !== 'number')) {
                 throw new Error('JSON.stringify');
             }

             // Make a fake root object containing our value under the key of ''.
             // Return the result of stringifying the value.

             return str('', { '': value });
         };
     }


     // If the JSON object does not yet have a parse method, give it one.

     if (typeof JSON.parse !== 'function') {
         cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g;
         JSON.parse = function (text, reviver) {

             // The parse method takes a text and an optional reviver function, and returns
             // a JavaScript value if the text is a valid JSON text.

             var j;

             function walk(holder, key) {

                 // The walk method is used to recursively walk the resulting structure so
                 // that modifications can be made.

                 var k, v, value = holder[key];
                 if (value && typeof value === 'object') {
                     for (k in value) {
                         if (Object.prototype.hasOwnProperty.call(value, k)) {
                             v = walk(value, k);
                             if (v !== undefined) {
                                 value[k] = v;
                             } else {
                                 delete value[k];
                             }
                         }
                     }
                 }
                 return reviver.call(holder, key, value);
             }


             // Parsing happens in four stages. In the first stage, we replace certain
             // Unicode characters with escape sequences. JavaScript handles many characters
             // incorrectly, either silently deleting them, or treating them as line endings.

             text = String(text);
             cx.lastIndex = 0;
             if (cx.test(text)) {
                 text = text.replace(cx, function (a) {
                     return '\\u' +
                             ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
                 });
             }

             // In the second stage, we run the text against regular expressions that look
             // for non-JSON patterns. We are especially concerned with '()' and 'new'
             // because they can cause invocation, and '=' because it can cause mutation.
             // But just to be safe, we want to reject all unexpected forms.

             // We split the second stage into 4 regexp operations in order to work around
             // crippling inefficiencies in IE's and Safari's regexp engines. First we
             // replace the JSON backslash pairs with '@' (a non-JSON character). Second, we
             // replace all simple value tokens with ']' characters. Third, we delete all
             // open brackets that follow a colon or comma or that begin the text. Finally,
             // we look to see that the remaining characters are only whitespace or ']' or
             // ',' or ':' or '{' or '}'. If that is so, then the text is safe for eval.

             if (
                 /^[\],:{}\s]*$/.test(
                     text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@')
                         .replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']')
                         .replace(/(?:^|:|,)(?:\s*\[)+/g, '')
                 )
             ) {

                 // In the third stage we use the eval function to compile the text into a
                 // JavaScript structure. The '{' operator is subject to a syntactic ambiguity
                 // in JavaScript: it can begin a block or an object literal. We wrap the text
                 // in parens to eliminate the ambiguity.

                 j = eval('(' + text + ')');

                 // In the optional fourth stage, we recursively walk the new structure, passing
                 // each name/value pair to a reviver function for possible transformation.

                 return typeof reviver === 'function'
                 ? walk({ '': j }, '')
                 : j;
             }

             // If the text is not JSON parseable, then a SyntaxError is thrown.

             throw new SyntaxError('JSON.parse');
         };
     }
 }());
