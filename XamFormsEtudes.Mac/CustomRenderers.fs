namespace XamFormsEtudes.Mac

open System
open Xamarin.Forms
open Xamarin.Forms.Platform.MacOS

open Foundation
open WebKit

open XamFormsEtudes.Controls



module CustomRenderers =
    let log = Diagnostics.Debug.WriteLine

    type HybridWebViewRenderer () =
        inherit ViewRenderer<HybridWebView, WKWebView> ()

        let mutable userController = null
        let javaScriptFunction = "function invokeFSharpAction (data) {\
                                    window.webkit.messageHandlers.invokeAction.postMessage(data)\
                                  }"

        interface IWKScriptMessageHandler with
            member self.DidReceiveScriptMessage (controller, message) =
                let msg = message.Body.ToString ()
                self.Element.InvokeAction msg

        override self.OnElementChanged (e : ElementChangedEventArgs<HybridWebView>) =
            base.OnElementChanged e

            if isNull self.Control
            then 
                let script = new WKUserScript (new NSString (javaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false)
                userController <- new WKUserContentController ()
                userController.AddUserScript script
                userController.AddScriptMessageHandler (self, "invokeAction")
                let config = new WKWebViewConfiguration (UserContentController = userController)
                let webView = new WKWebView (self.Frame, config)
                self.SetNativeControl webView

            if not (isNull <| box e.OldElement)  // e.OldElement <> null
            then
                userController.RemoveAllUserScripts ()
                userController.RemoveScriptMessageHandler "invokeAction"
                e.OldElement.Cleanup ()

            if not (isNull <| box e.NewElement)  // e.NewElement <> null
            then
                let fileName = IO.Path.Combine (NSBundle.MainBundle.BundlePath, "Contents", "Resources", self.Element.Uri)
                let request = new NSUrlRequest (new NSUrl (fileName, false))
                self.Control.LoadRequest request |> ignore

    [<assembly: ExportRenderer (typeof<HybridWebView>, typeof<HybridWebViewRenderer>)>] do ()
