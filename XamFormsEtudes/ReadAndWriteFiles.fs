namespace XamFormsEtudes

open System
open System.ComponentModel
open System.Diagnostics
open Xamarin.Forms

module ReadAndWriteFiles =

    type PageVM (fileName : string) =
        let fileService = PlatformServices.fileService
        let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs> ()
        interface INotifyPropertyChanged with
            [<CLIEvent>] member __.PropertyChanged = propertyChanged.Publish
        
        member val EntryText = "" with get, set
        member val FileText = "" with get, set

        member self.Save _ =
            let text = self.EntryText
            if text <> "" then do
                fileService.WriteText fileName text

        member self.Load _ =
            let text = fileService.ReadText fileName
            if text <> self.FileText then do
                self.FileText <- text
                propertyChanged.Trigger (self, PropertyChangedEventArgs ("FileText"))


    type Page (viewModel : PageVM) as self =
        inherit ContentPage ()
        let stack = StackLayout (VerticalOptions = LayoutOptions.Center)
        let label = Label (Text = "Type below and press Save then Load")
        let entry = Entry ()
        let fileText = Label ()
        let loadButton = Button (Text = "Load")
        let saveButton = Button (Text = "Save")
        do
            self.BindingContext <- viewModel
            entry.SetBinding (Entry.TextProperty, "EntryText")
            fileText.SetBinding (Label.TextProperty, "FileText")
            saveButton.Clicked.Add (viewModel.Save)
            loadButton.Clicked.Add (viewModel.Load)
            stack.Children.Add (label)
            stack.Children.Add (entry)
            stack.Children.Add (saveButton)
            stack.Children.Add (loadButton)
            stack.Children.Add (fileText)
            base.Content <- stack