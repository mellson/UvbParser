module OmrådeParser

open Områder
open UvbTyper
open FagbeskrivelserParser
open FSharp.Data

let parseOmråde (område : Område) = 
    let doc = HtmlDocument.Load(område.Url)
    let nodes = doc.Body().Descendants()

    // Bilag er en liste af id'er
    let bilag = 
        doc.CssSelect(".Bilag span")
        |> Seq.map (fun n -> n.AttributeValue("id"))
        |> Seq.skip (område.StartBilag - 1)
        |> Seq.toList
    
    parseFagbeskrivelser område.Url nodes bilag []
