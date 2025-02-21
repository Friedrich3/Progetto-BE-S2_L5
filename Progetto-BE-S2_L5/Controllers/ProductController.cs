using Microsoft.AspNetCore.Mvc;
using Progetto_BE_S2_L5.Models;
using Progetto_BE_S2_L5.Models.ViewModels;

namespace Progetto_BE_S2_L5.Controllers
{
    public class ProductController : Controller
    {
        private static List<Articolo> prodotti = new List<Articolo>()
        {
            new Articolo(){
                Id= Guid.Parse("39385563-a8e4-4613-8215-febfa2aadf54") ,
                Name= "adidas Scarpe Strutter, Cross Trainer Uomo" ,
                Price= 49.99m ,
                Description= "Queste scarpe offrono il padre vibes, hanno un design ingombrante che ti mantiene comodo tutto il giorno. La tomaia in pelle liscia è dotata di scollature, sovrapposizioni e caratteristiche 3 strisce. È ancorato su un'intersuola sagomata ad alto profilo e una suola antiscivolo.",
                Immagini =["https://m.media-amazon.com/images/I/619LPR-C6iS._AC_SY575_.jpg", "https://m.media-amazon.com/images/I/81jCyFoss+L._AC_SY575_.jpg", "https://m.media-amazon.com/images/I/61eD4Ozy1IL._AC_SY575_.jpg"]
            },
            new Articolo(){
                Id= Guid.Parse("e5b17473-fa4b-4b65-8619-a999f267b531") ,
                Name= "adidas Gamecourt 2.0 Tennis Shoes, Scarpe Uomo" ,
                Price= 52.90m ,
                Description= "Gioca in piena fiducia durante ogni game, set e match. La tomaia leggera in mesh e il tallone imbottito rendono queste scarpe adidas Gamecourt 2.0 le tue alleate ideali sul campo da tennis. L'intersuola integrale in EVA assicura un comfort e una sensibilità eccezionali. La suola adiwear fornisce un grip imbattibile sulle superfici dure senza sacrificare la resistenza nel tempo.",
                Immagini =["https://m.media-amazon.com/images/I/41KktYLAWQL._AC_SY695_.jpg", "https://m.media-amazon.com/images/I/817BV1sJssL._AC_SY575_.jpg", "https://m.media-amazon.com/images/I/81MzAh03wRL._AC_SY575_.jpg"]
            },
            new Articolo(){
                Id= Guid.Parse("45a87433-96b4-48ca-89a2-300be69f56f0") ,
                Name= "adidas Court Spec 2 Tennis Shoes, Scarpe Unisex-Adulto" ,
                Price= 59.90m ,
                Description= "Lascia che il tuo gioco risplenda con queste scarpe da tennis Court Spec 2 di Adidas. Con una suola in gomma all-court e un'intersuola flessibile Bounce, queste scarpe leggere offrono prestazioni solide su qualsiasi superficie. La rete traspirante e la pelle sintetica si uniscono per una tomaia dall'aspetto classico che ti terrà comodo durante intensi rally. Questo prodotto presenta almeno il 20% di materiali riciclati. Riutilizzando materiali già creati, contribuiamo a ridurre gli sprechi e la nostra dipendenza da risorse limitate e a ridurre l'impronta dei prodotti che realizziamo.",
                Immagini =["https://m.media-amazon.com/images/I/71ndvq0YQgL._AC_SY575_.jpg", "https://m.media-amazon.com/images/I/71VajxGBq4L._AC_SY575_.jpg", "https://m.media-amazon.com/images/I/71LF-lSsA7L._AC_SY575_.jpg"]
            },
         


        };
        public IActionResult Index()
        {
           var lista = new ListaProdottiViewModel()
           {
               ListaArticoli = prodotti
           };

            return View(lista);
        }
        [HttpGet("Product/Detail/{id:guid}")]
        public IActionResult Detail(Guid id)

        {
            var singleProduct = prodotti.FirstOrDefault(x => x.Id == id);
            if (singleProduct == null)
            {
                return RedirectToAction("Index");
            }


            return View(singleProduct);
        }

        public IActionResult Add()
        {
            var newProdotto = new ArticoloAddViewModel();
            return View(newProdotto);
        }
        [HttpPost]
        public IActionResult Add(ArticoloAddViewModel newProdotto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var newArticolo = new Articolo()
            {
                Id = Guid.NewGuid(),
                Name = newProdotto.Name,
                Price = newProdotto.Price,
                Description = newProdotto.Description,
                //Seleziona tutti gli elementi del array IMMAGINI , e li converte in una lista di strighe 
                Immagini = newProdotto.Immagini!.Select(e => new string(e)).ToList() as List<string>
                
            };
            prodotti.Add(newArticolo);
            return RedirectToAction("Index");
        }

        [HttpGet("/Product/Detail/Edit/{id:guid}")]
        public IActionResult Edit(Guid id)
        {
            var prodotto = prodotti.FirstOrDefault(item=> item.Id == id);
            if (prodotto == null)
            {
                return RedirectToAction("Index");
            }

            var editProduct = new ArticoloEditViewModel()
            {
                Id = prodotto.Id,
                Name = prodotto.Name,
                Price = prodotto.Price,
                Description = prodotto.Description,
                Immagini = prodotto.Immagini,
            };
            return View(editProduct);  
        }

        [HttpPost("Product/Detail/Save/{id:guid}")]
        public IActionResult EditSave(Guid id , ArticoloEditViewModel itemSalvato)
        {
            var prodotto = prodotti.FirstOrDefault(item => item.Id == id);
            if (prodotto == null)
            {
                return RedirectToAction("Index");
            }
            prodotto.Name = itemSalvato.Name;
            prodotto.Price = itemSalvato.Price;
            prodotto.Description = itemSalvato.Description;
            prodotto.Immagini = itemSalvato.Immagini!.Select(e => new string(e)).ToList() as List<string>;


            return RedirectToAction("Index");
        }


        [HttpGet("/Product/Detail/Delete/{id:guid}")]
        public IActionResult Delete(Guid id) 
        {
            var prodotto = prodotti.FirstOrDefault(item => item.Id == id);
            if (prodotto == null)
            {
                return RedirectToAction("Index");
            }
            var isRemoved = prodotti.Remove(prodotto);
            

            return RedirectToAction("Index");
        }



    }
}
