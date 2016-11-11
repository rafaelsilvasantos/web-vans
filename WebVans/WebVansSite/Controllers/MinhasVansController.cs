using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebVansSite.Models;
using Microsoft.AspNet.Identity;
using WebVansSite.Models.ViewModels;
using System.Web.Script.Serialization;

namespace WebVansSite.Controllers
{
    [Authorize]
    public class MinhasVansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MinhasVans
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var minhasVans = db.Vans
                             .Include(v => v.TipoServico)
                             .Include(v => v.CidadesAtendidas)
                             .Where(v => v.Proprietario.Id == userId)
                             .OrderBy(v => v.Modelo.Marca.Nome)
                             .Select(v => new IndexMinhasVanViewModel
                             {
                                 Ano = v.Ano,
                                 DataCadastro = v.DataCadastro,
                                 Id = v.ID,
                                 Marca = v.Modelo.Marca.Nome,
                                 Modelo = v.Modelo.Nome,
                                 TipoServico = v.TipoServico.Nome,
                                 CidadeEstado = v.CidadesAtendidas.FirstOrDefault().Nome + "/" + v.CidadesAtendidas.FirstOrDefault().Estado.Sigla,
                                 Placa = v.Placa
                             });

            return View(minhasVans.ToList());
        }

        // GET: MinhasVans/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();

            VanModel vanModel = db.Vans
                                .Include(v => v.TipoCarga)
                                .Include(v => v.TipoLocomocao)
                                .Include(v => v.TipoServico)
                                .Include(v => v.TipoVan)
                                .FirstOrDefault(v => v.ID == id && v.Proprietario.Id == userId);

            if (vanModel == null)
            {
                return HttpNotFound();
            }

            var viewModel = new DetalhesVanViewModel
            {
                Ano = vanModel.Ano,
                DataCadastro = vanModel.DataCadastro,
                ID = vanModel.ID,
                Marca = vanModel.Modelo.Marca.Nome,
                Modelo = vanModel.Modelo.Nome,
                Observacoes = vanModel.Observacoes,
                TipoCarga = vanModel.TipoCarga != null ? vanModel.TipoCarga.Nome : string.Empty,
                TipoLocomocao = vanModel.TipoLocomocao != null ? vanModel.TipoLocomocao.Nome : string.Empty,
                TipoServico = vanModel.TipoServico != null ? vanModel.TipoServico.Nome : string.Empty,
                TipoVan = vanModel.TipoVan != null ? vanModel.TipoVan.Nome : string.Empty,
                CidadeEstado = vanModel.CidadesAtendidas.FirstOrDefault().Nome + "/" + vanModel.CidadesAtendidas.FirstOrDefault().Estado.Sigla,
                Placa = vanModel.Placa
            };


            return View(viewModel);
        }

        // GET: MinhasVans/Cadastrar
        public ActionResult Cadastrar()
        {
            CadastrarVanViewModel viewModel = new CadastrarVanViewModel();

            viewModel.ListaTipoServico = new SelectList(db.TipoServicos.Where(ts => ts.Ativo).ToList(), "Id", "Nome");
            viewModel.ListaEstado = new SelectList(db.Estados.ToList(), "Id", "Nome");
            viewModel.ListaMarca = new SelectList(db.Marcas.Where(m => m.Ativo).ToList(), "Id", "Nome");
            viewModel.Ano = DateTime.Today.Year;

            return View(viewModel);
        }

        // POST: MinhasVans/Cadastrar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(CadastrarVanViewModel vanViewModel)
        {
            if (ModelState.IsValid)
            {
                VanModel van = new VanModel();

                van.Ano = vanViewModel.Ano;
                van.DataCadastro = DateTime.Now;
                van.Modelo = db.Modelos.Find(vanViewModel.ModeloId);
                van.Observacoes = vanViewModel.Observacoes;
                van.Proprietario = db.Users.Find(User.Identity.GetUserId());
                van.TipoServico = db.TipoServicos.Find(vanViewModel.TipoServicoId);
                van.TipoCarga = db.TipoCargas.Find(vanViewModel.TipoCargaId);
                van.TipoLocomocao = db.TipoLocomocaos.Find(vanViewModel.TipoLocomocaoId);
                van.TipoVan = db.TipoVans.Find(vanViewModel.TipoVanId);
                van.Placa = vanViewModel.Placa.ToUpper();

                van.CidadesAtendidas.Add(db.Cidades.Find(vanViewModel.CidadeId));

                db.Vans.Add(van);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vanViewModel);
        }

        // GET: MinhasVans/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();
            VanModel vanModel = db.Vans
                                .Include(v => v.TipoCarga)
                                .Include(v => v.TipoLocomocao)
                                .Include(v => v.TipoServico)
                                .Include(v => v.TipoVan)
                                .FirstOrDefault(v => v.ID == id && v.Proprietario.Id == userId);

            if (vanModel == null)
            {
                return HttpNotFound();
            }

            EditarVanViewModel viewModel = new EditarVanViewModel();

            viewModel.ListaTipoServico = new SelectList(db.TipoServicos.Where(ts => ts.Ativo).ToList(), "Id", "Nome");
            viewModel.ListaEstado = new SelectList(db.Estados.ToList(), "Id", "Nome");
            viewModel.ListaMarca = new SelectList(db.Marcas.ToList(), "Id", "Nome");

            var estadoId = vanModel.CidadesAtendidas.FirstOrDefault().Estado.Id;
            var marcaId = vanModel.Modelo.Marca.Id;

            viewModel.ListaCidade = new SelectList(db.Cidades.Where(c => c.Estado.Id == estadoId).ToList(), "Id", "Nome");
            viewModel.ListaModelo = new SelectList(db.Modelos.Where(m => m.Marca.Id == marcaId && m.Ativo == true).ToList(), "Id", "Nome");

            viewModel.Ano = vanModel.Ano;
            viewModel.ID = vanModel.ID;
            viewModel.MarcaId = vanModel.Modelo.Marca.Id;
            viewModel.ModeloId = vanModel.Modelo.Id;
            viewModel.Observacoes = vanModel.Observacoes;
            viewModel.TipoCargaId = vanModel.TipoCarga != null ? vanModel.TipoCarga.Id : default(int);
            viewModel.TipoLocomocaoId = vanModel.TipoLocomocao != null ? vanModel.TipoLocomocao.Id : default(int);
            viewModel.TipoServicoId = vanModel.TipoServico != null ? vanModel.TipoServico.Id : default(int);
            viewModel.TipoVanId = vanModel.TipoVan != null ? vanModel.TipoVan.Id : default(int);
            viewModel.EstadoId = vanModel.CidadesAtendidas.FirstOrDefault().Estado.Id;
            viewModel.CidadeId = vanModel.CidadesAtendidas.FirstOrDefault().Id;
            viewModel.Placa = vanModel.Placa;

            if (vanModel.TipoLocomocao != null)
            {
                viewModel.TipoLocomocao = db.TipoLocomocaos
                                          .Where(tl => tl.Ativo)
                                          .OrderBy(tl => tl.Nome)
                                          .Select(tl => new SelectListItem
                                          {
                                              Text = tl.Nome,
                                              Value = tl.Id.ToString()
                                          });
            }

            if (vanModel.TipoCarga != null)
            {
                viewModel.TipoCarga = db.TipoCargas
                                      .Where(tl => tl.Ativo)
                                      .OrderBy(tl => tl.Nome)
                                      .Select(tl => new SelectListItem
                                      {
                                          Text = tl.Nome,
                                          Value = tl.Id.ToString()
                                      });
            }

            if (vanModel.TipoVan != null)
            {
                viewModel.TipoVan = db.TipoVans
                                    .Where(tl => tl.Ativo)
                                    .OrderBy(tl => tl.Nome)
                                    .Select(tl => new SelectListItem
                                    {
                                        Text = tl.Nome,
                                        Value = tl.Id.ToString()
                                    });
            }

            return View(viewModel);
        }

        // POST: MinhasVans/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(EditarVanViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                VanModel vanModel = db.Vans
                                    .Include(v => v.TipoCarga)
                                    .Include(v => v.TipoLocomocao)
                                    .Include(v => v.TipoServico)
                                    .Include(v => v.TipoVan)
                                    .FirstOrDefault(v => v.ID == viewModel.ID);

                vanModel.Ano = viewModel.Ano;                
                vanModel.Modelo = db.Modelos.Find(viewModel.ModeloId);
                vanModel.Observacoes = viewModel.Observacoes;
                vanModel.TipoServico = db.TipoServicos.Find(viewModel.TipoServicoId);
                vanModel.TipoCarga = db.TipoCargas.Find(viewModel.TipoCargaId);
                vanModel.TipoLocomocao = db.TipoLocomocaos.Find(viewModel.TipoLocomocaoId);
                vanModel.TipoVan = db.TipoVans.Find(viewModel.TipoVanId);
                vanModel.Proprietario = db.Users.Find(User.Identity.GetUserId());
                vanModel.Placa = viewModel.Placa.ToUpper();

                vanModel.CidadesAtendidas.Clear();
                vanModel.CidadesAtendidas.Add(db.Cidades.Find(viewModel.CidadeId));

                try
                {
                    db.Entry(vanModel).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: MinhasVans/Apagar/5
        public ActionResult Apagar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();

            VanModel vanModel = db.Vans
                                .Include(v => v.TipoCarga)
                                .Include(v => v.TipoLocomocao)
                                .Include(v => v.TipoServico)
                                .Include(v => v.TipoVan)
                                .FirstOrDefault(v => v.ID == id && v.Proprietario.Id == userId);

            if (vanModel == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ApagarVanViewModel
            {
                Ano = vanModel.Ano,
                DataCadastro = vanModel.DataCadastro,
                ID = vanModel.ID,
                Marca = vanModel.Modelo.Marca.Nome,
                Modelo = vanModel.Modelo.Nome,
                Observacoes = vanModel.Observacoes,
                TipoCarga = vanModel.TipoCarga != null ? vanModel.TipoCarga.Nome : string.Empty,
                TipoLocomocao = vanModel.TipoLocomocao != null ? vanModel.TipoLocomocao.Nome : string.Empty,
                TipoServico = vanModel.TipoServico != null ? vanModel.TipoServico.Nome : string.Empty,
                TipoVan = vanModel.TipoVan != null ? vanModel.TipoVan.Nome : string.Empty,
                CidadeEstado = vanModel.CidadesAtendidas.FirstOrDefault().Nome + "/" + vanModel.CidadesAtendidas.FirstOrDefault().Estado.Sigla,
                Placa = vanModel.Placa
            };
            return View(viewModel);
        }

        // POST: MinhasVans/Apagar/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public ActionResult ApagarConfirmed(int id)
        {
            VanModel vanModel = db.Vans.Find(id);
            db.Vans.Remove(vanModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
