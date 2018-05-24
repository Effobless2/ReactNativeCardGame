const IMAGESPATH = card => {
    switch (card.value + card.color){
        case "1H" : {return require("../icons/cards/1H.png")}
        case "2H" : {return require("../icons/cards/2H.png")}
        case "3H" : {return require("../icons/cards/3H.png")}
        case "4H" : {return require("../icons/cards/4H.png")}
        case "5H" : {return require("../icons/cards/5H.png")}
        case "6H" : {return require("../icons/cards/6H.png")}
        case "7H" : {return require("../icons/cards/7H.png")}
        case "8H" : {return require("../icons/cards/8H.png")}
        case "9H" : {return require("../icons/cards/9H.png")}
        case "10H" : {return require("../icons/cards/10H.png")}
        case "11H" : {return require("../icons/cards/11H.png")}
        case "12H" : {return require("../icons/cards/12H.png")}
        case "13H" : {return require("../icons/cards/13H.png")}
        case "1S" : {return require("../icons/cards/1S.png")}
        case "2S" : {return require("../icons/cards/2S.png")}
        case "3S" : {return require("../icons/cards/3S.png")}
        case "4S" : {return require("../icons/cards/4S.png")}
        case "5S" : {return require("../icons/cards/5S.png")}
        case "6S" : {return require("../icons/cards/6S.png")}
        case "7S" : {return require("../icons/cards/7S.png")}
        case "8S" : {return require("../icons/cards/8S.png")}
        case "9S" : {return require("../icons/cards/9S.png")}
        case "10S" : {return require("../icons/cards/10S.png")}
        case "11S" : {return require("../icons/cards/11S.png")}
        case "12S" : {return require("../icons/cards/12S.png")}
        case "13S" : {return require("../icons/cards/13S.png")}
        case "1C" : {return require("../icons/cards/1C.png")}
        case "2C" : {return require("../icons/cards/2C.png")}
        case "3C" : {return require("../icons/cards/3C.png")}
        case "4C" : {return require("../icons/cards/4C.png")}
        case "5C" : {return require("../icons/cards/5C.png")}
        case "6C" : {return require("../icons/cards/6C.png")}
        case "7C" : {return require("../icons/cards/7C.png")}
        case "8C" : {return require("../icons/cards/8C.png")}
        case "9C" : {return require("../icons/cards/9C.png")}
        case "10C" : {return require( "../icons/cards/10C.png")}
        case "11C" : {return require( "../icons/cards/11C.png")}
        case "12C" : {return require( "../icons/cards/12C.png")}
        case "13C" : {return require( "../icons/cards/13C.png")}
        case "1D" : {return require( "../icons/cards/1D.png")}
        case "2D" : {return require( "../icons/cards/2D.png")}
        case "3D" : {return require( "../icons/cards/3D.png")}
        case "4D" : {return require( "../icons/cards/4D.png")}
        case "5D" : {return require( "../icons/cards/5D.png")}
        case "6D" : {return require( "../icons/cards/6D.png")}
        case "7D" : {return require( "../icons/cards/7D.png")}
        case "8D" : {return require( "../icons/cards/8D.png")}
        case "9D" : {return require( "../icons/cards/9D.png")}
        case "10D" : {return require( "../icons/cards/10D.png")}
        case "11D" : {return require( "../icons/cards/11D.png")}
        case "12D" : {return require( "../icons/cards/12D.png")}
        case "13D" : {return require( "../icons/cards/13D.png")}
        case "unknown" : {return require( "../icons/cards/unknown.png")}
    }
    
}

export default IMAGESPATH;