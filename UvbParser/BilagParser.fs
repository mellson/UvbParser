module BilagParser

open UvbTyper
open InfoParser
open ListeParser
open FSharp.Data

let parseBilag url (noder : HtmlNode seq) =     
    // Byg info om bilaget
    let info = (parseInfo ((noder |> Seq.nth 1).InnerText()) url).Value
    // Byg fagmålslisten ved at kigge på alt indhold mellem "2.1. Faglige mål" -> "2.2. Kernestof"
    let fagmål = parseListe noder "2.1. Faglige mål" "2.2. Kernestof"
    // Byg kernestofslisten ved at kigge på alt indhold mellem "2.2. Kernestof" -> "2.3. Supplerende stof"
    let kernestof = parseListe noder "2.2. Kernestof" "2.3. Supplerende stof"
    // Returner fagbeskrivelsen med det indhold vi har bygget
    { omraade = info.Område
      fag = info.Fag
      niveau = info.Niveau
      version = info.Version
      url = info.Url
      fagmaalList = fagmål
      kernestofList = kernestof }