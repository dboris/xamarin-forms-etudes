namespace XamFormsEtudes

open System
open System.ComponentModel
open Xamarin.Forms


module ReadFileResource =
    type PageVM () =
        let mutable text = "Loading..."
        let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs> ()
        interface INotifyPropertyChanged with
            [<CLIEvent>] member __.PropertyChanged = propertyChanged.Publish

        member self.Text
            with get () = text
            and set value = do
                text <- value
                propertyChanged.Trigger (self, PropertyChangedEventArgs ("Text"))

    type Page (viewModel : PageVM) as self =
        inherit ContentPage ()
        let stack = StackLayout (VerticalOptions = LayoutOptions.Center)
        let label = Label (Text = viewModel.Text)
        do
            self.BindingContext <- viewModel
            label.SetBinding (Label.TextProperty, "Text")
            stack.Children.Add (label)
            base.Content <- stack
