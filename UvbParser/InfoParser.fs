module InfoParser

open UvbTyper
open System.Globalization

(* Udtrækker informationer om faget fra en string og laver det om til en Info type.
   
   Fx ser inputtet til Biologi sådan ud: "Biologi B – htx, juni 2013"
   Det vil returnere en Info type der ser således ud:
   Info = 
    { Fag : Biologi
      Niveau : B
      Område : htx
      Url : https://www.retsinformation.dk/Forms/R0710.aspx?id=152550#Bil7
      Version : juni 2013 }
*)
let parseInfo (fagInfo : string) (url : string) = 
    let infoHelper (fagNiveau : string) (fagOmråde : string) (områdeVersion : string) = 
        // Specialtilfælde hvor vi ændrer navnet på et fag fordi det står forkert i EASY databasen
        let tempFag = fagNiveau.Substring(0, fagNiveau.Length - 2).Trim()
        let fag = 
            if tempFag = "Idéhistorie" then "Idehistorie"
            else tempFag
        
        let niveau = fagNiveau.Substring(fagNiveau.Length - 1, 1)
        match områdeVersion.Trim().Split ',' with
        | [| område; version |] -> 
            Some { Fag =
                       match fagOmråde.Trim() with
                       | "" -> fag.Trim()
                       | fagOmråde -> fag.Trim() + " " + fagOmråde.ToLower()
                   Niveau = niveau.Trim()
                   Område = område.Trim()
                   Url = url
                   Version = version.Trim() }
        | _ -> None

    match fagInfo.Trim().Split '–' with
    | [| fagNiveau; områdeVersion |] -> infoHelper (fagNiveau.Trim()) "" (områdeVersion.Trim())
    | [| fagNiveau; fagOmråde; områdeVersion |] -> 
        infoHelper (fagNiveau.Trim()) (fagOmråde.Trim()) (områdeVersion.Trim())
    | _ -> None