using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebVansSite.Models;

namespace WebVansSite.Controllers
{
    [Authorize]
    public class UtilController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string GetTipoLocomocao()
        {
            var tipoLocomocao = db.TipoLocomocaos
                                .Where(tl => tl.Ativo)
                                .OrderBy(tl => tl.Nome);

            SelectList lista = new SelectList(tipoLocomocao.ToArray(), "Id", "Nome");

            return new JavaScriptSerializer().Serialize(lista);
        }

        public string GetTipoCarga()
        {
            var tipoCarga = db.TipoCargas
                            .Where(tl => tl.Ativo)
                            .OrderBy(tl => tl.Nome);

            SelectList lista = new SelectList(tipoCarga.ToArray(), "Id", "Nome");

            return new JavaScriptSerializer().Serialize(lista);
        }

        public string GetTipoVan(int id)
        {
            var tipoVan = db.TipoVans
                          .Where(tl => tl.Ativo && tl.TipoCarga.Id == id)
                          .OrderBy(tl => tl.Nome);

            SelectList lista = new SelectList(tipoVan.ToArray(), "Id", "Nome");

            return new JavaScriptSerializer().Serialize(lista);
        }

        public string GetCidade(int id)
        {
            var cidade = db.Cidades
                          .Where(c => c.Estado.Id == id)
                          .OrderBy(c => c.Nome);

            SelectList lista = new SelectList(cidade.ToArray(), "Id", "Nome");

            return new JavaScriptSerializer().Serialize(lista);
        }

        public string GetModelo(int id)
        {
            var modelo = db.Modelos
                          .Where(m => m.Marca.Id == id)
                          .OrderBy(m => m.Nome);

            SelectList lista = new SelectList(modelo.ToArray(), "Id", "Nome");

            return new JavaScriptSerializer().Serialize(lista);
        }
    }
}