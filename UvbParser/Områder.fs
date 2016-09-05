module Områder

type Område = 
    { Navn : string
      Url : string
      StartBilag : int }

let HTX = 
    { Navn = "HTX"
      Url = "https://www.retsinformation.dk/Forms/R0710.aspx?id=152550"
      StartBilag = 7 }

let HHX = 
    { Navn = "HHX"
      Url = "https://www.retsinformation.dk/Forms/R0710.aspx?id=152537"
      StartBilag = 7 }

let VALG = 
    { Navn = "VALG"
      Url = "https://www.retsinformation.dk/Forms/R0710.aspx?id=132670"
      StartBilag = 2 }

let områder = HTX :: HHX :: VALG :: []