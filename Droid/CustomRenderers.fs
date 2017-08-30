namespace XamFormsEtudes.Droid

open System
open Xamarin.Forms
open Xamarin.Forms.Platform.Android

open XamFormsEtudes.Controls

open Android
open Java.Interop


module CustomRenderers =

    type JSBridge (hybridRenderer : HybridWebViewRenderer) =
        inherit Java.Lang.Object ()
        let hybridWebViewRenderer = new WeakReference<_> (hybridRenderer)

        [<Export ("invokeAction"); Webkit.JavascriptInterface>]
        member __.InvokeAction (data : string) =
            if (not << isNull) <| hybridWebViewRenderer
            then 
                match hybridWebViewRenderer.TryGetTarget () with
                | true, hybridRenderer -> hybridRenderer.Element.InvokeAction data
                | _ -> ()

    and HybridWebViewRenderer () =
        inherit ViewRenderer<HybridWebView, Webkit.WebView> ()

        let mutable userController = null
        let javaScriptFunction = "function invokeFSharpAction (data) {\
                                    jsBridge.invokeAction(data)\
                                  }"

        override self.OnElementChanged (e : ElementChangedEventArgs<HybridWebView>) =
            base.OnElementChanged e

            if isNull self.Control
            then 
                let webView = new Webkit.WebView (Forms.Context)
                webView.Settings.JavaScriptEnabled <- true
                self.SetNativeControl webView

            if (not << isNull) <| box e.OldElement
            then
                self.Control.RemoveJavascriptInterface "jsBridge"
                e.OldElement.Cleanup ()

            if (not << isNull) <| box e.NewElement
            then
                self.Control.AddJavascriptInterface (new JSBridge (self), "jsBridge")
                sprintf "file:///android_asset/%s" self.Element.Uri |> self.Control.LoadUrl
                self.InjectJS javaScriptFunction

        member self.InjectJS (script : string) =
            if (not << isNull) self.Control
            then sprintf "javascript: %s" script |> self.Control.LoadUrl

    [<assembly: ExportRenderer (typeof<HybridWebView>, typeof<HybridWebViewRenderer>)>] do ()


