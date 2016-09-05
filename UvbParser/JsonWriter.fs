module JsonWriter

open UvbTyper
open Newtonsoft.Json

// Skriver en liste af fagbeskrivelser til disk som en json fil
let writeJson (fagbeskrivelser : Fagbeskrivelse list) = 
    using (System.IO.File.CreateText(fagbeskrivelser.Head.omraade + ".json")) 
        (fun file1 -> file1.WriteLine(JsonConvert.SerializeObject(fagbeskrivelser, Formatting.Indented)))
