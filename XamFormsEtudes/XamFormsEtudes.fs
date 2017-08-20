namespace XamFormsEtudes

open Xamarin.Forms


type App () = 
    inherit Application ()
    let stack = StackLayout (VerticalOptions = LayoutOptions.Center, Padding = Thickness (40.0))
    let label = Label (XAlign = TextAlignment.Center, Text = "Main Page")
    let button = Button (Text = "Read File Resource")
    let mainPage = ContentPage (Content = stack)
    let readFilePageVM = ReadFileResource.PageVM ()
    let readFilePage = ReadFileResource.Page (readFilePageVM)
    let loadFilePage _ = do
        mainPage.Navigation.PushAsync (readFilePage) |> ignore
        //ReadFileResource.printAssemblyResources ()
        readFilePageVM.Text <- "Sample.txt" |> ReadFileResource.readFile 
    do
        button.Clicked.Add (loadFilePage)
        stack.Children.Add (label)
        stack.Children.Add (button)
        base.MainPage <- NavigationPage (mainPage)