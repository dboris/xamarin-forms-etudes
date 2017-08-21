namespace XamFormsEtudes.Droid

open System
open Xamarin.Forms

open XamFormsEtudes.PlatformServices


module PlatformServices =
    type FileIO () =
        interface IFileIO with
            member __.ReadText fileName =
                let documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal)
                let filePath = IO.Path.Combine (documentsPath, fileName)
                IO.File.ReadAllText (filePath)

            member __.WriteText fileName text =
                let documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal)
                let filePath = IO.Path.Combine (documentsPath, fileName)
                IO.File.WriteAllText (filePath, text)

[<assembly: Dependency (typeof<PlatformServices.FileIO>)>] do ()
