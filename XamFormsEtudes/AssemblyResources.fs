namespace XamFormsEtudes

open System
open System.Reflection
open System.Diagnostics

module AssemblyResources =
    type internal Marker = interface end
    let markerType = typeof<Marker>
    let assembly = (markerType.GetTypeInfo ()).Assembly

    let printAssemblyResources () =
        assembly.GetManifestResourceNames () |> Seq.iter (fun res -> Debug.WriteLine (res))

    let getResourceStream fileName =
        assembly.GetManifestResourceStream (fileName)

    let readFile fileName =
        // Xamarin guide suggests to use namespace qualified file name, but this doesn't work
        // https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/files/
        //let qualifiedName = sprintf "XamFormsEtudes.%s" fileName
        let stream = getResourceStream fileName
        use reader = new IO.StreamReader (stream)
        reader.ReadToEnd ()
